using AutoMapper;

using TopicManagementService.Application.Entities.Dtos;
using TopicManagementService.Core.Entities;

namespace TopicManagementService.Application.Mappers;

public class TopicMappingProfile : Profile
{
    public TopicMappingProfile() 
    {
        CreateMap<Topic, TopicDto>();
    }
}