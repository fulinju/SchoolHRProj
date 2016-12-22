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
        public ActionResult GetMembersList(string a_loginname = "")
        {
            Sql where = Condition.Builder.Like("A_LoginName", a_loginname).Create();
            return CommonPageList<T_Member>(where);
        }
        #endregion

        #region 删除会员
        public ActionResult MemberDel(string model)
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
        public ActionResult MealEdit(T_Member m, string id = "0")
        {
            if (id == "0")
            {
                return CommonAdd(m);
            }
            else
            {
                T_Member load = memberService.Load(id);
                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        Model valid = Model.Valid(load);
                        return valid.Result ? SaveMessage(memberService.Update(load)) : ErrorMessage(valid.Message);
                    }
                }
                return View(load);
            }
        }
        #endregion
	}
}