using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface IProductRepository
    {
        void SaveProduct(Product p);
        Product GetProduct(int id);
        void DeleteProduct(Product p);
        void UpdateProduct(Product p);
        List<Category> GetCategories();
        List<Product> GetProducts();

    }
}
