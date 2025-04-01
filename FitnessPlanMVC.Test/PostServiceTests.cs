using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using FitnessPlanMVC.Application.Service;
using FitnessPlanMVC.Application.ViewModels.Post;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using System.IO;

namespace FitnessPlanMVC.Tests
{
    public class PostServiceTests
    {
        private readonly Mock<IPostRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PostService _service;

        public PostServiceTests()
        {
            _mockRepo = new Mock<IPostRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new PostService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void AddPost_ValidData_ReturnsNewId()
        {
            // Arrange
            var vm = new NewPostVm();
            var post = new Post { Id = 123 };

            _mockMapper.Setup(m => m.Map<Post>(vm)).Returns(post);
            _mockRepo.Setup(r => r.AddPost(post)).Returns(123);

            // Act
            var result = _service.AddPost(vm);

            // Assert
            Assert.Equal(123, result);
            _mockRepo.Verify(r => r.AddPost(post), Times.Once);
        }

        [Fact]
        public void DeletePost_ValidId_DeletesPost()
        {
            var post = new Post { Id = 10 };
            _mockRepo.Setup(r => r.GetDetail(10)).Returns(post);

            _service.DeletePost(10);

            _mockRepo.Verify(r => r.DeletePost(post), Times.Once);
        }

        [Fact]
        public void GetAllPostForList_ReturnsPagedList()
        {
            // Arrange
            var posts = new List<Post>
            {   
        new Post { Id = 1, Title = "Test" },
        new Post { Id = 2, Title = "Sample" }
            }.AsQueryable();

            var mockRepo = new Mock<IPostRepository>();
            mockRepo.Setup(r => r.GetAllPost()).Returns(posts);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, PostForListVm>();
            });

            var mapper = config.CreateMapper();
            var service = new PostService(mockRepo.Object, mapper);

            // Act
            var result = service.GetAllPostForList(10, 1, "");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(2, result.Posts.Count);
        }


        [Fact]
        public void GetPostDetail_ValidId_ReturnsPostVm()
        {
            var post = new Post { Id = 2 };
            var vm = new PostDetailVm { Id = 2 };

            _mockRepo.Setup(r => r.GetDetail(2)).Returns(post);
            _mockMapper.Setup(m => m.Map<PostDetailVm>(post)).Returns(vm);

            var result = _service.GetPostDetail(2);

            Assert.Equal(2, result.Id);
        }
    }
}
