using FluentValidation;

namespace TopicManagementService.Application.Features.Topics.Queries;

public class GetChildSuptopicsQueryValidator : AbstractValidator<GetChildSuptopicsQuery>
{
    public GetChildSuptopicsQueryValidator()
    {
        RuleFor(x => x.TopicId).NotEmpty();
    }
}