using Data.Entities;
using Data.Utilities.Enumeration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Data.Utilities
{
    public class RolePermission : ActionFilterAttribute
    {
        private Enumerations.Roles[] Roles { get; set; }

        public RolePermission(params Enumerations.Roles[] roles)
        {
            Roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var userSession = (Employee)HttpContext.Current.Session[Constant.UserSession];

            if (userSession == null)
            {
                actionContext.HttpContext.Response.Redirect("~/Login/Login");
            }
            else
            {
                string[] roles = new string[Roles.Length];
                for (int i = 0; i < Roles.Length; i++)
                {
                    roles[i] = Roles[i].ToString();
                }

                if (!roles.Contains(userSession.Role.RoleName))
                {
                    actionContext.HttpContext.Response.Redirect("~/Login/Login");
                }
            }
            base.OnActionExecuting(actionContext);
        }
    }
}