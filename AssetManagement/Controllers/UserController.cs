﻿using System.Web.Mvc;
using Data.Entities;
using Data.Services;
using Data.Utilities;
using Data.Utilities.Enumeration;

namespace AssetManagement.Controllers
{
    [PermissionLogin]
    public class UserController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly IAssetService _assetServiceService;
        private readonly IPoRequestService _poRequestService;

        public UserController(IHistoryService historyService, IAssetService assetService, IPoRequestService poRequestService)
        {
            _historyService = historyService;
            _assetServiceService = assetService;
            _poRequestService = poRequestService;
        }

        public ActionResult GetAssetsDetail()
        {
            var employee = (Employee)Session[Constant.UserSession];
            var employeeId = employee.EmployeeID;
            var histories = _historyService.GetHistoriesByEmployeeId(employeeId);
            var assets = _assetServiceService.GetAssetsByHistories(histories);

            return View("AssetsDetail", assets);
        }

        public ActionResult RequestForm()
        {
            return View("RequestForm");
        }

        public ActionResult HandlePoRequest(int employeeId, string description, string device)
        {
            var status = _poRequestService.HandlePoRequest(employeeId, description, device);
            if (status == Enumerations.AddEntityStatus.Success)
            {
                return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else if (status == Enumerations.AddEntityStatus.Existed)
            {
                return Json(new { status = "Existed" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RequestsHistory()
        {
            var employee = (Employee)Session[Constant.UserSession];
            var employeeId = employee.EmployeeID;
            ViewBag.EmployeeId = employeeId;
            return View("RequestsHistory");
        }

        public ActionResult GetRequestHistoryPartial(int employeeId)
        {
            ViewBag.EmployeeId = employeeId;
            var poRequests = _poRequestService.GetPoRequestsByEmployeeId(employeeId);
            return View("_RequestHistoryPartial", poRequests);
        }
    }
}