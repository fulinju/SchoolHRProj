using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using PetaPoco;
using sl.model;

namespace sl.service.manager
{
    public class HRAManagerService
    {
        /// <summary>
        /// 单例模式DB
        /// </summary>
        public static readonly Database database = new Database("ConnectionString");

        #region 用户相关

        #region 获取用户列表
        public static Sql GetUserSql(string uLoginName)
        {
            Sql sql = Sql.Builder;

            uLoginName = "%" + uLoginName + "%";

            sql.Append("Select T_UserType.uLoginTypeValue,");
            sql.Append("T_User.pkId,");
            sql.Append("T_User.uLoginName,");
            sql.Append("T_User.uLoginTypeID,"); //不能少，重置的时候要用
            sql.Append("T_User.uPassword,");
            sql.Append("T_User.uUserName,");
            sql.Append("T_User.uPhone,");
            sql.Append("T_User.uMaiBox");
            sql.Append(" from T_User,T_UserType where uLoginName Like @0 and T_User.isDeleted = 0", uLoginName);
            sql.Append(" and T_UserType.uLoginTypeID = T_User.uLoginTypeID");
            return sql;
        }

        #endregion

        #region 检查用户是否存在
        public static T_User CheckUserExist(string uLoginName)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_User where uLoginName = @0", uLoginName);
            return database.FirstOrDefault<T_User>(sql);
        }
        #endregion

        #region 用户登录
        public static T_User CheckLogin(string name, string passwordMd5)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_User where uLoginName=@0 and uPassword = @1", name, passwordMd5);

            return database.FirstOrDefault<T_User>(sql);
        }

        #endregion

        #endregion

        #region 会员相关
        #region 获取会员列表
        public static Sql GetMemberSql(string mName, string mReviewResultID)
        {
            Sql sql = Sql.Builder;

            mName = "%" + mName + "%";
            mReviewResultID = "%" + mReviewResultID + "%";
            sql.Append("Select T_ReviewResult.mReviewResultValue,T_MemberType.mTypeValue,");
            sql.Append("T_Member.pkId,");
            sql.Append("T_Member.uLoginName,");
            sql.Append("T_Member.mReviewResultID,");
            sql.Append("T_Member.mTypeID,");
            sql.Append("T_Member.mApplyTime,");
            sql.Append("T_Member.mName,");
            sql.Append("T_Member.mOrganizationCode,");
            sql.Append("T_Member.mAddress,");
            sql.Append("T_Member.mCorporateName,");
            sql.Append("T_Member.mIDCardNo,");
            sql.Append("T_Member.mContacts,");
            sql.Append("T_Member.mContactsphone,");
            sql.Append("T_Member.mImgURL,");
            sql.Append("T_Member.mURL");
            sql.Append(" from T_Member,T_ReviewResult,T_MemberType where T_Member.mName Like @0 and T_Member.mReviewResultID Like @1 and T_Member.isDeleted = 0", mName, mReviewResultID);
            sql.Append(" and T_Member.mReviewResultID = T_ReviewResult.mReviewResultID and T_Member.mTypeID = T_MemberType.mTypeID");
            return sql;
        }


        #region 检查会员是否存在
        public static T_Member CheckMemberExist(string id)
        {
            Sql sql = Sql.Builder.Append("Select * from T_Member Where pkId = @0", id);
            return database.FirstOrDefault<T_Member>(sql);
        }

        #region 检查用户是否存在
        public static T_Member CheckMemberNameExist(string mName)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_Member where mName = @0", mName);
            return database.FirstOrDefault<T_Member>(sql);
        }
        #endregion
        #endregion

        #endregion
        #endregion

        #region 发布相关
        public static Sql GetPublishSql(string uLoginName)
        {
            Sql sql = Sql.Builder;

            uLoginName = "%" + uLoginName + "%";

            sql.Append("Select T_PMType.pmTypeValue,");
            sql.Append("T_PublishManage.pkId,");
            sql.Append("T_PublishManage.pmTypeID,");
            sql.Append("T_PublishManage.uLoginName,");
            sql.Append("SUBSTRING(REPLACE(CAST(T_PublishManage.pmTitle as nvarchar(4000)),' ',''),0,20)+'...'  as pmTitle,");
            sql.Append("T_PublishManage.pmADImgListID,");
            sql.Append("T_PublishManage.pmPublishTime,");
            //sql.Append("SUBSTRING(REPLACE(CAST(T_PublishManage.pmText as nvarchar(4000)),' ',''),0,20)+'...'  as pmText,"); //文章内容太长
            sql.Append("T_PublishManage.pmViews,");
            sql.Append("T_PublishManage.pmPreview");
            sql.Append(" from T_PMType,T_PublishManage where uLoginName Like @0 and T_PublishManage.isDeleted = 0", uLoginName);
            sql.Append(" and T_PMType.pmTypeID = T_PublishManage.pmTypeID");
            return sql;
        }

        /// <summary>
        /// 导出前N条 默认全部
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static List<PublishExportInfo> ExportTopPublish(int num = -1)
        {
            Sql sql = Sql.Builder;
            if (num == -1)
            {
                sql.Append("Select T_PMType.pmTypeValue,");
                sql.Append(" T_User.uUserName,");
                sql.Append("T_PublishManage.uLoginName,");
                sql.Append("T_PublishManage.pmTitle,");
                sql.Append("T_PublishManage.pmADImgListID,");
                sql.Append("T_PublishManage.pmPublishTime,");
                sql.Append("T_PublishManage.pmText,");
                sql.Append("T_PublishManage.pmViews,");
                sql.Append("T_PublishManage.pmPreview");
                sql.Append(" from T_PMType,T_PublishManage,T_User where T_PublishManage.isDeleted = 0");
                sql.Append(" and T_PMType.pmTypeID = T_PublishManage.pmTypeID");
                sql.Append(" and T_User.uLoginName = T_PublishManage.uLoginName");

            }
            else
            {
                sql.Append("Select top " + num);
                sql.Append(" T_PMType.pmTypeValue,");
                sql.Append(" T_User.uUserName,");
                sql.Append("T_PublishManage.uLoginName,");
                sql.Append("T_PublishManage.pmTitle,");
                sql.Append("T_PublishManage.pmADImgListID,");
                sql.Append("T_PublishManage.pmPublishTime,");
                sql.Append("T_PublishManage.pmText,");
                sql.Append("T_PublishManage.pmViews,");
                sql.Append("T_PublishManage.pmPreview");
                sql.Append(" from T_PMType,T_PublishManage,T_User where T_PublishManage.isDeleted = 0");
                sql.Append(" and T_PMType.pmTypeID = T_PublishManage.pmTypeID");
                sql.Append(" and T_User.uLoginName = T_PublishManage.uLoginName");
            }

            List<PublishExportInfo> list = database.Fetch<PublishExportInfo>(sql);
            return list;
        }

        public static List<PublishExportInfo> ExportSelectedPublishByID(string pkId)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select T_PMType.pmTypeValue,");
            sql.Append(" T_User.uUserName,");
            sql.Append("T_PublishManage.uLoginName,");
            sql.Append("T_PublishManage.pmTitle,");
            sql.Append("T_PublishManage.pmADImgListID,");
            sql.Append("T_PublishManage.pmPublishTime,");
            sql.Append("T_PublishManage.pmText,");
            sql.Append("T_PublishManage.pmViews,");
            sql.Append("T_PublishManage.pmPreview");
            sql.Append(" from T_PMType,T_PublishManage,T_User where T_PublishManage.isDeleted = 0");
            sql.Append(" and T_PMType.pmTypeID = T_PublishManage.pmTypeID");
            sql.Append(" and T_User.uLoginName = T_PublishManage.uLoginName");
            sql.Append(" and T_PublishManage.pkId = @0", pkId);
            return database.Fetch<PublishExportInfo>(sql);
        }

        public static List<T_ADImgList> GetPublishADIMGS(string pmADImgListID)
        {
            Sql sql = Sql.Builder;
            sql.Select("*");
            sql.From("T_ADImgList");
            sql.Where("pmADImgListID = @0", pmADImgListID);
            return database.Fetch<T_ADImgList>(sql);

        }

        #endregion

        #region 下载相关

        #region 获取下载列表Sql
        public static Sql GetDownloadSql(string dmTitle)
        {
            Sql sql = Sql.Builder;

            dmTitle = "%" + dmTitle + "%";

            sql.Append("Select T_DMType.dmTypeValue,");
            sql.Append("T_DownloadManage.pkId,");
            sql.Append("T_DownloadManage.dmTypeID,");
            sql.Append("T_DownloadManage.uLoginName,");
            sql.Append("T_DownloadManage.dmTitle,");
            sql.Append("T_DownloadManage.dmFileURL,");
            sql.Append("T_DownloadManage.dmDownloadNum,");
            sql.Append("T_DownloadManage.dmUploadTime");
            sql.Append(" from T_DMType,T_DownloadManage where uLoginName Like @0 and T_DownloadManage.isDeleted = 0", dmTitle);
            sql.Append(" and T_DMType.dmTypeID = T_DownloadManage.dmTypeID");
            return sql;
        }
        #endregion

        #region 根据pkId获取单个下载
        public static Sql GetDownloadByIDSql(string id)
        {
            Sql sql = Sql.Builder.Append("Select * from T_DownloadManage Where pkId = @0", id);
            return sql;
        }
        #endregion

        #region 根据typeID获取类型value
        public static string GetDownloadValueByID(string dmTypeID)
        {
            Sql sql = Sql.Builder.Append("Select * from T_DMType Where dmTypeID = @0", dmTypeID);
            T_DMType temp = database.FirstOrDefault<T_DMType>(sql);
            return temp.dmTypeValue;
        }
        #endregion

        #endregion


        #region 友情链接相关
        public static Sql GetLinksSql(string flName)
        {
            Sql sql = Sql.Builder;
            flName = "%" + flName + "%";
            sql.Select("T_FriendlyLink.pkId,T_FriendlyLink.uLoginName,"
                + "T_FriendlyLink.flName,T_FriendlyLink.flURL,"
                + "T_FriendlyLink.flImgURL,T_FriendlyLink.flAddTime,"
                 + "T_FriendlyLink.flTypeID,T_FLType.flTypeValue");
            sql.From("T_FriendlyLink,T_FLType");
            sql.Where("flName Like @0 and T_FriendlyLink.flTypeID = T_FLType.flTypeID and T_FriendlyLink.isDeleted = 0", flName);
            return sql;
        }
        #endregion

        #region 类型管理相关

        #region 查询用户类型
        public static List<T_UserType> GetUserTypeList()
        {
            List<T_UserType> userTypes = new List<T_UserType>();
            Sql sql = Sql.Builder;
            sql.Select("*")
                .From("T_UserType")
                .Where("isDeleted = 0")
                .OrderBy("uLoginTypeValue asc");
            userTypes = database.Fetch<T_UserType>(sql);
            return userTypes;
        }

        public static Sql GetUserTypeSql(string uLoginTypeValue)
        {
            Sql sql = Sql.Builder;

            uLoginTypeValue = "%" + uLoginTypeValue + "%";
            sql.Append("Select * from T_UserType where uLoginTypeValue Like @0 and isDeleted = 0", uLoginTypeValue);
            return sql;
        }


        #endregion

        #region 查询会员审核结果
        public static List<T_ReviewResult> GetReviewResults()
        {
            List<T_ReviewResult> reviewResults = new List<T_ReviewResult>();
            Sql sql = Sql.Builder;
            sql.Select("*")
                .From("T_ReviewResult")
                .Where("isDeleted = 0")
                .OrderBy("mReviewResultID asc");
            reviewResults = database.Fetch<T_ReviewResult>(sql);
            return reviewResults;
        }


        public static Sql GetReviewResultSql(string reviewResultValue)
        {
            Sql sql = Sql.Builder;
            reviewResultValue = "%" + reviewResultValue + "%";
            sql.Append("Select * from T_ReviewResult where mReviewResultValue Like @0 and isDeleted = 0", reviewResultValue);
            return sql;
        }
        #endregion

        #region 查询会员类型
        public static List<T_MemberType> GetMemberTypes()
        {
            List<T_MemberType> reviewResults = new List<T_MemberType>();
            Sql sql = Sql.Builder;
            sql.Select("*")
                .From("T_MemberType")
                .Where("isDeleted = 0")
                .OrderBy("mTypeID asc");
            reviewResults = database.Fetch<T_MemberType>(sql);
            return reviewResults;
        }

        public static Sql GetMemberTypeSql(string mTypeValue)
        {
            Sql sql = Sql.Builder;
            mTypeValue = "%" + mTypeValue + "%";
            sql.Append("Select * from T_MemberType where mTypeValue Like @0 and isDeleted = 0", mTypeValue);
            return sql;
        }
        #endregion

        #region 查询下载类型
        public static List<T_DMType> GetDownloadTypeList()
        {
            List<T_DMType> types = new List<T_DMType>();
            Sql sql = Sql.Builder;
            sql.Select("*")
                .From("T_DMType")
                .Where("isDeleted = 0")
                .OrderBy("dmTypeValue asc");
            types = database.Fetch<T_DMType>(sql);
            return types;
        }

        public static Sql GetDownloadTypeSql(string dmTypeValue)
        {
            Sql sql = Sql.Builder;
            dmTypeValue = "%" + dmTypeValue + "%";
            sql.Append("Select * from T_DMType where dmTypeValue Like @0 and isDeleted = 0", dmTypeValue);
            return sql;
        }
        #endregion

        #region 查询发布类型
        public static List<T_PMType> GetPublishTypes()
        {
            List<T_PMType> publishTypes = new List<T_PMType>();
            Sql sql = Sql.Builder;
            sql.Select("*")
                .From("T_PMType")
                .Where("isDeleted = 0")
                .OrderBy("pmTypeID asc");
            publishTypes = database.Fetch<T_PMType>(sql);
            return publishTypes;
        }

        public static Sql GetPublishTypeSql(string pmTypeValue)
        {
            Sql sql = Sql.Builder;
            pmTypeValue = "%" + pmTypeValue + "%";
            sql.Append("Select * from T_PMType where pmTypeValue Like @0 and isDeleted = 0", pmTypeValue);
            return sql;
        }
        #endregion

        #region 查询友情链接类型

        public static List<T_FLType> GetFLTypeList()
        {
            List<T_FLType> flTypes = new List<T_FLType>();
            Sql sql = Sql.Builder;
            sql.Select("*")
                .From("T_FLType")
                .Where("isDeleted = 0")
                .OrderBy("flTypeValue asc");
            flTypes = database.Fetch<T_FLType>(sql);
            return flTypes;
        }

        public static Sql GetFLTypeSql(string flTypeValue)
        {
            Sql sql = Sql.Builder;
            flTypeValue = "%" + flTypeValue + "%";
            sql.Append("Select * from T_FLType where flTypeValue Like @0 and isDeleted = 0", flTypeValue);
            return sql;
        }
        #endregion


        #region 是否存在于表中
        public static int ExistInTable(string tableName, string selectRow, string selectRowValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select count(*) from " + tableName + " where " + selectRow + "=@0", selectRowValue);

            return database.FirstOrDefault<int>(sql);
        }
        #endregion


        #region 通用的删除方法
        public static void CommonDelete(string refTableName, string refSelectRow, string typeNoID, string value)
        {
            Sql sql = Sql.Builder;
            sql.Append("UPDATE " + refTableName + " SET " + refSelectRow + " =@0 WHERE " + refSelectRow + " = @1", typeNoID, value);
            database.Execute(sql);
        }
        #endregion

        #region 检查用户类型是否存在
        public static T_UserType UserTypeIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_UserType where uLoginTypeValue = @0", typeValue);
            T_UserType result = database.FirstOrDefault<T_UserType>(sql);
            return result;
        }
        #endregion

        #region 检查下载类型是否存在
        public static T_DMType DownloadTypeIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_DMType where dmTypeValue = @0", typeValue);
            T_DMType result = database.FirstOrDefault<T_DMType>(sql);
            return result;
        }
        #endregion

        #region 检查发布类型是否存在
        public static T_PMType PublishTypeIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_PMType where pmTypeValue = @0", typeValue);
            T_PMType result = database.FirstOrDefault<T_PMType>(sql);
            return result;
        }
        #endregion

        #region 检查审核结果是否存在
        public static T_ReviewResult ReviewResultIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_ReviewResult where mReviewResultValue = @0", typeValue);
            T_ReviewResult result = database.FirstOrDefault<T_ReviewResult>(sql);
            return result;
        }
        #endregion

        #region 检查会员类型是否存在
        public static T_MemberType MemberTypetIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_MemberType where mTypeValue = @0", typeValue);
            T_MemberType result = database.FirstOrDefault<T_MemberType>(sql);
            return result;
        }
        #endregion

        #region 检查友情链接类型是否存在
        public static T_FLType FLTypetIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_FLType where flTypeValue = @0", typeValue);
            T_FLType result = database.FirstOrDefault<T_FLType>(sql);
            return result;
        }
        #endregion

        #endregion

    }
}