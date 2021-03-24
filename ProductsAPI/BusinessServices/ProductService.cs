using Microsoft.EntityFrameworkCore;
using ProductsAPI.BusinessServices.Interfaces;
using ProductsAPI.Entities;
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

        public void AddProduct(ProductModel product)
        {
            Product productEntity = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Available = product.Available,
                DateCreated = DateTime.Now,
            };

            _productRepository.Insert(productEntity, true);
        }

        public void DeleteProduct(long id)
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProductById(long id)
        {
            ProductModel productModel = null;
            var productEntity = _productRepository.GetById(id);
            if (productEntity != null)
            {
                productModel = new ProductModel
                {
                    Id = productEntity.Id,
                    Name = productEntity.Name,
                    Price = productEntity.Price,
                    Available = productEntity.Available,
                    Description = productEntity.Description,
                    DateCreated = productEntity.DateCreated,
                    Version = productEntity.RowVersion
                };
            }

            return productModel;
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

        public void UpdateProduct(ProductModel product)
        {
            Product productEntity = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Available = product.Available,                
                RowVersion = product.Version
            };
            try
            {
                _productRepository.Update(productEntity, true);

            }
            catch (DbUpdateException ex)
            {
                //todo handle concurrency
                // if RowVersion is not matched 
                throw;
            }
        }
    }
}
