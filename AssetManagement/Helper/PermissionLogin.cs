using Data.Entities;
using Data.Utilities;
using Data.Utilities.Enumeration;
using System.Web;
using System.Web.Mvc;

namespace AssetManagement.Helper
{
    public class PermissionLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            var userSession = (Employee)HttpContext.Current.Session[Constant.UserSession];
            if (userSession == null)
            {
                actionContext.HttpContext.Response.Redirect("~/Login/Login");
            }
        }
    }

    public class RolePermission : ActionFilterAttribute
    {
        private Enumerations.Roles Roles { get; set; }

        public RolePermission(Enumerations.Roles role)
        {
            Roles = role;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            var userSession = (Employee)HttpContext.Current.Session[Constant.UserSession];
            var roleSession = (Role)HttpContext.Current.Session[Constant.RoleSession];

            if (userSession == null)
            {
                actionContext.HttpContext.Response.Redirect("~/Login/Login");
            }
            else
            {
                if (!roleSession.RoleName.Equals(Roles.ToString()))
                {
                    actionContext.HttpContext.Response.Redirect("~/Login/Login");
                }
            }
        }
    }
}