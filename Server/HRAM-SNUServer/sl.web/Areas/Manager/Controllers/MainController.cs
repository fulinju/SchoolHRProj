using System.Collections.Generic;
using System.Web.Mvc;
using PetaPoco;
using sl.IService;
using sl.common;
using sl.model;
using sl.web.ui;
using sl.service.manager;

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
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_SysModule where IsDeleted = 0 and M_IsVisible = 1 Order By M_Sort asc");
            List<T_SysModule> list = HRAManagerService.database.Fetch<T_SysModule>(sql);
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