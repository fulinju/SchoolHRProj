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
    /// <summary>
    /// 文章相关API
    /// </summary>
    public class ArticleController : ApiController
    {

        //http://localhost:8888/api/Article/GetPublishList/?pageIndex=1&pageSize=2
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetPublishList(int pageIndex, int pageSize, string pmTypeID="%%")
        {
            Page<PublishInfo> list = HRAMApiService.GetPublishs(pageIndex, pageSize,pmTypeID);

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
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed,new JsonTip(ApiCode.GetPublishListFailedCode, ApiCode.GetPublishListFailedMessage));
            }
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetPublishDetail(string publishID)
        {
            PublishInfo publish = HRAMApiService.GetPublishDetail(publishID);

            if (publish != null)
            {
                return JsonUtils.toJson(HttpStatusCode.OK, publish);
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.GetPublishDetailFailedCode, ApiCode.GetPublishDetailFailedMessage));
            }
        }
    }
}
