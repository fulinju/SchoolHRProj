using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace sl.common
{
    public class CacheHelper
    {
        #region 创建缓存项的文件依赖
        /// <summary>
        /// 创建缓存项的文件依赖
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="fileName">文件绝对路径</param>
        public static void Insert(string key, object obj, string fileName)
        {
            //创建缓存依赖项
            CacheDependency dep = new CacheDependency(fileName);
            //创建缓存
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }
        #endregion

        #region 创建缓存项过期
        /// <summary>
        /// 创建缓存项过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void Insert(string key, object obj, int expires)
        {
            HttpContext.Current.Cache.Insert(key, obj, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
        }
        #endregion

        #region 获取缓存对象 Object
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns>object对象</returns>
        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            return HttpContext.Current.Cache.Get(key);
        }
        #endregion

        #region 获取缓存对象 T
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">T对象</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            object obj = Get(key);
            return obj == null ? default(T) : (T)obj;
        }
        #endregion

        #region 移除一个缓存对象
        /// <summary>
        /// 移除一个缓存对象
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            Object obj = Get(key);
            if (obj != null)
            {
                HttpContext.Current.Cache.Remove(key);
            }
        }
        #endregion

        #region 移除所有缓存对象
        /// <summary>
        /// 移除所有缓存对象
        /// </summary>
        public static void RemoveAll()
        {
            Cache _cache = HttpContext.Current.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            foreach (string key in al)
            {
                _cache.Remove(key);
            }
        }
        #endregion

        #region 获取缓存对象数目
        /// <summary>
        /// 获取缓存对象数目
        /// </summary>
        /// <returns></returns>
        public static int GetCount()
        {
            return HttpContext.Current.Cache.Count;
        }
        #endregion

    }
}
