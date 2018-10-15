using System;
using Data.Services;
using System.Web.Mvc;
using Data.Utilities;
using Data.Utilities.Enumeration;

namespace AssetManagement.Controllers
{
    //[PermissionLogin]
    //[RolePermission(Enumerations.Roles.Manager)]
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

        public ActionResult Index()
        {
            var assets = _assetService.GetAssets();
            return View(assets);
        }

        public ActionResult GetAssets()
        {
            var assets = _assetService.GetAssets();
            return View("_AssetPartial", assets);
        }

        //[RolePermission(Enumerations.Roles.Manager)]
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

        //[RolePermission(Enumerations.Roles.Manager)]
        [HttpPost]
        public ActionResult AssignAsset(int assetId, int employeeId)
        {
            var statusId = 2;
            _assetService.SetAssetStatus(assetId, statusId);
            var status = _historyService.HandleHistory(assetId, employeeId);
            
            if (status == Enumerations.AddEntityStatus.Success)
            {
                return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }

        //[RolePermission(Enumerations.Roles.Manager)]
        [HttpPost]
        public ActionResult RecallAsset(int assetId, string remark)
        {
            var statusId = 1;
            _assetService.SetAssetStatus(assetId, statusId);
            var status = _historyService.SaveCheckoutDate(assetId, remark);

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