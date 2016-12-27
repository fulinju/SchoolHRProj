using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sl.web.Areas.Manager.Controllers
{
    /// <summary>
    /// 发布管理的Controller
    /// </summary>
    public class PublishManagerController : Controller
    {
        //
        // GET: /Manager/PublishManager/
        public ActionResult PublishManagerView()
        {
            return View();
        }
	}
}