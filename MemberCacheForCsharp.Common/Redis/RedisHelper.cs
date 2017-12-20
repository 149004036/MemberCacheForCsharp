using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCacheForCsharp.Common.Redis
{
    public class RedisHelper :RedisBase
    {
        #region String
        /// <summary>
        /// 设置key的value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Set(string key,string value)
        {
            return client.Set<string>(key, value);
        }
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool Set(string key, string value, DateTime dt)
        {
            return client.Set<string>(key, value, dt);
        }
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        public static bool Set(string key, string value, TimeSpan sp)
        {
            return client.Set<string>(key, value, sp);
        }
        /// <summary>
        /// 设置多个key/value
        /// </summary>
        public static void Set(Dictionary<string, string> dic)
        {
            client.SetAll(dic);
        }
        /// <summary>
        /// 在原有key的value值之后追加value
        /// </summary>
        public static long Append(string key, string value)
        {
            return client.AppendToValue(key, value);
        }/// <summary>
         /// 获取key的value值
         /// </summary>
        public static string Get(string key)
        {
            return client.GetValue(key);
        }
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        public static List<string> Get(List<string> keys)
        {
            return client.GetValues(keys);
        }
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        public static List<T> Get<T>(List<string> keys)
        {
            return client.GetValues<T>(keys);
        }
        #endregion

        #region List
        /// <summary>
        /// 添加key/value
        /// </summary>     
        public static void AddList(string key, string value)
        {
            client.AddItemToList(key, value);
        }
        /// <summary>
        /// 添加key/value ,并设置过期时间
        /// </summary>  
        public static void AddList(string key, string value, DateTime dt)
        {
            client.AddItemToList(key, value);
            client.ExpireEntryAt(key, dt);
        }
        /// <summary>
        /// 添加key/value。并添加过期时间
        /// </summary>  
        public static void AddList(string key, string value, TimeSpan sp)
        {
            client.AddItemToList(key, value);
            client.ExpireEntryIn(key, sp);
        }
        /// <summary>
        /// 为key添加多个值
        /// </summary>  
        public static void AddList(string key, List<string> values)
        {
            client.AddRangeToList(key, values);
        }
        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        public static void AddList(string key, List<string> values, DateTime dt)
        {
            client.AddRangeToList(key, values);
            client.ExpireEntryAt(key, dt);
        }
        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        public static void AddList(string key, List<string> values, TimeSpan sp)
        {
            client.AddRangeToList(key, values);
            client.ExpireEntryIn(key, sp);
        }

        /// <summary>
        /// 获取key包含的所有数据集合
        /// </summary>  
        public static List<string> GetList(string key)
        {
            return client.GetAllItemsFromList(key);
        }

        /// <summary>
        /// 从list的头部移除一个数据，返回移除的值
        /// </summary>  
        public string RemoveStartFromList(string key)
        {
            return client.RemoveStartFromList(key);
        }
        #endregion
        /// <summary>
        /// 存储所有list实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public void StoreAll<T>(List<T> list)
        {
            client.StoreAll(list);
        }
        /// <summary>
        /// 存储单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Store<T>(T t)
        {
            client.Store(t);
        }
    }
}
