using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.Common.Config
{
    public class CacheConfig
    {
        /// <summary>
        /// redis写入连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRedisWriteConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["RedisConnection"].ConnectionString;
        }
        /// <summary>
        /// redis读取连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRedisReadConnectionString()
        {
            var readString = ConfigurationManager.ConnectionStrings["RedisReadConnection"];
            if (readString == null)
            {
                readString = ConfigurationManager.ConnectionStrings["RedisConnection"];
            }
            return readString.ConnectionString;
        }

        /// <summary>
        /// 最大读链接数
        /// </summary>
        public static int MaxReadPoolSize
        {
            
            get
            {
                string readSiz = ConfigurationManager.AppSettings["MaxReadPoolSize"];
                return string.IsNullOrEmpty(readSiz) ? 5 : Convert.ToInt32(readSiz);
            }
        }
        public static int MaxWritePoolSize
        {
            get
            {
                string writeSiz = ConfigurationManager.AppSettings["MaxWritePoolSize"];
                return string.IsNullOrEmpty(writeSiz) ? 5 : Convert.ToInt32(writeSiz);
            }
        }
    }
}
