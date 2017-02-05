using System.Web.Mvc;
using System.Web.Routing;

namespace sl.extension
{
    public static class ButtonExtensions
    {
        #region 封装EasyUI增删该查的按钮
        public static MvcHtmlString EasyuiLinkButton(this HtmlHelper htmlHelper, string id, string text, LinkButton button, object htmlAttributes = null)
        {
            if (button == null)
                button = new LinkButton("");
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Empty;
            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttribute("class", "easyui-linkbutton");
            builder.SetInnerText(text);
            builder.MergeAttribute("id", id);
            builder.MergeAttribute("href", "javascript:;");
            if (!string.IsNullOrEmpty(button.Icon))
                builder.MergeAttribute("icon", button.Icon);
            if (button.Disabled)
                builder.MergeAttribute("disabled", "true");
            if (!string.IsNullOrEmpty(button.OnClick))
                builder.MergeAttribute("onclick", button.OnClick);
            if (button.Plain)
                builder.MergeAttribute("plain", "true");
            if (htmlAttributes != null)
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString EasyuiLinkButtonForSave(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            LinkButton button = new LinkButton { Icon = "ext-icon-accept" };
            return EasyuiLinkButton(htmlHelper, "btnSave", "保存", button, htmlAttributes);
        }
        public static MvcHtmlString EasyuiLinkButtonForSearch(this HtmlHelper htmlHelper, string dgid, string formid, object htmlAttributes = null)
        {
            LinkButton button = new LinkButton { Icon = "icon-search", OnClick = string.Format("Search('{0}','{1}')", dgid, formid) };
            return EasyuiLinkButton(htmlHelper, "btnSearch", "查询", button, htmlAttributes);
        }
        public static MvcHtmlString EasyuiLinkButtonForAdd(this HtmlHelper htmlHelper, string dgid, string title, string url, int width = 700, int height = 500, object htmlAttributes = null, bool showButton = true)
        {
            LinkButton button = new LinkButton { Icon = "icon-add", OnClick = string.Format("Add('{0}','{1}','{2}',{3},{4})", dgid, title, url, width, height) };
            return EasyuiLinkButton(htmlHelper, "btnAdd", "添加", button, htmlAttributes);
        }
        public static MvcHtmlString EasyuiLinkButtonForEdit(this HtmlHelper htmlHelper, string dgid, string title, string url, int width = 700, int height = 500, object htmlAttributes = null, bool showButton = true)
        {
            LinkButton button = new LinkButton { Icon = "icon-edit", OnClick = string.Format("Edit('{0}','{1}','{2}',{3},{4})", dgid, title, url, width, height) };
            return EasyuiLinkButton(htmlHelper, "btnEdit", "修改", button, htmlAttributes);
        }
        public static MvcHtmlString EasyuiLinkButtonForDel(this HtmlHelper htmlHelper, string dgid, string url, object htmlAttributes = null)
        {
            LinkButton button = new LinkButton { Icon = "icon-remove", OnClick = string.Format("Delete('{0}','{1}')", dgid, url) };
            return EasyuiLinkButton(htmlHelper, "btnDel", "删除", button, htmlAttributes);
        }

        public static MvcHtmlString EasyuiLinkButtonForReload(this HtmlHelper htmlHelper, string dgid, object htmlAttributes = null)
        {
            LinkButton button = new LinkButton { Icon = "icon-reload", OnClick = string.Format("Reload('{0}')", dgid) };
            return EasyuiLinkButton(htmlHelper, "btnReload", "刷新", button, htmlAttributes);
        }
        #endregion
    }
}
