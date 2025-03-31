using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Application.ViewModels.Product;

namespace FitnessPlanMVC.Application.Interfaces
{
    public interface IProductService
    {
        ListProductForListVM GetAllProductForList(int pageSize, int pageNO, string searchString);

        int AddProduct(NewProductVm product);

        ProductDetailVm GetDetails(int productId);
        NewProductVm GetproductForEdit(int id);
        void UpdateProduct(NewProductVm model);
        void DeleteProduct(int id);
        //bool CanUserEditProduct(int productId, string userId, bool isAdmin);

    }
}
