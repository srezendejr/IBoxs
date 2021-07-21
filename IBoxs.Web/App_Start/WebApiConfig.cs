using IBoxs.Web.IoC;
using Microsoft.AspNet.OData.Extensions;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace IBoxs.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            GlobalConfiguration.Configuration.EnableDependencyInjection();
            // Web API routes
            config.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator());

            config.Filters.Add(new Filtros.ApplicationExceptionFilter());
            config.Select().Expand().Filter().OrderBy().MaxTop(null).Count();

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}/{id2}",
                new { id = RouteParameter.Optional, id2 = RouteParameter.Optional }, null);

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            ((Newtonsoft.Json.Serialization.DefaultContractResolver)config.Formatters.JsonFormatter.SerializerSettings
                .ContractResolver).IgnoreSerializableAttribute = true;
        }
    }
}
