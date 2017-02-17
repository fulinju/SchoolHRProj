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
    /// 发布管理的Controller
    /// </summary>
    public class PublishManagerController : BaseManagerController
    {
        private string PM_AD_HEAD = "PMAD_";

        //
        // GET: /Manager/PublishManager/
        public ActionResult PublishManagerView()
        {
            return View();
        }

        #region 查询发布信息
        public ActionResult GetPublishsList(string u_loginname = "")
        {
            Sql sql = Sql.Builder;

            u_loginname = "%" + u_loginname + "%";

            sql.Append("Select T_PMType.pm_typevalue,");
            sql.Append("T_PublishManage.pk_id,");
            sql.Append("T_PublishManage.pm_typeid,");
            sql.Append("T_PublishManage.u_loginname,");
            sql.Append("T_PublishManage.pm_title,");
            sql.Append("T_PublishManage.pm_adimglistid,");
            sql.Append("T_PublishManage.pm_publishtime,");
            sql.Append("T_PublishManage.pm_views,");
            sql.Append("T_PublishManage.pm_preview");
            sql.Append(" from T_PMType,T_PublishManage where U_LoginName Like @0 and T_PublishManage.IsDeleted = 0", u_loginname);
            sql.Append(" and T_PMType.PM_TypeID = T_PublishManage.PM_TypeID");

            return CommonPageList<dynamic>(sql, UtilsDB.DB);
        }
        #endregion

        #region 删除发布信息
        public ActionResult PublishDel(string model)
        {
            List<T_PublishManage> publishsList = JsonConvert.DeserializeObject<List<T_PublishManage>>(model);
            int flag = 0;
            foreach (var entity in publishsList)
            {
                entity.IsDeleted = true;
                //搜索相关联的发布广告图片列表 
                Sql sql = Sql.Builder;
                sql.Append("Update T_ADImgList set IsDeleted = 1 where PM_ADImgListID = @0", entity.PM_ADImgListID);
                UtilsDB.DB.Execute(sql);

                flag = UtilsDB.DB.Update(entity);
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑发布信息
        [ValidateInput(false)]
        public ActionResult PublishEdit(T_PublishManage m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    var validate = Model.Valid(m);
                    if (validate.Result)
                    {
                        m.PM_ADImgListID = PM_AD_HEAD + Utils.GetRamCode();
                        m.PM_Views = 0; //初始化浏览次数
                        m.IsDeleted = false;
                        object result = UtilsDB.DB.Insert(m);
                        return SaveMessage(result);
                    }
                    else
                    {
                        return Json(new JsonTip("0", validate.Message));
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_PublishManage load = UtilsDB.DB.SingleOrDefault<T_PublishManage>(obj);
                if (load == null)
                {
                    return Json(new JsonTip("0", "找不到该实体"));
                }

                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        int success = UtilsDB.DB.Update(load);
                        return SaveMessage(success);
                    }
                }
                return View("PublishEdit", load);
            }
        }
        #endregion



        public ActionResult PMAdView()
        {
            return View();
        }

        /// <summary>
        /// 获取广告图片
        /// </summary>
        /// <param name="m"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetJsonPMAdImgs(T_ADImgList m, string aid = "0")
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_ADImgList where PM_ADImgListID = @0 and IsDeleted = 0", aid);

            return CommonPageList<T_ADImgList>(sql, UtilsDB.DB);
        }

    }
}