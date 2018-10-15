using Data.Services;
using System.Web.Mvc;
using Data.Utilities;

namespace AssetManagement.Controllers
{
    [PermissionLogin]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //[RolePermission(Enumerations.Roles.User)]
        public ActionResult Index()
        {
            var model = _categoryService.GetAll();
            return View(model);
        }

        public ActionResult GridViewPartialView()
        {
            var model = _categoryService.GetAll();
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", model);
        }
    }
}

public enum HeaderViewRenderMode { Full, Title }