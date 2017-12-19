using MemberCacheForCsharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.Service
{
    public interface IHr_DepartmentService
    {
        bool Add(Hr_department model);
        bool Delete(int id);
        bool Update(Hr_department model);
        List<Hr_department> GetList();
        Hr_department GetModel(string depNo);
    }
}