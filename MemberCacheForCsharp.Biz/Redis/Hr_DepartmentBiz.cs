using MemberCacheForCsharp.Service.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberCacheForCsharp.Model;
using MemberCacheForCsharp.Common.Redis;

namespace MemberCacheForCsharp.Redis.Biz
{
    public class Hr_DepartmentBiz
    {
        public Hr_DepartmentService Hr_DepartmentService { get; set; }
        public RedisHelper RedisHelper { get; set; }
        /// <summary>
        /// 获取department列表
        /// </summary>
        /// <returns></returns>
        public IList<Hr_department> GetList()
        {
            IList<Hr_department> list = null;
            //list = RedisHelper.GetAll<Hr_department>();
            if (list == null || list.Count == 0)
            {
                list = Hr_DepartmentService.GetList();
                if (list != null)
                {
                    RedisHelper.StoreList(list);
                }
            }
            return list;
        }
    }
}
