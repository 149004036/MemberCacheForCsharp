using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MemberCacheForCsharp.Service.Config
{
    public class ServiceConfig
    {
        public string GetHrConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["hr"].ConnectionString;
        }
    }
}
