using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<IActionResult> CreateAsync([FromBody] TaskEntity request)
    {
        var task = await _taskService.CreateAsync(request);
        return Ok(task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TaskEntity request)
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
    public async Task<IActionResult> ChangeStatusAsync(Guid id, [FromBody] MyTaskStatus newStatus)
    {
        var task = await _taskService.ChangeStatusAsync(id, newStatus);
        return Ok(task);
    }

    [HttpPatch("{id}/assign")]
    public async Task<IActionResult> AssignAsync(Guid id, [FromBody] string assignee)
    {
        var task = await _taskService.AssignAsync(id, assignee);
        return Ok(task);
    }
}