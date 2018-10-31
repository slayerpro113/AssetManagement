using Data.Entities;
using Data.Services;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AssetManagement.Models;
using AutoMapper;
using Data.Utilities;
using Data.Utilities.Enumeration;

namespace AssetManagement.Controllers
{
   // [PermissionLogin]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Product(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }

        public ActionResult GetProductByCategoryId(int categoryId)
        {
            ViewBag.CategoryId = categoryId;          
            var products = Mapper.Map<List<ProductViewModel>>(_productService.GetProductsByCategoryId(categoryId));
            return View("_ProductPartial", products);
        }

        //[RolePermission(Enumerations.Roles.Manager)]
        [HttpPost, ValidateInput(false)]
        public ActionResult AddProduct(Product product, int categoryId)
        {
            try
            {
                _productService.AddProduct(product);
            }
            catch (Exception )
            {
                ViewData["EditError"] = "Please, Enter Your Information again.";
            }

            return GetProductByCategoryId(categoryId);
        }

       // [RolePermission(Enumerations.Roles.Manager)]
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateProduct(Product product, int categoryId)
        {
            try
            {
                _productService.UpdateProduct(product);
            }
            catch (Exception)
            {
                ViewData["EditError"] = "Please, Enter Your Information again.";
            }

            return GetProductByCategoryId(categoryId);
        }

        //[RolePermission(Enumerations.Roles.Manager)]
        public ActionResult DeleteProduct(int productId, int categoryId)
        {
            _productService.DeleteEntity(productId);
            return GetProductByCategoryId(categoryId);
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }
    }
}