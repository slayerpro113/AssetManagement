using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Service
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> productRepository) : base(unitOfWork, productRepository)
        {
            _productRepository = productRepository;
        }

        public IList<Product> GetProductsByCategoryId(int categoryId)
        {
            var products = _productRepository.GetProductsByCategoryId(categoryId);
            foreach (var product in products)
            {
               
                string path = HttpContext.Current.Server.MapPath("~/Image/Categories/" + product.Image);
                product.ImageBytes = File.ReadAllBytes(path);
            }
            return products;
        }

        public void UpdateProduct(Product product)
        {
            var newProduct = HandleImage(product);
            UpdateEntity(product);
        }

        public void AddProduct(Product product)
        {
            var newProduct = HandleImage(product);
            AddEntity(newProduct);
        }

        public Product HandleImage(Product product)
        {
            byte[] imageBytes = product.ImageBytes;
            string category = Category(product.CategoryID);

            using (var ms = new MemoryStream(imageBytes))
            {
                using (var image = Image.FromStream(ms))
                {
                    image.Save(HttpContext.Current.Server.MapPath("~/Image/Categories/" + category + "/" + category + product.ProductName + ".jpg"), ImageFormat.Jpeg);
                }
            }
            product.Image = category + "/" + category + product.ProductName + ".jpg";

            return product;
        }

        public string Category(int categoryId)
        {
            if (categoryId == 1)
            {
                string category = "Chair";
                return category;
            }
            else if (categoryId == 2)
            {
                string category = "Keyboard";
                return category;
            }
            else if (categoryId == 3)
            {
                string category = "Mouse";
                return category;
            }
            else if (categoryId == 4)
            {
                string category = "PC";
                return category;
            }
            else if (categoryId == 5)
            {
                string category = "Printer";
                return category;
            }
            else
            {
                string category = "Screen";
                return category;
            }
        }
    }
}