using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Domain.Interfaces
{
    public interface IProductRepository
    {
        void DeleteProduct(int productId);

        int AddProduct(Product product);

        Product GetProductById(int productId);

        IQueryable<Product> GetAllProduct();

        void UpdateProduct(Product updatingProduct);

        Product GetDetails(int productId);

    }
}
