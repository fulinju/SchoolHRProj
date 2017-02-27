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
        public static Sql GetUserSql(string u_loginname)
        {
            Sql sql = Sql.Builder;

            u_loginname = "%" + u_loginname + "%";

            sql.Append("Select T_UserType.u_logintypevalue,");
            sql.Append("T_User.pk_id,");
            sql.Append("T_User.u_loginname,");
            sql.Append("T_User.u_logintypeid,"); //不能少，重置的时候要用
            sql.Append("T_User.u_password,");
            sql.Append("T_User.u_username,");
            sql.Append("T_User.u_phone,");
            sql.Append("T_User.u_maibox");
            sql.Append(" from T_User,T_UserType where U_LoginName Like @0 and T_User.IsDeleted = 0", u_loginname);
            sql.Append(" and T_UserType.U_LoginTypeID = T_User.U_LoginTypeID");
            return sql;
        }
        #endregion

        #region 检查用户是否存在
        public static T_User CheckUserExist(string u_loginname)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_User where U_LoginName = @0", u_loginname);
            return database.FirstOrDefault<T_User>(sql);
        }
        #endregion

        #region 用户登录
        public static T_User CheckLogin(string name, string passwordMd5)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_User where U_LoginName=@0 and U_Password = @1", name, passwordMd5);

            return database.FirstOrDefault<T_User>(sql);
        }

        #endregion

        #endregion

        #region 会员
        #region 获取会员列表
        public static Sql GetMemberSql(string m_name)
        {
            Sql sql = Sql.Builder;

            m_name = "%" + m_name + "%";
            sql.Append("Select T_ReviewResult.m_reviewresultvalue,T_MemberType.m_typevalue,");
            sql.Append("T_Member.pk_id,");
            sql.Append("T_Member.u_loginname,");
            sql.Append("T_Member.m_reviewresultid,");
            sql.Append("T_Member.m_typeid,");
            sql.Append("T_Member.m_applytime,");
            sql.Append("T_Member.m_name,");
            sql.Append("T_Member.m_organizationcode,");
            sql.Append("T_Member.m_address,");
            sql.Append("T_Member.m_corporatename,");
            sql.Append("T_Member.m_idcardno,");
            sql.Append("T_Member.m_contacts,");
            sql.Append("T_Member.m_contactsphone,");
            sql.Append("T_Member.m_imgurl,");
            sql.Append("T_Member.m_url");
            sql.Append(" from T_Member,T_ReviewResult,T_MemberType where M_Name Like @0 and T_Member.IsDeleted = 0", m_name);
            sql.Append(" and T_Member.M_ReviewResultID = T_ReviewResult.M_ReviewResultID and T_Member.M_TypeID = T_MemberType.M_TypeID");
            return sql;
        }


        #region 检查会员是否存在
        public static T_Member CheckMemberExist(string id)
        {
            Sql sql = Sql.Builder.Append("Select * from T_Member Where pk_id = @0", id);
            return database.FirstOrDefault<T_Member>(sql);
        }
        #endregion

        #endregion
        #endregion

        #region 发布相关
        public static Sql GetPublishSql(string u_loginname)
        {
            Sql sql = Sql.Builder;

            u_loginname = "%" + u_loginname + "%";

            sql.Append("Select T_PMType.pm_typevalue,");
            sql.Append("T_PublishManage.pk_id,");
            sql.Append("T_PublishManage.pm_typeid,");
            sql.Append("T_PublishManage.u_loginname,");
            sql.Append("SUBSTRING(REPLACE(CAST(T_PublishManage.pm_title as nvarchar(4000)),' ',''),0,20)+'...'  as pm_title,");
            sql.Append("T_PublishManage.pm_adimglistid,");
            sql.Append("T_PublishManage.pm_publishtime,");
            sql.Append("SUBSTRING(REPLACE(CAST(T_PublishManage.pm_text as nvarchar(4000)),' ',''),0,20)+'...'  as pm_text,"); //文章内容太长
            sql.Append("T_PublishManage.pm_views,");
            sql.Append("T_PublishManage.pm_preview");
            sql.Append(" from T_PMType,T_PublishManage where U_LoginName Like @0 and T_PublishManage.IsDeleted = 0", u_loginname);
            sql.Append(" and T_PMType.PM_TypeID = T_PublishManage.PM_TypeID");
            return sql;
        }

        public static List<PublishInfo> GetTopPublish(int num = -1)
        {
            Sql sql = Sql.Builder;
            if (num == -1)
            {
                sql.Append("Select T_PMType.pm_typevalue,");
                sql.Append(" T_User.u_username,");
                sql.Append("T_PublishManage.u_loginname,");
                sql.Append("T_PublishManage.pm_title,");
                sql.Append("T_PublishManage.pm_adimglistid,");
                sql.Append("T_PublishManage.pm_publishtime,");
                sql.Append("T_PublishManage.pm_text,");
                sql.Append("T_PublishManage.pm_views,");
                sql.Append("T_PublishManage.pm_preview");
                sql.Append(" from T_PMType,T_PublishManage,T_User where T_PublishManage.IsDeleted = 0");
                sql.Append(" and T_PMType.PM_TypeID = T_PublishManage.PM_TypeID");
                sql.Append(" and T_User.U_LoginName = T_PublishManage.U_LoginName");

            }
            else
            {
                sql.Append("Select top " + num);
                sql.Append(" T_PMType.pm_typevalue,");
                sql.Append(" T_User.u_username,");
                sql.Append("T_PublishManage.u_loginname,");
                sql.Append("T_PublishManage.pm_title,");
                sql.Append("T_PublishManage.pm_adimglistid,");
                sql.Append("T_PublishManage.pm_publishtime,");
                sql.Append("T_PublishManage.pm_text,");
                sql.Append("T_PublishManage.pm_views,");
                sql.Append("T_PublishManage.pm_preview");
                sql.Append(" from T_PMType,T_PublishManage,T_User where T_PublishManage.IsDeleted = 0");
                sql.Append(" and T_PMType.PM_TypeID = T_PublishManage.PM_TypeID");
                sql.Append(" and T_User.U_LoginName = T_PublishManage.U_LoginName");
            }

            List<PublishInfo> list = database.Fetch<PublishInfo>(sql);
            return list;
        }



        #endregion

        #region 下载相关

        #region 获取下载列表Sql
        public static Sql GetDownloadSql(string dm_title)
        {
            Sql sql = Sql.Builder;

            dm_title = "%" + dm_title + "%";

            sql.Append("Select T_DMType.dm_typevalue,");
            sql.Append("T_DownloadManage.pk_id,");
            sql.Append("T_DownloadManage.dm_typeid,");
            sql.Append("T_DownloadManage.u_loginname,");
            sql.Append("T_DownloadManage.dm_title,");
            sql.Append("T_DownloadManage.dm_fileurl,");
            sql.Append("T_DownloadManage.dm_downloadnum,");
            sql.Append("T_DownloadManage.dm_uploadtime");
            sql.Append(" from T_DMType,T_DownloadManage where U_LoginName Like @0 and T_DownloadManage.IsDeleted = 0", dm_title);
            sql.Append(" and T_DMType.DM_TypeID = T_DownloadManage.DM_TypeID");
            return sql;
        }
        #endregion

        #region 根据pk_id获取单个下载
        public static Sql GetDownloadByIDSql(string id)
        {
            Sql sql = Sql.Builder.Append("Select * from T_DownloadManage Where pk_id = @0", id);
            return sql;
        }
        #endregion

        #region 根据typeID获取类型value
        public static string GetDownloadValueByID(string dm_typeid)
        {
            Sql sql = Sql.Builder.Append("Select * from T_DMType Where DM_TypeID = @0", dm_typeid);
            T_DMType temp = database.FirstOrDefault<T_DMType>(sql);
            return temp.DM_TypeValue;
        }
        #endregion

        #endregion


        #region 友情链接相关
        public static Sql GetLinksSql(string fl_name)
        {
            Sql sql = Sql.Builder;
            fl_name = "%" + fl_name + "%";
            sql.Append("select * from T_FriendlyLink where FL_Name Like @0 and IsDeleted = 0", fl_name);
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
                .OrderBy("U_LoginTypeID asc");
            userTypes = database.Fetch<T_UserType>(sql);
            return userTypes;
        }

        public static Sql GetUserTypeSql(string u_logintypevalue)
        {
            Sql sql = Sql.Builder;

            u_logintypevalue = "%" + u_logintypevalue + "%";
            sql.Append("Select * from T_UserType where U_LoginTypeValue Like @0 and IsDeleted = 0", u_logintypevalue);
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
                .OrderBy("M_ReviewResultID asc");
            reviewResults = database.Fetch<T_ReviewResult>(sql);
            return reviewResults;
        }


        public static Sql GetReviewResultSql(string reviewResultValue)
        {
            Sql sql = Sql.Builder;
            reviewResultValue = "%" + reviewResultValue + "%";
            sql.Append("Select * from T_ReviewResult where M_ReviewResultValue Like @0 and IsDeleted = 0", reviewResultValue);
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
                .OrderBy("M_TypeID asc");
            reviewResults = database.Fetch<T_MemberType>(sql);
            return reviewResults;
        }

        public static Sql GetMemberTypeSql(string mTypeValue)
        {
            Sql sql = Sql.Builder;
            mTypeValue = "%" + mTypeValue + "%";
            sql.Append("Select * from T_MemberType where M_TypeValue Like @0 and IsDeleted = 0", mTypeValue);
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
                .OrderBy("DM_TypeValue asc");
            types = database.Fetch<T_DMType>(sql);
            return types;
        }

        public static Sql GetDownloadTypeSql(string dm_typevalue)
        {
            Sql sql = Sql.Builder;
            dm_typevalue = "%" + dm_typevalue + "%";
            sql.Append("Select * from T_DMType where DM_TypeValue Like @0 and IsDeleted = 0", dm_typevalue);
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
                .OrderBy("PM_TypeID asc");
            publishTypes = database.Fetch<T_PMType>(sql);
            return publishTypes;
        }

        public static Sql GetPublishTypeSql(string pm_typevalue)
        {
            Sql sql = Sql.Builder;
            pm_typevalue = "%" + pm_typevalue + "%";
            sql.Append("Select * from T_PMType where PM_TypeValue Like @0 and IsDeleted = 0", pm_typevalue);
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
            sql.Append("Select * from T_UserType where U_LoginTypeValue = @0", typeValue);
            T_UserType result = database.FirstOrDefault<T_UserType>(sql);
            return result;
        }
        #endregion

        #region 检查下载类型是否存在
        public static T_DMType DownloadTypeIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_DMType where DM_TypeValue = @0", typeValue);
            T_DMType result = database.FirstOrDefault<T_DMType>(sql);
            return result;
        }
        #endregion

        #region 检查发布类型是否存在
        public static T_PMType PublishTypeIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_PMType where PM_TypeValue = @0", typeValue);
            T_PMType result = database.FirstOrDefault<T_PMType>(sql);
            return result;
        }
        #endregion

        #region 检查审核结果是否存在
        public static T_ReviewResult ReviewResultIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_ReviewResult where M_ReviewResultValue = @0", typeValue);
            T_ReviewResult result = database.FirstOrDefault<T_ReviewResult>(sql);
            return result;
        }
        #endregion

        #region 检查会员类型是否存在
        public static T_MemberType MemberTypetIsExist(string typeValue)
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_MemberType where M_TypeValue = @0", typeValue);
            T_MemberType result = database.FirstOrDefault<T_MemberType>(sql);
            return result;
        }
        #endregion

        #endregion

    }
}