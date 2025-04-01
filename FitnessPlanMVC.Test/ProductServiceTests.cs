using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using FitnessPlanMVC.Application.Service;
using FitnessPlanMVC.Application.ViewModels.Product;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ProductService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetDetails_ValidId_ReturnsProductDetailVm()
        {
            var product = new Product { Id = 1 };
            var vm = new ProductDetailVm { Id = 1 };

            _mockRepo.Setup(r => r.GetDetails(1)).Returns(product);
            _mockMapper.Setup(m => m.Map<ProductDetailVm>(product)).Returns(vm);

            var result = _service.GetDetails(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetproductForEdit_ValidId_ReturnsNewProductVm()
        {
            var product = new Product { Id = 2 };
            var vm = new NewProductVm { Id = 2 };

            _mockRepo.Setup(r => r.GetProductById(2)).Returns(product);
            _mockMapper.Setup(m => m.Map<NewProductVm>(product)).Returns(vm);

            var result = _service.GetproductForEdit(2);

            Assert.Equal(2, result.Id);
        }

        [Fact]
        public void UpdateProduct_ValidModel_UpdatesEntity()
        {
            var vm = new NewProductVm { Id = 5 };
            var mapped = new Product { Id = 5 };

            _mockMapper.Setup(m => m.Map<Product>(vm)).Returns(mapped);

            _service.UpdateProduct(vm);

            _mockRepo.Verify(r => r.UpdateProduct(mapped), Times.Once);
        }

        [Fact]
        public void DeleteProduct_ValidId_DeletesProduct()
        {
            _service.DeleteProduct(9);

            _mockRepo.Verify(r => r.DeleteProduct(9), Times.Once);
        }

        [Fact]
        public void AddProduct_ValidModel_ReturnsId()
        {
            var vm = new NewProductVm();
            var mapped = new Product { Id = 55 };

            _mockMapper.Setup(m => m.Map<Product>(vm)).Returns(mapped);
            _mockRepo.Setup(r => r.AddProduct(mapped)).Returns(55);

            var result = _service.AddProduct(vm);

            Assert.Equal(55, result);
        }

        [Fact]
        public void GetAllProductForList_WithSearch_ReturnsPagedResult()
        {
            // Arrange
            var products = new List<Product>
    {
        new Product { Id = 1, Name = "Banan" },
        new Product { Id = 2, Name = "Baton" },
        new Product { Id = 3, Name = "Brokuł" }
    }.AsQueryable();

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetAllProduct()).Returns(products);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductForListVM>();
            });

            var mapper = config.CreateMapper();

            var service = new ProductService(mockRepo.Object, mapper);

            // Act
            var result = service.GetAllProductForList(10, 1, "B");

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(3, result.Products.Count); // Banan, Baton, Brokuł
        }

    }
}
