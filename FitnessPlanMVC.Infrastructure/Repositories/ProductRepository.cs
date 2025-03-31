using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessPlanMVC.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FitnessPlanMVCDbContext _context;

        public ProductRepository(FitnessPlanMVCDbContext context)
        {
            _context = context;
        }

        public void DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }


        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;

        }

        public Product GetProductById(int productId)
        {
            var product = _context.Products.FirstOrDefault(i => i.Id == productId);
            return product;
        }

        public Product GetDetails(int nutritionalValueId)
        {
            var nv = _context.Products.FirstOrDefault(n => n.Id == nutritionalValueId);
            return nv;
        }

        public IQueryable<Product> GetAllProduct()
        {
            var products = _context.Products.AsQueryable();
            return products;
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.Local.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).State = EntityState.Detached;
            }
            _context.Attach(product);
            _context.Entry(product).Property("Name").IsModified = true;
            _context.Entry(product).Property("Calories").IsModified = true;
            _context.Entry(product).Property("Protein").IsModified = true;
            _context.Entry(product).Property("Fat").IsModified = true;
            _context.Entry(product).Property("Carbohydrates").IsModified = true;
            _context.SaveChanges();
        }
    }
}