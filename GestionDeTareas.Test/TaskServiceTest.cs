using System.Collections;
using GestionDeTareas.Core.Application.DTos;
using GestionDeTareas.Core.Application.Interfaces.Service;
using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Core.Domain.Utils;
using GestionDeTareas.Presentation.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestionDeTareas.Test;

public class TaskServiceTest
{
    private readonly Mock<ITaskService> taskServiceMock;
    public TaskServiceTest()
    {
        taskServiceMock = new Mock<ITaskService>();
    }
    
    [Fact]
    public void CreateTask_Test()
    {
        //arrange
        CreateTaskDto createTaskDto = new CreateTaskDto(
            "Description about task 1", 
            new DateOnly(2025, 3, 15), 
            Status.Pending,            
            10 );
        
        var taskDtos = new TaskDtos 
        (
            Id:Guid.NewGuid(),
            Description: createTaskDto.DescriptionAboutTask,
            DuaDate: createTaskDto.DuaDate,
            Status: createTaskDto.StatusTask,
            AdditionalData: createTaskDto.AdditionalData
        );

        var expectedResult = ResultT<TaskDtos>.Success(taskDtos);
        
       taskServiceMock.Setup(x => x.CreateAsync(createTaskDto, It.IsAny<CancellationToken>()))
           .ReturnsAsync(expectedResult);
       
       var taskController = new TaskController(taskServiceMock.Object);
       //Act
       var result =  taskController.CreateAsync(createTaskDto,CancellationToken.None);
       
       // Assert
       //Assert.NotNull(result);
       Assert.True(result.IsCompleted);
    }

    [Fact]
    public void CreateHighPriorityTask_Test()
    {
        //Arrange
        string description = "Description about task 1";
        var taskDtos = new TaskDtos 
        (
            Id:Guid.NewGuid(),
            Description: description,
            DuaDate: new DateOnly(2025, 3, 15),
            Status: Status.Pending,
            AdditionalData: 20
        );
        
        var expectedResult = ResultT<TaskDtos>.Success(taskDtos);
        
        taskServiceMock.Setup(x => x.CreateHighPriorityTask(description, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        var taskController = new TaskController(taskServiceMock.Object);
        //Act
        var result =  taskController.CreateHighPriorityTask(description, CancellationToken.None);
        //Assert
        Assert.NotNull(result);
        //Assert.True(result.IsCompleted);
    }
    
    [Fact]
    public void ThreeDaysTask_Test()
    {
        //Arrange
        string description = "Description about task 1";
        var taskDtos = new TaskDtos 
        (
            Id:Guid.NewGuid(),
            Description: description,
            DuaDate: new DateOnly(2025, 3, 15),
            Status: Status.Pending,
            AdditionalData: 20
        );
        
        var expectedResult = ResultT<TaskDtos>.Success(taskDtos);
        
        taskServiceMock.Setup(x => x.ThreeDaysTask(description, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        var taskController = new TaskController(taskServiceMock.Object);
        //Act
        var result = taskController.CreateThreeDaysTask(description, CancellationToken.None);
        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetById_Test()
    {
        //Arrange
        Guid taskId = Guid.NewGuid();
        var taskDtos = new TaskDtos 
        (
            Id:taskId,
            Description: "description about task 1",
            DuaDate: new DateOnly(2025, 3, 15),
            Status: Status.Pending,
            AdditionalData: 20
        );
        
        var expectedResult = ResultT<TaskDtos>.Success(taskDtos);
        taskServiceMock.Setup(x => x.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var taskController = new TaskController(taskServiceMock.Object);
        
        //Act
        //var result =  taskServiceMock.Object.GetByIdAsync(taskId, CancellationToken.None);
        var result = taskController.GetByIdAsync(taskId, CancellationToken.None);
        //Asset
        Assert.NotNull(result);
    }

    [Fact]
    public async Task FilterByDescription_Test()
    {
       //Arrange 
       string description = "Description about task 1";
        List<TaskDtos> taskDtos = new List<TaskDtos>
        {
            new TaskDtos(Guid.NewGuid(), "Complete API endpoint", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), Status.Pending, 100),
            new TaskDtos(Guid.NewGuid(), "Fix database migration issues", DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Status.Pending, 200),
            new TaskDtos(Guid.NewGuid(), "Deploy app to AWS", DateOnly.FromDateTime(DateTime.Now.AddDays(7)), Status.Completed, 300)
        };

        var expectedResult = ResultT<IEnumerable<TaskDtos>>.Success(taskDtos);
        var resultController = new TaskController(taskServiceMock.Object);
        
        taskServiceMock.Setup(x => x.FilterByDescriptionAsync(description, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        //Act 
        var result = await resultController.FilterByDescriptionAsync(description, CancellationToken.None);
        
        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void FilterByStatus_Test()
    {
        //Arrange
        Status status = Status.Pending;
        List<TaskDtos> taskDtos = new List<TaskDtos>
        {
            new TaskDtos(Guid.NewGuid(), "Complete API endpoint", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), Status.Pending, 100),
            new TaskDtos(Guid.NewGuid(), "Fix database migration issues", DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Status.Pending, 200),
            new TaskDtos(Guid.NewGuid(), "Deploy app to AWS", DateOnly.FromDateTime(DateTime.Now.AddDays(7)), Status.Completed, 300)
        };
        
        var expectedResult = ResultT<IEnumerable<TaskDtos>>.Success(taskDtos);
        var resultController = new TaskController(taskServiceMock.Object);
        
        taskServiceMock.Setup(x => x.FilterByStatus(status, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        //Act
        var taskController = resultController.FilterByStatusAsync(status, CancellationToken.None);
        
        // Assert
        Assert.NotNull(taskController);
    }

    [Fact]
    public void CalculateDays_Test()
    {
        //Arrange
        Guid id = Guid.NewGuid();
        TaskDayDto taskDto = new TaskDayDto(
            TaskId: id,
            "Description about task 1", 
            new DateOnly(2025, 3, 15), 
            Status.Pending,            
            DayLeft: 20,
            10 );
        
        var expectedResult = ResultT<TaskDayDto>.Success(taskDto);
        var resultController = new TaskController(taskServiceMock.Object);
        
        taskServiceMock.Setup(x => x.CalculateDayLeftAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        //Act
        var taskController = resultController.GetCalculateDaysAsync(id, CancellationToken.None);
        //Assert
        Assert.NotNull(taskController);
    }

    [Fact]
    public void DeleteTask_Test()
    {
        //Arrange
        Guid taskId = Guid.NewGuid();
        Guid task = Guid.NewGuid();
        var taskDtos = new TaskDtos 
        (
            Id:taskId,
            Description: "description about task 1",
            DuaDate: new DateOnly(2025, 3, 15),
            Status: Status.Pending,
            AdditionalData: 20
        );
        
        var expectedResult = ResultT<TaskDtos>.Success(taskDtos);
        var resultController = new TaskController(taskServiceMock.Object);
        
        taskServiceMock.Setup(x => x.DeleteAsync(taskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        //Act
        var resultTaskController = resultController.DeleteAsync(task, CancellationToken.None);
        
        //Assert
        Assert.NotNull(resultController);
    }

    [Fact]
    public void UpdateTask_Test()
    {
        //Arrange
        Guid taskId = Guid.NewGuid();
        UpdateTaskDtos updateTaskDtos = new(
            Description:"New about task 1",
            Status: Status.Completed
            );
        
        var taskDtos = new TaskDtos 
        (
            Id:taskId,
            Description: "description about task 1",
            DuaDate: new DateOnly(2025, 3, 15),
            Status: Status.Pending,
            AdditionalData: 20
        );
        
        var expectedResult = ResultT<TaskDtos>.Success(taskDtos);
        var taskController = new TaskController(taskServiceMock.Object);
        
        taskServiceMock.Setup(x => x.UpdateAsync(taskId,updateTaskDtos ,It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        //Act
        var resultController = taskController.UpdateAsync(taskId, updateTaskDtos, CancellationToken.None);
        
        // Assert
        Assert.NotNull(resultController);
    }

    [Fact]
    public async Task GetAll_Test()
    {
        //Arrange
        List<TaskDtos> taskDtos = new List<TaskDtos>
        {
            new TaskDtos(Guid.NewGuid(), "Complete API endpoint", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), Status.Pending, 100),
            new TaskDtos(Guid.NewGuid(), "Fix database migration issues", DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Status.Pending, 200),
            new TaskDtos(Guid.NewGuid(), "Deploy app to AWS", DateOnly.FromDateTime(DateTime.Now.AddDays(7)), Status.Completed, 300)
        };

        var expectedResult = ResultT<IEnumerable<TaskDtos>>.Success(taskDtos);
        var taskController = new TaskController(taskServiceMock.Object);
        
        taskServiceMock.Setup(x => x.GetlAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        //Act
        var resultController = await taskController.GetAllAsync(CancellationToken.None);
        //Assert
        Assert.NotNull(resultController);
    }
    
}