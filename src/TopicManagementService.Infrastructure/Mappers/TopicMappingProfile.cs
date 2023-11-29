using AutoMapper;

using TopicManagementService.Core.Entities;
using TopicManagementService.Infrastructure.Db.DbEntities;

namespace TopicManagementService.Infrastructure.Mappers;

public class TopicMappingProfile : Profile
{
    public TopicMappingProfile() 
    {
        CreateMap<TopicDbEntity, Topic>();
    }
}