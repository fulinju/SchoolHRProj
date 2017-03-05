using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PetaPoco;
using sl.model;
using sl.validate;
using common.utils;
using sl.common;
using sl.service.api;
using sl.web.ui;


namespace sl.web.Areas.api.Controllers
{
    /// <summary>
    /// 账户相关API
    /// </summary>
    public class AccountController : ApiController
    {
    
        /// <summary>
        /// 通过邮箱注册
        /// </summary>
        /// <param name="mailbox"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RegisterByMailbox(string mailbox)
        {
            bool isMail = Validate.IsEmail(mailbox);

            if (isMail == false)
            {
                return JsonUtils.toJson(new JsonTip(ApiCode.MailboxValidCode, ApiCode.MailboxValidMessage)); //邮箱格式错误
            }


            T_User user = HRAMApiService.GetUserByMail(mailbox);
            if (user != null)
            {
                return JsonUtils.toJson(new JsonTip(ApiCode.MailboxRegisteredCode, ApiCode.MailboxRegisteredMessage)); //邮箱已注册
            }

            string initPwd = Utils.RandomStr(4, true);
            string initPwdMd5 = Security.MD5Encrypt(initPwd);

            object insertFlag = HRAMApiService.RegisterUserByMail(mailbox,initPwdMd5);
            if (insertFlag != null)
            {
                string result = MailUtils.SendMailByQQ(mailbox, "Hi,HRAM 注册", "HRAM 注册", "欢迎使用HRAM，初始密码是" + initPwd, "初始密码已发送至" + mailbox, "发送失败");
                return JsonUtils.toJson(new JsonTip(ApiCode.RegisterSuccessedCode, ApiCode.RegisterSuccessedBymailMessage)); //注册成功，密码发送到邮箱
            }
            else
            {
                return JsonUtils.toJson(new JsonTip(ApiCode.RegisterFailedCode, ApiCode.RegisterFailedMessage)); //注册失败
            }
        }

        /// <summary>
        /// 通过用户名和密码注册
        /// </summary>
        /// <param name="uLoginName"></param>
        /// <param name="uPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RegisterByNameAndPwd(string uLoginName, string uPassword)
        {
            T_User user = HRAMApiService.GetUserByLoginName(uLoginName);
            if (user != null)
            {
                return JsonUtils.toJson(new JsonTip(ApiCode.LoginNameRegisteredCode, ApiCode.LoginNameRegisteredMessage)); //用户名已注册
            }
            string initPwdMd5 = Security.MD5Encrypt(uPassword);
            object insertFlag = HRAMApiService.RegisterUserByLoginName(uLoginName, initPwdMd5);

            if (insertFlag != null)
            {
                return JsonUtils.toJson(new JsonTip(ApiCode.RegisterSuccessedCode, ApiCode.RegisterSuccessedMessage)); //注册成功
            }
            else
            {
                return JsonUtils.toJson(new JsonTip(ApiCode.RegisterFailedCode, ApiCode.RegisterFailedMessage)); //注册失败
            }

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginStr"></param>
        /// <param name="uPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Login([FromBody] LoginModel loginModel)
        {
            T_User byName = HRAMApiService.GetUserByLoginNameAndPwd(loginModel.uLoginStr, loginModel.uPassword);
            T_User byMail = HRAMApiService.GetUserByMailAndPwd(loginModel.uLoginStr, loginModel.uPassword);
            if (byName != null)
            {
                return JsonUtils.toJson(byName);
            }
            else if (byMail != null)
            {
                return JsonUtils.toJson(byMail);
            }
            else
            {
                return JsonUtils.toJson(new JsonTip(ApiCode.LoginFailedCode, ApiCode.LoginFailedMessage)); //登录失败
            }

        }


        [HttpGet]
        public string SendMail()
        {
            return MailUtils.SendMailByQQ("862573026@qq.com", "自己的发的", "邮件标题", "这是一份邮件", "发送成功", "发送失败");
        }
    }
}
