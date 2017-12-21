using MemberCacheForCsharp.Model;
using MemberCacheForCsharp.Redis.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MemberCacheForCsharp.Redis.WebApi.Controllers
{
    public class RedisController : ApiController
    {
        public Hr_DepartmentBiz Hr_DepartmentBiz { get; set; }

        [HttpGet]
        [Route("redis/getDepartmentList")]
        public List<Hr_department> GetDepartmentList()
        {
            List<Hr_department> list = null;
            IList<Hr_department> deparments = Hr_DepartmentBiz.GetList();
            if (deparments != null)
            {
                list = deparments.ToList();
            }
            return list;
        }
    }
}
