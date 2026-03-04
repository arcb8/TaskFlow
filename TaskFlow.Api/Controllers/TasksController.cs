using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Contracts;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    
    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id) 
    {
        var task = await _taskService.GetByIdAsync(id);
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskEntityRequest request)
    {
        var task = await _taskService.CreateAsync(request);
        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { id = task.Id },
            task
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateTaskEntityRequest request)
    {
        var task = await _taskService.UpdateAsync(id, request);
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _taskService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatusAsync(Guid id, [FromBody] ChangeStatusRequest request)
    {
        var task = await _taskService.ChangeStatusAsync(id, request);
        return Ok(task);
    }

    [HttpPatch("{id}/assign")]
    public async Task<IActionResult> AssignAsync(Guid id, [FromBody] AssignRequest request)
    {
        var task = await _taskService.AssignAsync(id, request);
        return Ok(task);
    }
}