using MemberCacheForCsharp.Service.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberCacheForCsharp.Model;

namespace MemberCacheForCsharp.Redis.Biz
{
    public class Hr_DepartmentBiz
    {
        Hr_DepartmentService Hr_DepartmentService { get; set; }

        public List<Hr_department> GetList()
        {
            List<Hr_department> list = new List<Hr_department>();
            return list;
        }
    }
}
