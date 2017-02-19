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
    /// 下载管理API
    /// </summary>
    public class DownloadController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetDownloadList(int pageIndex, int pageSize)
        {
            Page<DownLoadInfo> list = HRAMApiService.GetDownloads(pageIndex, pageSize);

            //return Json(new { total = list.TotalItems, rows = list.Items });

            if (list != null)
            {
                var isLastPage = false;
                if (list.CurrentPage >= list.TotalPages)
                {
                    isLastPage = true;
                }
                return JsonUtils.toJson(new { pageIndex = pageIndex, pageSize = pageSize, isLastPage = isLastPage, pageItems = list.Items.Count, resultList = list.Items });
                //return JsonUtils.toJson(new { total = list.TotalItems, pageIndex = pageIndex, pageSize = pageSize, resultList = list.Items });
            }
            else
            {
                return JsonUtils.toJson("错误");
            }
        }
    }
}
