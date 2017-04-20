using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using sl.IService;
using sl.model;
using sl.common;
using sl.web.ui;
using sl.validate;
using Newtonsoft.Json;
using PetaPoco;
using sl.service.manager;

namespace sl.web.Areas.Manager.Controllers
{
    /// <summary>
    /// 用户管理的Controller
    /// </summary>
    public class UserController : BaseManagerController
    {
        //
        // GET: /Manager/User/

        public ActionResult UsersView()
        {
            return View();
        }

        #region 查询
        public ActionResult GetUsersList(string uLoginName)
        {
            Sql sql = HRAManagerService.GetUserSql(uLoginName);

            return CommonPageList<dynamic>(sql,HRAManagerService.database);
        }
        #endregion

        #region 删除用户
        public ActionResult UsersDel(string model)
        {
            List<T_User> usersList = JsonConvert.DeserializeObject<List<T_User>>(model);
            int flag = 0;
            foreach (var entity in usersList)
            {
                entity.isDeleted = true;
                flag = HRAManagerService.database.Update(entity); //假删除
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑用户
        public ActionResult UsersEdit(T_User m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    string passwordMd5 = Security.MD5Encrypt(m.uPassword);
                    m.uPassword = passwordMd5;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.isDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_User load = HRAManagerService.database.SingleOrDefault<T_User>(obj);

                String oldPwd = load.uPassword;
                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        if (!oldPwd.Equals(m.uPassword)) //密码有改动,调用MD5加密
                        {
                            string passwordMd5 = Security.MD5Encrypt(load.uPassword);
                            load.uPassword = passwordMd5;
                        }
                        Model valid = Model.Valid(load);
                        return valid.Result ? SaveMessage(HRAManagerService.database.Update(load)) : ErrorMessage(valid.Message);
                    }
                }
                return View(load);
            }
        }
        #endregion

        #region 重置密码
        public ActionResult ResetUserPWD(string model)
        {
            List<T_User> usersList = JsonConvert.DeserializeObject<List<T_User>>(model);
            int flag = 0;

            foreach (var entity in usersList)
            {
                string initPwd = entity.uLoginName; //初始化密码为登录名
                string passwordMd5 = Security.MD5Encrypt(initPwd);
                entity.uPassword = passwordMd5;
                flag = HRAManagerService.database.Update(entity);
            }
            return Json(flag == 1 ? new JsonTip("1", "重置密码为登录名成功") : new JsonTip("0", "重置失败!"));
        }
        #endregion


        #region 检查用户名是否存在
        [HttpPost]
        public ActionResult CheckUserIsExist(string uLoginName)
        {
            T_User user = HRAManagerService.CheckUserExist(uLoginName);
            if (user != null)
            {
                return Json(new { state = false, message = "此用户名已存在，请更换" });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion
    }
}