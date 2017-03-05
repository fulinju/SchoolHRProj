using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Web.Mvc;
using PetaPoco;
using sl.IService;
using sl.common;
using sl.model;
using sl.service;
using sl.validate;
using System;

namespace sl.web.ui
{
    public class BaseManagerController : BaseController
    {
        private string _sort;

        public string Sort
        {
            get
            {
                string sortfield = DTRequest.GetString("sort");
                string order = DTRequest.GetString("order");
                if (sortfield != "" && order != "")
                    _sort = sortfield + " " + (string.IsNullOrEmpty(order) ? "asc" : order);
                return _sort;
            }
            set { _sort = value; }
        }

        protected int PageIndex { get { return DTRequest.GetInt("page", 1); } }
        protected int PageSize { get { return DTRequest.GetInt("rows", 20); } }

        public BaseManagerController()
        {
        }
        /// <summary>
        /// 若Session中没有缓冲用户，则重定向到登陆页
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            T_User user = GetSessionManagerInfo();
            //为了调试取消重定向
            //if (GetSessionManagerInfo() == null)
            //{
            //    filterContext.Result = RedirectToAction("Login", "Login", new { Areas = "Manager" });
            //    return;
            //}
            base.OnActionExecuting(filterContext);
        }

        public T_User UserContext
        {
            get { return GetSessionManagerInfo(); }
        }

        public T_User GetSessionManagerInfo()
        {
            T_User manager = Session[Key.MANAGER_INFO] as T_User;
            if (manager == null)
            {
                string username = Utils.GetCookie(Key.MANAGER_NAME);
                string password = Utils.GetCookie(Key.MANAGER_PASS);
                if (username != "" || password != "")
                {
                    username = Security.DesDecrypt(CachedConfigContext.Current.WebSiteConfig.WebSiteKey, username);
                    //ITUserService service = DIContainer.Resolve<ITUserService>();
                    //Condition where = Condition.Builder.Equal("uLoginName", username).Equal("uLoginTypeValue", password);
                    //manager = service.Load(where.Create());
                    Database DB = new Database("ConnectionString");
                    Sql sql = Sql.Builder;
                    sql.Append("Select * from T_User where uLoginName = @0 and uPassword = @0", username, password);
                    manager = DB.FirstOrDefault<T_User>(sql);

                    if (manager != null)
                    {
                        Session[Key.MANAGER_INFO] = manager;
                        return manager;
                    }
                }
                return null;
            }
            return manager;
        }

        #region 通用CRUD
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public ActionResult CommonAdd<T>(Database DB, T t)
        {
            if (Request.IsPost())
            {
                var validate = Model.Valid(t);
                if (!validate.Result)
                    return ErrorMessage(validate.Message);
                //object result = DIContainer.Resolve<IBaseDao<T>>().Insert(t);
                object result = DB.Insert(t);
                return SaveMessage(result);
            }
            return View(t);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //public ActionResult CommonUpdate<T>(Database DB,T t, int id = 0) where T : class
        //{
        //    IBaseDao<T> service = DIContainer.Resolve<IBaseDao<T>>();
        //    Sql sql = Sql.Builder;
        //    sql.Append("Select * from ");
        //    T load = service.Load(id);
        //    if (Request.IsPost())
        //    {
        //        var validate = Model.Valid(t);
        //        if (!validate.Result)
        //        {
        //            return ErrorMessage(validate.Message);
        //        }
        //        bool updateSuccess = TryUpdateModel(load);
        //        return updateSuccess ? SaveMessage(service.Update(load)) : ErrorMessage("更新实体错误...");
        //    }
        //    return View(load);
        //}

        /// <summary>
        /// 通用的删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        //public ActionResult CommonDelete<T>(int id)
        //{
        //    bool success = DIContainer.Resolve<IBaseDao<T>>().DeleteById(id);
        //    return DelMessage(success);
        //}

        /// <summary>
        /// 联表分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="showFields"></param>
        /// <param name="where"></param>
        /// <param name="joinTable"></param>
        /// <returns></returns>
        //public ActionResult DynPageList<T>(string showFields, Sql where, List<Join> joinTable)
        //{
        //    Page<dynamic> dynPager = DIContainer.Resolve<IBaseDao<T>>().DynPager(showFields, PageIndex, PageSize, joinTable, where, Sort);
        //    return Json(new { total = dynPager.TotalItems, rows = dynPager.Items }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="where"></param>
        ///// <returns></returns>
        //public ActionResult CommonPageList<T>(Sql where)
        //{
        //    Page<T> list = DIContainer.Resolve<IBaseDao<T>>().Pager(PageIndex, PageSize, where, Sort);
        //    return Json(new { total = list.TotalItems, rows = list.Items });
        //    /*return Json(new Dictionary<string, object> { { "total", list.TotalItems }, { "rows", list.Items } },
        //                JsonRequestBehavior.AllowGet);*/
        //}

        /// <summary>
        /// 由Page进行分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult CommonPageList<T>(Sql sql,Database DB)
        {
            sql.OrderBy(Sort); //排序
            Page<T> list = DB.Page<T>(PageIndex, PageSize, sql);
            return Json(new { total = list.TotalItems, rows = list.Items });
            /*return Json(new Dictionary<string, object> { { "total", list.TotalItems }, { "rows", list.Items } },
                        JsonRequestBehavior.AllowGet);*/
        }

        #endregion
    }
}
