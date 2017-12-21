using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.Common.Redis
{
   public abstract class RedisBase : IDisposable
    {
        public RedisManager RedisManager { get; set; }

        public static IRedisClient client { get; set; }
        private bool _disposed = false;
        static RedisBase()
        {
            client = RedisManager.GetClient();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    client.Dispose();
                    client = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 保存数据DB文件到服务器
        /// </summary>
        public void Save()
        {
            client.Save();
        }
        /// <summary>
        /// 异步保存数据DB文件到服务器
        /// </summary>
        public void SaveAsync()
        {
            client.SaveAsync();
        }
    }
}
