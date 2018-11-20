using Data.Entities;
using System.Web;
using System.Web.Mvc;

namespace Data.Utilities
{
    public class PermissionLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var userSession = (Employee)HttpContext.Current.Session[Constant.UserSession];
            if (userSession == null)
            {
                actionContext.HttpContext.Response.Redirect("~/Login/Login");
            }
            //base.OnActionExecuting(actionContext);

        }
    }
}