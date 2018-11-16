using Data.Services;
using Data.Utilities;
using Data.Utilities.Enumeration;
using System.Linq;
using System.Web.Mvc;

namespace AssetManagement.Controllers
{
    [PermissionLogin]
    [RolePermission(Enumerations.Roles.Manager)]
    public class ManagerController : Controller
    {
        private readonly IPoRequestService _poRequestService;
        private readonly IQuoteService _quoteService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IAssetService _assetService;

        public ManagerController(IPoRequestService poRequestService, IQuoteService quoteService, IOrderService orderService, IOrderDetailService orderDetailService, IAssetService assetService)
        {
            _poRequestService = poRequestService;
            _quoteService = quoteService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _assetService = assetService;
        }

        public ActionResult GetPoRequestsFromStaff()
        {
            var poRequests = _poRequestService.GetPoRequestsFromStaff();
            return View("PoRequestsFromStaff", poRequests);
        }

        public ActionResult GetPoRequestsFromStaffPartial()
        {
            var poRequests = _poRequestService.GetPoRequestsFromStaff();
            return PartialView("_PoRequestsFromStaffPartial", poRequests);
        }

        public ActionResult GetQuotesByPoRequestId(int poRequestId)
        {
            ViewBag.PoRequestId = poRequestId;
            var quotes = _quoteService.GetQuotesByPoRequestId(poRequestId);
            return PartialView("_QuoteDetailPartial", quotes);
        }

        public ActionResult SelectQuote(int poRequestId, int quoteId)
        {
            if (_poRequestService.IsExistQuoteId(poRequestId, quoteId))
            {
                var status = _poRequestService.HandleSelectQuote(poRequestId, quoteId);

                if (status == Enumerations.UpdateEntityStatus.Success)
                {
                    return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { status = "Failed" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            return View("Orders", orders);
        }

        public ActionResult GetOrdersPartial()
        {
            var orders = _orderService.GetOrders();
            return PartialView("_OrdersPartial", orders);
        }

        public ActionResult GetOrderDetailByOrderIdPartial(int orderId)
        {
            ViewBag.OrderId = orderId;
            var orderDetails = _orderDetailService.GetOrderDetailByOrderId(orderId);
            return PartialView("_OrderDetailPartial", orderDetails);
        }

        public ActionResult GetChartData()
        {
            var data = _assetService.GetChartData().Select(_ => new { name = _.VendorName, y = _.PercentVendor }).ToArray();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get3DChartData()
        {
            var data = _assetService.Get3DChartData().Select(_ => new { name = _.Brand, y = _.PercentBrand }).ToArray();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}