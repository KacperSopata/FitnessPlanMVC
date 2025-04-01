using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using FitnessPlanMVC.Application.Services;
using FitnessPlanMVC.Application.ViewModels.UserMeal;
using FitnessPlanMVC.Application.ViewModels.Product;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;

public class UserMealServiceTests
{
    private readonly Mock<IUserMealRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UserMealService _service;

    public UserMealServiceTests()
    {
        _mockRepo = new Mock<IUserMealRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new UserMealService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public void AddMeal_ValidModel_ReturnsId()
    {
        var vm = new UserNewMealVm();
        var meal = new Meal { Id = 1 };

        _mockMapper.Setup(m => m.Map<Meal>(vm)).Returns(meal);
        _mockRepo.Setup(r => r.AddMeal(meal)).Returns(1);

        var result = _service.AddMeal(vm);

        Assert.Equal(1, result);
    }

    [Fact]
    public void AddProductToMeal_ValidModel_ReturnsId()
    {
        var vm = new NewProductInUserInMealVm();
        var product = new MealProduct { Id = 10 };

        _mockMapper.Setup(m => m.Map<MealProduct>(vm)).Returns(product);
        _mockRepo.Setup(r => r.AddProduct(product)).Returns(10);

        var result = _service.AddProductToMeal(vm);

        Assert.Equal(10, result);
    }

    [Fact]
    public void DeleteMeal_ValidId_CallsRepoDelete()
    {
        _service.DeleteMeal(5);
        _mockRepo.Verify(r => r.DeleteMeal(5), Times.Once);
    }

    [Fact]
    public void DoesProductExistInMeal_ReturnsTrue()
    {
        _mockRepo.Setup(r => r.DoesProductExistInMeal(1, 2)).Returns(true);
        var result = _service.DoesProductExistInMeal(1, 2);
        Assert.True(result);
    }

    [Fact]
    public void GetMealDate_ReturnsCorrectDate()
    {
        var expectedDate = new DateTime(2024, 1, 1);
        _mockRepo.Setup(r => r.GetMealData(3)).Returns(expectedDate);

        var result = _service.GetMealDate(3);

        Assert.Equal(expectedDate, result);
    }
}
