using DevExpress.Web.Mvc;
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
        private readonly IEmployeeService _employeeService;
        private readonly IOrderService _orderService;


        public StaffController(IPoRequestService poRequestService, IAssetService assetService,
            IHistoryService historyService, IQuoteService quoteService, IEmployeeService employeeService, IOrderService orderService)
        {
            _poRequestService = poRequestService;
            _assetService = assetService;
            _historyService = historyService;
            _quoteService = quoteService;
            _employeeService = employeeService;
            _orderService = orderService;
        }

        public ActionResult GetPoRequestsFromUsers()
        {
            var poRequests = _poRequestService.GetPoRequestsFromUsers();
            return View("PoRequests", poRequests);
        }

        public ActionResult GetPoRequestsFromUsersPartial()
        {
            var poRequests = _poRequestService.GetPoRequestsFromUsers();
            return PartialView("_PoRequestsPartial", poRequests);
        }

        public ActionResult LoadAssetCbb(string categoryName)
        {
            var assets = _assetService.GetAvailableAssetsByCategoryName(categoryName.Trim());
            return PartialView("_AssetCbbPartial", assets);
        }

        [HttpPost]
        public ActionResult AssignAsset(int poRequestId, int employeeId, int assetId, string assignRemark,
            string staffAssign)
        {
            var employee = _employeeService.GetEntity(employeeId);
            var asset = _assetService.GetEntity(assetId);
            _poRequestService.SetStatusAndFinishDate(poRequestId);
            var status = _historyService.HandleAssign(employee, asset, assignRemark, staffAssign);

            if (status == Enumerations.AddEntityStatus.Success)
            {
                return Json(new {status = "Success"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {status = "Failed"}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult HandleQuote(HttpPostedFileBase image, string productName, string brand, string category,
            string vendor, decimal price, string warranty, string note, int poRequestId)
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
                return Json(new {status = "Success"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {status = "Failed"}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SubmitRequest(int poRequestId, string staffSubmit)
        {
            var status = _poRequestService.HandleSubmitRequest(poRequestId, staffSubmit);

            if (status == Enumerations.UpdateEntityStatus.Success)
            {
                return Json(new {status = "Success"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {status = "Failed"}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSubmittedPoRequests()
        {
            var poRequests = _poRequestService.GetSubmittedPoRequests();
            return View("SubmittedPoRequests", poRequests);
        }

        public ActionResult GetSubmittedPoRequestsPartial()
        {
            var poRequests = _poRequestService.GetSubmittedPoRequests();
            return PartialView("_SubmittedPoRequestsPartial", poRequests);
        }

        public ActionResult CreateOrder(string poRequestIdArray)
        {
            var status = _orderService.HandleCreateOrder(poRequestIdArray);

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