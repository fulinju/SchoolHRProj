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

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pmTypeID"></param>
        /// <returns></returns>
        public static Page<PublishInfo> GetPublishs(int pageIndex, int pageSize, string pmTypeID)
        {
            Sql sql = Sql.Builder;

            //Left join去重 记得uLoginName和pmTypeID唯一性
            sql.Append("Select T_PMType.pmTypeValue,T_User.uUserName,");
            sql.Append("T_PublishManage.pkId as publishID,");
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
            sql.Append(" where T_PublishManage.pmTypeID like @0 ", pmTypeID);
            sql.Append(" and T_PublishManage.isDeleted = 0 ");
            sql.OrderBy("T_PublishManage.pmPublishTime desc");
            Page<PublishInfo> list = database.Page<PublishInfo>(pageIndex, pageSize, sql);
            if (list != null)
            {
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
            }

            return list;
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="publishID"></param>
        /// <returns></returns>
        public static PublishInfo GetPublishDetail(string publishID)
        {
            Sql sql = Sql.Builder;

            //因为查看了次详情 浏览+1
            Sql sql2 = Sql.Builder;
            sql2.Append("UPDATE T_PublishManage SET pmViews = (pmViews+1) WHERE pkId = @0", publishID);
            database.Execute(sql2);

            //Left join去重 记得uLoginName和pmTypeID唯一性
            sql.Select("T_PMType.pmTypeValue,T_User.uUserName,"
                + "T_PublishManage.pkId as publishID,"
                + "T_PublishManage.pmTypeID,"
                + "T_PublishManage.uLoginName,"
                + "T_PublishManage.pmTitle,"
                + "T_PublishManage.pmADImgListID,"
                + "T_PublishManage.pmPublishTime,"
                + "T_PublishManage.pmText,"
                + "T_PublishManage.pmViews,"
                + "T_PublishManage.pmPreview");

            sql.From("T_PublishManage");
            sql.Append(" left join T_PMType on T_PublishManage.pmTypeID = T_PMType.pmTypeID ");
            sql.Append(" left join T_User on T_User.uLoginName = T_PublishManage.uLoginName ");
            sql.Where("T_PublishManage.pkId = @0 and T_PublishManage.isDeleted = 0", publishID);
            PublishInfo publish = database.FirstOrDefault<PublishInfo>(sql);
            if (publish != null)
            {
                if (publish.pmADImgListID == null)
                {
                    publish.pmADImgList = (new List<ADImgInfo>()); //空列表
                }
                else
                {
                    if (GetAdImgs(publish.pmADImgListID) == null)
                    {
                        publish.pmADImgList = (new List<ADImgInfo>()); //空列表
                    }
                    else
                    {
                        publish.pmADImgList = (GetAdImgs(publish.pmADImgListID));

                    }
                }
            }

            return publish;
        }

        /// <summary>
        /// 获取广告图片
        /// </summary>
        /// <param name="pmADImgListID"></param>
        /// <returns></returns>
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
        #endregion


        #region 会员相关
        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Page<MemberInfo> GetMembers(int pageIndex, int pageSize, string mTypeID, string mReviewResultID)
        {
            Sql sql = Sql.Builder;

            sql.Append("Select T_MemberType.mTypeValue,T_ReviewResult.mReviewResultValue,T_User.uUserName,");
            sql.Append("T_Member.pkId as memberID,");
            sql.Append("T_Member.*");
            sql.Append(" from T_Member left join T_MemberType on T_Member.mTypeID = T_MemberType.mTypeID ");
            sql.Append(" left join T_ReviewResult on T_Member.mReviewResultID = T_ReviewResult.mReviewResultID ");
            sql.Append(" left join T_User on T_User.uLoginName = T_Member.uLoginName");
            sql.Append(" where T_Member.isDeleted = 0");
            sql.Append(" and T_Member.mTypeID like @0", mTypeID);
            sql.Append(" and T_Member.mReviewResultID like @0", mReviewResultID);
            sql.OrderBy("T_Member.mApplyTime desc");

            Page<MemberInfo> list = database.Page<MemberInfo>(pageIndex, pageSize, sql);

            return list;
        }

        /// <summary>
        /// 获取会员详情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static MemberInfo GetMemberDetail(String memberID)
        {
            Sql sql = Sql.Builder;

            sql.Append("Select T_MemberType.mTypeValue,T_ReviewResult.mReviewResultValue,T_User.uUserName,");
            sql.Append("T_Member.pkId as memberID,");
            sql.Append("T_Member.*");
            sql.Append(" from T_Member left join T_MemberType on T_Member.mTypeID = T_MemberType.mTypeID ");
            sql.Append(" left join T_ReviewResult on T_Member.mReviewResultID = T_ReviewResult.mReviewResultID ");
            sql.Append(" left join T_User on T_User.uLoginName = T_Member.uLoginName");
            sql.Append(" where T_Member.pkId = @0", memberID);
            sql.Append(" and T_Member.isDeleted = 0");
            sql.Append("  order by T_Member.mReviewResultID asc");

            MemberInfo member = database.FirstOrDefault<MemberInfo>(sql);

            return member;
        }
        #endregion


        #region 下载相关
        /// <summary>
        /// 获取下载列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dmTypeID"></param>
        /// <returns></returns>
        public static Page<DownLoadInfo> GetDownloads(int pageIndex, int pageSize, string dmTypeValue)
        {
            Sql sql = Sql.Builder;

            sql.Select("T_DMType.dmTypeValue,T_User.uUserName,T_DownloadManage.*,"
                + "T_DownloadManage.pkId as downloadID");
            sql.Append("from T_DownloadManage left join T_DMType on T_DownloadManage.dmTypeID = T_DMType.dmTypeID ");
            sql.Append("left join T_User on T_User.uLoginName = T_DownloadManage.uLoginName");
            sql.Append("where T_DMType.dmTypeValue like @0", dmTypeValue);
            sql.Append("and T_DownloadManage.isDeleted = 0");
            sql.OrderBy("T_DownloadManage.dmUploadTime desc");
            Page<DownLoadInfo> list = database.Page<DownLoadInfo>(pageIndex, pageSize, sql);

            return list;
        }

        /// <summary>
        /// 获得单个下载
        /// </summary>
        /// <param name="downloadID"></param>
        /// <returns></returns>
        public static T_DownloadManage GetDownload(string downloadID)
        {
            Sql sql = Sql.Builder;
            sql.Select("*");
            sql.From("T_DownloadManage");
            sql.Where("T_DownloadManage.pkId = @0", downloadID);
            T_DownloadManage target = database.FirstOrDefault<T_DownloadManage>( sql);

            return target;
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

        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="uPhone"></param>
        /// <returns></returns>
        public static T_User GetUserByPhone(string uPhone)
        {
            Sql sql = Sql.Builder;
            sql.Append("select * from T_User where uPhone = @0", uPhone);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }

        //public static LoginModel GetUserByMailAndPwd(string uMaiBox, string uPassword)
        //{
        //    Sql sql = Sql.Builder;
        //    sql.Select("uLoginName as uLoginStr,uClientKey"
        //    + ",uUserName,uPhone"
        //    + ",uMaiBox");
        //    sql.From("T_User");
        //    sql.Where(" uMaiBox = @0 and uPassword = @1", uMaiBox, uPassword);
        //    LoginModel user = database.FirstOrDefault<LoginModel>(sql);
        //    return user;
        //}

        /// <summary>
        /// 用邮箱注册
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static object RegisterUserByMail(string mail, string pwdMD5)
        {
            T_User user = new T_User();
            user.uMaiBox = mail;
            user.uLoginName = mail;
            user.uPassword = pwdMD5;
            user.uLoginTypeID = ConstantData.TYPE_DEFALUT_ID;
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
            sql.Select("*");
            sql.From("T_User");
            sql.Where("uLoginName = @0", uLoginName);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public static T_User GetUserByID(string uID)
        {
            Sql sql = Sql.Builder;
            sql.Select("*");
            sql.From("T_User");
            sql.Where("pkId = @0", uID);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }

        /// <summary>
        /// 根据ID、用户名、密码获取用户
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static T_User GetUserByIDAndLoginNameAndPassword(string uID,string loginName,string password)
        {
            Sql sql = Sql.Builder;
            sql.Select("*");
            sql.From("T_User");
            sql.Where("pkId = @0 and uLoginName = @1 and uPassword = @2", uID,loginName,password);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }

        /// <summary>
        /// 根据ID、用户名、邮箱获取用户
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="loginName"></param>
        /// <param name="uMaiBox"></param>
        /// <returns></returns>
        public static T_User GetUserByIDAndLoginNameAndMailBox(string uID, string loginName, string uMaiBox)
        {
            Sql sql = Sql.Builder;
            sql.Select("*");
            sql.From("T_User");
            sql.Where("pkId = @0 and uLoginName = @1 and uMaiBox = @2", uID, loginName, uMaiBox);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }


        public static T_User GetUserByLoginNameAndMailBox( string loginName, string uMaiBox)
        {
            Sql sql = Sql.Builder;
            sql.Select("*");
            sql.From("T_User");
            sql.Where("uLoginName = @0 and uMaiBox = @1", loginName, uMaiBox);
            T_User user = database.FirstOrDefault<T_User>(sql);
            return user;
        }


        /// <summary>
        /// 根据用户名密码获取用户
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public static UserModel GetUserByLoginNameAndPwd(LoginModel loginInfo)
        {
            Sql sql = Sql.Builder;
            sql.Select("pkId as uID,uLoginName as uLoginStr,uClientKey,uUserName,uPhone,uMaiBox");
            sql.From("T_User");
            sql.Where(" uLoginName = @0 and uPassword = @1", loginInfo.uLoginStr, loginInfo.uPassword);
            UserModel user = database.FirstOrDefault<UserModel>(sql);

            if (user != null)
            {
                UpdateClientKey(user.uID,loginInfo.uClientKey);
                user.uClientKey = loginInfo.uClientKey;
                return user;
            }

            Sql sql2 = Sql.Builder;
            sql2.Select("uLoginName as uLoginStr,uClientKey,uUserName,uPhone,uMaiBox");
            sql2.From("T_User");
            sql2.Where(" uMaiBox = @0 and uPassword = @1", loginInfo.uLoginStr, loginInfo.uPassword);
            UserModel user2 = database.FirstOrDefault<UserModel>(sql2);

            if (user2 != null)
            {
                UpdateClientKey(user2.uID, loginInfo.uClientKey);
                user2.uClientKey = loginInfo.uClientKey;
                return user2;
            }

            return null;
        }

        public static void UpdateClientKey(string uID, string uClientKey)
        {
            Sql sql = Sql.Builder;
            sql.Append("UPDATE T_User SET uClientKey = @0 WHERE pkId = @1", uClientKey,uID);
            database.Execute(sql);
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
            user.uLoginTypeID = ConstantData.TYPE_DEFALUT_ID;
            object id = database.Insert(user);
            return id;
        }

        #endregion


        #region 友情链接相关
        public static Page<FriendlyLinkInfo> GetFriendlyLinks(int pageIndex, int pageSize)
        {
            Sql sql = Sql.Builder;

            sql.Select("T_User.uUserName,T_FLType.flTypeValue,T_FriendlyLink.pkId as friendlyLinkID,T_FriendlyLink.*");
            sql.Append(" from T_FriendlyLink left join T_User on T_User.uLoginName = T_FriendlyLink.uLoginName ");
            sql.Append(" left join T_FLType on T_FriendlyLink.flTypeID = T_FLType.flTypeID ");
            sql.Where("T_FriendlyLink.isDeleted = 0");
            sql.OrderBy("T_FriendlyLink.flAddTime desc");

            Page<FriendlyLinkInfo> list = database.Page<FriendlyLinkInfo>(pageIndex, pageSize, sql);

            return list;
        }

        /// <summary>
        /// 根据ID获取友情链接
        /// </summary>
        /// <param name="flTypeID"></param>
        /// <returns></returns>
        public static List<T_FriendlyLink> GetFriendlyLinksByTypeID(string flTypeID)
        {
            Sql sql = Sql.Builder;

            sql.Select("*");
            sql.From("T_FriendlyLink");
            sql.Where("T_FriendlyLink.flTypeID = @0 and T_FriendlyLink.isDeleted = 0", flTypeID);

            List<T_FriendlyLink> list = database.Fetch<T_FriendlyLink>(sql);

            return list;
        }
        #endregion


        #region 类型相关

        /// <summary>
        /// 获取会员类型
        /// </summary>
        /// <returns></returns>
        public static List<MemberTypeInfo> GetMemberType()
        {
            Sql sql = Sql.Builder;

            sql.Append("Select T_MemberType.mTypeValue,T_ReviewResult.mReviewResultValue,");
            sql.Append("T_Member.pkId as memberID,");
            sql.Append("T_Member.*");
            sql.Append(" from T_Member left join T_MemberType on T_Member.mTypeID = T_MemberType.mTypeID ");
            sql.Append(" left join T_ReviewResult on T_Member.mReviewResultID = T_ReviewResult.mReviewResultID ");
            sql.Append(" where T_Member.isDeleted = 0");

            List<MemberTypeInfo> list = database.Fetch<MemberTypeInfo>(sql);

            return list;
        }

        /// <summary>
        /// 获取友情链接类型
        /// </summary>
        /// <returns></returns>
        public static List<T_FLType> GetFLType()
        {
            Sql sql = Sql.Builder;

            sql.Select("*");
            sql.From("T_FLType");
            sql.Where("isDeleted = 0 and flTypeID != '@0'", ConstantData.TYPE_DEFALUT_ID);
            List<T_FLType> list = database.Fetch<T_FLType>(sql);
            return list;
        }

        #endregion


    }
}