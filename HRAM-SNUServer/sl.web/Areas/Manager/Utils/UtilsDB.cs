using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sl.model;
using PetaPoco;

namespace sl.web
{
    /// <summary>
    /// 数据库通用方法
    /// </summary>
    public class UtilsDB
    {
        /// <summary>
        /// C#特有的单例模式写法,调用Database
        /// </summary>
        public static readonly Database DB = new Database("ConnectionString");

        #region 查询用户类型
        public static List<T_UserType> GetUserTypeList()
        {
            List<T_UserType> userTypes = new List<T_UserType>();
            Sql sql = Sql.Builder;
            sql.Select("*")
                .From("T_UserType")
                .OrderBy("A_LoginTypeID asc");
            userTypes = DB.Fetch<T_UserType>(sql);
            return userTypes;
        }
        #endregion
    }
}