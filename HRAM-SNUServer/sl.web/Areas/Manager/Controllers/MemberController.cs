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
        public ActionResult GetMembersList(string u_loginname = "")
        {
            Sql sql = Sql.Builder;

            u_loginname = "%" + u_loginname + "%";
            sql.Append("Select * from T_Member where U_LoginName Like @0 and IsDeleted = 0", u_loginname);

            return CommonPageList<T_Member>(sql, UtilsDB.DB);
        }
        #endregion

        #region 删除会员
        public ActionResult MembersDel(string model)
        {
            List<T_Member> membersList = JsonConvert.DeserializeObject<List<T_Member>>(model);
            int flag = 0;
            foreach (var entity in membersList)
            {
                flag = UtilsDB.DB.Delete(entity);
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑会员
        public ActionResult MemberEdit(T_Member m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    var validate = Model.Valid(m);
                    if (validate.Result)
                    {
                        m.M_ImgURL = UploadFile();
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
                Sql sql = Sql.Builder.Append("Select * from T_Member Where pk_id = @0", id);
                T_Member load = UtilsDB.DB.FirstOrDefault<T_Member>(sql);
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
                            Utils.DeleteFile(load.M_ImgURL);
                            string fileName = UploadFile();
                            if (fileName != "")
                            {
                                load.M_ImgURL = fileName;
                            }
                        }
                        int success = UtilsDB.DB.Update(load);
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
                HttpPostedFileBase fileBase = Request.Files["M_ImgURL"];
                if (fileBase != null && fileBase.FileName != "")
                {
                    fileName = Key.MemberIconsPath + Utils.GetRamCode() + "." + Utils.GetFileExt(fileBase.FileName);
                    fileBase.SaveAs(Server.MapPath(fileName));
                }
            }
            return fileName;
        }
        #endregion

        #region 删除图片
        [HttpPost]
        public ActionResult DelM_ImgUrl(string id = "0")
        {
            Sql sql = Sql.Builder.Append("Select * from T_Member Where pk_id = @0", id);
            var m = UtilsDB.DB.FirstOrDefault<T_Member>(sql);
            int success = 0;
            if (m != null)
            {
                Utils.DeleteFile(m.M_ImgURL);
                m.M_ImgURL = string.Empty;
                success = UtilsDB.DB.Update(m);
            }
            return SaveMessage(success);

        }
        #endregion
    }
}