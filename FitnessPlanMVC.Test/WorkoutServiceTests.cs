using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using FitnessPlanMVC.Application.Service;
using FitnessPlanMVC.Application.ViewModels.Workout;

public class WorkoutServiceTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepo = new();
    private readonly Mock<IExerciseRepository> _mockExerciseRepo = new();
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly WorkoutService _service;

    public WorkoutServiceTests()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

        _service = new WorkoutService(
            _mockWorkoutRepo.Object,
            _mockExerciseRepo.Object,
            _mockMapper.Object,
            _mockUserManager.Object);
    }

    [Fact]
    public async Task AddWorkout_ValidUser_ReturnsWorkoutId()
    {
        // Arrange
        var vm = new NewWorkoutVm { UserId = "123" };
        var workout = new Workout();
        var user = new ApplicationUser { Id = "123" };

        _mockMapper.Setup(m => m.Map<Workout>(vm)).Returns(workout);
        _mockUserManager.Setup(u => u.FindByIdAsync("123")).ReturnsAsync(user);
        _mockWorkoutRepo.Setup(r => r.AddWorkout(workout)).Returns(42);

        // Act
        var result = await _service.AddWorkout(vm);

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void AddExerciseToWorkout_ValidModel_ReturnsId()
    {
        // Arrange
        var vm = new NewWorkoutExerciseVm { ExerciseId = 1, WorkoutId = 2 };
        var mapped = new WorkoutExercise { Id = 10 };

        _mockMapper.Setup(m => m.Map<WorkoutExercise>(vm)).Returns(mapped);
        _mockWorkoutRepo.Setup(r => r.AddExercise(mapped)).Returns(10);

        // Act
        var result = _service.AddExerciseToWorkout(vm);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void DeleteWorkout_ValidId_DeletesWorkout()
    {
        // Act
        _service.DeleteWorkout(5);

        // Assert
        _mockWorkoutRepo.Verify(r => r.DeleteWorkout(5), Times.Once);
    }

    [Fact]
    public void GetWorkoutExerciseById_ReturnsMappedVm()
    {
        // Arrange
        var entity = new WorkoutExercise { Id = 7 };
        var vm = new NewWorkoutExerciseVm { Id = 7 };

        _mockWorkoutRepo.Setup(r => r.GetWorkoutExerciseById(7)).Returns(entity);
        _mockMapper.Setup(m => m.Map<NewWorkoutExerciseVm>(entity)).Returns(vm);

        // Act
        var result = _service.GetWorkoutExerciseById(7);

        // Assert
        Assert.Equal(7, result.Id);
    }
}
