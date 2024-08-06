using Elastic.Apm;
using ElasticApm.ParentSegmentBug.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticApm.ParentSegmentBug.Controllers;

[ApiController]
[Route("app")]
public class AppController : ControllerBase
{
    private readonly FirstService _firstService;

    public AppController(FirstService firstService)
    {
        _firstService = firstService;
    }

    [HttpGet("wrong-case")]
    public async Task<IActionResult> WrongCase()
    {
        await _firstService.WrongCaseAsync(Agent.Tracer.CurrentTransaction);
        return Ok("Hello from wrong-case");
    }

    [HttpGet("bypass-transaction-case")]
    public async Task<IActionResult> BypassTransactionCase()
    {
        await _firstService.BypassTransactionCaseAsync(Agent.Tracer.CurrentTransaction);
        return Ok("Hello from bypass-transaction-case");
    }

    [HttpGet("bypass-span-case")]
    public async Task<IActionResult> BypassSpanCase()
    {
        await _firstService.BypassSpanCaseAsync(Agent.Tracer.CurrentTransaction);
        return Ok("Hello from bypass-span-case");
    }
}
