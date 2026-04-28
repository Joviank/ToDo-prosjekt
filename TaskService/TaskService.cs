using System.Collections.Generic;

namespace TaskService;

public class TaskService
{
    private readonly List<TaskItem> tasks = new();
    private int nextId = 1;
    public TaskItem AddTask(string title)
    {
        var task = new TaskItem(nextId++, title);
        tasks.Add(task);
        return task;
    }

    public List<TaskItem> GetTasks()
    {
        return tasks.ToList();
    }

    public void DeleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            throw new KeyNotFoundException("Task not found");
        }

        tasks.Remove(task);
    }

    public void CompleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            throw new KeyNotFoundException("Task not found");
        }

        task.IsCompleted = true;
    }
}
