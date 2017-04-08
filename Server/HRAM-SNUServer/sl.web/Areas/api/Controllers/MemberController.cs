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


namespace sl.web.Areas.api.Controllers
{
    /// <summary>
    /// 会员管理API
    /// </summary>
    public class MemberController : ApiController
    {
        public HttpResponseMessage GetPublishList(int pageIndex, int pageSize)
        {


            Page<PublishInfo> list = HRAMApiService.GetPublishs(pageIndex, pageSize);

            //return Json(new { total = list.TotalItems, rows = list.Items });

            if (list != null)
            {
                Console.WriteLine(list);
                var isLastPage = false;
                if (list.CurrentPage >= list.TotalPages)
                {
                    isLastPage = true;
                }
                return JsonUtils.toJson(new { totalItems = list.Items.Count, isLastPage = isLastPage, pageIndex = pageIndex, pageSize = pageSize, resultList = list.Items });
            }
            else
            {
                return JsonUtils.toJson("错误");
            }
        }
    }
}
