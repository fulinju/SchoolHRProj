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
            bool isMail = CommonValidate.IsEmail(register.mailbox);

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
                string result = MailUtils.SendMailByQQ(register.mailbox, "Hi,HRAM 注册", "HRAM 注册", "欢迎使用HRAM，账户：" + register.mailbox + "，初始密码：" + initPwd + "（区分大小写），请及时修改密码哦！", "初始密码已发送至" + register.mailbox, "发送失败");
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
            object insertFlag = HRAMApiService.RegisterUserByLoginName(register.uLoginName, register.uPassword);

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

            UserModel login = HRAMApiService.GetUserByLoginNameAndPwd(loginInfo);

            if (login != null)
            {
                login = TokenUtils.UpdateToken(login);

               
                HttpContext.Current.Session[login.uID.ToString()] = login;

                UserModel currentUser = HttpContext.Current.Session[login.uID.ToString()] as UserModel;


                return JsonUtils.toJson(HttpStatusCode.OK, login);
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.LoginFailedCode, ApiCode.LoginFailedMessage)); //登录失败
            }

        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [TokenValidateAttribute]
        [HttpPost]
        public HttpResponseMessage ModifyUserInfo([FromBody]UserModel userInfo)
        {
            string uID = userInfo.uID;
            T_User target = HRAMApiService.GetUserByID(uID); //安全验证有点弱 获取原来的

            T_User userByMaiBox = HRAMApiService.GetUserByMail(userInfo.uMaiBox);
            if (userByMaiBox != null && userInfo.uMaiBox != target.uMaiBox) //排除自己的
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.MailboxRegisteredCode, ApiCode.MailboxRegisteredMessage)); //邮箱已注册
            }

            T_User userByPhone = HRAMApiService.GetUserByPhone(userInfo.uPhone);
            if (userByPhone != null && userInfo.uPhone != target.uPhone)
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.PhoneRegisteredCode, ApiCode.PhoneRegisteredMessage)); //Phone已注册
            }



            if (target != null)
            {
                target.uMaiBox = userInfo.uMaiBox; //更安全的可以做成修改前验证
                target.uPhone = userInfo.uPhone;
                target.uUserName = userInfo.uUserName;
                HRAMApiService.database.Update(target);
                return JsonUtils.toJson(HttpStatusCode.OK, new JsonTip(ApiCode.ModifyUserInfoSucceedCode, ApiCode.ModifyUserInfoSucceedMessage));
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.ModifyUserInfoFailedCode, ApiCode.ModifyUserInfoFailedMessage));
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modify"></param>
        /// <returns></returns>
        //[TokenValidateAttribute]
        [HttpPost]
        public HttpResponseMessage ModifyPassword([FromBody]ModifyPasswordModel modify)
        {
            string uID = modify.uID;
            string loginName = modify.uLoginStr;
            string oldPassword = modify.uOldPassword;
            T_User target = HRAMApiService.GetUserByIDAndLoginNameAndPassword(uID, loginName, oldPassword);

            if (target != null)
            {
                target.uPassword = modify.uNewPassword;
                HRAMApiService.database.Update(target);
                return JsonUtils.toJson(HttpStatusCode.OK, new JsonTip(ApiCode.ModifyPasswordSucceedCode, ApiCode.ModifyPasswordSucceedMessage));
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.ModifyPasswordFailedCode, ApiCode.ModifyPasswordFailedMessage));
            }
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="modify"></param>
        /// <returns></returns>
        //[TokenValidateAttribute]
        [HttpPost]
        public HttpResponseMessage ForgetPassword([FromBody]UserModel forget)
        {
            string loginName = forget.uLoginStr;
            string uMaiBox = forget.uMaiBox;
            T_User target = HRAMApiService.GetUserByLoginNameAndMailBox(loginName, uMaiBox);

            if (target != null)
            {
                string initPwd = Utils.RandomStr(4, true);
                string initPwdMd5 = Security.MD5Encrypt(initPwd);

                target.uPassword = initPwdMd5;
                HRAMApiService.database.Update(target);
                string result = MailUtils.SendMailByQQ(uMaiBox, "Hi！HRAM忘记密码初始化", "HRAM 忘记密码", "账户：" + uMaiBox + "，重置密码：" + initPwd + "（区分大小写），请及时修改密码哦！", "重置密码已发送至" + uMaiBox, "发送失败");//合理的是发一个验证链接 跳到一个Web修改
                return JsonUtils.toJson(HttpStatusCode.OK, new JsonTip(ApiCode.InitPasswordByMailSucceedCode, ApiCode.InitPasswordByMailSucceedMessage));
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.InitPasswordByMailFailedCode, ApiCode.InitPasswordByMailFailedMessage));
            }
        }
    }


}
