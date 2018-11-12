using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Utilities.Enumeration;
using System.Web;
using System.Web.Mvc;

namespace Data.Utilities
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
        private Enumerations.Roles[] Roles { get; set; }

        public RolePermission(params Enumerations.Roles[] roles)
        {
            Roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            base.OnActionExecuting(actionContext);

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

//                var role0 = rolesString[0];
//                var role1 = rolesString[1];
//                bool check = userSession.Role.RoleName != role0 && userSession.Role.RoleName != role1;

                if (!roles.Contains(userSession.Role.RoleName))
                {
                    actionContext.HttpContext.Response.Redirect("~/Login/Login");
                }
            }
        }
    }
}