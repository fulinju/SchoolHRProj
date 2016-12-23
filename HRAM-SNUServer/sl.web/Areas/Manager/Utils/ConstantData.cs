using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sl.web
{
    /// <summary>
    /// 通用数据
    /// </summary>
    public class ConstantData
    {
        public static int ErrorCode = -404;
        public static string ErrorMsg = "Error";

        public static string AdminTypeCode = "0001";

        #region 默认的审核结果ID
        public static string[] DefaultReviewIDs = {"0001","0002","0003"};
        public static string DefaultReviewID1 = "0001";
        public static string DefaultReviewID2 = "0002";
        public static string DefaultReviewID3 = "0003";
        #endregion
    }
}