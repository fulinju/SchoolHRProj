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
        //[HttpGet]
        //public HttpResponseMessage GetTypeList()
        //{
        //    Page<PublishInfo> list = HRAMApiService.GetPublishs(pageIndex, pageSize, pmTypeID);

        //    //return Json(new { total = list.TotalItems, rows = list.Items });

        //    if (list != null)
        //    {
        //        var isLastPage = false;
        //        if (list.CurrentPage >= list.TotalPages)
        //        {
        //            isLastPage = true;
        //        }

        //        return JsonUtils.toJson(HttpStatusCode.OK, new { totalItems = list.Items.Count, isLastPage = isLastPage, pageIndex = pageIndex, pageSize = pageSize, resultList = list.Items });
        //    }
        //    else
        //    {
        //        return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.GetPublishListFailedCode, ApiCode.GetPublishListFailedMessage));
        //    }
        //}
    }
}
