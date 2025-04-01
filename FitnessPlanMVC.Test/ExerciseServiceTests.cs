using AutoMapper;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.Service;
using FitnessPlanMVC.Application.ViewModels.Exercise;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FitnessPlanMVC.Tests
{
    public class ExerciseServiceTests
    {
        private readonly Mock<IExerciseRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ExerciseService _service;

        public ExerciseServiceTests()
        {
            _mockRepo = new Mock<IExerciseRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ExerciseService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetExerciseDetail_ValidId_ReturnsDetailVm()
        {
            var exercise = new Exercise { Id = 1, Name = "Push Up" };
            var vm = new ExerciseDetailVm { Id = 1, Name = "Push Up" };

            _mockRepo.Setup(r => r.GetDetail(1)).Returns(exercise);
            _mockMapper.Setup(m => m.Map<ExerciseDetailVm>(exercise)).Returns(vm);

            var result = _service.GetExerciseDetail(1);

            Assert.NotNull(result);
            Assert.Equal("Push Up", result.Name);
        }

        [Fact]
        public void GetExerciseDetailByWorkoutExercise_ValidId_ReturnsDetailVm()
        {
            var exercise = new Exercise { Id = 2, Name = "Squat" };
            var vm = new ExerciseDetailVm { Id = 2, Name = "Squat" };

            _mockRepo.Setup(r => r.GetDetailByWorkoutExercise(2)).Returns(exercise);
            _mockMapper.Setup(m => m.Map<ExerciseDetailVm>(exercise)).Returns(vm);

            var result = _service.GetExerciseDetailByWorkoutExercise(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }
    }
}
