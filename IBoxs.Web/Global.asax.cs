using IBoxs.Web.IoC;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace IBoxs.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(FilterConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HttpConfiguration config = GlobalConfiguration.Configuration;
            ControllerBuilder.Current.SetControllerFactory(typeof(StructureMapControllerFactory));

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Context.SetSessionStateBehavior(SessionStateBehavior.ReadOnly);

        }

        protected void Application_BeginRequest()
        {
            CultureInfo newCulture = new CultureInfo("pt-BR");
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "/";
            newCulture.DateTimeFormat.LongDatePattern = "dd/MM/yyyy HH:mm:ss";
            newCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
            newCulture.DateTimeFormat.TimeSeparator = ":";
            newCulture.NumberFormat.NumberDecimalSeparator = ",";
            newCulture.NumberFormat.CurrencySymbol = "R$";
            newCulture.NumberFormat.NumberGroupSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }

        protected void Application_Error()
        {

        }
    }
}
