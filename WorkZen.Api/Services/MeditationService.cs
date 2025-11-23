using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WorkZen.Api.DTOs.Meditations;
using WorkZen.Api.Services.Interfaces;

namespace WorkZen.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/meditations")]
public class MeditationsController : ControllerBase
{
    private readonly IMeditationService _service;

    public MeditationsController(IMeditationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var result = await _service.GetAllAsync(page, size);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMeditationDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMeditationDto dto)
    {
        var ok = await _service.UpdateAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}