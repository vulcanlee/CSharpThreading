using System.Web;
using System.Web.Mvc;

namespace MT041ASP.NET_MVC與同步內容的體驗
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
