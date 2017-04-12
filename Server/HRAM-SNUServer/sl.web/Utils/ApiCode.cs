using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sl.web
{
    public class ApiCode
    {
        #region 用户相关
        public static string RegisterSuccessedCode = "10000"; //注册成功
        public static string RegisterSuccessedMessage = "注册成功"; //注册成功
        public static string RegisterSuccessedBymailMessage = "注册成功，密码已发送到邮箱"; //注册成功

        public static string RegisterFailedCode = "10001"; //注册失败
        public static string RegisterFailedMessage = "注册失败"; //注册失败

        public static string MailboxValidCode = "10002"; //注册邮箱格式错误
        public static string MailboxValidMessage = "请输入正确的邮箱"; //注册邮箱格式错误

        public static string MailboxRegisteredCode = "10003"; //注册邮箱格式错误
        public static string MailboxRegisteredMessage = "该邮箱已被注册"; //注册邮箱格式错误

        public static string LoginNameRegisteredCode = "10004"; //用户名已被注册
        public static string LoginNameRegisteredMessage = "该用户名已被注册"; //用户名已被注册

        public static string LoginFailedCode = "20001"; //登录失败
        public static string LoginFailedMessage = "用户名或密码不正确"; //登录失败
        #endregion 
    }
}