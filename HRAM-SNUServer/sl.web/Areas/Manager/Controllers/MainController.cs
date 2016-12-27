using System.Collections.Generic;
using System.Web.Mvc;
using PetaPoco;
using PetaPoco.Orm;
using sl.IService;
using sl.common;
using sl.model;
using sl.web.ui;

namespace sl.web.Areas.Manager.Controllers
{
    /// <summary>
    /// 主页面
    /// </summary>
    public class MainController : BaseManagerController
    {
        //
        // GET: /Manager/Main/
        public ActionResult Main()
        {
            var where = Condition.Builder.Equal("M_IsDeleted", false).Equal("M_IsVisible", true).Create();
            List<T_SysModule> list = DIContainer.Resolve<ISysModuleService>().List(where, "M_Sort asc");
            return View(list);
        }

        public ActionResult Logout()
        {
            Session[Key.MANAGER_INFO] = null;
            Session.Abandon();
            Utils.WriteCookie(Key.MANAGER_NAME, "", -1);
            Utils.WriteCookie(Key.MANAGER_PASS, "", -1);
            return RedirectToAction("Login", "Login", new { area = "Manager" });
        }
	}
}