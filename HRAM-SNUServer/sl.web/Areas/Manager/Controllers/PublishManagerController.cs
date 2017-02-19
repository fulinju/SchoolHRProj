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
            Sql sql = HRAManagerService.GetPublishSql(u_loginname);

            return CommonPageList<dynamic>(sql, HRAManagerService.database);
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
                HRAManagerService.database.Execute(sql);

                flag = HRAManagerService.database.Update(entity);
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
                        object result = HRAManagerService.database.Insert(m);
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
                T_PublishManage load = HRAManagerService.database.SingleOrDefault<T_PublishManage>(obj);
                if (load == null)
                {
                    return Json(new JsonTip("0", "找不到该实体"));
                }

                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        int success = HRAManagerService.database.Update(load);
                        return SaveMessage(success);
                    }
                }
                return View("PublishEdit", load);
            }
        }
        #endregion

        #region 广告图片
        public ActionResult PMAdView()
        {
            return View();
        }

        
        #region 获取广告图片
        public ActionResult GetJsonPMAdImgs(T_ADImgList m, string aid = "0")
        {
            Sql sql = Sql.Builder;
            sql.Append("Select T_PublishManage.pm_title,T_ADImgList.pk_id,T_ADImgList.pm_adimglisturl,T_ADImgList.pm_adimglistnum");
            sql.Append(" from T_ADImgList,T_PublishManage ");
            sql.Append(" where T_ADImgList.PM_ADImgListID =  T_PublishManage.PM_ADImgListID");
            sql.Append(" and  T_ADImgList.PM_ADImgListID= @0  and T_ADImgList.IsDeleted = 0 ", aid);
            //sql.Append(" Order by T_ADImgList.pm_adimglistnum asc"); 排序出现错误
            return CommonPageList<dynamic>(sql, HRAManagerService.database);
        }
        #endregion

        #region 删除广告图片条目
        public ActionResult PMAdDel(string model)
        {
            List<T_ADImgList> publishsList = JsonConvert.DeserializeObject<List<T_ADImgList>>(model);
            int flag = 0;
            foreach (var entity in publishsList)
            {
                entity.IsDeleted = true; //可以考虑直接删除
                flag = HRAManagerService.database.Update(entity);
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑广告图片
        [ValidateInput(false)]
        public ActionResult PMAdEdit(T_ADImgList m, string aid = "0", string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    var validate = Model.Valid(m);
                    if (validate.Result)
                    {
                        m.PM_ADImgListID = aid;
                        m.PM_ADImgListURL = UploadFile();
                        m.IsDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
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
                T_ADImgList load = HRAManagerService.database.SingleOrDefault<T_ADImgList>(obj);
                if (load == null)
                {
                    return Json(new JsonTip("0", "找不到该实体"));
                }

                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        if (Request.Files.Count > 0)
                        {
                            Utils.DeleteFile(load.PM_ADImgListURL);
                            string fileName = UploadFile();
                            if (fileName != "")
                            {
                                load.PM_ADImgListURL = fileName;
                            }
                        }

                        int success = HRAManagerService.database.Update(load);
                        return SaveMessage(success);
                    }
                }
                return View("PMAdEdit", load);
            }
        }
        #endregion

        #region 上传图片
        private string UploadFile()
        {
            string fileName = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase fileBase = Request.Files["PM_ADImgListURL"];  
                if (fileBase != null && fileBase.FileName != "")
                {
                    if(!DirFile.IsExistDirectory(Key.PublishAdImgsPath)){
                        DirFile.CreateDir(Key.PublishAdImgsPath);
                    }
                    fileName = Key.PublishAdImgsPath + Utils.GetRamCode() + "." + Utils.GetFileExt(fileBase.FileName);
                    fileBase.SaveAs(Server.MapPath(fileName));
                }
            }
            return fileName;
        }
        #endregion

        //应当加个更换的
        #region 删除图片
        [HttpPost]
        public ActionResult DelImg(string id = "0")
        {
            Sql sql = Sql.Builder.Append("Select * from T_ADImgList Where pk_id = @0", id);
            var m = HRAManagerService.database.FirstOrDefault<T_ADImgList>(sql);
            int success = 0;
            if (m != null)
            {
                Utils.DeleteFile(m.PM_ADImgListURL);
                m.PM_ADImgListURL = string.Empty;
                success = HRAManagerService.database.Update(m);
            }
            //换成 DelMessage
            if (success == 1)
            {
                return Json(Message("删除成功"), JsonRequestBehavior.AllowGet); //删除成功
            }
            else
            {
                return Json(Message("删除失败"), JsonRequestBehavior.AllowGet); //删除失败
            }
        }
        #endregion


        #endregion


        #region 编辑发布信息
        [ValidateInput(false)]
        public ActionResult EditPublishText(T_PublishManage m, string id = "0")
        {
            Object obj = id;
            T_PublishManage load = HRAManagerService.database.SingleOrDefault<T_PublishManage>(obj);
            if (load == null)
            {
                return Json(new JsonTip("0", "找不到该实体"));
            }

            if (Request.IsPost())
            {
                if (TryUpdateModel(load))
                {
                    int success = HRAManagerService.database.Update(load);
                    return SaveMessage(success);
                }
            }
            return View("EditPublishText", load);
        }
        #endregion
    }
}