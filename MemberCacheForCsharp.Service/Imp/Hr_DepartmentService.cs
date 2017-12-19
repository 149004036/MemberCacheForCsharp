using MemberCacheForCsharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlSugar;

namespace MemberCacheForCsharp.Service.Imp
{
    public class Hr_DepartmentService : IHr_DepartmentService
    {
        public SugarDao Dao { get; set; }
        public bool Add(Hr_department model)
        {
            bool result = false;
            using (var db = Dao.GetDao())
            {
                var rows = db.Insert(model);
                if (rows != null)
                {
                    int count = Convert.ToInt32(rows);
                    if (count > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            using (var db = Dao.GetDao())
            {
                result = db.Update<Hr_department>(new { IsDelete = 1, ModifyTime = DateTime.Now }, it => it.Id == id);
            }
            return result;
        }
        public bool Update(Hr_department model)
        {
            bool result = false;
            using (var db = Dao.GetDao())
            {
                result = db.Update(model);
            }
            return result;
        }
        public List<Hr_department> GetList()
        {
            List<Hr_department> list = null;
            using (var db = Dao.GetDao())
            {
                list = db.Queryable<Hr_department>().Where(o => o.IsDelete == 0).ToList();
            }
            return list;
        }
        public Hr_department GetModel(string depNo)
        {
            Hr_department model = null;
            using (var db = Dao.GetDao())
            {
                model = db.Queryable<Hr_department>().Where(o => o.DepNo == depNo).FirstOrDefault();
            }
            return model;
        }
    }
}
