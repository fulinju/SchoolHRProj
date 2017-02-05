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
        public ActionResult GetUsersList(string u_loginname)
        {
            Sql sql = Sql.Builder;

            u_loginname = "%" + u_loginname + "%";

            sql.Append("Select T_UserType.u_logintypevalue,");
            sql.Append("T_User.pk_id,");
            sql.Append("T_User.u_loginname,");
            sql.Append("T_User.u_logintypeid,"); //不能少，重置的时候要用
            sql.Append("T_User.u_password,");
            sql.Append("T_User.u_username,");
            sql.Append("T_User.u_phone,");
            sql.Append("T_User.u_maibox");
            sql.Append(" from T_User,T_UserType where U_LoginName Like @0 and T_User.IsDeleted = 0", u_loginname);
            sql.Append(" and T_UserType.U_LoginTypeID = T_User.U_LoginTypeID");
            List<dynamic> list = UtilsDB.DB.Fetch<dynamic>(sql);

            string json = JsonConvert.SerializeObject(list);

            return CommonPageList<dynamic>(sql, UtilsDB.DB);
        }
        #endregion

        #region 删除用户
        public ActionResult UsersDel(string model)
        {
            List<T_User> usersList = JsonConvert.DeserializeObject<List<T_User>>(model);
            int flag = 0;
            foreach (var entity in usersList)
            {
                entity.IsDeleted = true;
                flag = UtilsDB.DB.Update(entity); //假删除
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
                    string passwordMd5 = Security.MD5Encrypt(m.U_Password);
                    m.U_Password = passwordMd5;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.IsDeleted = false;
                        object result = UtilsDB.DB.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_User load = UtilsDB.DB.SingleOrDefault<T_User>(obj);

                String oldPwd = load.U_Password;
                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        if (!oldPwd.Equals(m.U_Password)) //密码有改动,调用MD5加密
                        {
                            string passwordMd5 = Security.MD5Encrypt(load.U_Password);
                            load.U_Password = passwordMd5;
                        }
                        Model valid = Model.Valid(load);
                        return valid.Result ? SaveMessage(UtilsDB.DB.Update(load)) : ErrorMessage(valid.Message);
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
                string initPwd = entity.U_LoginName; //初始化密码为登录名
                string passwordMd5 = Security.MD5Encrypt(initPwd);
                entity.U_Password = passwordMd5;
                flag = UtilsDB.DB.Update(entity);
            }
            return Json(flag == 1 ? new JsonTip("1", "重置成功") : new JsonTip("0", "重置失败!"));
        }
        #endregion


        #region 检查用户名是否存在
        [HttpPost]
        public ActionResult CheckUserIsExist(string u_loginname)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_User where U_LoginName = @0", u_loginname);
            T_User user = UtilsDB.DB.FirstOrDefault<T_User>(sql);
            if (user != null)
            {
                return Json(new { state = false, message = "此用户名已存在，请更换" });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion
    }
}