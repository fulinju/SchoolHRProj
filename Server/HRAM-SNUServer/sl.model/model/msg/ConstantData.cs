using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sl.model
{
    /// <summary>
    /// 通用数据
    /// </summary>
    public class ConstantData
    {
        public static int ErrorCode = -404;
        public static string ErrorMsg = "Error";

        public static string AdminTypeCode = "0001";

        public static string TYPE_DEFALUT_ID = "-0000";

        public static string CANT_DEL_DEFAULT = "不允许删除默认类型";

        public static string CANT_DEL_ADMIN_TYPE = "不允许删除管理员类型";
    }
}