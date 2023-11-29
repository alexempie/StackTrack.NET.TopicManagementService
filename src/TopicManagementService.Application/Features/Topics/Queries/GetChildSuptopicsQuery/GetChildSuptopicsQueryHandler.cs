using AutoMapper;
using FluentResults;
using MediatR;

using TopicManagementService.Application.Entities.Dtos;
using TopicManagementService.Common.Enums;
using TopicManagementService.Common.Services.Interfaces;
using TopicManagementService.Core.Repositories.Interfaces;

namespace TopicManagementService.Application.Features.Topics.Queries;

public class GetChildSuptopicsQueryHandler : IRequestHandler<GetChildSuptopicsQuery, Result<IReadOnlyCollection<TopicDto>>>
{
    private readonly IMapper _mapper;
    private readonly IErrorMessageService _errorMessageService;
    private readonly ITopicRepository _topicRepository;
 
    public GetChildSuptopicsQueryHandler(
        IMapper mapper,
        IErrorMessageService errorMessageService,
        ITopicRepository topicRepository)
    {
        _mapper = mapper;
        _errorMessageService = errorMessageService;
        _topicRepository = topicRepository;
    }

    public async Task<Result<IReadOnlyCollection<TopicDto>>> Handle(GetChildSuptopicsQuery query, CancellationToken cancellationToken)
    {
        var topic = await _topicRepository.GetTopicByIdAsync(query.TopicId);
        if (topic == null)
        {
            var topicNotFoundErrorMessage = _errorMessageService.GetErrorMessage(ErrorMessageCode.TopicNotFound);

            return Result.Fail<IReadOnlyCollection<TopicDto>>(topicNotFoundErrorMessage);
        }

        var childrenSubtopics = await _topicRepository.GetChildSuptopicsAsync(query.TopicId);

        return Result.Ok(_mapper.Map<IReadOnlyCollection<TopicDto>>(childrenSubtopics));
    }
}