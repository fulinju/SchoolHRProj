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
    /// 会员管理的Controller
    /// </summary>
    public class MemberController : BaseManagerController
    {
        //
        // GET: /Manager/Member/
        private ITMemberService memberService;

        public MemberController(ITMemberService memberService)
        {
            this.memberService = memberService;
        }

        public ActionResult MemberView()
        {
            return View();
        }

        #region 查询
        public ActionResult GetMembersList(string u_loginname = "")
        {
            Sql where = Condition.Builder.Like("U_LoginName", u_loginname).Create();
            return CommonPageList<T_Member>(where);
        }
        #endregion

        #region 删除会员
        public ActionResult MembersDel(string model)
        {
            List<T_Member> userEntityList = JsonConvert.DeserializeObject<List<T_Member>>(model);
            bool flag = true;
            foreach (var entity in userEntityList)
            {
                flag = memberService.Delete(entity);
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
                        object result = memberService.Insert(m);
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
                T_Member load = memberService.Load(id);
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
                        bool success = memberService.Update(load);
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
            var m = memberService.Load(id);
            bool success = false;
            if (m != null)
            {
                Utils.DeleteFile(m.M_ImgURL);
                m.M_ImgURL = string.Empty;
                success = memberService.Update(m);
            }
            return SaveMessage(success);

        }
        #endregion
    }
}