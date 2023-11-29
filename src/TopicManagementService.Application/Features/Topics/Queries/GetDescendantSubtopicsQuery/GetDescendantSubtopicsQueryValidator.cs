using FluentValidation;

using TopicManagementService.Application.Features.Topics.Queries;

public class GetDescendantSubtopicsQueryValidator : AbstractValidator<GetDescendantSubtopicsQuery>
{
    public GetDescendantSubtopicsQueryValidator()
    {
        RuleFor(x => x.TopicId).NotEmpty();
    }
}