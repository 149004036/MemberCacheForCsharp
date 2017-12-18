using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.Service.Tests
{
    [TestClass]
    public class SugarDaoTest : BaseTest
    {
       public SugarDao sugarDao { get; set; }
        /// <summary>
        /// 生成entity
        /// </summary>
        [TestMethod]
        public void testAddTable()
        {
            try
            {
                SqlSugarClient dao = sugarDao.GetDao();
                dao.ClassGenerating.ForeachTables(dao, tableName =>
                {
                    var className = tableName.Replace(".", "_");
                    className = className.Substring(0, 1).ToUpper() + className.Substring(1);
                    dao.AddMappingTable(new KeyValue() { Key = className, Value = tableName });
                });
                dao.ClassGenerating.CreateClassFiles(dao, (@"D:\生成表MemberCacheForCsharpModel\DB"), "MemberCacheForCsharp.Model");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
