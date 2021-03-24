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
        int AddProduct(ProductModel product);
        Task UpdateProduct(ProductModel product);
        Task DeleteProduct(long id);
    }
}
