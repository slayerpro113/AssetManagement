using System.Web.Mvc;
using Data.Services;
using Data.Utilities;
using Data.Utilities.Enumeration;

namespace AssetManagement.Controllers
{
    //[PermissionLogin]
    public class PORequestController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IPORequest _poRequest;

        public PORequestController(IProductService productService, ICategoryService categoryService, IPORequest poRequest)
        {
            _productService = productService;
            _categoryService = categoryService;
            _poRequest = poRequest;
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

        public ActionResult LoadDataCbbProduct()
        {
            var products = _productService.GetAll();
            return PartialView("_CbbProductPartial", products);
        }

        public ActionResult HandlePoRequest(int productId, int employeeId, string description)
        {
           var status = _poRequest.HandlePoRequest(productId, employeeId, description);
            if (status == Enumerations.AddEntityStatus.Success)
            {
                return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}