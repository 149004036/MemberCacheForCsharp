using Autofac;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace MemberCacheForCsharp.WebApi.Tests
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).AsSelf().PropertiesAutowired();
            builder.RegisterWebApiModelBinderProvider();
            builder.RegisterWebApiFilterProvider(config);

            Assembly service = Assembly.Load("MemberCacheForCsharp.Service");
            Assembly biz = Assembly.Load("MemberCacheForCsharp.Biz");
            Assembly common = Assembly.Load("MemberCacheForCsharp.Common");

            builder.RegisterAssemblyTypes(service).PropertiesAutowired();
            builder.RegisterAssemblyTypes(biz).PropertiesAutowired();
            builder.RegisterAssemblyTypes(common).PropertiesAutowired();

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            appBuilder.UseWebApi(config);
        }
    }
}
