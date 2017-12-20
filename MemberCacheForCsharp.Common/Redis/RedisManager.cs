using MemberCacheForCsharp.Common.Config;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.Common.Redis
{
    public class RedisManager
    {
        private static PooledRedisClientManager clientManager;

        private static void CreateManager()
        {
            RedisClientManagerConfig redisClientManagerConfig = new RedisClientManagerConfig
            {
                MaxWritePoolSize = CacheConfig.MaxWritePoolSize,
                MaxReadPoolSize = CacheConfig.MaxReadPoolSize
            };
             
            string[] WriteServerConStr = SplitString(CacheConfig.GetRedisWriteConnectionString(), ",");
            string[] ReadServerConStr = SplitString(CacheConfig.GetRedisReadConnectionString(), ",");
            clientManager = new PooledRedisClientManager(WriteServerConStr, ReadServerConStr, redisClientManagerConfig);

        }
        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }
        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static IRedisClient GetClient()
        {
            if (clientManager == null)
                CreateManager();
            return clientManager.GetClient();
        }
    }
}
