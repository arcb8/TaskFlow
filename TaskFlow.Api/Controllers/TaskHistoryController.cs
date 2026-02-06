using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Controllers;

[ApiController]
[Route("tasks/{taskId}/history")]
public class TaskHistoryController : ControllerBase
{
    private readonly ITaskHistoryService _taskHistoryService;

    public TaskHistoryController(ITaskHistoryService taskHistoryService)
    {
        _taskHistoryService = taskHistoryService;
    }

    // GET: /tasks/{taskId}/history
    [HttpGet]
    public async Task<IActionResult> GetAllByTaskId(Guid taskId)
    {
        var history = await _taskHistoryService.GetAllByTaskIdAsync(taskId);
        return Ok(history);
    }

    // GET: /tasks/{taskId}/history/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var history = await _taskHistoryService.GetByIdAsync(id);

        if (history == null)
            return NotFound();

        return Ok(history);
    }

    // POST: /tasks/{taskId}/history
    [HttpPost]
    public async Task<IActionResult> Create(
        Guid taskId,
        [FromBody] TaskHistory history)
    {
        history.TaskId = taskId;

        var created = await _taskHistoryService.CreateAsync(history);
        return Ok(created);
    }

    // DELETE: /tasks/{taskId}/history/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _taskHistoryService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}