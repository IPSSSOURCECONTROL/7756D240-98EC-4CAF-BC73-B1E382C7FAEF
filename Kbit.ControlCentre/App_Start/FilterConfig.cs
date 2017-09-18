using System.Web.Mvc;
using Kbit.ControlCentre.Cors;

namespace Kbit.ControlCentre
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new JsonNetFilterAttribute());
        }
    }
}
