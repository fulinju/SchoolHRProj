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
        private string ERR_DMTYPE_EXIST = "下载类型已存在";
        private string ERR_PMTYPE_EXIST = "发布类型已存在";
        private string ERR_MEMBERTYPE_EXIST = "会员类型已存在";
        private string ERR_REVIEW_EXIST = "审核结果类型已存在";
        private string ERR_FL_EXIST = "友情链接类型已存在";

        private string TYPE_HEAD_USER = "USER_";
        private string TYPE_HEAD_DM = "DM";
        private string TYPE_HEAD_PM = "PM";
        private string TYPE_HEAD_MEMBER = "MEMBER_";
        private string TYPE_HEAD_REVIEW = "REVIEW_";
        private string TYPE_HEAD_FL = "FL_";

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
            string uLoginTypeValue = "";
            if (m != null)
            {
                if (m.userType != null)
                {
                    if (m.userType.uLoginTypeValue != null)
                    {
                        uLoginTypeValue = m.userType.uLoginTypeValue;
                    }
                }
            }

            Sql sql = HRAManagerService.GetUserTypeSql(uLoginTypeValue);
            return CommonPageList<T_UserType>(sql, HRAManagerService.database);
        }
        #endregion

        #region 查询下载类型
        public ActionResult GetDMTypes(TypeModels m)
        {
            string dmTypeValue = "";
            if (m != null)
            {
                if (m.dmType != null)
                {
                    if (m.dmType.dmTypeValue != null)
                    {
                        dmTypeValue = m.dmType.dmTypeValue;
                    }
                }
            }

            Sql sql = HRAManagerService.GetDownloadTypeSql(dmTypeValue);

            return CommonPageList<T_DMType>(sql, HRAManagerService.database);
        }
        #endregion

        #region 查询发布类型
        public ActionResult GetPMTypes(TypeModels m)
        {
            string pmTypeValue = "";
            if (m != null)
            {
                if (m.pmType != null)
                {
                    if (m.pmType.pmTypeValue != null)
                    {
                        pmTypeValue = m.pmType.pmTypeValue;
                    }
                }
            }

            Sql sql = HRAManagerService.GetPublishTypeSql(pmTypeValue);
            return CommonPageList<T_PMType>(sql, HRAManagerService.database);
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
                    if (m.reviewResult.mReviewResultValue != null)
                    {
                        reviewResultValue = m.reviewResult.mReviewResultValue;
                    }
                }
            }

            Sql sql = HRAManagerService.GetReviewResultSql(reviewResultValue);
            return CommonPageList<T_ReviewResult>(sql, HRAManagerService.database);
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
                    if (m.memberType.mTypeValue != null)
                    {
                        mTypeValue = m.memberType.mTypeValue;
                    }
                }
            }

            Sql sql = HRAManagerService.GetMemberTypeSql(mTypeValue);
            return CommonPageList<T_MemberType>(sql, HRAManagerService.database);
        }
        #endregion

        #region 查询友情链接类型
        public ActionResult GetFLTypes(TypeModels m)
        {
            string flTypeValue = "";
            if (m != null)
            {
                if (m.memberType != null)
                {
                    if (m.memberType.mTypeValue != null)
                    {
                        flTypeValue = m.memberType.mTypeValue;
                    }
                }
            }

            Sql sql = HRAManagerService.GetFLTypeSql(flTypeValue);
            return CommonPageList<T_FLType>(sql, HRAManagerService.database);
        }
        #endregion



        #region 删除用户类型
        public ActionResult UserTypesDel(string model)
        {
            List<T_UserType> userEntityList = JsonConvert.DeserializeObject<List<T_UserType>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in userEntityList)
            {
                if (entity.uLoginTypeID == ConstantData.AdminTypeCode)
                {
                    return Json(new JsonTip("0", ConstantData.CANT_DEL_ADMIN_TYPE));
                }

                if (entity.uLoginTypeID == ConstantData.TYPE_DEFALUT_ID)
                {
                    return Json(new JsonTip("0", ConstantData.CANT_DEL_DEFAULT));
                }

                if (ExistInTable("T_User", "uLoginTypeID", entity.uLoginTypeID))
                {
                    listValue.Add(entity.uLoginTypeID); //获取存在于用户表的ID
                }
            }

            return CommonDelete(userEntityList, listValue, "T_UserType", "pkId", "T_User", "uLoginTypeID");
        }
        #endregion

        #region 删除下载类型
        public ActionResult DMTypesDel(string model)
        {
            List<T_DMType> dmEntityList = JsonConvert.DeserializeObject<List<T_DMType>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in dmEntityList)
            {
                if (entity.dmTypeID == ConstantData.TYPE_DEFALUT_ID)
                {
                    return Json(new JsonTip("0", ConstantData.CANT_DEL_DEFAULT));
                }

                if (ExistInTable("T_DownloadManage", "dmTypeID", entity.dmTypeID))
                {
                    listValue.Add(entity.dmTypeID); //获取存在于用户表的ID
                }
            }

            return CommonDelete(dmEntityList, listValue, "T_DMType", "pkId", "T_DownloadManage", "dmTypeID");
        }
        #endregion

        #region 删除发布类型
        public ActionResult PMTypesDel(string model)
        {
            List<T_PMType> pmEntityList = JsonConvert.DeserializeObject<List<T_PMType>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in pmEntityList)
            {
                if (entity.pmTypeID == ConstantData.TYPE_DEFALUT_ID)
                {
                    return Json(new JsonTip("0", ConstantData.CANT_DEL_DEFAULT));
                }

                if (ExistInTable("T_PublishManage", "pmTypeID", entity.pmTypeID))
                {
                    listValue.Add(entity.pmTypeID); //获取存在于用户表的ID
                }
            }
            return CommonDelete(pmEntityList, listValue, "T_PMType", "pkId", "T_PublishManage", "pmTypeID");
        }
        #endregion

        #region 删除审核结果
        public ActionResult ReviewResultsDel(string model)
        {
            List<T_ReviewResult> reviewEntityList = JsonConvert.DeserializeObject<List<T_ReviewResult>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in reviewEntityList)
            {
                if (entity.mReviewResultID == ConstantData.TYPE_DEFALUT_ID)
                {
                    return Json(new JsonTip("0", ConstantData.CANT_DEL_DEFAULT));
                }

                if (ExistInTable("T_ReviewResult", "mReviewResultID", entity.mReviewResultID))
                {
                    listValue.Add(entity.mReviewResultID); //获取存在于用户表的ID
                }

            }
            return CommonDelete(reviewEntityList, listValue, "T_ReviewResult", "pkId", "T_Member", "mReviewResultID");
        }
        #endregion

        #region 删除会员类型
        public ActionResult MemberTypesDel(string model)
        {

            List<T_MemberType> memberTypes = JsonConvert.DeserializeObject<List<T_MemberType>>(model);

            List<string> listValue = new List<string>();
            foreach (var entity in memberTypes)
            {
                if (entity.mTypeID == ConstantData.TYPE_DEFALUT_ID)
                {
                    return Json(new JsonTip("0", ConstantData.CANT_DEL_DEFAULT));
                }

                if (ExistInTable("T_Member", "mTypeID", entity.mTypeID))
                {
                    listValue.Add(entity.mTypeID); //获取存在于用户表的ID
                }
            }
            return CommonDelete(memberTypes, listValue, "T_MemberType", "pkId", "T_Member", "mTypeID");
        }
        #endregion

        #region 删除友情链接类型
        public ActionResult FLTypesDel(string model)
        {

            List<T_FLType> flTypes = JsonConvert.DeserializeObject<List<T_FLType>>(model);
           

            List<string> listValue = new List<string>();
            foreach (var entity in flTypes)
            {
                if (entity.flTypeID == ConstantData.TYPE_DEFALUT_ID)
                {
                    return Json(new JsonTip("0", ConstantData.CANT_DEL_DEFAULT));
                }
                if (ExistInTable("T_FriendlyLink", "flTypeID", entity.flTypeID))
                {
                    listValue.Add(entity.flTypeID); //获取存在于友情链接表的ID
                }
            }
            return CommonDelete(flTypes, listValue, "T_FLType", "pkId", "T_FriendlyLink", "flTypeID");
        }
        #endregion



        #region 编辑用户类型
        public ActionResult UserTypeEdit(T_UserType m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    m.uLoginTypeID = TYPE_HEAD_USER + Utils.GetRamCode();
                    m.isDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.isDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_UserType load = HRAManagerService.database.SingleOrDefault<T_UserType>(obj);
                if (Request.IsPost())
                {
                    load.uLoginTypeValue = m.uLoginTypeValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(HRAManagerService.database.Update(load)) : ErrorMessage(valid.Message);
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
                    m.dmTypeID = TYPE_HEAD_DM + Utils.GetRamCode();
                    m.isDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.isDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_DMType load = HRAManagerService.database.SingleOrDefault<T_DMType>(obj);
                if (Request.IsPost())
                {
                    load.dmTypeValue = m.dmTypeValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(HRAManagerService.database.Update(load)) : ErrorMessage(valid.Message);
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
                    m.pmTypeID = TYPE_HEAD_PM + Utils.GetRamCode();
                    m.isDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.isDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_PMType load = HRAManagerService.database.SingleOrDefault<T_PMType>(obj);
                if (Request.IsPost())
                {
                    load.pmTypeValue = m.pmTypeValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(HRAManagerService.database.Update(load)) : ErrorMessage(valid.Message);
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
                    m.mReviewResultID = TYPE_HEAD_REVIEW + Utils.GetRamCode();
                    m.isDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.isDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_ReviewResult load = HRAManagerService.database.SingleOrDefault<T_ReviewResult>(obj);
                if (Request.IsPost())
                {
                    load.mReviewResultValue = m.mReviewResultValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(HRAManagerService.database.Update(load)) : ErrorMessage(valid.Message);
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
                    m.mTypeID = TYPE_HEAD_MEMBER + Utils.GetRamCode();
                    m.isDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.isDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_MemberType load = HRAManagerService.database.SingleOrDefault<T_MemberType>(obj);
                if (Request.IsPost())
                {
                    load.mTypeValue = m.mTypeValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(HRAManagerService.database.Update(load)) : ErrorMessage(valid.Message);
                }
                return View(load);
            }
        }
        #endregion

        #region 编辑友情链接类型
        public ActionResult FLTypeEdit(T_FLType m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    m.flTypeID = TYPE_HEAD_FL + Utils.GetRamCode();
                    m.isDeleted = false;

                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.isDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_FLType load = HRAManagerService.database.SingleOrDefault<T_FLType>(obj);
                if (Request.IsPost())
                {
                    load.flTypeValue = m.flTypeValue;
                    Model valid = Model.Valid(load);
                    return valid.Result ? SaveMessage(HRAManagerService.database.Update(load)) : ErrorMessage(valid.Message);
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
            int exist = HRAManagerService.ExistInTable(tableName, selectRow, selectRowValue);
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
                HRAManagerService.CommonDelete(refTableName, refSelectRow, ConstantData.TYPE_DEFALUT_ID, listValue[i]);
            }

            foreach (var entity in m)
            {
                flag = HRAManagerService.database.Delete(tableName, pkID, entity);
            }

            return DelMessage(flag);
        }
        #endregion


        /// <summary>
        /// 可以考虑把传入类型值和类型 switch判断
        /// </summary>

        #region 检查用户类型是否存在
        [HttpPost]
        public ActionResult CheckUserTypeIsExist(string uLoginTypeValue)
        {
            T_UserType temp = HRAManagerService.UserTypeIsExist(uLoginTypeValue);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_USERTYPE_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

        #region 检查下载类型是否存在
        [HttpPost]
        public ActionResult CheckDMTypeIsExist(string dmTypeValue)
        {
            T_DMType temp = HRAManagerService.DownloadTypeIsExist(dmTypeValue);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_DMTYPE_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

        #region 检查发布类型是否存在
        [HttpPost]
        public ActionResult CheckPMTypeIsExist(string pmTypeValue)
       {
           T_PMType temp = HRAManagerService.PublishTypeIsExist(pmTypeValue);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_PMTYPE_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

        #region 检查审核结果是否存在
        [HttpPost]
        public ActionResult CheckReviewResultIsExist(string mReviewResultValue)
        {

            T_ReviewResult temp = HRAManagerService.ReviewResultIsExist(mReviewResultValue);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_REVIEW_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

        #region 检查会员类型是否存在
        [HttpPost]
        public ActionResult CheckMTypeIsExist(string mTypeValue)
        {
            T_MemberType temp = HRAManagerService.MemberTypetIsExist(mTypeValue);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_MEMBERTYPE_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

        #region 检查友情链接是否存在
        [HttpPost]
        public ActionResult CheckFLTypeIsExist(string flTypeValue)
        {
            T_FLType temp = HRAManagerService.FLTypetIsExist(flTypeValue);
            if (temp != null)
            {
                return Json(new { state = false, message = ERR_FL_EXIST });
            }
            return Json(new { state = true, message = string.Empty });
        }
        #endregion

    }




}