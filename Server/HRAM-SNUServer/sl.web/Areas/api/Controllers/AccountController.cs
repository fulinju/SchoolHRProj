using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PetaPoco;
using sl.model;

namespace sl.web.Areas.api.Controllers
{
    /// <summary>
    /// 账户相关API
    /// </summary>
    public class AccountController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Register([FromBody]RegisterUser newUser)
        {
            return null;
        }
    }
}
