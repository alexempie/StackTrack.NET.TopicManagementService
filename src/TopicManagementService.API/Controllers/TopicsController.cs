using MediatR;
using Microsoft.AspNetCore.Mvc;

using TopicManagementService.Application.Features.Topics.Queries;

namespace TopicManagementService.API.Controllers;

[Route("api/topics")]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TopicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("base")]
    public async Task<IActionResult> GetBaseTopics()
    {
        var result = await _mediator.Send(new GetBaseTopicsQuery());
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("{topicId}/subtopics")]
    public async Task<IActionResult> GetChildSuptopics([FromRoute] GetChildSuptopicsQuery query)
    {
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("{topicId}/descendant-subtopics")]
    public async Task<IActionResult> GetDescendantSuptopics([FromRoute] GetDescendantSubtopicsQuery query)
    {
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }
}