using System.Web;

namespace Data.Utilities
{
    public static class SessionHelper
    {
        public static object GetSessionValue(string sessionName)
        {
            return HttpContext.Current.Session[sessionName];
        }
    }
}
