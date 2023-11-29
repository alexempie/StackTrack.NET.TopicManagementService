using FluentResults;
using MediatR;

using TopicManagementService.Application.Entities.Dtos;

namespace TopicManagementService.Application.Features.Topics.Queries;

public class GetBaseTopicsQuery : IRequest<Result<IReadOnlyCollection<TopicDto>>>
{
}