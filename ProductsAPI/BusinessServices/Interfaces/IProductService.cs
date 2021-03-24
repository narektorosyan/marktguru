using ProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.BusinessServices.Interfaces
{
    public interface IProductService
    {
        IList<ProductModel> GetProducts();
        ProductModel GetProductById(long id);
        void AddProduct(ProductModel product);
        void UpdateProduct(ProductModel product);
        void DeleteProduct(long id);
    }
}
