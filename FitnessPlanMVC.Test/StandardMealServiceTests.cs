using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using FitnessPlanMVC.Application.Services;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Application.ViewModels.StandardMeal;
using FitnessPlanMVC.Domain.Model;
using System.Linq;

public class StandardMealServiceTests
{
    private readonly Mock<IStandardMealRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly StandardMealService _service;

    public StandardMealServiceTests()
    {
        _mockRepo = new Mock<IStandardMealRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new StandardMealService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public void AddMealsToDay_WhenMealsDoNotExist_AddsDefaultMeals()
    {
        var date = DateTime.Today;
        _mockRepo.Setup(r => r.MealsExistForDate(date)).Returns(false);

        _mockMapper.Setup(m => m.Map<List<Meal>>(It.IsAny<List<StandardMealForListVm>>()))
                   .Returns(new List<Meal>());

        _service.AddMealsToDay(date);

        _mockRepo.Verify(r => r.AddMeals(It.IsAny<List<Meal>>()), Times.Once);
    }

    [Fact]
    public void AddMealsToDay_WhenMealsExist_DoesNotAdd()
    {
        var date = DateTime.Today;
        _mockRepo.Setup(r => r.MealsExistForDate(date)).Returns(true);

        _service.AddMealsToDay(date);

        _mockRepo.Verify(r => r.AddMeals(It.IsAny<List<Meal>>()), Times.Never);
    }

    [Fact]
    public void DeleteProduct_ValidId_CallsRepoDelete()
    {
        _service.DeleteProduct(5);
        _mockRepo.Verify(r => r.DeleteProduct(5), Times.Once);
    }

    [Fact]
    public void GetAllMealsForList_ReturnsCombinedVm()
    {
        var date = DateTime.Today;
        var meals = new List<Meal>
        {
            new Meal { Id = 1, Name = "Breakfast" } // Bez Products, jeśli nie istnieje
        }.AsQueryable();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Meal, StandardMealForListVm>();
            cfg.CreateMap<MealProduct, StandardMealDetailVm>();
        });

        var mapper = config.CreateMapper();

        var mockRepo = new Mock<IStandardMealRepository>();
        mockRepo.Setup(r => r.GetAllMeals(date)).Returns(meals);
        mockRepo.Setup(r => r.GetGrammageForProduct(It.IsAny<int>())).Returns(100);

        var service = new StandardMealService(mockRepo.Object, mapper);

        // Act
        var result = service.GetAllMealsForList(date);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ListStandardMealsForListVm>(result);
        Assert.Single(result.Meals);
        Assert.Empty(result.Products); // ponieważ brak produktów
    }

    [Fact]
    public void GetMealById_ReturnsCombinedVm()
    {
        _mockRepo.Setup(r => r.GetAllMealsById(1))
                 .Returns(new List<Meal> { new Meal { Id = 1, Name = "Obiad" } }.AsQueryable());

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Meal, StandardMealForListVm>();
            cfg.CreateMap<MealProduct, StandardMealDetailVm>();
        });

        var mapper = config.CreateMapper();
        var service = new StandardMealService(_mockRepo.Object, mapper);

        var result = service.GetMealById(1);

        Assert.NotNull(result);
        Assert.IsType<ListStandardMealsForListVm>(result);
        Assert.Single(result.Meals);
    }
}
