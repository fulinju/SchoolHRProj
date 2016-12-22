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
    /// 类型管理
    /// </summary>
    public class TypeManagerController : BaseManagerController
    {
        private string ERR_MODIFY_ADMINTYPE = "不能更改管理员类型";
        private string ERR_USERTYPEID_EXIST = "用户类型ID已存在";
        //
        // GET: /Manager/TypeManager/
        private ITUserTypeService userTypeService;
        private ITDMTypeService dmTypeService;
        private ITPMTypeService pmTypeService;

        public TypeManagerController
            (ITUserTypeService userTypeService,
            ITDMTypeService dmTypeService,
            ITPMTypeService pmTypeService)
        {
            this.userTypeService = userTypeService;
            this.dmTypeService = dmTypeService;
            this.pmTypeService = pmTypeService;
        }

        public ActionResult TypeManagerView()
        {
            TypeModels types = new TypeModels();
            return View(types);
        }

        #region 查询用户类型
        public ActionResult GetUserTypes(TypeModels m)
        {
            string a_logintypevalue = "";
            if (m != null)
            {
                if (m.userType != null)
                {
                    if (m.userType.A_LoginTypeValue != null)
                    {
                        a_logintypevalue = m.userType.A_LoginTypeValue;
                    }
                }
            }

            Sql where = Condition.Builder.Like("A_LoginTypeValue", a_logintypevalue).Create();

            return CommonPageList<T_UserType>(where);
        }
        #endregion

        #region 删除用户类型
        public ActionResult UserTypesDel(string model)
        {
            List<T_UserType> userEntityList = JsonConvert.DeserializeObject<List<T_UserType>>(model);
            bool flag = true;
            foreach (var entity in userEntityList)
            {
                if (entity.A_LoginTypeID.Equals(ConstantData.AdminTypeCode))
                {
                    return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, ERR_MODIFY_ADMINTYPE)); //不能删除管理员类型
                }
                else
                {
                    flag = userTypeService.Delete(entity);
                }
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑用户类型
        public ActionResult UserTypeEdit(T_UserType m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    string loginTypeID = m.A_LoginTypeID;
                    Sql sql = Sql.Builder;
                    sql.Append("Select count(*) from T_UserType where A_LoginTypeID='" + loginTypeID+"'");
                    int exist = UtilsDB.DB.Execute(sql);
                    if (exist > 0)
                    {
                        return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, ERR_USERTYPEID_EXIST));
                    }
                    else
                    {
                        return CommonAdd(m);
                    }
                }
                return View(m);
            }
            else
            {
                T_UserType load = userTypeService.Load(id);

                if (Request.IsPost())
                {
                    if (load.A_LoginTypeID.Equals(ConstantData.AdminTypeCode))
                    {
                        return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, ERR_MODIFY_ADMINTYPE));
                    }

                    if (TryUpdateModel(load))
                    {
                        Model valid = Model.Valid(load);
                        return valid.Result ? SaveMessage(userTypeService.Update(load)) : ErrorMessage(valid.Message);
                    }
                }
                return View(load);
            }
        }
        #endregion

        #region 查询下载类型
        public ActionResult GetDMTypes(TypeModels m)
        {
             string dm_typevalue = "";
            if (m != null)
            {
                if (m.dmType != null)
                {
                    if (m.dmType.DM_TypeValue != null)
                    {
                        dm_typevalue = m.dmType.DM_TypeValue;
                    }
                }
            }

            Sql where = Condition.Builder.Like("DM_TypeID", dm_typevalue).Create();

            return CommonPageList<T_DMType>(where);
        }
        #endregion

        #region 删除下载类型
        public ActionResult DMTypesDel(string model)
        {
            List<T_DMType> dmEntityList = JsonConvert.DeserializeObject<List<T_DMType>>(model);
            bool flag = true;
            foreach (var entity in dmEntityList)
            {
                flag = dmTypeService.Delete(entity);
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑下载类型
        public ActionResult DMTypeEdit(T_DMType m, string id = "0")
        {
            if (id == "0")
            {
                return CommonAdd(m);
            }
            else
            {
                T_DMType load = dmTypeService.Load(id);

                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        Model valid = Model.Valid(load);
                        return valid.Result ? SaveMessage(dmTypeService.Update(load)) : ErrorMessage(valid.Message);
                    }
                }
                return View(load);
            }
        }
        #endregion

        #region 查询发布类型
        public ActionResult GetPMTypes(TypeModels m)
        {
            string pm_typevalue = "";
            if (m != null)
            {
                if (m.pmType != null)
                {
                    if (m.pmType.PM_TypeValue != null)
                    {
                        pm_typevalue = m.pmType.PM_TypeValue;
                    }
                }
            }
            Sql where = Condition.Builder
                .Like("PM_TypeValue",pm_typevalue)
                .Create();

            return CommonPageList<T_PMType>(where);
        }
        #endregion

        #region 删除发布类型
        public ActionResult PMTypesDel(string model)
        {
            List<T_PMType> pmEntityList = JsonConvert.DeserializeObject<List<T_PMType>>(model);
            bool flag = true;
            foreach (var entity in pmEntityList)
            {
                flag = pmTypeService.Delete(entity);
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑发布类型
        public ActionResult PMTypeEdit(T_PMType m, string id = "0")
        {
            if (id == "0")
            {
                return CommonAdd(m);
            }
            else
            {
                T_PMType load = pmTypeService.Load(id);

                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        Model valid = Model.Valid(load);
                        return valid.Result ? SaveMessage(pmTypeService.Update(load)) : ErrorMessage(valid.Message);
                    }
                }
                return View(load);
            }
        }
        #endregion
    }
}