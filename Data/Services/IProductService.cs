using System.Collections.Generic;
using Data.DTO;
using Data.Entities;

namespace Data.Services
{
    public interface IProductService : IBaseService<Product>
    {
        IList<Product> GetProductsByCategoryId(int categoryId);
        void UpdateProduct(Product product);
        void AddProduct(Product product);
        IList<BaseModel> GetDataAutocomplete(string productName, string categoryName);
    }
}