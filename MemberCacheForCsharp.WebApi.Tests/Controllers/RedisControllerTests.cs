using MemberCacheForCsharp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.WebApi.Tests.Controllers
{
    [TestClass]
    public class RedisControllerTests : BaseTest
    {
        [TestMethod]
        public void GetDailyWorkProjectList()
        {
            string url = "redis/getDepartmentList";
            //var o = new { };
            var response = GetJson<List<Hr_department>>(url, null);
            Assert.IsNotNull(response);
        }
    }
}
