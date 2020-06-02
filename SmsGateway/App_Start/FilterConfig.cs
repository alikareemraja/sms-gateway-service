using System.Web;
using System.Web.Mvc;
using SmsGateway.HttpHeader;

namespace SmsGateway
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
