using Data.Services;
using System.Web.Mvc;

namespace AssetManagement.Controllers
{
    public class StaffController : Controller
    {
        private readonly IPoRequestService _poRequestService;

        public StaffController(IPoRequestService poRequestService)
        {
            _poRequestService = poRequestService;
        }

        public ActionResult RequestList()
        {
            var poRequests = _poRequestService.GetAll();
            return View(poRequests);
        }
    }
}