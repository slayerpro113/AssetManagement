using Data.Services;
using System.Web.Mvc;
using Data.Utilities;
using Data.Utilities.Enumeration;

namespace AssetManagement.Controllers
{
    [PermissionLogin]

    public class ManagerController : Controller
    {
        private readonly IPoRequestService _poRequestService;
        private readonly IQuoteService _quoteService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;


        public ManagerController(IPoRequestService poRequestService, IQuoteService quoteService, IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _poRequestService = poRequestService;
            _quoteService = quoteService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
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
            var orders = _orderService.GetAll();
            return View("Orders", orders);
        }

        public ActionResult GetOrdersPartial()
        {
            var orders = _orderService.GetAll();
            return PartialView("_OrdersPartial", orders);
        }

        public ActionResult GetOrderDetailByOrderIdPartial(int orderId)
        {
            ViewBag.OrderId = orderId;
            var orderDetails = _orderDetailService.GetOrderDetailByOrderId(orderId);
            return PartialView("_OrderDetailPartial", orderDetails);
        }
    }
}