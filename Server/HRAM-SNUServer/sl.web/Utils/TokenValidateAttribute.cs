using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using sl.model;
using common.utils;
using sl.web.ui;

using System.Web.SessionState;//
using sl.common;

namespace sl.web
{
    public class TokenValidateAttribute : ActionFilterAttribute, IRequiresSessionState
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            try
            {
                // 从Header中获取uToken
                string[] tokenList = filterContext.Request.Headers.GetValues("uToken").ToArray();
                string uToken = tokenList[0];

                string[] uIdList = filterContext.Request.Headers.GetValues("uID").ToArray();
                string uID = uIdList[0];
                
                UserModel currentUser = HttpContext.Current.Session[uID.ToString()] as UserModel;

                //string savedToken = Utils.GetCookie(uID);


                foreach (var key in HttpContext.Current.Session.Keys)
                {
                    string keys = key.ToString();
                    string session = HttpContext.Current.Session[key.ToString()].ToString();
                    Console.WriteLine("key: " + keys);
                    Console.WriteLine("value: " + session);
                }


                if (currentUser == null)
                {
                    filterContext.Response = JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.TokenNotLoginErrorCode, ApiCode.TokenNotLoginErrorMessage));
                }
                else if (currentUser.uToken.Equals(uToken))                // 判断uToken是否相等
                {
                    if (Convert.ToDateTime(currentUser.uTokenExpiredTime) > DateTime.Now)
                    {
                        // 更新过期时间
                        currentUser.uTokenExpiredTime = DateTime.Now.AddMinutes(60).ToString("yyyy-MM-dd HH:mm:ss");

                        HttpContext.Current.Session[uID.ToString()] = currentUser;
                    }
                    else
                    {
                        HttpContext.Current.Session[uID.ToString()] = null;

                        filterContext.Response = JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.TokenExpiredErrorCode, ApiCode.TokenExpiredErrorMessage));
                    }
                }
                else
                {
                    filterContext.Response = JsonUtils.toJson(HttpStatusCode.Unauthorized, new JsonTip(ApiCode.TokenCheckedErrorCode, ApiCode.TokenCheckedErrorMessage));
                }
            }


            catch (Exception ex)
            {
                ex.ToString();
                filterContext.Response = JsonUtils.toJson(HttpStatusCode.Unauthorized, new JsonTip(ApiCode.TokenGetErrorCode, ApiCode.TokenGetErrorMessage));
            }
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}