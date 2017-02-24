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
                        HttpPostedFileBase fileBase = GetFileBase();
                        if (fileBase == null || fileBase.FileName == "")
                        {
                            m.DM_FileURL = "";
                        }
                        else if (CheckUploadFile(fileBase))
                        {
                            return ErrorMessage("上传格式错误");
                        }
                        else
                        {
                            m.DM_FileURL = GetSavedFileName(fileBase);
                            m.DM_DownloadNum = 0; //初始化下载数量
                            m.IsDeleted = false;
                            object result = HRAManagerService.database.Insert(m);
                            SaveFile(fileBase, m.DM_FileURL);// 存储
                            return SaveMessage(result);
                        }

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
                            HttpPostedFileBase fileBase = GetFileBase();
                            if (fileBase == null || fileBase.FileName =="")
                            {
                                load.DM_FileURL = "";
                            }
                            else if (CheckUploadFile(fileBase))
                            {
                                return ErrorMessage("上传格式错误");
                            }
                            else
                            {
                                Utils.DeleteFile(load.DM_FileURL);
                                string fileName = GetSavedFileName(fileBase);                             
                                load.DM_FileURL = fileName;
                                SaveFile(fileBase, load.DM_FileURL);// 存储
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
            int flag = 0;
            if (m != null)
            {
                Utils.DeleteFile(m.DM_FileURL);
                m.DM_FileURL = string.Empty;
                flag = HRAManagerService.database.Update(m);
            }
            //换成 DelMessage

            return DelMessage(flag);

        }
        #endregion

        /// <summary>
        /// 获取存储的名称
        /// </summary>
        /// <param name="fileBase"></param>
        /// <returns></returns>
        public string GetSavedFileName(HttpPostedFileBase fileBase)
        {
            string fileName = "";

            fileName = Key.DownloadFilesPath + Utils.GetRamCode() + "." + Utils.GetFileExt(fileBase.FileName);
          
            return fileName;
        }

        /// <summary>
        /// 存储文件
        /// </summary>
        private void SaveFile(HttpPostedFileBase fileBase,string fileName)
        {
            if (!DirFile.IsExistDirectory(Key.DownloadFilesPath))
            {
                DirFile.CreateDir(Key.DownloadFilesPath);
            }

            fileBase.SaveAs(Server.MapPath(fileName)); //存储操作
        }


        #region 检查上传文件
        public bool CheckUploadFile(HttpPostedFileBase fileBase)
        {
            if (Request.Files.Count > 0)
            {
                if (fileBase != null && fileBase.FileName != "")
                {
                    string extension = Utils.GetFileExt(fileBase.FileName);

                    if (extension == "doc" || extension == "docx"
                   || extension == "xls" || extension == "xlsx"
                   || extension == "ppt" || extension == "pptx"
                   || extension == "txt")
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 获取文件FileBase
        /// </summary>
        /// <returns></returns>
        public HttpPostedFileBase GetFileBase()
        {
            return Request.Files["DM_FileURL"];
        }
    }
}