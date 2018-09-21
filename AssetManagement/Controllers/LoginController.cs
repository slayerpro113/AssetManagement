using AssetManagement.Helper;
using AssetManagement.Models;
using Data.Services;
using System.Web.Mvc;

namespace AssetManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly IPersonnelService _personnelService;

        public LoginController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var status = _personnelService.CheckLogin(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (status == 1)
                {
                    return Json(new { status = "WrongUserName", message = "UserName is incorrect" }, JsonRequestBehavior.AllowGet);
                }
                else if (status == 2)
                {
                    return Json(new { status = "WrongPassword", message = "Password is incorrect" }, JsonRequestBehavior.AllowGet);
                }
                else if(status == 3 )
                {
                    var user = _personnelService.GetPersonnelByUserName(model.UserName);
                    Session.Add(Constants.USER_SESSION, user);
                }
            }
            return Json(new{status = "failed", message = "Password is incorrect" }, JsonRequestBehavior.AllowGet);
        }
    }
}