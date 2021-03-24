using ProductsAPI.Entities;
using ProductsAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Repositories
{
    public class ProductRepository : GenericRepository<Entities.Product>, IProductRepository
    {
        private ProductContext productContext;

        public ProductRepository(ProductContext context)
            : base(context)
        {
            productContext = context;
        }
    }
}
