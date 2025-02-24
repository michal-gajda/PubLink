namespace PubLink.Infrastructure.PostgreSql.Services;

using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using PubLink.Infrastructure.PostgreSql.Interfaces;
using PubLink.Infrastructure.PostgreSql.Models;

internal sealed partial class ReadService(ILogger<ReadService> logger, PostgreSqlOptions options) : IReadService
{
    private const string QUERY =
        """
        WITH cte AS
        (
            SELECT
                user_email,
                type,
                entity_type,
                created_date,
                (MAX(created_date) OVER w - MIN(created_date) OVER w) AS duration,
                jsonb_array_length(affected_columns::jsonb) AS number_of_entities_changed,
                entity_id
            FROM
                audit_log
            WHERE organization_id = @OrganizationId
            WINDOW w AS (PARTITION BY correlation_id)
            ORDER BY
                created_date DESC
            LIMIT @Limit
            OFFSET @Offset
        )
        SELECT
            user_email,
            type,
            entity_type,
            document_header.number AS contract_number,
            cte.created_date,
            duration,
            COALESCE(number_of_entities_changed, 0) AS number_of_entities_changed
        FROM
            cte
            LEFT JOIN document_header
                ON cte.entity_id = document_header.id AND entity_type = 1
        ORDER BY
            cte.created_date DESC
        """;

    public async Task<IEnumerable<AuditLogDto>> GetAuditLogs(Guid organizationId, int page, int pageSize)
    {
        this.LogGetUserActions(organizationId);

        await using var connection = new NpgsqlConnection(options.ConnectionString);

        var offset = Math.Max((page - 1) * pageSize, val2: 0);

        var param = new
        {
            OrganizationId = organizationId,
            Offset = offset,
            Limit = pageSize,
        };

        var result = await connection.QueryAsync<AuditLogDto>(QUERY, param);

        return result;
    }

    public async Task<IEnumerable<Guid>> GetOrganizationIds()
    {
        this.LogGetOrganizationIds();

        await using var connection = new NpgsqlConnection(options.ConnectionString);

        return await connection.QueryAsync<Guid>("SELECT DISTINCT(organization_id) FROM audit_log ORDER BY organization_id ASC");
    }

    [LoggerMessage(LogLevel.Information, Message = "GetOrganizationIds")]
    private partial void LogGetOrganizationIds();

    [LoggerMessage(LogLevel.Information, Message = "GetUserActions for {organizationId}")]
    private partial void LogGetUserActions(Guid organizationId);
}
