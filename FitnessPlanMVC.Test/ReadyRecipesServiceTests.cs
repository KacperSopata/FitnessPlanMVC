using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using FitnessPlanMVC.Application.Services;
using FitnessPlanMVC.Application.ViewModels.ReadyRecipes;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace FitnessPlanMVC.Tests
{
    public class ReadyRecipesServiceTests
    {
        private readonly Mock<IReadyRecipesRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReadyRecipesService _service;

        public ReadyRecipesServiceTests()
        {
            _mockRepo = new Mock<IReadyRecipesRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ReadyRecipesService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void AddReadyRecipes_WithImage_ReturnsCorrectId()
        {
            // Arrange
            var mockRepo = new Mock<IReadyRecipesRepository>();
            var mockMapper = new Mock<IMapper>();

            var fileContent = new byte[] { 1, 2, 3 };
            var stream = new MemoryStream(fileContent);
            var formFile = new FormFile(stream, 0, fileContent.Length, "Image", "image.jpg");

            var vm = new NewReadyRecipesVm
            {
                Title = "Healthy Meal",
                ImageContent = formFile,
                Image = fileContent
            };

            var mapped = new ReadyRecipes { Id = 42, Title = "Healthy Meal", Image = fileContent };

            mockMapper.Setup(m => m.Map<ReadyRecipes>(vm)).Returns(mapped);
            mockRepo.Setup(r => r.AddReadyRecipes(mapped)).Returns(42);

            var service = new ReadyRecipesService(mockRepo.Object, mockMapper.Object);

            // Act
            var result = service.AddReadyRecipes(vm);

            // Assert
            Assert.Equal(42, result);
        }


        [Fact]
        public void DeleteReadyRecipes_ExistingId_DeletesRecipe()
        {
            var recipe = new ReadyRecipes { Id = 5 };
            _mockRepo.Setup(r => r.GetDetail(5)).Returns(recipe);

            _service.DeleteReadyRecipes(5);

            _mockRepo.Verify(r => r.DeleteReadyRecipes(recipe), Times.Once);
        }

        [Fact]
        public void GetAllReadyRecipesForList_ReturnsFilteredPagedResults()
        {
            var recipes = new List<ReadyRecipes>
    {
        new ReadyRecipes { Id = 1, Title = "Salad" },
        new ReadyRecipes { Id = 2, Title = "Soup" },
        new ReadyRecipes { Id = 3, Title = "Smoothie" }
    }.AsQueryable();

            // Mapper konfiguracyjny
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReadyRecipes, ReadyRecipesForListVm>();
            });

            var mapper = config.CreateMapper();

            var mockRepo = new Mock<IReadyRecipesRepository>();
            mockRepo.Setup(r => r.GetAllReadyRecipes()).Returns(recipes);

            var service = new ReadyRecipesService(mockRepo.Object, mapper);

            // Act
            var result = service.GetAllReadyRecipesForList(2, 1, "");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ReadyRecipes.Count); // 2 elementy na pierwszej stronie
            Assert.Equal(3, result.Count); // łącznie 3
        }


        [Fact]
        public void GetReadyRecipesDetail_ValidId_ReturnsMappedVm()
        {
            var recipe = new ReadyRecipes { Id = 7, Title = "Protein Shake" };
            var recipeVm = new ReadyRecipesDetailVm { Id = 7, Title = "Protein Shake" };

            _mockRepo.Setup(r => r.GetDetail(7)).Returns(recipe);
            _mockMapper.Setup(m => m.Map<ReadyRecipesDetailVm>(recipe)).Returns(recipeVm);

            var result = _service.GetReadyRecipesDetail(7);

            Assert.NotNull(result);
            Assert.Equal(7, result.Id);
        }
    }
}
