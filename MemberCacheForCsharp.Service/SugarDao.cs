using log4net;
using MemberCacheForCsharp.Service.Config;
using MySqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.Service
{
    public class SugarDao
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SugarDao));
        public ServiceConfig Config { get; set; }

        public SqlSugarClient GetDao()
        {
            var db = new SqlSugarClient(Config.GetHrConnectionString());
            db.IsEnableLogEvent = true;//Enable log events
            db.LogEventStarting = (sql, par) => {
                logger.Debug(sql + ",param: " + par);
            };
            return db;
        }

        public T execResult<T>(Func<SqlSugarClient, T> fun)
        {
            T model = default(T);
            using (SqlSugarClient db = GetDao())
            {
                if (fun != null)
                    model = fun(db);
            }
            return model;
        }

        public void exec(Action<SqlSugarClient> action)
        {
            using (SqlSugarClient db = GetDao())
            {
                if (action != null)
                    action(db);
            }
        }

        public bool execTran(Action<SqlSugarClient> action)
        {
            bool bt = true;
            using (SqlSugarClient db = GetDao())
            {
                //db.is = true;
                db.CommandTimeOut = 30000;
                try
                {
                    db.BeginTran();
                    //db.BeginTran(IsolationLevel.ReadCommitted);+3
                    if (action != null)
                        action(db);
                    db.CommitTran();
                }
                catch (Exception e)
                {
                    db.RollbackTran();
                    bt = false;
                    logger.Error("execTran -- error", e);
                }
            }
            return bt;

        }
    }
}
