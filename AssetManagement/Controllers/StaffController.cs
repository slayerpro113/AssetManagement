using System;
using System.Collections.Generic;
using DevExpress.Web.Mvc;
using System.Web;
using Data.Services;
using System.Web.Mvc;
using AssetManagement.Models;
using AutoMapper;
using Data.Entities;
using Data.Utilities;
using Data.Utilities.Enumeration;
using DevExpress.Web.Internal;

namespace AssetManagement.Controllers
{
    [PermissionLogin]
    [RolePermission(Enumerations.Roles.Staff, Enumerations.Roles.Manager)]
    public class StaffController : Controller
    {
        private readonly IPoRequestService _poRequestService;
        private readonly IAssetService _assetService;
        private readonly IHistoryService _historyService;
        private readonly IQuoteService _quoteService;
        private readonly IEmployeeService _employeeService;
        private readonly IOrderService _orderService;

        public StaffController(IPoRequestService poRequestService, IAssetService assetService,
            IHistoryService historyService, IQuoteService quoteService, IEmployeeService employeeService,
            IOrderService orderService)
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
        public ActionResult AssignAsset(int poRequestId, int employeeId, int assetId, string staffAssign)
        {
            var employee = _employeeService.GetEntity(employeeId);
            var asset = _assetService.GetEntity(assetId);

            var status = _historyService.HandleAssign(poRequestId, employee, asset , staffAssign);

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
        public ActionResult HandleQuote(HttpPostedFileBase image, string productName, string brand,
            string vendor, decimal price, string warranty, int poRequestId)
        {
            var poRequest = _poRequestService.GetEntity(poRequestId);

            if (warranty.Length == 0)
            {
                warranty = "0";
            }

            var quote = new Quote
            {
                ProductName = productName.Trim(),
                Brand = brand.Trim(),
                CategoryName = poRequest.CategoryName.Trim(),
                Vendor = vendor.Trim(),
                Price = price,
                Warranty = int.Parse(warranty)
            };

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

        [HttpPost]
        public ActionResult SubmitRequest(int poRequestId, string staffSubmit)
        {
            var status = _poRequestService.HandleSubmitRequest(poRequestId, staffSubmit);

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
        public ActionResult CreateOrder(string poRequestIdString)
        {
            var status = _orderService.HandleCreateOrder(poRequestIdString);

            if (status == Enumerations.AddEntityStatus.Success)
            {
                return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetQuotesByPoRequestId(int poRequestId)
        {
            ViewBag.PoRequestId = poRequestId;
            var quotes = Mapper.Map<List<QuoteViewModel>>(_quoteService.GetQuotesByPoRequestId(poRequestId));
            return PartialView("_EditQuotePartial", quotes);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditQuote(Quote quote, int poRequestId)
        {
            try
            {
                _quoteService.EditQuote(quote);
            }
            catch (Exception e)
            {
                ViewData["EditError"] = "Please, Enter Quote Information again.";
            }

            return GetQuotesByPoRequestId(poRequestId);
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }
    }
}