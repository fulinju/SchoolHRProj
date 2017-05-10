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

namespace sl.web.Areas.Manager.Controllers
{
    /// <summary>
    /// 会员管理的Controller
    /// </summary>
    public class MemberController : BaseManagerController
    {
        //
        // GET: /Manager/Member/
      
        public ActionResult MemberView()
        {
            return View();
        }

        #region 查询
        public ActionResult GetMembersList(string mName = "")
        {
            Sql sql = HRAManagerService.GetMemberSql(mName);

            return CommonPageList<dynamic>(sql, HRAManagerService.database);
        }
        #endregion

        #region 删除会员
        public ActionResult MembersDel(string model)
        {
            List<T_Member> membersList = JsonConvert.DeserializeObject<List<T_Member>>(model);
            int flag = 0;
            foreach (var entity in membersList)
            {
                Utils.DeleteFile(entity.mImgURL);
                entity.isDeleted = true;
                flag = HRAManagerService.database.Update(entity);
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑会员
        [ValidateInput(false)]
        public ActionResult MemberEdit(T_Member m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    var validate = Model.Valid(m);
                    if (validate.Result)
                    {
                        m.mImgURL = UploadFile();
                        m.mApplyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"); //未选择时间 获取当前时间
                        if (CachedConfigContext.Current.WebSiteConfig.WebSiteKey != null) //加密Key不为空
                        {
                            m.uLoginName = Security.DesDecrypt(CachedConfigContext.Current.WebSiteConfig.WebSiteKey, Utils.GetCookie(Key.MANAGER_NAME));//DES解密
                        }
                        else
                        {
                            m.uLoginName = Utils.GetCookie(Key.MANAGER_NAME);
                        }
                        m.isDeleted = false;
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
                T_Member load = HRAManagerService.database.SingleOrDefault<T_Member>(obj);
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
                            Utils.DeleteFile(load.mImgURL);
                            string fileName = UploadFile();
                            if (fileName != "")
                            {
                                load.mImgURL = fileName;
                            }
                        }
                        int success = HRAManagerService.database.Update(load);
                        return SaveMessage(success);
                    }
                }
                return View("MemberEdit", load);
            }
        }
        #endregion

        #region 上传图片
        private string UploadFile()
        {
            string fileName = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase fileBase = Request.Files["mImgURL"];
                if (fileBase != null && fileBase.FileName != "")
                {
                    if (!DirFile.IsExistDirectory(Key.MemberIconsPath)) //判断文件夹是否存在
                    {
                        DirFile.CreateDir(Key.MemberIconsPath);
                    }

                    fileName = Key.MemberIconsPath + Utils.GetRamCode() + "." + Utils.GetFileExt(fileBase.FileName);
                    fileBase.SaveAs(Server.MapPath(fileName));
                }
            }
            //string message = "上传成功!";
            //return Json(new JsonTip("0", message));
            return fileName;
        }
        #endregion

        #region 删除图片
        [HttpPost]
        public ActionResult DelImg(string id = "0")
        {

            var m = HRAManagerService.CheckMemberExist(id);
            int success = 0;
            if (m != null)
            {
                Utils.DeleteFile(m.mImgURL);
                m.mImgURL = string.Empty;
                success = HRAManagerService.database.Update(m);
            }
            //换成 DelMessage
            if(success == 1){
                return Json(new JsonTip("1", "删除成功"));
                //return Json(Message("删除成功"), JsonRequestBehavior.AllowGet); //删除成功
            }
            else
            {
                return Json(new JsonTip("0", "删除失败"));//删除失败
            } 
        }
        #endregion

        #region 编辑会员单位简介
        [ValidateInput(false)]
        public ActionResult EditMemberSummary(T_Member m, string id = "0")
        {
            Object obj = id;
            T_Member load = HRAManagerService.database.SingleOrDefault<T_Member>(obj);
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
            return View("EditMemberSummary", load);
        }
        #endregion


        #region 检查会员名是否存在
        [HttpPost]
        public ActionResult CheckMemberNameIsExist(string mName)
        {
            T_Member member = HRAManagerService.CheckMemberNameExist(mName);
            if (member != null)
            {
                return Json(new { state = false, message = "此会员名已存在，请更换" });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion
        
    }
}