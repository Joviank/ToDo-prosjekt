using Microsoft.AspNetCore.Mvc;
using TaskService;
using TaskService.Api.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace TaskService.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService= taskService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetTask() {
        var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
        if(string.IsNullOrEmpty(authHeader))
        {
            return BadRequest("You shall  not pass with NO authorization header!");
        }
        var token = authHeader.Replace("Bearer ", "");

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var userId = jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("Token is missing User ID");
        }

        var tasks = _taskService.GetTasks(userId);
        return Ok(tasks);
    }

    [HttpPost]
    public ActionResult<TaskItem> AddTask([FromBody] CreateTaskRequest request)
    {
        var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
        if(string.IsNullOrEmpty(authHeader))
        {
            return BadRequest("You shall  not pass with NO authorization header!");
        }
        var token = authHeader.Replace("Bearer ", "");

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var userId = jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("Token is missing User ID");
        }

        var newTask = _taskService.AddTask(request.Title, userId);
        return Ok(newTask);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        _taskService.DeleteTask(id);
        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    public IActionResult CompleteTask(int id)
    {
        _taskService.CompleteTask(id);
        return NoContent();
    }
}