using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PetaPoco;
using sl.model;
using common.utils;
using sl.web.ui;
using sl.service.api;


namespace sl.web.Areas.api.Controllers
{
    /// <summary>
    /// 会员管理API
    /// </summary>
    public class MemberController : ApiController
    {
        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetMemberList(int pageIndex, int pageSize, string mTypeID="%%", string mReviewResultID="%%")
        {
            Page<MemberInfo> list = HRAMApiService.GetMembers(pageIndex, pageSize, mTypeID, mReviewResultID);

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
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.GetMemberListFailedCode, ApiCode.GetDownloadListFailedMessage));
            }
        }

        /// <summary>
        /// 获取会员详情
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetMemberDetail(String memberID)
        {
            MemberInfo member = HRAMApiService.GetMemberDetail(memberID);


            if (member != null)
            {

                return JsonUtils.toJson(HttpStatusCode.OK, member);
            }
            else
            {
                return JsonUtils.toJson(HttpStatusCode.PreconditionFailed, new JsonTip(ApiCode.GetMemberDetailFailedCode, ApiCode.GetMemberDetailFailedMessage));
            }
        }


    }
}
