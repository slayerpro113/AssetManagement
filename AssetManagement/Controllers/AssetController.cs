using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Data.Services;
using Data.Utilities;
using Data.Utilities.Enumeration;
using System.Web.Mvc;

namespace AssetManagement.Controllers
{
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
            return PartialView("_HistorieslPartial", histories);
        }

        public ActionResult LoadDataCbb()
        {
            var employees = _employeeService.GetAll();
            return PartialView("_ComboboxPartial", employees);
        }

        [HttpPost]
        public ActionResult AssignWithoutRequest(int employeeId, int assetId, string assignRemark, int staffAssignId)
        {
            var employee = _employeeService.GetEntity(employeeId);
            var asset = _assetService.GetEntity(assetId);

            if (asset.Barcode == null || asset.MonthDepreciation == null)
            {
                return Json(new {status = "Failed"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var status = _historyService.HandleAssignWithoutRequest(employee, asset, staffAssignId);

                if (status == Enumerations.AddEntityStatus.Success)
                {
                    return Json(new {status = "Success"}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new {status = "Failed"}, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult RecallAsset(int assetId, int staffRecallId)
        {
            var status = _historyService.HandleRecall(assetId, staffRecallId);

            if (status == Enumerations.UpdateEntityStatus.Success)
            {
                return Json(new {status = "Success"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {status = "Failed"}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult HandleEnterDetail(int assetId, string barcode, int monthsOfDepreciation)
        {
            if (string.IsNullOrEmpty(barcode) || monthsOfDepreciation < 0)
            {
                return Json(new {status = "Failed"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var status = _assetService.HandleEnterDetail(assetId, barcode, monthsOfDepreciation);

                if (status == Enumerations.UpdateEntityStatus.Success)
                {
                    return Json(new {status = "Success"}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new {status = "Failed"}, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult GenerateBarCode()
        {
            var barcode = _assetService.GenerateBarCode().Split('|');
            return Json(new {barcode = barcode[0], src = barcode[1]}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GenerateBarCode2(string barcode)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (Bitmap bitMap = new Bitmap(barcode.Length * 40, 80))
                {
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                        Font oFont = new Font("IDAutomationHC39M", 16);
                        PointF point = new PointF(2f, 2f);
                        SolidBrush whiteBrush = new SolidBrush(Color.White);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        SolidBrush blackBrush = new SolidBrush(Color.Black);
                        graphics.DrawString("*" + barcode + "*", oFont, blackBrush, point);
                    }

                    bitMap.Save(memoryStream, ImageFormat.Jpeg);

                    ViewBag.BarcodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            return View("AssetsInStock");
        }
    }
}