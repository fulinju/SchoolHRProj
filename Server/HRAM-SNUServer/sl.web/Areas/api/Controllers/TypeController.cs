using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Net.Http;
using System.Web.Http;
using PetaPoco;
using sl.model;
using common.utils;
using sl.service.api;
using sl.web.ui;

namespace sl.web.Areas.api.Controllers
{
    public class TypeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetTypeList()
        {
            List<T_FLType> list = HRAMApiService.GetFLType();

            //return Json(new { total = list.TotalItems, rows = list.Items });

            if (list != null)
            {
                return JsonUtils.toJson(HttpStatusCode.OK, new {item = list.Count, result = list });
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.GetFLTypeListFailedCode, ApiCode.GetFLTypeListFailedMessage));
            }
        }
    }
}
