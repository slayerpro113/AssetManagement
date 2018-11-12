﻿using System.Collections.Generic;
using Data.Services;
using Data.Utilities.Enumeration;
using System.Web.Mvc;
using Data.Entities;
using Data.Utilities;

namespace AssetManagement.Controllers
{
    [PermissionLogin]
    [RolePermission(Enumerations.Roles.Staff, Enumerations.Roles.Manager)]

    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly IHistoryService _historyService;
        private readonly IEmployeeService _employeeService;

        public AssetController(IAssetService assetService, IHistoryService historyService,
            IEmployeeService employeeService)
        {
            _assetService = assetService;
            _historyService = historyService;
            _employeeService = employeeService;
        }

        public ActionResult AssetsInStock()
        {
            var assets = _assetService.GetAssetsInStock();
            return View("AssetsInStock", assets);
        }

        public ActionResult CallBackAssetsInStock()
        {
            var assets = _assetService.GetAssetsInStock();
            return View("_AssetsInStockPartial", assets);
        }

        public ActionResult AssetsInUse()
        {
            var assets = _assetService.GetAssetsInUse();
            return View("AssetsInUse", assets);
        }

        public ActionResult CallBackAssetsInUse()
        {
            var assets = _assetService.GetAssetsInUse();
            return View("_AssetsInUsePartial", assets);
        }

        public ActionResult ShowHistories(int assetId)
        {
            ViewBag.AssetID = assetId;
            var histories = _historyService.GetHistoriesByAssetId(assetId);
            return PartialView("_AssetHistorieslPartial", histories);
        }
        
        public ActionResult LoadDataCbb()
        {
            var employees = _employeeService.GetAll();
            return PartialView("_ComboboxPartial", employees);
        }

        [HttpPost]
        public ActionResult AssignWithoutRequest(int employeeId, int assetId, string assignRemark, string staffAssign)
        {
            var employee = _employeeService.GetEntity(employeeId);
            var asset = _assetService.GetEntity(assetId);
            
            if (asset.Barcode == null || asset.MonthDepreciation == null)
            {
                return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var status = _historyService.HandleAssignWithoutRequest(employee, asset, staffAssign);

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

        [HttpPost]
        public ActionResult RecallAsset(int assetId, string recallRemark, string staffRecall)
        {
            var status = _historyService.HandleRecall(assetId, staffRecall);

            if (status == Enumerations.UpdateEntityStatus.Success)
            {
                return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult HandleEnterDetail(int assetId, string barcode, int monthsOfDepreciation)
        {
            if (string.IsNullOrEmpty(barcode) || monthsOfDepreciation < 0)
            {
                return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var status = _assetService.HandleEnterDetail(assetId, barcode, monthsOfDepreciation);

                if (status == Enumerations.UpdateEntityStatus.Success)
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
}