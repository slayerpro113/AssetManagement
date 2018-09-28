using Data.Entities;
using Data.Services;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace AssetManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductService _productService;

        public CategoryController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }

        public ActionResult GetProducts(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            var products = _productService.GetProductsByCategoryId(categoryId);
            return View("_GridViewPartial", products);
        }

        public ActionResult UpdateProduct(Product product)
        {
            _productService.UpdateProduct(product);
            return View("_GridViewPartial");
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }
    }
}