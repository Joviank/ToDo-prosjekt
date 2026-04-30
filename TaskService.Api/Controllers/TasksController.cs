using Microsoft.AspNetCore.Mvc;
using TaskService;

namespace TaskService.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskManager;

    public TasksController(TaskService taskService)
    {
        _taskManager = taskService;
    }

    [HttpGet]
    public IEnumerable<TaskItem> GetTasks()
    {
        return _taskManager.GetTasks();
    }
    
    [HttpPost]
    public TaskItem AddTask(string title)
    {
        return _taskManager.AddTask(title);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        _taskManager.DeleteTask(id);
        return NoContent();
    }
}