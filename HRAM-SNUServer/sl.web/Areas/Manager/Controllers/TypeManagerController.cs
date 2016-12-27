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
    /// 再次封装 "编辑" 有点麻烦
    /// </summary>
    public class TypeManagerController : BaseManagerController
    {
        //private string ERR_EXIST_THIS_TYPE_USER = "存在引用了该类型的用户";
        //private string ERR_EXIST_THIS_TYPE_MEMBER = "存在引用了该类型的会员";
        //private string ERR_EXIST_THIS_TYPE_DM = "存在引用了该类型的下载内容";
        //private string ERR_EXIST_THIS_TYPE_PM = "存在引用了该类型的发布内容";
        //private string ERR_EXIST_THIS_TYPE_REVIEWRESULT = "存在引用了该类型的审核类别";

        //private string ERR_MODIFY_ADMINTYPE = "不能更改管理员类型";
        //private string ERR_MODIFY_DEFAULTREVIEW = "不能更默认的审核结果";

        private string ERR_USERTYPEID_EXIST = "用户类型ID已存在";
        private string ERR_DMTYPEID_EXIST = "下载类型ID已存在";
        private string ERR_PMTYPEID_EXIST = "发布类型ID已存在";
        private string ERR_MEMBERTYPEID_EXIST = "会员类型ID已存在";
        private string ERR_REVIEWID_EXIST = "审核结果ID已存在";

       


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
                    if (m.userType.U_LoginTypeValue != null)
                    {
                        a_logintypevalue = m.userType.U_LoginTypeValue;
                    }
                }
            }

            Sql where = Condition.Builder.Like("U_LoginTypeValue", a_logintypevalue).Create();

            return CommonPageList<T_UserType>(where);
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




        #region 删除用户类型
        public ActionResult UserTypesDel(string model)
        {
            List<T_UserType> userEntityList = JsonConvert.DeserializeObject<List<T_UserType>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in userEntityList)
            {
                if (ExistInTable("T_User", "U_LoginTypeID", entity.U_LoginTypeID))
                {
                    listValue.Add(entity.U_LoginTypeID); //获取存在于用户表的ID
                }
            }

            return CommonDelete(userEntityList, listValue, "T_UserType", "pk_id", "T_User", "U_LoginTypeID");
        }
        #endregion

        #region 删除下载类型
        public ActionResult DMTypesDel(string model)
        {
            List<T_DMType> dmEntityList = JsonConvert.DeserializeObject<List<T_DMType>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in dmEntityList)
            {
                if (ExistInTable("T_DownloadManage", "DM_TypeID", entity.DM_TypeID))
                {
                    listValue.Add(entity.DM_TypeID); //获取存在于用户表的ID
                }
            }

            return CommonDelete(dmEntityList, listValue, "T_DMType", "pk_id", "T_DownloadManage", "DM_TypeID");
        }
        #endregion

        #region 删除发布类型
        public ActionResult PMTypesDel(string model)
        {
            List<T_PMType> pmEntityList = JsonConvert.DeserializeObject<List<T_PMType>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in pmEntityList)
            {
                if (ExistInTable("T_PublishManage", "PM_TypeID", entity.PM_TypeID))
                {
                    listValue.Add(entity.PM_TypeID); //获取存在于用户表的ID
                }
            }
            return CommonDelete(pmEntityList, listValue, "T_PMType", "pk_id", "T_PublishManage", "PM_TypeID");
        }
        #endregion

        #region 删除审核结果
        public ActionResult ReviewResultsDel(string model)
        {
            List<T_ReviewResult> reviewEntityList = JsonConvert.DeserializeObject<List<T_ReviewResult>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in reviewEntityList)
            {
                if (ExistInTable("T_ReviewResult", "M_ReviewResultID", entity.M_ReviewResultID))
                {
                    listValue.Add(entity.M_ReviewResultID); //获取存在于用户表的ID
                }

            }
            return CommonDelete(reviewEntityList, listValue, "T_ReviewResult", "pk_id", "T_Member", "M_ReviewResultID");
        }
        #endregion

        #region 删除会员类型
        public ActionResult MemberTypesDel(string model)
        {

            List<T_MemberType> memberTypes = JsonConvert.DeserializeObject<List<T_MemberType>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in memberTypes)
            {
                if (ExistInTable("T_Member", "M_TypeID", entity.M_TypeID))
                {
                    listValue.Add(entity.M_TypeID); //获取存在于用户表的ID
                }
            }
            return CommonDelete(memberTypes, listValue, "T_MemberType", "pk_id", "T_Member", "M_TypeID");
        }
        #endregion



        #region 编辑用户类型
        public ActionResult UserTypeEdit(T_UserType m, string id = "0")
        {
            if (id == "0")
            {
                return CommonInsert(m, "T_UserType", "U_LoginTypeID", m.U_LoginTypeID, ERR_USERTYPEID_EXIST);
            }
            else
            {
                T_UserType load = userTypeService.Load(id);
                if (Request.IsPost())
                {
                    m.pk_id = id;
                    return CommomEdit(m, "T_UserType", "pk_id", "T_User", "U_LoginTypeID", load.U_LoginTypeID, m.U_LoginTypeID, ERR_USERTYPEID_EXIST);

                }
                return View(load);
            }
        }
        #endregion

        #region 编辑下载类型
        public ActionResult DMTypeEdit(T_DMType m, string id = "0")
        {

            if (id == "0")
            {
                return CommonInsert(m, "T_DMType", "DM_TypeID", m.DM_TypeID, ERR_DMTYPEID_EXIST);
            }
            else
            {
                T_DMType load = dmTypeService.Load(id);
                if (Request.IsPost())
                {
                    m.pk_id = id;
                    return CommomEdit(m, "T_DMType", "pk_id", "T_DownloadManage", "DM_TypeID", load.DM_TypeID, m.DM_TypeID, ERR_DMTYPEID_EXIST);
                }
                return View(load);
            }
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
                    return CommomEdit(m, "T_MemberType", "pk_id", "T_Member", "M_TypeID", load.M_TypeID, m.M_TypeID, ERR_MEMBERTYPEID_EXIST);

                }
                return View(load);
            }
        }
        #endregion





        #region 判断是否存在于表中
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
        #endregion

        #region 判断是否存在于表中的通用插入
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
        #endregion

        #region 更改的通用方法
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
                    sql.Append("UPDATE " + refTableName + " SET " + refSelectRow + " =@0 WHERE " + refSelectRow + " = @1", changedValue, beforeValue);
                    UtilsDB.DB.Execute(sql);
                }
                result = 1;
            }

            return valid.Result ? SaveMessage(result) : ErrorMessage(valid.Message);
        }
        #endregion

        #region 通用的删除方法
        public ActionResult CommonDelete<T>(List<T> m, List<string> listValue, string tableName, string pkID, string refTableName, string refSelectRow)
        {
            int flag = 1;
            //把删除的ID初始化置为TYPE_NO_ID:-0000
            for (int i = 0; i < listValue.Count; i++)
            {
                Sql sql = Sql.Builder;
                sql.Append("UPDATE " + refTableName + " SET " + refSelectRow + " =@0 WHERE " + refSelectRow + " = @1", ConstantData.TYPE_NO_ID,listValue[i]);
                UtilsDB.DB.Execute(sql);
            }

            foreach (var entity in m)
            {
                flag = UtilsDB.DB.Delete(tableName, pkID, entity);
            }

            return DelMessage(flag);
        }
        #endregion

    }




}