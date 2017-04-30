using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
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
        public HttpResponseMessage RegisterByMailbox([FromBody]RegisterModel register)
        {
            bool isMail = Validate.IsEmail(register.mailbox);

            if (isMail == false)
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.MailboxValidCode, ApiCode.MailboxValidMessage)); //邮箱格式错误
            }


            T_User user = HRAMApiService.GetUserByMail(register.mailbox);
            if (user != null)
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.MailboxRegisteredCode, ApiCode.MailboxRegisteredMessage)); //邮箱已注册
            }

            string initPwd = Utils.RandomStr(4, true);
            string initPwdMd5 = Security.MD5Encrypt(initPwd);

            object insertFlag = HRAMApiService.RegisterUserByMail(register.mailbox, initPwdMd5);
            if (insertFlag != null)
            {
                string result = MailUtils.SendMailByQQ(register.mailbox, "Hi,HRAM 注册", "HRAM 注册", "欢迎使用HRAM，账户：" + register.mailbox + "，初始密码：" + initPwd, "初始密码已发送至" + register.mailbox, "发送失败");
                return JsonUtils.toJson(HttpStatusCode.OK, new JsonTip(ApiCode.RegisterSuccessedCode, ApiCode.RegisterSuccessedBymailMessage)); //注册成功，密码发送到邮箱
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.RegisterFailedCode, ApiCode.RegisterFailedMessage)); //注册失败
            }
        }

        /// <summary>
        /// 通过用户名和密码注册
        /// </summary>
        /// <param name="uLoginName"></param>
        /// <param name="uPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RegisterByNameAndPwd([FromBody] RegisterModel register)
        {
            T_User user = HRAMApiService.GetUserByLoginName(register.uLoginName);
            if (user != null)
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.LoginNameRegisteredCode, ApiCode.LoginNameRegisteredMessage)); //用户名已注册
            }
            string initPwdMd5 = Security.MD5Encrypt(register.uPassword);
            object insertFlag = HRAMApiService.RegisterUserByLoginName(register.uLoginName, initPwdMd5);

            if (insertFlag != null)
            {
                return JsonUtils.toJson(HttpStatusCode.OK, new JsonTip(ApiCode.RegisterSuccessedCode, ApiCode.RegisterSuccessedMessage)); //注册成功
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.RegisterFailedCode, ApiCode.RegisterFailedMessage)); //注册失败
            }

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginStr"></param>
        /// <param name="uPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Login([FromBody]LoginModel loginInfo)
        {
            //LoginModel loginInfo = new LoginModel();
            //loginInfo.uLoginStr = uLoginStr;
            //loginInfo.uPassword = uPassword;
            //loginInfo.uClientKey = uClientKey;

            UserModel login = HRAMApiService.GetUserByLoginNameAndPwd(loginInfo);

            if (login != null)
            {
                login = TokenUtils.UpdateToken(login);
                //login.uTokenActiveTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //login.uTokenExpiredTime = DateTime.Parse(DateTime.Now.AddMinutes(60).ToString("yyyy-MM-dd HH:mm:ss"));
                //login.uToken = Security.MD5Encrypt(login.uID.ToString() + login.uUserName + DateTime.UtcNow.ToString() + Guid.NewGuid().ToString());

                HttpContext.Current.Session[login.uID.ToString()] = login;
                return JsonUtils.toJson(HttpStatusCode.OK, login);
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.LoginFailedCode, ApiCode.LoginFailedMessage)); //登录失败
            }

        }


        [HttpGet]
        public string SendMail()
        {
            return MailUtils.SendMailByQQ("862573026@qq.com", "自己的发的", "邮件标题", "这是一份邮件", "发送成功", "发送失败");
        }
    }
}
