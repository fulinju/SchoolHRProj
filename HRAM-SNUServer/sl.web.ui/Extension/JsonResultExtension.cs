using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Converters;

namespace sl.web.ui.Extension
{
    public class JsonResultExtension : JsonResult
    {
        /// <summary>
        /// 格式化字符串
        /// </summary>
        public string FormateStr
        {
            get;
            set;
        }

        /// <summary>
        /// 重写执行视图
        /// </summary>
        /// <param name="context">上下文</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                IsoDateTimeConverter datetime = new IsoDateTimeConverter { DateTimeFormat = FormateStr };
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Data, datetime);
                response.Write(jsonString);
            }
        }

    }
}
