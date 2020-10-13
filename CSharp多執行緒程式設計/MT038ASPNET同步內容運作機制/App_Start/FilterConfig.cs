using System.Web;
using System.Web.Mvc;

namespace MT038ASPNET同步內容運作機制
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
