namespace PubLink.Infrastructure.PostgreSql.Profiles;

using AutoMapper;
using Source = PubLink.Infrastructure.PostgreSql.Models.AuditLogDto;
using Target = PubLink.Application.AuditLogs.QueryResults.AuditLog;

internal sealed class AuditLogDtoProfile : Profile
{
    public AuditLogDtoProfile()
    {
        this.CreateMap<Source, Target>()
            .ForMember(target => target.UserEmail, options => options.MapFrom(source => source.UserEmail))
            .ForMember(target => target.Type, options => options.MapFrom(source => source.Type.ToString()))
            .ForMember(target => target.ContractNumber,
                options => options.MapFrom(source => source.ContractNumber))
            .ForMember(target => target.StartAt, options => options.MapFrom(source => source.CreatedDate))
            .ForMember(target => target.Duration, options => options.MapFrom(source => source.Duration))
            .ForMember(target => target.NumberOfEntitiesChanged, options => options.MapFrom(source => source.NumberOfEntitiesChanged))
            ;
    }
}
