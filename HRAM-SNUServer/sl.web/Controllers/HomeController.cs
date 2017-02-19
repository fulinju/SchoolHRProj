using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sl.model;
using PetaPoco;
using sl.web.ui;
using sl.common;
using sl.service.manager;

namespace sl.web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginByLoginName(string name, string pwd, bool remberpassword = true)
        {
            string errormessage = "错误信息初始化";
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd))
            {
                T_User user = new T_User();
                string passwordMd5 = Security.MD5Encrypt(pwd); //优化为一般MD5

                user = HRAManagerService.CheckLogin(name, passwordMd5);

                if (user != null)
                {
                    //写缓存
                    Session[Key.MANAGER_INFO] = user;
                    if (remberpassword)
                    {
                        Utils.WriteCookie(Key.MANAGER_NAME, name);
                        Utils.WriteCookie(Key.MANAGER_PASS, pwd);
                    }

                    return Json(new JsonTip(JsonTipHelper.SuccessJsonTip, Url.Action("Index", "Home")), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    errormessage = "该用户不存在";

                }
            }
            else
            {
                errormessage = "用户名或者密码不能为空";
            }
            return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, errormessage), JsonRequestBehavior.AllowGet);
        }
	}
}