using Xunit;

namespace TaskService.test;

public class TaskServiceTest
{
    [Fact]
    public void AddTask_ShouldAddTaskToList()
    {
        // Arrange
        var service = new TaskService();

        // Act
        service.AddTask("Blame Microsoft");

        // Assert
        Assert.Single(service.GetTasks());
    }

    [Fact]
    public void AddTask_ShouldStoreCorrectTitle()
    {
        // Arrange
        var service = new TaskService();

        // Act
        service.AddTask("Pet a cat");

        // Assert
        var task = service.GetTasks().First();
        Assert.Equal("Pet a cat", task.Title);
    }

    [Fact]

    public void DeleteTask_ShouldRemoveTask()
    {
        // Arrange
        var service = new TaskService();
        var task = service.AddTask("Stop copy pasting");

        // Act
        service.DeleteTask(task.Id);

        // Assert
        Assert.Empty(service.GetTasks());
    }

    [Fact]
    public void CompleteTask_ShouldMarkTaskAsCompleted()
    {
        // Given
        var service = new TaskService();
        var task = service.AddTask("Get more ideas for tasks");
    
        // When
        service.CompleteTask(task.Id);
    
        // Then
        Assert.True(service.GetTasks().First().IsCompleted);
    }

    [Fact]
    public void CompleteTask_ShouldThrow_WhenTaskDoesntExist()
    {
        // Arrange
        var service = new TaskService();

        // Act and Assert
        Assert.Throws<KeyNotFoundException>(() => service.CompleteTask(500));
    }

}
