using Autofac;
using MemberCacheForCsharp.Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.WebApi.Tests
{
    [TestClass]
    public class BaseTest
    {
        private IContainer _container;
        private static string address = "http://localhost:9000/";
        private static LoginMockContext context = new LoginMockContext();

        /// <summary>
        /// testclass初始化
        /// </summary>
        /// <param name="test"></param>
        [AssemblyInitialize]
        public static void TestClassInit(TestContext test)
        {
            context.Address = address;
            context.Init();
        }
        /// <summary>
        /// 
        /// </summary>
        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            context.Distory();
        }
        /// <summary>
        /// get 请求获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public T GetJson<T>(string url, object o)
        {
            return WebClientUtils.GetParamJson<T>(address + url, o);
        }

        /// <summary>
        /// post 请求获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public T PostJson<T>(string url, object o)
        {
            return WebClientUtils.PostBodyJson<T>(address + url, o);
        }
        /// <summary>
        /// post url 传参
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public T PostUrlJson<T>(string url, object o)
        {
            return WebClientUtils.PostUrlJson<T>(address + url, o);
        }

        /// <summary>
        /// post 上传文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="o"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public T PostFilesJson<T>(string url, object o, List<string> files)
        {
            return WebClientUtils.PostFilesJson<T>(address + url, o, files);
        }

        [TestInitialize]
        public void TestInit()
        {
            _container = ContainerConfig.RegisterBuilder();
            Type ty = this.GetType();
            PropertyInfo[] pro = ty.GetProperties();
            if (pro != null)
            {
                foreach (var propertyInfo in pro)
                {
                    Type methodType = propertyInfo.PropertyType;
                    object obj = _container.Resolve(methodType);
                    if (obj != null)
                        propertyInfo.SetValue(this, obj);
                }
            }
        }
    }
}
