using System.Web.Caching;
using Core.Config;
using sl.common;
using sl.model;

namespace sl.service
{
    public class CachedConfigContext : ConfigContext
    {
        public static CachedConfigContext Current = new CachedConfigContext();

        public override T Get<T>(string index = null)
        {
            var fileName = GetConfigFileName<T>(index);
            var key = "ConfigFile_" + fileName;
            var content = Caching.Get(key);
            if (content != null)
                return (T)content;

            var value = base.Get<T>(index);
            Caching.Set(key, value, new CacheDependency(ConfigService.GetFilePath(fileName)));
            return value;
        }


        public WebSiteConfig WebSiteConfig
        {
            get { return Get<WebSiteConfig>(); }
        }
    }
}
