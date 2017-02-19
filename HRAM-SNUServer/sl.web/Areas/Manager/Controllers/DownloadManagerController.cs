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
            Sql sql = HRAManagerService.GetDownloadSql(dm_title);

            return CommonPageList<dynamic>(sql, HRAManagerService.database);
        }
        #endregion


        #region 删除下载
        public ActionResult DownloadDel(string model)
        {
            List<T_DownloadManage> entityList = JsonConvert.DeserializeObject<List<T_DownloadManage>>(model);
            int flag = 0;
            foreach (var entity in entityList)
            {
                entity.IsDeleted = true;
                flag = HRAManagerService.database.Update(entity); //假删除
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑下载
        public ActionResult DownloadEdit(T_DownloadManage m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    var validate = Model.Valid(m);
                    if (validate.Result)
                    {
                        m.DM_FileURL = UploadFile();
                        m.DM_DownloadNum = 0; //初始化下载数量
                        m.IsDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
                        return SaveMessage(result);
                    }
                    else
                    {
                        return ErrorMessage(validate.Message);

                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_DownloadManage load = HRAManagerService.database.SingleOrDefault<T_DownloadManage>(obj);

                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        if (Request.Files.Count > 0)
                        {
                            Utils.DeleteFile(load.DM_FileURL);
                            string fileName = UploadFile();
                            if (fileName != "")
                            {
                                load.DM_FileURL = fileName;
                            }
                        }

                        Model valid = Model.Valid(load);
                        return valid.Result ? SaveMessage(HRAManagerService.database.Update(load)) : ErrorMessage(valid.Message);
                    }
                }
                return View(load);
            }
        }
        #endregion


        #region 删除下载
        [HttpPost]
        public ActionResult DelFile(string id = "0")
        {
            Sql sql = HRAManagerService.GetDownloadByIDSql(id);
            var m = HRAManagerService.database.FirstOrDefault<T_DownloadManage>(sql);
            int success = 0;
            if (m != null)
            {
                Utils.DeleteFile(m.DM_FileURL);
                m.DM_FileURL = string.Empty;
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

        #region 上传文件
        public string UploadFile()
        {
            string fileName = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase fileBase = Request.Files["DM_FileURL"];
                if (fileBase != null && fileBase.FileName != "")
                {
                    if (!DirFile.IsExistDirectory(Key.DownloadFilesPath))
                    {
                        DirFile.CreateDir(Key.DownloadFilesPath);
                    }

                    fileName = Key.DownloadFilesPath + Utils.GetRamCode() + "." + Utils.GetFileExt(fileBase.FileName);
                    fileBase.SaveAs(Server.MapPath(fileName));
                }
            }
            return fileName;

            //return Json(Message("上传成功")); //上传成功
        }
        #endregion
    }
}