using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;

namespace MemberCacheForCsharp.Service.Tests
{
    [TestClass]
    public class BaseTest
    {
        private IContainer container;

        public BaseTest()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Assembly service = Assembly.Load("MemberCacheForCsharp.Service");
            builder.RegisterAssemblyTypes(service).PropertiesAutowired();
            container = builder.Build();
            Type type = this.GetType();
            PropertyInfo[] pro = type.GetProperties();
            if (pro != null)
            {
                foreach(PropertyInfo propertyInfo in pro)
                {
                    Type methodType = propertyInfo.PropertyType;
                    object obj = container.Resolve(methodType);
                    if (obj != null)
                    {
                        propertyInfo.SetValue(this, obj);
                    }
                }
            }
        }
        protected IContainer GetContainer()
        {
            return container;
        }
    }
}
