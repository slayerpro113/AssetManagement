using Data.Services;
using Data.Utilities.Enumeration;
using System.Web.Mvc;

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

       // [RolePermission(Enumerations.Roles.Manager)]
        [HttpPost]
        public ActionResult AssignAsset(int employeeId, int assetId, string assignRemark, string staffAssign)
        {
            var employee = _employeeService.GetEntity(employeeId);
            var asset = _assetService.GetEntity(assetId);
            var status = _historyService.HandleAssignWithoutRequest(employee, asset, assignRemark, staffAssign);

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
        public ActionResult RecallAsset(int assetId, string recallRemark, string staffRecall)
        {
            var status = _historyService.HandleRecall(assetId, recallRemark, staffRecall);

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