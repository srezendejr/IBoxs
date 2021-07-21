using IBoxs.Web.Filtros;
using System.Web.Http;
using System.Web.Mvc;

namespace IBoxs.Web
{
    public class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ApplicationExceptionFilter());
            config.Filters.Add(new System.Web.Http.AuthorizeAttribute());
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
