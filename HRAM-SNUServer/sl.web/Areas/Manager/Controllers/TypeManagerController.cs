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
    /// 二次封装 "编辑" 有点麻烦
    /// </summary>
    public class TypeManagerController : BaseManagerController
    {
        private string ERR_EXIST_THIS_TYPE_USER = "存在引用了该类型的用户";
        private string ERR_EXIST_THIS_TYPE_MEMBER = "存在引用了该类型的会员";
        private string ERR_EXIST_THIS_TYPE_DM = "存在引用了该类型的下载内容";
        private string ERR_EXIST_THIS_TYPE_PM = "存在引用了该类型的发布内容";
        private string ERR_EXIST_THIS_TYPE_REVIEWRESULT = "存在引用了该类型的审核类别";

        private string ERR_USERTYPEID_EXIST = "用户类型ID已存在";
        private string ERR_DMTYPEID_EXIST = "下载类型ID已存在";
        private string ERR_PMTYPEID_EXIST = "发布类型ID已存在";
        private string ERR_MEMBERTYPEID_EXIST = "会员类型ID已存在";
        private string ERR_REVIEWID_EXIST = "审核结果ID已存在";

        private string ERR_MODIFY_ADMINTYPE = "不能更改管理员类型";
        private string ERR_MODIFY_DEFAULTREVIEW = "不能更默认的审核结果";


        //
        // GET: /Manager/TypeManager/
        private ITUserTypeService userTypeService;
        private ITDMTypeService dmTypeService;
        private ITPMTypeService pmTypeService;
        private ITReviewResultService reviewResultService;
        private ITMemberTypeService memberTypeService;

        public TypeManagerController
            (ITUserTypeService userTypeService,
            ITDMTypeService dmTypeService,
            ITPMTypeService pmTypeService,
            ITReviewResultService reviewResultService,
            ITMemberTypeService memberTypeService)
        {
            this.userTypeService = userTypeService;
            this.dmTypeService = dmTypeService;
            this.pmTypeService = pmTypeService;
            this.reviewResultService = reviewResultService;
            this.memberTypeService = memberTypeService;
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
            int flag = 1;
            foreach (var entity in userEntityList)
            {
                if (ExistInTable("T_User", "A_LoginTypeID", entity.A_LoginTypeID))
                {
                    return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, ERR_EXIST_THIS_TYPE_USER)); //存在于用户表
                }

                if (entity.A_LoginTypeID.Equals(ConstantData.AdminTypeCode))
                {
                    return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, ERR_MODIFY_ADMINTYPE)); //不能删除管理员类型
                }
            }

            foreach (var entity in userEntityList)
            {
                flag = UtilsDB.DB.Delete("T_UserType", "pk_id", entity);

            }

            return DelMessage(flag);
        }
        #endregion

        #region 编辑用户类型
        public ActionResult UserTypeEdit(T_UserType m, string id = "0")
        {
            if (id == "0")
            {
                return CommonInsert(m, "T_UserType", "A_LoginTypeID", m.A_LoginTypeID, ERR_USERTYPEID_EXIST);
            }
            else
            {
                T_UserType load = userTypeService.Load(id);
                if (Request.IsPost())
                {
                    m.pk_id = id;
                    return CommomEdit(m, "T_UserType", "pk_id", "T_User", "A_LoginTypeID", load.A_LoginTypeID, m.A_LoginTypeID, ERR_USERTYPEID_EXIST);

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
                if (Request.IsPost())
                {
                    string dmTypeID = m.DM_TypeID;
                    Sql sql = Sql.Builder;
                    sql.Append("Select count(*) from T_DMType where DM_TypeID=@0", dmTypeID);
                    int exist = UtilsDB.DB.FirstOrDefault<int>(sql);
                    if (exist > 0)
                    {
                        return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, ERR_DMTYPEID_EXIST));
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
                .Like("PM_TypeValue", pm_typevalue)
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
                return CommonInsert(m, "T_PMType", "PM_TypeID", m.PM_TypeID, ERR_PMTYPEID_EXIST);
            }
            else
            {
                T_PMType load = pmTypeService.Load(id);
                if (Request.IsPost())
                {
                    m.pk_id = id;
                    return CommomEdit(m, "T_PMType", "pk_id", "T_PublishManage", "PM_TypeID", load.PM_TypeID, m.PM_TypeID, ERR_PMTYPEID_EXIST);
                }
                return View(load);
            }
        }
        #endregion

        #region 查询审核结果
        public ActionResult GetReviewResults(TypeModels m)
        {
            string reviewResultValue = "";
            if (m != null)
            {
                if (m.reviewResult != null)
                {
                    if (m.reviewResult.M_ReviewResultValue != null)
                    {
                        reviewResultValue = m.reviewResult.M_ReviewResultValue;
                    }
                }
            }

            Sql where = Condition.Builder.Like("M_ReviewResultValue", reviewResultValue).Create();

            return CommonPageList<T_ReviewResult>(where);
        }
        #endregion

        #region 删除审核结果
        public ActionResult ReviewResultsDel(string model)
        {
            List<T_ReviewResult> reviewEntityList = JsonConvert.DeserializeObject<List<T_ReviewResult>>(model);
            bool flag = true;
            foreach (var entity in reviewEntityList)
            {
                int exist = Array.IndexOf(ConstantData.DefaultReviewIDs, entity.M_ReviewResultID);
                if (exist != -1) //等于-1不存在
                {
                    return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, ERR_MODIFY_DEFAULTREVIEW));
                }
                else
                {
                    flag = reviewResultService.Delete(entity);
                }
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑审核结果
        public ActionResult ReviewResultEdit(T_ReviewResult m, string id = "0")
        {
            if (id == "0")
            {
                return CommonInsert(m, "T_ReviewResult", "M_ReviewResultID", m.M_ReviewResultID, ERR_REVIEWID_EXIST); 
            }
            else
            {
                T_ReviewResult load = reviewResultService.Load(id);
                if (Request.IsPost())
                {
                    m.pk_id = id;
                    return CommomEdit(m, "T_ReviewResult", "pk_id", "T_Member", "M_ReviewResultID", load.M_ReviewResultID, m.M_ReviewResultID, ERR_REVIEWID_EXIST);
                }
                return View(load);
            }
        }
        #endregion

        #region 查询会员类型
        public ActionResult GetMemberTypes(TypeModels m)
        {
            string mTypeValue = "";
            if (m != null)
            {
                if (m.memberType != null)
                {
                    if (m.memberType.M_TypeValue != null)
                    {
                        mTypeValue = m.memberType.M_TypeValue;
                    }
                }
            }

            Sql where = Condition.Builder.Like("M_TypeValue", mTypeValue).Create();

            return CommonPageList<T_MemberType>(where);
        }
        #endregion

        #region 删除会员类型
        public ActionResult MemberTypesDel(string model)
        {
            List<T_MemberType> memberTypes = JsonConvert.DeserializeObject<List<T_MemberType>>(model);
            bool flag = true;
            foreach (var entity in memberTypes)
            {
                flag = memberTypeService.Delete(entity);
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑会员类型
        public ActionResult MemberTypeEdit(T_MemberType m, string id = "0")
        {
            if (id == "0")
            {
                return CommonInsert(m, "T_MemberType", "M_TypeID", m.M_TypeID, ERR_MEMBERTYPEID_EXIST);
            }
            else
            {
                T_MemberType load = memberTypeService.Load(id);
                if (Request.IsPost())
                {
                    m.pk_id = id;
                    return CommomEdit(m,"T_MemberType", "pk_id", "T_Member", "M_TypeID",load.M_TypeID,m.M_TypeID, ERR_MEMBERTYPEID_EXIST);
                    
                }
                return View(load);
            }
        }
        #endregion

        #region 通用方法
        /// <summary>
        /// 判断是否存在于表中
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="selectRow"></param>
        /// <param name="selectRowValue"></param>
        /// <returns></returns>
        public bool ExistInTable(string tableName, string selectRow, string selectRowValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select count(*) from " + tableName + " where " + selectRow + "=@0", selectRowValue);
            int exist = UtilsDB.DB.FirstOrDefault<int>(sql);
            if (exist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否存在于表中的通用插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m">实体类</param> 
        /// <param name="tableName">表名</param>
        /// <param name="selectRow">需要判断的列</param>
        /// <param name="selectRowValue">需要判断的列的值</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public ActionResult CommonInsert<T>(T m, string tableName, string selectRow, string selectRowValue, string errMsg)
        {
            if (Request.IsPost())
            {
                if (ExistInTable(tableName, selectRow, selectRowValue))
                {
                    return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, errMsg));
                }
                else
                {
                    return CommonAdd(m);
                }
            }
            return View(m);
        }
       
        /// <summary>
        /// 更改的通用方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="modified"></param>
        /// <param name="tableName"></param>
        /// <param name="pkID"></param>
        /// <param name="refTableName"></param>
        /// <param name="refSelectRow"></param>
        /// <param name="refSelectRowValue"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public ActionResult CommomEdit<T>(T m, string tableName, string pkID, string refTableName, string refSelectRow, string beforeValue, string changedValue, string errMsg)
        {

            if (beforeValue != changedValue)//ID修改了
            {
                if (ExistInTable(tableName, refSelectRow, changedValue))
                {
                    return Json(new JsonTip(JsonTipHelper.ErrorJsonTip, errMsg)); //ID已使用
                }
            }

            Model valid = Model.Valid(m);
            int result = 0;
            if (UtilsDB.DB.Update(tableName, pkID, m) == 1)
            {
                if (ExistInTable(refTableName, refSelectRow, beforeValue))
                {
                    //其他表有这个数据 进行联表修改
                    Sql sql = Sql.Builder;
                    sql.Append("UPDATE " + refTableName + " SET " + refSelectRow + " = '" + changedValue + "' WHERE " + refSelectRow + " = '" + beforeValue + "' ");
                    UtilsDB.DB.Execute(sql);
                }
                result = 1;
            }

            return valid.Result ? SaveMessage(result) : ErrorMessage(valid.Message);
        }
        #endregion
    }




}