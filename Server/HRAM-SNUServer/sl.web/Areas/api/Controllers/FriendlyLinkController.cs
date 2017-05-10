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
using Newtonsoft.Json;

namespace sl.web.Areas.api.Controllers
{
    public class FriendlyLinkController : ApiController
    {
        /// <summary>
        /// 获取友情链接列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetFriendlyLinkList(int pageIndex, int pageSize)
        {
            Page<FriendlyLinkInfo> list = HRAMApiService.GetFriendlyLinks(pageIndex, pageSize);

            //return Json(new { total = list.TotalItems, rows = list.Items });

            if (list != null)
            {
                var isLastPage = false;
                if (list.CurrentPage >= list.TotalPages)
                {
                    isLastPage = true;
                }

                return JsonUtils.toJson(HttpStatusCode.OK, new { totalItems = list.Items.Count, isLastPage = isLastPage, pageIndex = pageIndex, pageSize = pageSize, resultList = list.Items });
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.GetPublishListFailedCode, ApiCode.GetPublishListFailedMessage));
            }
        }

     


        /// <summary>
        /// 根据类型获得友情链接 前12条
        /// </summary>
        [HttpPost]
        public HttpResponseMessage FindFriendlyLinkListByTypeTop12(string[] ids)
        {
            //List<string> valueIDs = JsonConvert.DeserializeObject<List<string>>(model);
            List<T_FriendlyLink>[] links = new List<T_FriendlyLink>[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                links[i] = HRAMApiService.GetFriendlyLinksByTypeID(ids[i]);
            }

            //return Json(new { total = list.TotalItems, rows = list.Items });

            if (links != null)
            {
                return JsonUtils.toJson(HttpStatusCode.OK, new { item = links.Length, result = links });
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.GetPublishListFailedCode, ApiCode.GetPublishListFailedMessage));
            }
        }

    }
}
