using Data.Entities;
using System.Web;
using System.Web.Mvc;

namespace AssetManagement.Helper
{
    public class PermissionLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            var session = (Personnel)HttpContext.Current.Session[Constants.USER_SESSION];
            if (session == null)
            {
                actionContext.HttpContext.Response.Redirect("~/Login/Login");
            }
        }
    }
}