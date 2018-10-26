using System.Web;
using Data.Services;
using System.Web.Mvc;
using Data.Entities;
using Data.Utilities.Enumeration;
using DevExpress.Web.Internal;

namespace AssetManagement.Controllers
{
    public class StaffController : Controller
    {
        private readonly IPoRequestService _poRequestService;
        private readonly IAssetService _assetService;
        private readonly IHistoryService _historyService;
        private readonly IQuoteService _quoteService;


        public StaffController(IPoRequestService poRequestService, IAssetService assetService, IHistoryService historyService, IQuoteService quoteService)
        {
            _poRequestService = poRequestService;
            _assetService = assetService;
            _historyService = historyService;
            _quoteService = quoteService;
        }

        public ActionResult GetRequestsUsingAsset()
        {
            var poRequests = _poRequestService.GetPoRequests();
            return View("PoRequests", poRequests);
        }

        public ActionResult GetRequestsUsingAssetPartial()
        {
            var poRequests = _poRequestService.GetPoRequests();
            return View("_PoRequestsPartial", poRequests);
        }

        public ActionResult LoadAssetCbb(string categoryName)
        {
            var assets = _assetService.GetAvailableAssetsByCategoryName(categoryName.Trim());
            return PartialView("_AssetCbbPartial", assets);
        }

        [HttpPost]
        public ActionResult AssignAsset(int poRequestId, int assetId, string assignRemark, string staffAssign)
        {
            var poRequest = _poRequestService.GetEntity(poRequestId);
            var asset = _assetService.GetEntity(assetId);
            var status = _historyService.HandleAssign(poRequest, asset, assignRemark, staffAssign);

            if (status == Enumerations.AddEntityStatus.Success)
            {
                return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }       

        [HttpPost]
        public ActionResult HandleQuote(HttpPostedFileBase image, string productName, string brand, string category, string vendor, decimal price, string warranty, string note, int poRequestId)
        {
            if (warranty.Length == 0)
            {
                warranty = "0";
            }
            var quote = new Quote
            {
                ProductName = productName,

                Brand = brand,
                CategoryName = category,
                Vendor = vendor,
                Price = price,
                Warranty = int.Parse(warranty),
                Note = note
            };
            var poRequest = _poRequestService.GetEntity(poRequestId);

            var status = _quoteService.HandleQuote(image, quote, poRequest);

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
}