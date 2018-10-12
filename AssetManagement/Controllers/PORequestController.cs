using System.Web.Mvc;
using Data.Services;

namespace AssetManagement.Controllers
{
    public class PORequestController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public PORequestController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadDataCbbCategory()
        {
            var categories = _categoryService.GetAll();
            return PartialView("_CbbCategoryPartial", categories);
        }

        public ActionResult LoadDataCbbProduct(int categoryId)
        {
            var products = _productService.GetProductsByCategoryId(categoryId);
            return PartialView("_CbbProductPartial", products);
        }
    }
}