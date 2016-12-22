using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PetaPoco;
using PetaPoco.Orm;
using Newtonsoft.Json;
using sl.IService;
using sl.common;
using sl.model;
using sl.web.ui;
using sl.validate;
using sl.extension.MvcExtensions.Controlls;
using sl.extension;

namespace sl.web.Areas.Manager.Controllers
{
    //
    // GET: /Manager/System/
    /// <summary>
    /// 系统模块管理类
    /// </summary>
    public class SystemController : BaseManagerController
    {
        private ISysModuleService moduleService;

        public SystemController(ISysModuleService moduleService)
        {
            this.moduleService = moduleService;
        }

        #region 模块增删改查

        public ActionResult ModuleView()
        {
            return View();
        }
        public ActionResult ModuleList()
        {
            Sql where = Condition.Builder.Equal("IsDeleted", false).Create();
            var list = moduleService.TreeList(@where, "Sort asc");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModuleAdd(T_SysModule m, int id = 0)
        {
            if (id == 0)
            {
                if (Request.IsPost())
                {
                    m.IsDeleted = false;
                    m.Sort = 30;
                    m.IsVisible = true;
                    m.IsSingle = false;
                }
                return CommonAdd(m);
            }
            else
            {
                T_SysModule load = moduleService.Load(id);
                if (load == null)
                    return ErrorMessage("没有获取到Model的值");
                if (Request.IsPost())
                {
                    Model valid = Model.Valid(m);
                    if (!valid.Result)
                        return ErrorMessage(valid.Message);
                    if (!TryUpdateModel(load))
                        return ErrorMessage("更新失败");
                    bool success = moduleService.Update(load);
                    return SaveMessage(success);
                }
                return View(load);
            }
        }

        public ActionResult ModuleTree()
        {
            Sql where = Condition.Builder.Equal("IsDeleted", false).Create();
            var treeNodes = moduleService.List(@where, "Sort asc").Select(p => new TreeNode
            {
                id = p.ModuleID,
                Pid = p.ParentNo,
                text = p.ModuleName
            }).ToList();
            var jsons = TreeNode.CreateTree(treeNodes);
            return Json(jsons, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModuleDelete(string model)
        {
            List<T_SysModule> sysMenus = JsonConvert.DeserializeObject<List<T_SysModule>>(model);
            bool flag = true;
            foreach (var sysMenu in sysMenus)
            {
                sysMenu.IsDeleted = true;
                flag = moduleService.Update(sysMenu) && flag;
            }
            return DelMessage(flag);
        }
        #endregion

        //获取所有图标，并缓存1天
        public ActionResult GetIcons()
        {
            var cache = DataCache.GetCache("SystemIcons");
            if (cache != null)
            {
                return Json(cache);
            }
            var rootPath = HttpContext.Server.MapPath("~/Images/icons/");
            string dirPath = rootPath + "32X32\\";

            var files = DirFile.GetFileNames(dirPath);
            var listFiles = new List<string>();
            foreach (var file in files)
            {
                listFiles.Add(file.Replace(dirPath, "/Images/icons/32X32/"));
            }
            //缓存一天
            DataCache.SetCache("SystemIcons", listFiles, new TimeSpan(24, 0, 0));
            return Json(listFiles);
        }
    }

}