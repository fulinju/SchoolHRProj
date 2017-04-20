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
using sl.service;
using NPinyin;

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
        public ActionResult GetDownloadList(string dmTitle)
        {
            Sql sql = HRAManagerService.GetDownloadSql(dmTitle);

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
                DirFile.DeleteFile(entity.dmFileURL);
                flag = HRAManagerService.database.Delete(entity); //直接删
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
                            return ErrorMessage("请上传文件");
                        }
                        else if (CheckUploadFile(fileBase))
                        {
                            return ErrorMessage("上传格式错误");
                        }
                        else
                        {
                            if (m.dmUploadTime == null)
                            {
                                m.dmUploadTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"); //未选择时间 获取当前时间
                            }
                            m.dmFileURL = SaveFile(fileBase, HRAManagerService.GetDownloadValueByID(m.dmTypeID));// 存储
                            m.uLoginName = Security.DesDecrypt(CachedConfigContext.Current.WebSiteConfig.WebSiteKey, Utils.GetCookie(Key.MANAGER_NAME));//DES解密
                            m.dmDownloadNum = 0; //初始化下载数量
                            m.isDeleted = false;
                            object result = HRAManagerService.database.Insert(m);
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

                if (load == null)
                {
                    return Json(new JsonTip("0", "找不到该实体"));
                }

                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {

                        if (m.dmFileURL != null)
                        {
                            HttpPostedFileBase fileBase = GetFileBase();
                            if (fileBase == null || fileBase.FileName == "")
                            {
                                return ErrorMessage("请上传文件");
                            }
                            else if (CheckUploadFile(fileBase))
                            {
                                return ErrorMessage("上传格式错误");
                            }

                            Utils.DeleteFile(load.dmFileURL);
                            //string fileName = GetSavedFileName(fileBase);
                            load.dmFileURL = SaveFile(fileBase, HRAManagerService.GetDownloadValueByID(m.dmTypeID));// 存储
                        }


                        if (m.dmUploadTime == null)
                        {
                            load.dmUploadTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"); //未选择时间 获取当前时间
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
                Utils.DeleteFile(m.dmFileURL);
                m.dmFileURL = string.Empty;
                flag = HRAManagerService.database.Update(m);
            }
            //换成 DelMessage

            DirFile.DeleteFile(m.dmFileURL);
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

            fileName = Utils.GetRamCode() + "." + Utils.GetFileExt(fileBase.FileName);

            return fileName;
        }

        /// <summary>
        /// 存储文件
        /// </summary>
        private string SaveFile(HttpPostedFileBase fileBase, string typeName)
        {
            string typeNamePinYin = Pinyin.GetPinyin(typeName); //转拼音 免得不识别
            typeNamePinYin = typeNamePinYin.Replace(" ", "");

            string tagetPath = Key.DownloadFilesPath + typeNamePinYin + "/";
            if (!DirFile.IsExistDirectory(tagetPath))
            {
                DirFile.CreateDir(tagetPath);
            }

            tagetPath = tagetPath + GetSavedFileName(fileBase);

            fileBase.SaveAs(Server.MapPath(tagetPath)); //存储操作
            return tagetPath;
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
            return Request.Files["dmFileURL"];
        }
    }
}