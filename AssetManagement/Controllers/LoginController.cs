﻿using Data.Services;
using Data.Utilities;
using Data.Utilities.Enumeration;
using System;
using System.Web.Mvc;

namespace AssetManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRoleService _roleService;

        public LoginController(IEmployeeService personnelService, IRoleService roleService)
        {
            _employeeService = personnelService;
            _roleService = roleService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            try
            {
                var loginStatus = _employeeService.CheckLogin(userName, Encryptor.MD5Hash(password));

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
                    var user = _employeeService.GetEmployeeByUserName(userName);
                    var roleName = user.Role.RoleName;
                    Session.Add(Constant.UserSession, user);
                    Session.Add(Constant.RoleSession, roleName);
                    return Json(new { status = "Succsess" , employeeId = user.EmployeeID, roleName = roleName }, JsonRequestBehavior.AllowGet);
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