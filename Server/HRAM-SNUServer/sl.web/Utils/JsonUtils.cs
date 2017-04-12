using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;

namespace common.utils
{
    public class JsonUtils
    {
        /// <summary>
        /// 转化为Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HttpResponseMessage toJson(HttpStatusCode stateCode, Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                str = serializer.Serialize(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage {StatusCode = stateCode, Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        } 
    }
}