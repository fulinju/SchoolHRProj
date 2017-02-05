using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PetaPoco;
using Newtonsoft.Json;
using sl.IService;
using sl.common;
using sl.model;
using sl.web.ui;
using sl.validate;
using sl.extension.MvcExtensions.Controlls;
using sl.extension;

using System.IO;

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
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_SysModule where IsDeleted = 0 Order By M_Sort asc");

            var list = moduleService.TreeList(UtilsDB.DB, sql,0); // 0显示根目录

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //修改下拉列表时模块的树状结构
        public ActionResult ModuleTree()
        {
            Sql sql = Sql.Builder;
            sql.Append("Select * from T_SysModule where IsDeleted =0  Order By M_Sort asc");
            var treeNodes = UtilsDB.DB.Fetch<T_SysModule>(sql).Select(p => new TreeNode
            {
                id = p.pk_id,
                Pid = p.M_ParentNo,
                text = p.M_Name
            }).ToList();

            var jsons = TreeNode.CreateTree(treeNodes,-1); //使得根节点显现
            var list = moduleService.TreeList(UtilsDB.DB, sql,0); //需要外套一层 0为根目录
            return Json(jsons, JsonRequestBehavior.AllowGet);
        }

        //增加模块
        public ActionResult ModuleAdd(T_SysModule m, int id = 0) //JS var的坑，类型需要统一
        {
            if (id == 0)
            {
                if (Request.IsPost())
                {
                    if (m.M_ParentNo == null)
                    {
                        m.M_ParentNo = 0;
                    }

                    m.IsDeleted = false;
                    m.M_Sort = 0;
                    m.M_IsVisible = true;
                }
                return CommonAdd(UtilsDB.DB,m);
            }
            else
            {
                Object obj = id;
                T_SysModule load = UtilsDB.DB.SingleOrDefault<T_SysModule>(obj);
                if (load == null)
                    return ErrorMessage("没有获取到Model的值");
                if (Request.IsPost())
                {
                    Model valid = Model.Valid(m);
                    if (!valid.Result)
                        return ErrorMessage(valid.Message);
                    if (!TryUpdateModel(load))
                        return ErrorMessage("更新失败");
                    return valid.Result ? SaveMessage(UtilsDB.DB.Update(load)) : ErrorMessage(valid.Message);
                }
                return View(load);
            }
        }

        public ActionResult ModuleDelete(string model)
        {
            List<T_SysModule> sysMenus = JsonConvert.DeserializeObject<List<T_SysModule>>(model);
            bool flag = true;
            foreach (var sysMenu in sysMenus)
            {
                sysMenu.IsDeleted = true;

                bool updateFlag = false;
                if (UtilsDB.DB.Update(sysMenu) == 1)
                {
                    updateFlag = true;
                }
                flag = updateFlag && flag;
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


       //搜寻Controller的名称列表 可以放在页面初始化里面 点击后获得一个下拉列表
        public void searchControllerName()
        {
            var controllerPath = HttpContext.Server.MapPath("~/Areas/Manager/Controllers"); //Controller的路径

            var files = Directory.GetFiles(controllerPath, "*.cs");

            for (int i = 0; i < files.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine(files[i]);
            }
        }
    }
}