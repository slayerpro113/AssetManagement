using AssetManagement.Models;
using Data.Services;
using Data.Utilities;
using Data.Utilities.Enumeration;
using System;
using System.Web.Mvc;

namespace AssetManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly IPersonnelService _personnelService;
        private readonly IRolePersonnelService _rolePersonnelService;
        private readonly IRoleService _roleService;

        public LoginController(IPersonnelService personnelService, IRolePersonnelService rolePersonnelService, IRoleService roleService)
        {
            _personnelService = personnelService;
            _rolePersonnelService = rolePersonnelService;
            _roleService = roleService;
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
            try
            {
                var loginStatus = _personnelService.CheckLogin(model.UserName, Encryptor.MD5Hash(model.PassWord));

                if (loginStatus == Enumerations.LoginStatus.WrongUserName)
                {
                    return Json(new { status = "WrongUserName" }, JsonRequestBehavior.AllowGet);
                }
                else if (loginStatus == Enumerations.LoginStatus.WrongPassword)
                {
                    return Json(new { status = "WrongPassword" }, JsonRequestBehavior.AllowGet);
                }
                else if (loginStatus == Enumerations.LoginStatus.Succsess)
                {
                    var user = _personnelService.GetPersonnelByUserName(model.UserName);
                    var rolePersonnel = _rolePersonnelService.GetRolePersonnelByPersonnelId(user.PersonnelID);
                    var role = _roleService.Get(rolePersonnel.RoleID);
                    Session.Add(Constant.UserSession, user);
                    Session.Add(Constant.RoleSession, role);
                    return Json(new { status = "Succsess" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }
    }
}