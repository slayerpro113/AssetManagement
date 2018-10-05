﻿using Data.Entities;
using Data.Services;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AssetManagement.Models;
using AutoMapper;

namespace AssetManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAssetService _assetService;

        public ProductController(IProductService productService, IAssetService assetService)
        {
            _productService = productService;
            _assetService = assetService;
        }

        public ActionResult Index(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }

        public ActionResult GetProducts(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            
            var products = Mapper.Map<List<ProductViewModel>>(_productService.GetProductsByCategoryId(categoryId));
            return View("_ProductPartial", products);
        }

        //[RolePermission(Enumerations.Roles.Manager)
        [HttpPost, ValidateInput(false)]
        public ActionResult AddProduct(Product product, int categoryId)
        {
            try
            {
                _productService.AddProduct(product);
            }
            catch (Exception e)
            {
                ViewData["EditError"] = "Please, Enter Your Information again.";
            }

            return GetProducts(categoryId);
        }

        //[RolePermission(Enumerations.Roles.Manager)
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateProduct(Product product, int categoryId)
        {
            try
            {
                _productService.UpdateProduct(product);
            }
            catch (Exception e)
            {
                ViewData["EditError"] = "Please, Enter Your Information again.";
            }

            return GetProducts(categoryId);
        }

        //[RolePermission(Enumerations.Roles.Manager)
        public ActionResult DeleteProduct(int productId, int categoryId)
        {
            _productService.DeleteEntity(productId);
            return GetProducts(categoryId);
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }
    }
}