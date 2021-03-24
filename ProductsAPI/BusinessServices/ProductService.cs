using ProductsAPI.BusinessServices.Interfaces;
using ProductsAPI.Models;
using ProductsAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.BusinessServices
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            if (productRepository != null)
                this._productRepository = productRepository;
        }

        public int AddProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<ProductModel> GetProducts()
        {
            var productEntites = _productRepository.GetAll();

            return productEntites.Select(p => new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Available = p.Available,
                Description = p.Description,
                DateCreated = p.DateCreated,
                Version = p.RowVersion
            }).ToList();
        }

        public Task UpdateProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}
