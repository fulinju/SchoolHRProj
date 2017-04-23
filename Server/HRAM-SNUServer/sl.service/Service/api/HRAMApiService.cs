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
            sql.Append(" where T_PublishManage.pmTypeID like @0 ",pmTypeID);
            sql.Append(" and T_PublishManage.isDeleted = 0 ");
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


        #region 友情链接相关
        public static Page<FriendlyLinkInfo> GetFriendlyLinks(int pageIndex, int pageSize)
        {
            Sql sql = Sql.Builder;
           
            sql.Select("T_User.uUserName,T_FLType.flTypeValue,T_FriendlyLink.pkId as friendlyLinkID,T_FriendlyLink.*");
            sql.Append(" from T_FriendlyLink left join T_User on T_User.uLoginName = T_FriendlyLink.uLoginName ");
            sql.Append(" left join T_FLType on T_FriendlyLink.flTypeID = T_FLType.flTypeID ");
            sql.Where("T_FriendlyLink.isDeleted = 0");

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
            sql.Where("isDeleted = 0 and flTypeID != '@0'" ,ConstantData.TYPE_DEFALUT_ID);
            List<T_FLType> list = database.Fetch<T_FLType>(sql);
            return list;
        }

        #endregion


    }
}