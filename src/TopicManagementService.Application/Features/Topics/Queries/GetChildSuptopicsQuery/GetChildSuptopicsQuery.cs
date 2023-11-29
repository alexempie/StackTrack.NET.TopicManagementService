using FluentResults;
using MediatR;

using TopicManagementService.Application.Entities.Dtos;

namespace TopicManagementService.Application.Features.Topics.Queries;

public class GetChildSuptopicsQuery : IRequest<Result<IReadOnlyCollection<TopicDto>>>
{
    public Guid TopicId { get; set; }
}