using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sl.web.constant
{
    /// <summary>
    /// 通用数据
    /// </summary>
    public class ConstantData
    {
        public static int ErrorCode = -404;
        public static string ErrorMsg = "Error";

        public static string AdminTypeCode = "0001";

        public static string TYPE_NO_ID = "-0000";


        //参数设置 <-----参数需要重新设置----->
        //http的域名
        //public static String HOST = "http://sdk.open.api.igexin.com/apiex.htm";

        //https的域名
        public static String HOST = "https://api.getui.com/apiex.htm";

        //定义常量, appId、appKey、masterSecret 采用本文档 "第二步 获取访问凭证 "中获得的应用配置
        public static String APPID = "VwQuLF3kbe6VNZdP5YwYc2";
        public static String APPKEY = "0Ty9HwRFFXAPMKgnhSWCe";
        public static String MASTERSECRET = "F9PvaUiGWj6CBgx2NGtkFA";
    }
}