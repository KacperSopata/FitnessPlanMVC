using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using FitnessPlanMVC.Application.Services;
using FitnessPlanMVC.Application.ViewModels.ReadyPlanWorkout;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Tests
{
    public class ReadyPlanWorkoutServiceTests
    {
        private readonly Mock<IReadyPlanWorkoutRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReadyPlanWorkoutService _service;

        public ReadyPlanWorkoutServiceTests()
        {
            _mockRepo = new Mock<IReadyPlanWorkoutRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ReadyPlanWorkoutService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void AddReadyPlanWorkout_ValidModel_ReturnsId()
        {
            var vm = new NewReadyPlanWorkoutVm
            {
                Name = "Cardio",
                PlanType = "Endurance",
                Difficulty = "Medium",
                Description = "Some desc",
                PlanDetails = "Steps",
            };

            var plan = new ReadyPlanWorkout { Id = 10 };

            _mockRepo.Setup(r => r.AddReadyPlanWorkout(It.IsAny<ReadyPlanWorkout>()))
                     .Callback<ReadyPlanWorkout>(p => p.Id = plan.Id);

            var result = _service.AddReadyPlanWorkout(vm);

            Assert.Equal(10, result);
        }

        [Fact]
        public void DeleteReadyPlanWorkout_ValidId_Deletes()
        {
            var plan = new ReadyPlanWorkout { Id = 5 };
            _mockRepo.Setup(r => r.GetDetail(5)).Returns(plan);

            _service.DeleteReadyPlanWorkout(5);

            _mockRepo.Verify(r => r.DeleteReadyPlanWorkout(plan), Times.Once);
        }

        [Fact]
        public void GetAllReadyPlanWorkoutsForList_WithSearch_ReturnsFilteredList()
        {
            var plans = new List<ReadyPlanWorkout>
            {
                new ReadyPlanWorkout { Id = 1, Name = "Plan A", PlanType = "Type1", PlanDetails = "..." },
                new ReadyPlanWorkout { Id = 2, Name = "Plan B", PlanType = "Type2", PlanDetails = "..." }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAllPlanReadyWorkout()).Returns(plans);

            var result = _service.GetAllReadyPlanWorkoutsForList(10, 1, "Plan");

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetPlansByDifficulty_ReturnsCorrectQuery()
        {
            var plans = new List<ReadyPlanWorkout>
            {
                new ReadyPlanWorkout { Id = 1, Difficulty = "Easy" }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetPlansByDifficulty("Easy")).Returns(plans);

            var result = _service.GetPlansByDifficulty("Easy");

            Assert.Single(result);
        }

        [Fact]
        public void GetPlansByTypeAndDifficulty2_ReturnsFilteredMappedList()
        {
            var plans = new List<ReadyPlanWorkout>
            {
                new ReadyPlanWorkout
                {
                    Id = 1,
                    Name = "Plan X",
                    PlanType = "Strength",
                    Difficulty = "Hard",
                    Description = "Desc",
                    PlanDetails = "Steps"
                }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAllPlanReadyWorkout()).Returns(plans);

            var result = _service.GetPlansByTypeAndDifficulty2("Strength", "Hard");

            Assert.Single(result);
            Assert.Equal("Plan X", result[0].Name);
        }
    }
}
