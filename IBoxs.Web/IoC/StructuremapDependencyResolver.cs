using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace IBoxs.Web.IoC
{
    public class StructuremapDependencyResolver: IDependencyResolver
    {
        private readonly IContainer _container;
        public StructuremapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;
            try
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                         ? _container.TryGetInstance(serviceType)
                         : _container.GetInstance(serviceType);
            }
            catch
            {

                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }

    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                return controllerType == null
                           ? base.GetControllerInstance(requestContext, null)
                           : StructureMapContainer.Container.GetInstance(controllerType) as IController;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
    }

    public class ServiceActivator : IHttpControllerActivator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var type = controllerDescriptor.ControllerType;
            if (type.BaseType != typeof(ApiController))
                throw new InvalidOperationException("Invalid API Controller");

            var controller = StructureMapContainer.Container.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }

	public class CustomPageHandlerFactory : PageHandlerFactory
	{
		private static object GetInstance(Type type)
		{
			// for instance using the Common Service Locator:
			return DependencyResolution.Instance.GetInstance(type);
		}

		public override IHttpHandler GetHandler(HttpContext context,
			string requestType, string virtualPath, string path)
		{
			var handler = base.GetHandler(context, requestType,
				virtualPath, path);

			if (handler != null)
			{
				InitializeInstance(handler);
				HookChildControlInitialization(handler);
			}

			return handler;
		}

		private void HookChildControlInitialization(object handler)
		{
			Page page = handler as Page;

			if (page != null)
			{
				// Child controls are not created at this point.
				// They will be when PreInit fires.
				page.PreInit += (s, e) => InitializeChildControls(page);
			}
		}

		private static void InitializeChildControls(Control contrl)
		{
			var childControls = GetChildControls(contrl);

			foreach (var childControl in childControls)
			{
				InitializeInstance(childControl);
				InitializeChildControls(childControl);
			}
		}

		private static IEnumerable<Control> GetChildControls(Control ctrl)
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

			return (
				from field in ctrl.GetType().GetFields(flags)
				let type = field.FieldType
				where typeof(UserControl).IsAssignableFrom(type)
				let userControl = field.GetValue(ctrl) as Control
				where userControl != null
				select userControl).ToArray();
		}

		private static void InitializeInstance(object instance)
		{
			Type pageType = instance.GetType().BaseType;

			var ctor = GetInjectableConstructor(pageType);

			if (ctor != null)
			{
				try
				{
					var args = GetMethodArguments(ctor);

					ctor.Invoke(instance, args);
				}
				catch (Exception ex)
				{
					var msg = string.Format("The type {0} " +
						"could not be initialized. {1}", pageType,
						ex.Message);

					throw new InvalidOperationException(msg, ex);
				}
			}
		}

		private static ConstructorInfo GetInjectableConstructor(
			Type type)
		{
			var overloadedPublicConstructors = (
				from ctor in type.GetConstructors()
				where ctor.GetParameters().Length > 0
				select ctor).ToArray();

			if (overloadedPublicConstructors.Length == 0)
			{
				return null;
			}

			if (overloadedPublicConstructors.Length == 1)
			{
				return overloadedPublicConstructors[0];
			}

			throw new InvalidOperationException(string.Format("The type {0} has multiple public overloaded constructors and can't be initialized.", type));
		}

		private static object[] GetMethodArguments(MethodBase method)
		{
			return (
				from parameter in method.GetParameters()
				let parameterType = parameter.ParameterType
				select GetInstance(parameterType)).ToArray();
		}
	}

}