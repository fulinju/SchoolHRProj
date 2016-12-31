using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using sl.common;
using sl.model;
using sl.web.ui;
using sl.IService;
using sl.service;
using PetaPoco;
using PetaPoco.Orm;

namespace sl.web.Areas.Manager.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Manager/Login/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginCheck(string loginName, string loginPwd, string checkCode, bool remberpassword = true)
        {
            string errormessage;

            //验证码判断
            if (!string.IsNullOrEmpty(checkCode) && !string.IsNullOrEmpty(CreateCheckCodeImage.checkCode))
            {

                if (!checkCode.ToUpper().Equals(CreateCheckCodeImage.checkCode.ToUpper())) //不区分大小写
                {
                    errormessage = "验证码错误!";
                    return Json(new JsonTip("0", errormessage));
                }
            }

            //账户判断
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(loginPwd))
            {
                errormessage = "用户名或者密码不能为空";
                return Json(new JsonTip("0", errormessage));
            }

            string passwordMd5 = Security.MD5Encrypt(loginPwd);

            //Condition where = Condition.Builder.Equal("A_LoginName", loginName).Equal("A_Password", loginPwd);

            Sql sql = Sql.Builder;
            sql.Append("Select * from T_User where U_LoginName=@0 and U_Password = @1", loginName, passwordMd5);
            T_User loginer = UtilsDB.DB.FirstOrDefault<T_User>(sql);

            if (loginer == null)
            {
                errormessage = "用户名或者密码错误!";
                return Json(new JsonTip("0", errormessage));
            }

            if (!loginer.U_LoginTypeID.Equals(ConstantData.AdminTypeCode))
            {
                errormessage = "该用户不是管理员";
            }
            else
            {
                UtilsDB.DB.Update(loginer);
                Session[Key.MANAGER_INFO] = loginer;
                if (remberpassword)
                {
                    if (CachedConfigContext.Current.WebSiteConfig.WebSiteKey != null) //加密Key不为空
                    {
                        Utils.WriteCookie(Key.MANAGER_NAME, Security.DesEncrypt(CachedConfigContext.Current.WebSiteConfig.WebSiteKey, loginer.U_LoginName));//DES加密
                    }
                    else
                    {
                        Utils.WriteCookie(Key.MANAGER_NAME, loginer.U_LoginName);
                    }
                    Utils.WriteCookie(Key.MANAGER_PASS, passwordMd5);
                }
                return Json(new JsonTip("1", Url.Action("Main", "Main", new { area = "Manager" })));
            }

            return Json(new JsonTip("0", errormessage));
        }


        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            //string code = StringPlus.GenerateCode(4, 1);  
            CreateCheckCodeImage.checkCode = CreateCheckCodeImage.GenerateCheckCode();
            MemoryStream ms = CreateCheckCodeImage.Production(CreateCheckCodeImage.checkCode);
            byte[] buffurPic = ms.ToArray();

            return File(buffurPic, "image/jpeg");
        }
    }
}