using AutoMapper;
using FitnessPlanMVC.Application.Service;
using FitnessPlanMVC.Application.ViewModels.Challenge;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FitnessPlanMVC.Tests
{
    public class ChallengeServiceTests
    {
        private readonly Mock<IChallengeRepository> _mockChallengeRepo;
        private readonly Mock<IUserChallengeRepository> _mockUserChallengeRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ChallengeService _service;

        public ChallengeServiceTests()
        {
            _mockChallengeRepo = new Mock<IChallengeRepository>();
            _mockUserChallengeRepo = new Mock<IUserChallengeRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ChallengeService(_mockChallengeRepo.Object, _mockUserChallengeRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetAllChallenges_ReturnsMappedList()
        {
            var challenges = new List<Challenge> { new Challenge { Id = 1 }, new Challenge { Id = 2 } };
            var vm = new List<ChallengeForListVm> { new ChallengeForListVm { Id = 1 }, new ChallengeForListVm { Id = 2 } };

            _mockChallengeRepo.Setup(r => r.GetAll()).Returns(challenges);
            _mockMapper.Setup(m => m.Map<List<ChallengeForListVm>>(challenges)).Returns(vm);

            var result = _service.GetAllChallenges();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void AddChallenge_ReturnsId()
        {
            var vm = new NewChallengeVm { StartDate = DateTime.Today, DurationInDays = 10 };
            var mapped = new Challenge { Id = 5, StartDate = DateTime.Today, DurationInDays = 10 };

            _mockMapper.Setup(m => m.Map<Challenge>(vm)).Returns(mapped);
            _mockChallengeRepo.Setup(r => r.Add(mapped)).Returns(5);

            var result = _service.AddChallenge(vm);

            Assert.Equal(5, result);
        }

        [Fact]
        public void GetChallengeById_ReturnsMappedVm()
        {
            var entity = new Challenge { Id = 2 };
            var vm = new ChallengeDetailVm { Id = 2 };

            _mockChallengeRepo.Setup(r => r.GetById(2)).Returns(entity);
            _mockMapper.Setup(m => m.Map<ChallengeDetailVm>(entity)).Returns(vm);

            var result = _service.GetChallengeById(2);

            Assert.Equal(2, result.Id);
        }

        [Fact]
        public void AssignUserToChallenge_UserNotEnrolled_AssignsUser()
        {
            _mockUserChallengeRepo.Setup(r => r.IsUserEnrolled("user1", 1)).Returns(false);

            _service.AssignUserToChallenge(1, "user1");

            _mockUserChallengeRepo.Verify(r => r.AssignUserToChallenge(It.IsAny<UserChallenge>()), Times.Once);
        }

        //[Fact]
        //public void UpdateUserProgress_AddsProgressAndMarksCompleted()
        //{
        //    var challenge = new Challenge { Goal = 10 };
        //    var userChallenge = new UserChallenge { Id = 1, ChallengeId = 1, UserId = "u1", Progress = 5, Challenge = challenge };

        //    _mockUserChallengeRepo.Setup(r => r.GetByUserId("u1")).Returns(new List<UserChallenge> { userChallenge });

        //    _service.UpdateUserProgress(1, "u1", 5);

        //    _mockUserChallengeRepo.Verify(r => r.UpdateProgress(1, 10), Times.Once);
        //    _mockUserChallengeRepo.Verify(r => r.MarkAsCompleted(1), Times.Once);
        //}
    }
}