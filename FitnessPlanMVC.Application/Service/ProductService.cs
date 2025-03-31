using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Product;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Application.Service
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public ProductDetailVm GetDetails(int productId)
        {
            var product = _productRepo.GetDetails(productId);
            var productVm = _mapper.Map<ProductDetailVm>(product);
            return productVm;
        }

        public NewProductVm GetproductForEdit(int id)
        {
            var product = _productRepo.GetProductById(id);
            var productVm = _mapper.Map<NewProductVm>(product);
            return productVm;
        }

        public void UpdateProduct(NewProductVm model)
        {
            var product = _mapper.Map<Product>(model);
            _productRepo.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            _productRepo.DeleteProduct(id);
        }

        //public bool CanUserEditProduct(int productId, string userId, bool isAdmin)
        //{

        //    var product = _productRepo.GetProductById(productId);
        //    if (product == null) return false;

        //    return isAdmin || product.UserId == userId;

        //}


        public ListProductForListVM GetAllProductForList(int pageSize, int pageNO, string searchString)
        {
            var products = _productRepo.GetAllProduct().Where(p => p.Name.StartsWith(searchString))
                .ProjectTo<FitnessPlanMVC.Application.ViewModels.Product.ProductForListVM>(_mapper.ConfigurationProvider).ToList();
            var productToShow = products.Skip(pageSize * (pageNO - 1)).Take(pageSize).ToList();
            var productsList = new ListProductForListVM()
            {
                PageSize = pageSize,
                CurrentPage = pageNO,
                SearchString = searchString,
                Products = productToShow,
                Count = products.Count
            };
            return productsList;
        }

        public int AddProduct(NewProductVm product)
        {
            var prod = _mapper.Map<Product>(product);
            var id = _productRepo.AddProduct(prod);
            return id;
        }

    }
}