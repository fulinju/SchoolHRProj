using System.Web.Mvc;
using Core.Config;
using sl.model;
using sl.web.ui;

namespace sl.web.Areas.Manager.Controllers
{
    /// <summary>
    /// 网站配置信息的Controller
    /// </summary>
    public class ConfigController:BaseManagerController
    {
        #region 网站基本信息配置
        public ActionResult WebSiteConfig()
        {
            ConfigContext context = new ConfigContext(new FileConfigService());
            WebSiteConfig model = context.Get<WebSiteConfig>();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveWebSiteConfig(WebSiteConfig model)
        {
            ConfigContext context = new ConfigContext(new FileConfigService());
            WebSiteConfig getModel = context.Get<WebSiteConfig>();
            UpdateModel(getModel);
            context.Save(model);
            return SaveMessage(true);
        }
        #endregion
    }
}