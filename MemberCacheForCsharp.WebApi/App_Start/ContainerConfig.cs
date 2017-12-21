using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using System.Web.Http;
using System.Reflection;

namespace MemberCacheForCsharp.WebApi
{
    public class ContainerConfig
    {
        public static void Register()
        {
            var container = RegisterBuilder();
            var webApiResolve = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolve;
        }

        public static IContainer RegisterBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).AsSelf().PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterWebApiModelBinderProvider();

            Assembly service = Assembly.Load("MemberCacheForCsharp.Service");
            Assembly biz = Assembly.Load("MemberCacheForCsharp.Biz");
            Assembly common = Assembly.Load("MemberCacheForCsharp.Common");

            builder.RegisterAssemblyTypes(service).PropertiesAutowired();
            builder.RegisterAssemblyTypes(biz).PropertiesAutowired();
            builder.RegisterAssemblyTypes(common).PropertiesAutowired();
            return builder.Build();
        }
    }
}