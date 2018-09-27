using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;
using System.Collections.Generic;

namespace Service
{
    public class ProductService : BaseService<Product, AssetManagementEntities>, IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> productRepository) : base(unitOfWork, productRepository)
        {
            _productRepository = productRepository;
        }

        public IList<Product> GetProductsByCategoryId(int categoryId)
        {
            var products = _productRepository.GetProductsByCategoryId(categoryId);
            return products;
        }
    }
}