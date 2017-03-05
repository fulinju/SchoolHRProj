using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using PetaPoco;
using sl.model;

namespace sl.service.api
{
    public class HRAMApiService
    {
        /// <summary>
        /// C#特有的单例模式写法,调用Database
        /// </summary>
        public static readonly Database database = new Database("ConnectionString");

        #region 文章相关
        public static Page<PublishInfo> GetPublishs(int pageIndex, int pageSize)
        {
            Sql sql = Sql.Builder;

            //Left join去重 记得uLoginName和pmTypeID唯一性
            sql.Append("select T_PMType.pmTypeValue,T_User.uUserName,T_PublishManage.* ");
            sql.Append("from T_PublishManage left join T_PMType on T_PublishManage.pmTypeID = T_PMType.pmTypeID ");
            sql.Append("left join T_User on T_User.uLoginName = T_PublishManage.uLoginName ");

            //List<dynamic> pulishs = UtilsDB.DB.Fetch<dynamic>(sql);

            Page<PublishInfo> list = database.Page<PublishInfo>(pageIndex, pageSize, sql);

            return list;
        }
        #endregion

        #region 下载相关
        public static Page<DownLoadInfo> GetDownloads(int pageIndex, int pageSize)
        {
            Sql sql = Sql.Builder;

            //Left join去重 记得uLoginName和pmTypeID唯一性
            sql.Append("select T_DMType.dmTypeValue,T_User.uUserName,T_DownloadManage.* ");
            sql.Append("from T_DownloadManage left join T_DMType on T_DownloadManage.dmTypeID = T_DMType.dmTypeID ");
            sql.Append("left join T_User on T_User.uLoginName = T_DownloadManage.uLoginName ");

            //List<dynamic> pulishs = UtilsDB.DB.Fetch<dynamic>(sql);

            Page<DownLoadInfo> list = database.Page<DownLoadInfo>(pageIndex, pageSize, sql);

            return list;
        }
        #endregion

        #region 账户相关

        /// <summary>
        /// 根据邮箱获取用户
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static T_User GetUserByMail(string uMaiBox)
        {
            Sql sql = Sql.Builder;
            sql.Append("select * from T_User where uMaiBox = @0", uMaiBox);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }

        public static T_User GetUserByMailAndPwd(string uMaiBox, string uPassword)
        {
            Sql sql = Sql.Builder;
            sql.Append("select * from T_User where uMaiBox = @0 and uPassword", uMaiBox, uPassword);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }

        /// <summary>
        /// 用邮箱注册
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static object RegisterUserByMail(string mail,string pwdMD5)
        {
           T_User user =new T_User();
           user.uMaiBox = mail;
           user.uPassword = pwdMD5;
           user.uLoginTypeID = FinalData.TypeUserID;
           object id = database.Insert(user);
           return id;
        }

        /// <summary>
        /// 通过用户名获取用户
        /// </summary>
        /// <param name="uLoginName"></param>
        /// <returns></returns>
        public static T_User GetUserByLoginName(string uLoginName)
        {
            Sql sql = Sql.Builder;
            sql.Append("select * from T_User where uLoginName = @0", uLoginName);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }

        public static T_User GetUserByLoginNameAndPwd(string uLoginName, string uPassword)
        {
            Sql sql = Sql.Builder;
            sql.Append("select * from T_User where uLoginName = @0 and uPassword = @1", uLoginName, uPassword);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }

        /// <summary>
        /// 用户名注册
        /// </summary>
        /// <param name="uLoginName"></param>
        /// <param name="pwdMD5"></param>
        /// <returns></returns>
        public static object RegisterUserByLoginName(string uLoginName, string pwdMD5)
        {
            T_User user = new T_User();
            user.uLoginName = uLoginName;
            user.uPassword = pwdMD5;
            user.uLoginTypeID = FinalData.TypeUserID;
            object id = database.Insert(user);
            return id;
        }

        #endregion

    }
}