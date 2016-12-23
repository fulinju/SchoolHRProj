using System;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using sl.web.ui.Extension;


namespace sl.web.ui
{
    public class BaseController : Controller
    {

        protected JsonTip Success()
        {
            return new JsonTip("1", "保存成功!");
        }

        protected JsonTip Fail()
        {
            return new JsonTip("0", "保存失败!");
        }


        protected ActionResult SuccessMessage(string message = "")
        {
            return Json(new JsonTip("1", message));
        }
        protected ActionResult SuccessMessage(bool success = false, string message = "")
        {
            return Json(success ? new JsonTip("1", message + "成功!") : new JsonTip("0", message + "失败!"));
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            try
            {
                log.Error(filterContext.Exception.Message);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            base.OnException(filterContext);
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        #region 通用Json
        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="contentEncoding">内容编码</param>
        /// <param name="behavior">行为</param>
        /// <returns>JsonReuslt</returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResultExtension()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                FormateStr = "yyyy-MM-dd HH:mm:ss"
            };
        }

        protected ContentResult JsonP(string callback, object data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return Content(string.Format("{0}({1})", callback, json));
        }
        protected ActionResult SaveMessage(object result)
        {
            return Json(result != null ? new JsonTip("1", "保存成功") : new JsonTip("0", "保存失败!"));
        }
        protected ActionResult SaveMessage(bool success)
        {
            return Json(success ? new JsonTip("1", "保存成功") : new JsonTip("0", "保存失败!"));
        }
        protected ActionResult DelMessage(bool flag)
        {
            return Json(flag ? new JsonTip("1", "删除成功") : new JsonTip("0", "删除失败!"));
        }

        protected ActionResult DelMessage(int flag)
        {
            return Json(flag == 1 ? new JsonTip("1", "删除成功") : new JsonTip("0", "删除失败!"));
        }

        protected ActionResult ErrorMessage(string message = "")
        {
            return Json(new JsonTip("0", message));
        }
        #endregion


        protected ActionResult ErrorPage(string message)
        {
            ViewData["message"] = message;
            return View("Error");
        }

    }
}
