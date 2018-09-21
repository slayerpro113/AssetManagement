using AssetManagement.Helper;
using Data.Services;
using Data.Entities;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Index()
        {
            var userSession = (Personnel) System.Web.HttpContext.Current.Session[Constants.USER_SESSION];
            ViewBag.FullName = userSession.FullName;
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