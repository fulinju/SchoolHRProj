﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using sl.IService;
using sl.model;
using sl.common;
using sl.web.ui;
using sl.validate;
using Newtonsoft.Json;
using PetaPoco;
using sl.service.manager;

namespace sl.web.Areas.Manager.Controllers
{
    /// <summary>
    /// 友情链接Controller
    /// </summary>
    public class FriendLinkController : BaseManagerController
    {
        //
        // GET: /Manager/FriendLink/
        public ActionResult FriendLinkView()
        {
            return View();
        }

        #region 查询链接
        public ActionResult GetLinkList(string fl_name)
        {
            Sql sql = HRAManagerService.GetLinksSql(fl_name);
            return CommonPageList<T_FriendlyLink>(sql, HRAManagerService.database);
        }
        #endregion


        #region 删除链接
        public ActionResult LinkDel(string model)
        {
            List<T_FriendlyLink> entityList = JsonConvert.DeserializeObject<List<T_FriendlyLink>>(model);
            int flag = 0;
            foreach (var entity in entityList)
            {
                entity.IsDeleted = true;
                flag = HRAManagerService.database.Update(entity); //假删除
            }
            return DelMessage(flag);
        }
        #endregion

        #region 编辑链接
        public ActionResult LinkEdit(T_FriendlyLink m, string id = "0")
        {
            if (id == "0")
            {
                if (Request.IsPost())
                {
                    var validate = Model.Valid(m);
                    if (!validate.Result)
                    {
                        return ErrorMessage(validate.Message);
                    }
                    else
                    {
                        m.IsDeleted = false;
                        object result = HRAManagerService.database.Insert(m);
                        return SaveMessage(result);
                    }
                }
                return View(m);
            }
            else
            {
                Object obj = id;
                T_FriendlyLink load = HRAManagerService.database.SingleOrDefault<T_FriendlyLink>(obj);

                if (Request.IsPost())
                {
                    if (TryUpdateModel(load))
                    {
                        Model valid = Model.Valid(load);
                        return valid.Result ? SaveMessage(HRAManagerService.database.Update(load)) : ErrorMessage(valid.Message);
                    }
                }
                return View(load);
            }
        }
        #endregion
	}
}