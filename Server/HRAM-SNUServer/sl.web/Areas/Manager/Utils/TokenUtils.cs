using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sl.model;
using sl.common;

namespace sl.web
{
    public class TokenUtils
    {
        public static UserModel UpdateToken(UserModel model)
        {

            UserModel result = new UserModel();

            result = model;

            result.uTokenActiveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            result.uTokenExpiredTime = DateTime.Now.AddMinutes(60).ToString("yyyy-MM-dd HH:mm:ss");
            result.uToken = Security.MD5Encrypt(result.uID.ToString() + result.uUserName + DateTime.UtcNow.ToString() + Guid.NewGuid().ToString());

            return result;

        }
    }
}