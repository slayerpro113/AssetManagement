using Data.Services;
using System.Web.Mvc;
using Data.Utilities.Enumeration;

namespace AssetManagement.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IPoRequestService _poRequestService;

        public ManagerController(IPoRequestService poRequestService)
        {
            _poRequestService = poRequestService;
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
            var quotes = _poRequestService.GetQuotesByPoRequestId(poRequestId);
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
    }
}