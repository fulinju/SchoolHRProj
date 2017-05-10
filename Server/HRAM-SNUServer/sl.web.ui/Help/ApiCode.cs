using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sl.web.ui
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

        public static string MailboxRegisteredCode = "10003"; //该邮箱已被注册
        public static string MailboxRegisteredMessage = "该邮箱已被注册"; //该邮箱已被注册

        public static string LoginNameRegisteredCode = "10004"; //用户名已被注册
        public static string LoginNameRegisteredMessage = "该用户名已被注册"; //用户名已被注册

        public static string PhoneRegisteredCode = "10005"; //该手机已被注册
        public static string PhoneRegisteredMessage = "该手机已被注册"; //该手机已被注册

        public static string LoginFailedCode = "11001"; //登录失败
        public static string LoginFailedMessage = "用户名或密码不正确"; //登录失败

        public static string ModifyUserInfoSucceedCode = "12001"; //修改用户信息成功
        public static string ModifyUserInfoSucceedMessage = "修改用户信息成功"; //修改用户信息成功

        public static string ModifyUserInfoFailedCode = "12002"; //修改用户信息失败
        public static string ModifyUserInfoFailedMessage = "修改用户信息失败"; //修改用户信息失败

        public static string ModifyPasswordSucceedCode = "12003"; //修改密码成功
        public static string ModifyPasswordSucceedMessage = "修改密码成功"; //修改密码成功

        public static string ModifyPasswordFailedCode = "12004"; //修改密码失败
        public static string ModifyPasswordFailedMessage = "修改密码失败"; ////修改密码失败

        public static string InitPasswordByMailSucceedCode = "12005"; //密码已发送到邮箱
        public static string InitPasswordByMailSucceedMessage = "密码已发送到邮箱"; //密码已发送到邮箱

        public static string InitPasswordByMailFailedCode = "12006"; //初始化密码失败
        public static string InitPasswordByMailFailedMessage = "初始化密码失败"; //初始化密码失败


        #endregion 

        #region 发布相关
        public static string GetPublishListFailedCode = "20001"; //获取发布列表失败
        public static string GetPublishListFailedMessage = "获取发布列表失败"; //

        public static string GetPublishDetailFailedCode = "20002"; //获取发布详情失败
        public static string GetPublishDetailFailedMessage = "获取发布详情失败"; //
        #endregion

        #region 下载相关
        public static string GetDownloadListFailedCode = "30001"; //获取下载列表失败
        public static string GetDownloadListFailedMessage = "获取下载列表失败"; //

        public static string UpdateDownloadNumSucceedCode = "30002"; //更新下载数量成功
        public static string UpdateDownloadNumSucceedMessage = "更新下载数量成功"; //

        public static string UpdateDownloadNumFailedCode = "30003"; //更新下载数量失败
        public static string UpdateDownloadNumFailedMessage = "更新下载数量失败"; //
        #endregion

        #region 会员相关
        public static string GetMemberListFailedCode = "40001"; //获取会员列表失败
        public static string GetMemberListFailedMessage = "获取会员列表失败"; //

        public static string GetMemberDetailFailedCode = "40002"; //获取会员详情失败
        public static string GetMemberDetailFailedMessage = "获取会员详情失败"; //
        #endregion

        #region 类型相关
        public static string GetFLTypeListFailedCode = "50001"; //获取友情链接类型列表失败
        public static string GetFLTypeListFailedMessage = "获取友情链接类型列表失败"; //获取友情链接类型列表失败
        #endregion

        #region 验证相关
        public static string TokenExpiredErrorCode = "60001"; //用户令牌已过期,请重新登录
        public static string TokenExpiredErrorMessage = "用户令牌已过期,请重新登录"; //用户令牌已过期,请重新登录

        public static string TokenCheckedErrorCode = "60002"; //用户令牌验证未通过,该请求已被拒绝
        public static string TokenCheckedErrorMessage = "用户令牌验证未通过,该请求已被拒绝"; //用户令牌已过期,请重新登录

        public static string TokenNotLoginErrorCode = "60003"; //请先登录
        public static string TokenNotLoginErrorMessage = "请先登录"; //请先登录

        public static string TokenGetErrorCode = "600043"; //获取用户令牌失败,该请求已被拒绝
        public static string TokenGetErrorMessage = "获取用户令牌失败,该请求已被拒绝"; //获取用户令牌失败,该请求已被拒绝

        public static string TokenIdGetErrorCode = "60005"; //获取用户ID失败,该请求已被拒绝
        public static string TokenIdGetErrorMessage = "获取用户ID失败,该请求已被拒绝"; //获取用户ID失败,该请求已被拒绝


        #endregion
    }
}