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
using PetaPoco.Orm;


namespace sl.web.Areas.Manager.Controllers
{
    /// <summary>
    /// 用户管理的Controller
    /// </summary>
    public class UserController : BaseManagerController
    {
        //
        // GET: /Manager/User/
        private ITUserService usersService;

        public UserController(ITUserService usersService)
        {
            this.usersService = usersService;
        }

        public ActionResult UsersView()
        {
            return View();
        }

        #region 查询
        public ActionResult GetUsersList(string a_loginname = "")
        {
            Sql where = Condition.Builder.Like("A_LoginName", a_loginname).Create();
            return CommonPageList<T_User>(where);
        }
        #endregion

        #region 删除用户
        public ActionResult UsersDel(string model)
        {
            List<T_User> userEntityList = JsonConvert.DeserializeObject<List<T_User>>(model);
            bool flag = true;
            foreach (var entity in userEntityList)
            {
                flag = usersService.Delete(entity);
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
                    string passwordMd5 = Security.MD5Encrypt(m.A_Password);
                    m.A_Password = passwordMd5;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        object result = DIContainer.Resolve<IBaseDao<T_User>>().Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                T_User load = usersService.Load(id);
                String oldPwd = load.A_Password;
                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        if (!oldPwd.Equals(m.A_Password)) //密码有改动,调用MD5加密
                        {
                            string passwordMd5 = Security.MD5Encrypt(load.A_Password);
                            load.A_Password = passwordMd5;
                        }
                        Model valid = Model.Valid(load);
                        return valid.Result ? SaveMessage(usersService.Update(load)) : ErrorMessage(valid.Message);
                    }
                }
                return View(load);
            }
        }
        #endregion
	}
}