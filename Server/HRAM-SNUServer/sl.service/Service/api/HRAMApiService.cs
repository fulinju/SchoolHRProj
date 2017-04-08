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
            sql.Append("Select T_PMType.pmTypeValue,T_User.uUserName,");
            sql.Append("T_PublishManage.pkId,");
            sql.Append("T_PublishManage.pmTypeID,");
            sql.Append("T_PublishManage.uLoginName,");
            sql.Append("SUBSTRING(REPLACE(CAST(T_PublishManage.pmTitle as nvarchar(4000)),' ',''),0,20)+'...'  as pmTitle,");
            sql.Append("T_PublishManage.pmADImgListID,");
            sql.Append("T_PublishManage.pmPublishTime,");
            sql.Append("SUBSTRING(REPLACE(CAST(T_PublishManage.pmText as nvarchar(4000)),' ',''),0,50)+'...'  as pmText,"); //文章内容太长
            sql.Append("T_PublishManage.pmViews,");
            sql.Append("T_PublishManage.pmPreview");
            sql.Append(" from T_PublishManage left join T_PMType on T_PublishManage.pmTypeID = T_PMType.pmTypeID ");
            sql.Append(" left join T_User on T_User.uLoginName = T_PublishManage.uLoginName ");
            //sql.Append(" left join T_ADImgList on T_ADImgList.pmADImgListID = T_PublishManage.pmADImgListID  ");
            sql.Append(" where T_PublishManage.isDeleted = 0 ");
            //            List<PublishInfo> list = database.Fetch<PublishInfo>(pageIndex, pageSize, sql);
            Page<PublishInfo> list = database.Page<PublishInfo>(pageIndex, pageSize, sql);

            for (int i = 0; i < list.Items.Count; i++)
            {
                if (list.Items[i].pmADImgListID == null)
                {
                    list.Items[i].pmADImgList = (new List<ADImgInfo>()); //空列表
                }
                else
                {
                    if (GetAdImgs(list.Items[i].pmADImgListID) == null)
                    {
                        list.Items[i].pmADImgList = (new List<ADImgInfo>()); //空列表
                    }
                    else
                    {
                        list.Items[i].pmADImgList = (GetAdImgs(list.Items[i].pmADImgListID));

                    }
                }
            }

            return list;
        }
        #endregion

        #region 获取文章详情
        //public static PublishInfo GetPublishDetail(int pageIndex, int pageSize)
        //{
        //    Sql sql = Sql.Builder;

        //    //Left join去重 记得uLoginName和pmTypeID唯一性
        //    sql.Append("Select T_PMType.pmTypeValue,T_User.uUserName,");
        //    sql.Append("T_PublishManage.pkId,");
        //    sql.Append("T_PublishManage.pmTypeID,");
        //    sql.Append("T_PublishManage.uLoginName,");
        //    sql.Append("SUBSTRING(REPLACE(CAST(T_PublishManage.pmTitle as nvarchar(4000)),' ',''),0,20)+'...'  as pmTitle,");
        //    sql.Append("T_PublishManage.pmADImgListID,");
        //    sql.Append("T_PublishManage.pmPublishTime,");
        //    sql.Append("SUBSTRING(REPLACE(CAST(T_PublishManage.pmText as nvarchar(4000)),' ',''),0,50)+'...'  as pmText,"); //文章内容太长
        //    sql.Append("T_PublishManage.pmViews,");
        //    sql.Append("T_PublishManage.pmPreview");
        //    sql.Append(" from T_PublishManage left join T_PMType on T_PublishManage.pmTypeID = T_PMType.pmTypeID ");
        //    sql.Append(" left join T_User on T_User.uLoginName = T_PublishManage.uLoginName ");
        //    //sql.Append(" left join T_ADImgList on T_ADImgList.pmADImgListID = T_PublishManage.pmADImgListID  ");
        //    sql.Append(" where T_PublishManage.isDeleted = 0 ");
        //    //            List<PublishInfo> list = database.Fetch<PublishInfo>(pageIndex, pageSize, sql);
        //    Page<PublishInfo> list = database.Page<PublishInfo>(pageIndex, pageSize, sql);

        //    for (int i = 0; i < list.Items.Count; i++)
        //    {
        //        if (list.Items[i].pmADImgListID == null)
        //        {
        //            list.Items[i].pmADImgList = (new List<ADImgInfo>()); //空列表
        //        }
        //        else
        //        {
        //            if (GetAdImgs(list.Items[i].pmADImgListID) == null)
        //            {
        //                list.Items[i].pmADImgList = (new List<ADImgInfo>()); //空列表
        //            }
        //            else
        //            {
        //                list.Items[i].pmADImgList = (GetAdImgs(list.Items[i].pmADImgListID));

        //            }
        //        }
        //    }

        //    return list;
        //}
        #endregion

        public static List<ADImgInfo> GetAdImgs(string pmADImgListID)
        {
            Sql sql = Sql.Builder;
            sql.Select("*");
            sql.From("T_ADImgList");
            sql.Where("pmADImgListID= @0 and isDeleted = 0", pmADImgListID);
            sql.OrderBy("pmADImgListNum");
            List<ADImgInfo> list = database.Fetch<ADImgInfo>(sql);
            return list;
        }

        #region 会员相关
        public static Page<MemberInfo> GetMembers(int pageIndex, int pageSize)
        {
            Sql sql = Sql.Builder;

            //Left join去重 记得uLoginName和pmTypeID唯一性
            sql.Append("select T_PMType.pmTypeValue,T_User.uUserName,T_PublishManage.* ");
            sql.Append("from T_PublishManage left join T_PMType on T_PublishManage.pmTypeID = T_PMType.pmTypeID ");
            sql.Append("left join T_User on T_User.uLoginName = T_PublishManage.uLoginName ");

            //List<dynamic> pulishs = UtilsDB.DB.Fetch<dynamic>(sql);

            Page<MemberInfo> list = database.Page<MemberInfo>(pageIndex, pageSize, sql);

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
            sql.Append("select * from T_User where uMaiBox = @0 and uPassword = @1", uMaiBox, uPassword);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }

        /// <summary>
        /// 用邮箱注册
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static object RegisterUserByMail(string mail, string pwdMD5)
        {
            T_User user = new T_User();
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