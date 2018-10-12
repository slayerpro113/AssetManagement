﻿using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Data.Utilities.Enumeration;

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
            Enumerations.CategoryName category = Category(product.CategoryID);

            using (var ms = new MemoryStream(imageBytes))
            {
                using (var image = Image.FromStream(ms))
                {
                    image.Save(HttpContext.Current.Server.MapPath("~/Image/Categories/" + category + "/" + category + product.ProductID + ".jpg"), ImageFormat.Jpeg);
                }
            }
            product.Image = category + "/" + category + product.ProductID + ".jpg";

            return product;
        }

        public Enumerations.CategoryName Category(int categoryId)
        {
            if (categoryId == 1)
            {              
                return Enumerations.CategoryName.Chair;
            }
            else if (categoryId == 2)
            {
                return Enumerations.CategoryName.Keyboard;
            }
            else if (categoryId == 3)
            {
                return Enumerations.CategoryName.Mouse;
            }
            else if (categoryId == 4)
            {
                return Enumerations.CategoryName.PC;
            }
            else if (categoryId == 5)
            {
                return Enumerations.CategoryName.Printer;
            }
            else
            {
                return Enumerations.CategoryName.Screen;
            }
        }
    }
}