using System.Collections.Generic;
using DevExpress.Web.Mvc;
using System.Web.Mvc;
using Data.Entities;
using Data.Services;

namespace AssetManagement.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly IHistoryService _historyService;

        public AssetController(IAssetService assetService, IHistoryService historyService)
        {
            _assetService = assetService;
            _historyService = historyService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAssets()
        {
            var assets = _assetService.GetAssets();
            return View("_AssetPartial", assets);
        }

        public ActionResult MasterDetailDetailPartial(int assetId)
        {
            ViewBag.AssetID = assetId;
            var histories = _historyService.GetHistoriesByAssetId(assetId);
            return PartialView("_MasterDetailPartial", histories);
        }
    }
}