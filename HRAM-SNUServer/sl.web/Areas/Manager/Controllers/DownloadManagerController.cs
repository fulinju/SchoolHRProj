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
    /// 下载管理
    /// </summary>
    public class DownloadManagerController : BaseManagerController
    {
        //
        // GET: /Manager/DownloadManager/
        public ActionResult DownloadManagerView()
        {
            return View();
        }

        #region 查询下载列表
        public ActionResult GetDownloadList(string dm_title)
        {
            Sql sql = Sql.Builder;

            dm_title = "%" + dm_title + "%";

            sql.Append("Select T_DMType.dm_typevalue,");
            sql.Append("T_DownloadManage.pk_id,");
            sql.Append("T_DownloadManage.u_loginname,");
            sql.Append("T_DownloadManage.dm_title,");
            sql.Append("T_DownloadManage.dm_fileurl,");
            sql.Append("T_DownloadManage.dm_downloadnum,");
            sql.Append("T_DownloadManage.dm_uploadtime");
            sql.Append(" from T_DMType,T_DownloadManage where U_LoginName Like @0 and T_DownloadManage.IsDeleted = 0", dm_title);
            sql.Append(" and T_DMType.DM_TypeID = T_DownloadManage.DM_TypeID");

            List<dynamic> list = UtilsDB.DB.Fetch<dynamic>(sql);

            //string json = JsonConvert.SerializeObject(list);

            return CommonPageList<dynamic>(sql, UtilsDB.DB);
        }
        #endregion


        #region 删除链接
        public ActionResult DownloadDel(string model)
        {
            List<T_DownloadManage> entityList = JsonConvert.DeserializeObject<List<T_DownloadManage>>(model);
            int flag = 0;
            foreach (var entity in entityList)
            {
                entity.IsDeleted = true;
                flag = UtilsDB.DB.Update(entity); //假删除
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑链接
        public ActionResult DownloadEdit(T_DownloadManage m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.DM_DownloadNum = 0; //初始化下载数量
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
                T_DownloadManage load = UtilsDB.DB.SingleOrDefault<T_DownloadManage>(obj);

                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
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