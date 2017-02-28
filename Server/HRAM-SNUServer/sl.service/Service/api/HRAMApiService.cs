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

            //Left join去重 记得U_LoginName和PM_TypeID唯一性
            sql.Append("select T_PMType.PM_TypeValue,T_User.U_UserName,T_PublishManage.* ");
            sql.Append("from T_PublishManage left join T_PMType on T_PublishManage.PM_TypeID = T_PMType.PM_TypeID ");
            sql.Append("left join T_User on T_User.U_LoginName = T_PublishManage.U_LoginName ");

            //List<dynamic> pulishs = UtilsDB.DB.Fetch<dynamic>(sql);

            Page<PublishInfo> list = database.Page<PublishInfo>(pageIndex, pageSize, sql);

            return list;
        }
        #endregion

        #region 下载相关
        public static Page<DownLoadInfo> GetDownloads(int pageIndex, int pageSize)
        {
            Sql sql = Sql.Builder;

            //Left join去重 记得U_LoginName和PM_TypeID唯一性
            sql.Append("select T_DMType.DM_TypeValue,T_User.U_UserName,T_DownloadManage.* ");
            sql.Append("from T_DownloadManage left join T_DMType on T_DownloadManage.DM_TypeID = T_DMType.DM_TypeID ");
            sql.Append("left join T_User on T_User.U_LoginName = T_DownloadManage.U_LoginName ");

            //List<dynamic> pulishs = UtilsDB.DB.Fetch<dynamic>(sql);

            Page<DownLoadInfo> list = database.Page<DownLoadInfo>(pageIndex, pageSize, sql);

            return list;
        }
        #endregion

    }
}