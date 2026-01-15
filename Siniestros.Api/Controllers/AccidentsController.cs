using MediatR;
using Microsoft.AspNetCore.Mvc;
using Siniestros.Application.Accidents.Create;
using Siniestros.Application.Accidents.Search;

namespace Siniestros.Api.Controllers;

[ApiController]
[Route("api/accidents")]
public sealed class AccidentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccidentsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAccidentCommand command, CancellationToken ct)
    {
        var id = await _mediator.Send(command, ct);
        return Created($"/api/accidents/{id}", new { id });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search([FromQuery] GetAccidentsQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }
}
