using AutoMapper;
using FluentResults;
using MediatR;

using TopicManagementService.Application.Entities.Dtos;
using TopicManagementService.Core.Repositories.Interfaces;

namespace TopicManagementService.Application.Features.Topics.Queries;

public class GetBaseTopicsQueryHandler : IRequestHandler<GetBaseTopicsQuery, Result<IReadOnlyCollection<TopicDto>>>
{
    private readonly IMapper _mapper;
    private readonly ITopicRepository _topicRepository;
 
    public GetBaseTopicsQueryHandler(
        IMapper mapper,
        ITopicRepository topicRepository)
    {
        _mapper = mapper;
        _topicRepository = topicRepository;
    }

    public async Task<Result<IReadOnlyCollection<TopicDto>>> Handle(GetBaseTopicsQuery query, CancellationToken cancellationToken)
    {
        var baseTopics = await _topicRepository.GetBaseTopicsAsync();
        
        return Result.Ok(_mapper.Map<IReadOnlyCollection<TopicDto>>(baseTopics));
    }
}