using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.Common.Redis
{
   public abstract class RedisBase //: IDisposable
    {
        public static IRedisClient client { get; private set; }
        private bool _disposed = false;
        static RedisBase()
        {
            //client = 
        }
    }
}
