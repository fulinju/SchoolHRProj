using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Net.Http;
using System.Web.Http;
using PetaPoco;
using sl.model;
using common.utils;

namespace sl.web.Areas.api.Controllers
{
    public class PublishController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetPublishsList()
        {
            Sql sql = Sql.Builder;

            //sql.Append("Select * from T_PublishManage");

            sql.Append("Select T_PMType.pm_typevalue,");
            sql.Append("T_PublishManage.pk_id,");
            sql.Append("T_PublishManage.pm_typeid,");
            sql.Append("T_PublishManage.u_loginname,");
            sql.Append("T_PublishManage.pm_title,");
            sql.Append("T_PublishManage.pm_adimglistid,");
            sql.Append("T_PublishManage.pm_publishtime,");
            sql.Append("T_PublishManage.pm_views,");
            sql.Append("T_PublishManage.pm_preview");
            sql.Append(" from T_PMType,T_PublishManage where T_PublishManage.IsDeleted = 0");
            sql.Append(" and T_PMType.PM_TypeID = T_PublishManage.PM_TypeID");

            //List<dynamic> pulishs = UtilsDB.DB.Fetch<dynamic>(sql);

            Page<T_PublishManage> list = UtilsDB.DB.Page<T_PublishManage>(1, 20, sql);

            var lists = list.remove

            //return Json(new { total = list.TotalItems, rows = list.Items });

            if (list != null)
            {
                return JsonUtils.toJson(new { total = list.TotalItems, rows = list.Items });
            }
            else
            {
                return JsonUtils.toJson("错误");
            }
        }
    }
}
