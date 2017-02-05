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

        private string ERR_USERTYPE_EXIST = "用户类型已存在";
        private string ERR_DMTYPE_EXIST = "下载类型ID已存在";
        private string ERR_PMTYPE_EXIST = "发布类型ID已存在";
        private string ERR_MEMBERTYPE_EXIST = "会员类型ID已存在";
        private string ERR_REVIEW_EXIST = "审核结果ID已存在";

        private string TYPE_HEAD_USER = "USER_";
        private string TYPE_HEAD_DM = "DM_";
        private string TYPE_HEAD_PM = "PM_";
        private string TYPE_HEAD_MEMBER = "MEMBER_";
        private string TYPE_HEAD_REVIEW = "REVIEW_";

        //
        //// GET: /Manager/TypeManager/
        //private ITUserTypeService userTypeService;
        //private ITDMTypeService dmTypeService;
        //private ITPMTypeService pmTypeService;
        //private ITReviewResultService reviewResultService;
        //private ITMemberTypeService memberTypeService;

        //public TypeManagerController
        //    (ITUserTypeService userTypeService,
        //    ITDMTypeService dmTypeService,
        //    ITPMTypeService pmTypeService,
        //    ITReviewResultService reviewResultService,
        //    ITMemberTypeService memberTypeService)
        //{
        //    this.userTypeService = userTypeService;
        //    this.dmTypeService = dmTypeService;
        //    this.pmTypeService = pmTypeService;
        //    this.reviewResultService = reviewResultService;
        //    this.memberTypeService = memberTypeService;
        //}

        public ActionResult TypeManagerView()
        {
            TypeModels types = new TypeModels();
            return View(types);
        }

        #region 查询用户类型
        public ActionResult GetUserTypes(TypeModels m)
        {
            string u_logintypevalue = "";
            if (m != null)
            {
                if (m.userType != null)
                {
                    if (m.userType.U_LoginTypeValue != null)
                    {
                        u_logintypevalue = m.userType.U_LoginTypeValue;
                    }
                }
            }

            Sql sql = Sql.Builder;

            u_logintypevalue = "%" + u_logintypevalue + "%";
            sql.Append("Select * from T_UserType where U_LoginTypeValue Like @0 and IsDeleted = 0", u_logintypevalue);

            return CommonPageList<T_UserType>(sql, UtilsDB.DB);
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

            Sql sql = Sql.Builder;
            dm_typevalue = "%" + dm_typevalue + "%";
            sql.Append("Select * from T_DMType where DM_TypeValue Like @0 and IsDeleted = 0", dm_typevalue);

            return CommonPageList<T_DMType>(sql, UtilsDB.DB);
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

            Sql sql = Sql.Builder;
            pm_typevalue = "%" + pm_typevalue + "%";
            sql.Append("Select * from T_PMType where PM_TypeValue Like @0 and IsDeleted = 0", pm_typevalue);
            return CommonPageList<T_PMType>(sql, UtilsDB.DB);
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

            Sql sql = Sql.Builder;
            reviewResultValue = "%" + reviewResultValue + "%";
            sql.Append("Select * from T_ReviewResult where M_ReviewResultValue Like @0 and IsDeleted = 0", reviewResultValue);
            return CommonPageList<T_ReviewResult>(sql, UtilsDB.DB);
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

            Sql sql = Sql.Builder;
            mTypeValue = "%" + mTypeValue + "%";
            sql.Append("Select * from T_MemberType where M_TypeValue Like @0 and IsDeleted = 0", mTypeValue);
            return CommonPageList<T_MemberType>(sql, UtilsDB.DB);
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
                if (ExistInTable("T_Member", "pk_id", entity.M_TypeID))
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
                if (Request.IsPost())
                {
                    m.U_LoginTypeID = TYPE_HEAD_USER + Utils.GetRamCode();
                    m.IsDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.IsDeleted = false;
                        object result = UtilsDB.DB.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_UserType load = UtilsDB.DB.SingleOrDefault<T_UserType>(obj);
                if (Request.IsPost())
                {
                    load.U_LoginTypeValue = m.U_LoginTypeValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(UtilsDB.DB.Update(load)) : ErrorMessage(valid.Message);
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
                if (Request.IsPost())
                {
                    m.DM_TypeID = TYPE_HEAD_DM + Utils.GetRamCode();
                    m.IsDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.IsDeleted = false;
                        object result = UtilsDB.DB.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_DMType load = UtilsDB.DB.SingleOrDefault<T_DMType>(obj);
                if (Request.IsPost())
                {
                    load.DM_TypeValue = m.DM_TypeValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(UtilsDB.DB.Update(load)) : ErrorMessage(valid.Message);
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
                if (Request.IsPost())
                {
                    m.PM_TypeID = TYPE_HEAD_PM + Utils.GetRamCode();
                    m.IsDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.IsDeleted = false;
                        object result = UtilsDB.DB.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_PMType load = UtilsDB.DB.SingleOrDefault<T_PMType>(obj);
                if (Request.IsPost())
                {
                    load.PM_TypeValue = m.PM_TypeValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(UtilsDB.DB.Update(load)) : ErrorMessage(valid.Message);
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
                if (Request.IsPost())
                {
                    m.M_ReviewResultID = TYPE_HEAD_REVIEW + Utils.GetRamCode();
                    m.IsDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.IsDeleted = false;
                        object result = UtilsDB.DB.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_ReviewResult load = UtilsDB.DB.SingleOrDefault<T_ReviewResult>(obj);
                if (Request.IsPost())
                {
                    load.M_ReviewResultValue = m.M_ReviewResultValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(UtilsDB.DB.Update(load)) : ErrorMessage(valid.Message);
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
                if (Request.IsPost())
                {
                    m.M_TypeID = TYPE_HEAD_MEMBER + Utils.GetRamCode();
                    m.IsDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.IsDeleted = false;
                        object result = UtilsDB.DB.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_MemberType load = UtilsDB.DB.SingleOrDefault<T_MemberType>(obj);
                if (Request.IsPost())
                {
                    load.M_TypeValue = m.M_TypeValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(UtilsDB.DB.Update(load)) : ErrorMessage(valid.Message);
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


        #region 通用的删除方法
        public ActionResult CommonDelete<T>(List<T> m, List<string> listValue, string tableName, string pkID, string refTableName, string refSelectRow)
        {
            int flag = 1;
            //把删除的ID初始化置为TYPE_NO_ID:-0000
            for (int i = 0; i < listValue.Count; i++)
            {
                Sql sql = Sql.Builder;
                sql.Append("UPDATE " + refTableName + " SET " + refSelectRow + " =@0 WHERE " + refSelectRow + " = @1", ConstantData.TYPE_NO_ID, listValue[i]);
                UtilsDB.DB.Execute(sql);
            }

            foreach (var entity in m)
            {
                flag = UtilsDB.DB.Delete(tableName, pkID, entity);
            }

            return DelMessage(flag);
        }
        #endregion



        #region 检查用户类型是否存在
        [HttpPost]
        public ActionResult CheckUserTypeIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_UserType where U_LoginTypeValue = @0", typeValue);
            T_UserType temp = UtilsDB.DB.FirstOrDefault<T_UserType>(sql);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_USERTYPE_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

        #region 检查下载类型是否存在
        [HttpPost]
        public ActionResult CheckDMTypeIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_DMType where DM_TypeValue = @0", typeValue);
            T_DMType temp = UtilsDB.DB.FirstOrDefault<T_DMType>(sql);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_DMTYPE_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

        #region 检查发布类型是否存在
        [HttpPost]
        public ActionResult CheckPMTypeIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_PMType where PM_TypeValue = @0", typeValue);
            T_PMType temp = UtilsDB.DB.FirstOrDefault<T_PMType>(sql);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_PMTYPE_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

        #region 检查审核结果是否存在
        [HttpPost]
        public ActionResult CheckReviewResultIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_ReviewResult where M_ReviewResultValue = @0", typeValue);
            T_ReviewResult temp = UtilsDB.DB.FirstOrDefault<T_ReviewResult>(sql);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_REVIEW_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

        #region 检查会员类型是否存在
        [HttpPost]
        public ActionResult CheckMTypeIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_MemberType where M_TypeValue = @0", typeValue);
            T_MemberType temp = UtilsDB.DB.FirstOrDefault<T_MemberType>(sql);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_MEMBERTYPE_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion
    }




}