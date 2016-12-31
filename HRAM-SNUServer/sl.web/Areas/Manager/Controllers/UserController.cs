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
            sql.Append("Select * from T_User where U_LoginName Like @0 and IsDeleted = 0", u_loginname);

            return CommonPageList<T_User>(sql,UtilsDB.DB);
        }
        #endregion


        #region 删除用户
        public ActionResult UsersDel(string model)
        {
            List<T_User> usersList = JsonConvert.DeserializeObject<List<T_User>>(model);
            int flag = 0;
            foreach (var entity in usersList)
            {
                flag = UtilsDB.DB.Delete(entity);
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
                        //object result = DIContainer.Resolve<IBaseDao<T_User>>().Insert(m);
                        m.IsDeleted = false;
                        object result = UtilsDB.DB.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                //T_User load = UtilsDB.DB.SingleOrDefault<T_User>(id);
                Sql sql = Sql.Builder.Append("Select * from T_User Where pk_id = @0", id);
                T_User load = UtilsDB.DB.FirstOrDefault<T_User>(sql);
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
	}
}