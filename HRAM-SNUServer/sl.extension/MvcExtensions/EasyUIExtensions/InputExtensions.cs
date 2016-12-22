using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;

namespace sl.extension
{
    public static class InputExtensions
    {
        #region 文本框

        public static MvcHtmlString Input(this HtmlHelper htmlHelper, string id, string value = "", object htmlAttributes = null)
        {
            return CreateInput(htmlHelper, id, value, "input", null, htmlAttributes);
        }

        public static MvcHtmlString Input(this HtmlHelper htmlHelper, string id, InputWidthType? widthType, string value = "", object htmlAttributes = null)
        {
            return CreateInput(htmlHelper, id, value, "input", widthType, htmlAttributes);
        }

        public static MvcHtmlString InputFor<T, TKey>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TKey>> expression, InputWidthType? widthType, object htmlAttributes = null)
        {
            RouteValueDictionary valueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            valueDictionary.AddCssClass("input");
            valueDictionary.AddCssClass(ResolveWidth(widthType));
            return htmlHelper.TextBoxFor(expression, valueDictionary);
        }

        public static MvcHtmlString InputFor<T, TKey>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TKey>> expression, IDictionary<string, object> htmlAttributes = null)
        {
            if (htmlAttributes == null)
                htmlAttributes = new Dictionary<string, object>();
            htmlAttributes.AddCssClass("input");
            return htmlHelper.TextBoxFor(expression, htmlAttributes);
        }

        public static MvcHtmlString InputFor<T, TKey>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TKey>> expression, object htmlAttributes = null)
        {
            return InputFor(htmlHelper, expression, null, htmlAttributes);
        }

        public static MvcHtmlString InputFor<T, TKey>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TKey>> expression)
        {
            return InputFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString UploadTextBox(this HtmlHelper htmlHelper, string id, InputWidthType? width = InputWidthType.Long, string value = "", bool isWater = false, bool isThumnail = false, string isEnorZh = "")
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("<div class='upload'><input type=\"text\" id=\"{0}\" name=\"{0}\" class=\"input left {1}\" value=\"{2}\" {3} />", id, ResolveWidth(width), value, isEnorZh);
            s.AppendFormat("<a href=\"javascript:;\" class=\"files\">");
            s.AppendFormat("<input type=\"file\" id=\"FileUpload\" name=\"FileUpload\" onchange=\"SingleUpload('{2}','FileUpload',{0},{1})\" {3} />", isWater ? 1 : 0, isThumnail ? 1 : 0, id, isEnorZh);
            s.Append("</a>");
            s.Append("<span class=\"uploading\">正在上传，请稍后.....</span><span>请上传正确格式的图片</span></div>");
            return MvcHtmlString.Create(s.ToString());
        }

        #endregion

        #region EasyUi日期框

        public static MvcHtmlString EasyUiDateBox(this HtmlHelper htmlHelper, string id, string value, object htmlAttributes = null)
        {
            return CreateInput(htmlHelper, id, value, "easyui-datebox", null, htmlAttributes);
        }

        public static MvcHtmlString EasyUiDateBoxFor<T, TKey>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TKey>> expression, object htmlAttributes = null)
        {
            RouteValueDictionary valueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            valueDictionary.AddCssClass("easyui-datebox");
            valueDictionary.AddCssClass("input");
            return htmlHelper.TextBoxFor(expression, valueDictionary);
        }

        #endregion

        #region EasyUi日期时间框

        public static MvcHtmlString EasyUiDateTimeBox(this HtmlHelper htmlHelper, string id, string value, object htmlAttributes = null)
        {
            return CreateInput(htmlHelper, id, value, "easyui-datetimebox", null, htmlAttributes);
        }

        public static MvcHtmlString EasyUiDateTimeBoxFor<T, TKey>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TKey>> expression, object htmlAttributes = null)
        {
            RouteValueDictionary valueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            valueDictionary.AddCssClass("easyui-datetimebox");
            valueDictionary.AddCssClass("input");
            return htmlHelper.TextBoxFor(expression, valueDictionary);
        }

        #endregion

        #region EasyUi数字框

        public static MvcHtmlString EasyUiNumberTextBox(this HtmlHelper htmlHelper, string id, string value, object htmlAttributes = null)
        {
            return CreateInput(htmlHelper, id, value, "easyui-numberspinner", null, htmlAttributes);
        }


        public static MvcHtmlString EasyUiNumberTextBoxFor<T, TKey>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TKey>> expression, object htmlAttributes = null)
        {
            return CreateInputFor(htmlHelper, expression, "easyui-numberspinner", null, htmlAttributes);
        }
        #endregion

        #region EasyUi提示框

        public static MvcHtmlString EasyUiToolTip(this HtmlHelper htmlHelper, string title, string content, string postion = "bottom", object htmlAttributes = null)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttribute("title", title);
            builder.MergeAttribute("position", postion);
            builder.SetInnerText(content);
            if (htmlAttributes != null)
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.AddCssClass("easyui-tooltip");
            builder.MergeAttribute("href", "javascript:;");
            return MvcHtmlString.Create(builder.ToString());
        }

        #endregion

        public static MvcHtmlString EasyUiTree(this HtmlHelper htmlHelper, string id, string url)
        {
            TagBuilder builder = new TagBuilder("ul");
            builder.MergeAttribute("id", id);
            builder.MergeAttribute("url", url);
            builder.AddCssClass("easyui-tree");
            return MvcHtmlString.Create(builder.ToString());
        }
        public static MvcHtmlString EasyUiDropDownListTree(this HtmlHelper htmlHelper, string id, string url, string value, int width = 240)
        {
            TagBuilder builder = new TagBuilder("select");
            builder.MergeAttribute("id", id);
            builder.MergeAttribute("name", id);
            builder.MergeAttribute("url", url);
            builder.AddCssClass("easyui-combotree");
            builder.MergeAttribute("value", value);
            builder.MergeAttribute("data-options", string.Format("valueField:'id',textField:'Text',width:{0}", width));
            return MvcHtmlString.Create(builder.ToString());
        }
        public static MvcHtmlString EasyUiDropDownListTreeFor<T, TKey>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TKey>> expression, string url, int width = 240, object htmlAttributes = null)
        {
            RouteValueDictionary valueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            valueDictionary.AddCssClass("easyui-combotree");
            valueDictionary.Add("url", url);
            valueDictionary.Add("data-options", string.Format("valueField:'id',textField:'Text',width:{0}", width));
            return htmlHelper.TextBoxFor(expression, valueDictionary);
        }

        #region 单选和复选
        /// <summary>
        /// 内用  生成radio 或者 checkbox 并 可排列
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="id">控件id</param>
        /// <param name="items">列表项源</param>
        /// <param name="canMultichoice">是否允许多选（false：radio；true: checkbox）</param>
        /// <param name="htmlAttribute">自定义属性</param>
        /// <param name="repeatColumns">排列列数</param>
        /// <param name="repeatDirection">排列方向</param>
        /// <returns></returns>
        public static MvcHtmlString CheckButtonList(this HtmlHelper htmlHelper, string id, IEnumerable<SelectListItem> items, bool canMultichoice = false, object htmlAttribute = null,
            int repeatColumns = 1, RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            string mainTagId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(id);
            if (string.IsNullOrEmpty(mainTagId))
            {
                throw new ArgumentException("filed can't be null or empty !", "name");
            }
            TagBuilder tableTag = new TagBuilder("table");
            tableTag.AddCssClass("radio-main");

            StringBuilder tableInnerContent = new StringBuilder();
            List<SelectListItem> selectListItems = items.ToList();
            if (repeatColumns == 1)//默认单行或列排列
            {
                if (repeatDirection == RepeatDirection.Horizontal)//默认 水平 单排列
                {
                    var trTag = new TagBuilder("tr");
                    foreach (var item in selectListItems)
                    {
                        var tdTag = RenderRadioTd(htmlHelper, mainTagId, item, canMultichoice, htmlAttribute);
                        trTag.InnerHtml = tdTag.ToString();
                        tableInnerContent.Append(trTag);
                    }
                }
                else//垂直
                {
                    foreach (var item in selectListItems)
                    {
                        var trTag = new TagBuilder("tr");
                        var tdTag = RenderRadioTd(htmlHelper, mainTagId, item, canMultichoice, htmlAttribute);
                        trTag.InnerHtml = tdTag.ToString();
                        tableInnerContent.Append(trTag);
                    }
                }
            }
            else//多行或列 排列
            {
                int itemsCounter = selectListItems.Count();
                bool isPerfectDivid = itemsCounter % repeatColumns == 0;
                int repeatNumber = itemsCounter / repeatColumns; //重复次数
                StringBuilder trContent = new StringBuilder();
                TagBuilder trTag = new TagBuilder("tr");

                for (int i = 0; i < itemsCounter; i++)
                {
                    TagBuilder tdTag = RenderRadioTd(htmlHelper, mainTagId, selectListItems[i], canMultichoice, htmlAttribute);
                    trContent.Append(tdTag);
                    if (i != 0 && ((i + 1) % repeatColumns) == 0 && (i + 1) / repeatColumns != repeatNumber)
                    {
                        trTag.InnerHtml = trContent.ToString();
                        tableInnerContent.Append(trTag);
                        trContent.Clear();
                    }
                    else if (i == itemsCounter - 1)
                    {
                        trTag.InnerHtml = trContent.ToString();
                        tableInnerContent.Append(trTag);
                    }
                }
            }

            tableTag.InnerHtml = tableInnerContent.ToString();
            return MvcHtmlString.Create(tableTag.ToString());
        }
        /// <summary>
        /// 生成内部 的 td 所有内容
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="mainTagName">主标签id</param>
        /// <param name="item">选项</param>
        /// <param name="canMultichoice">radio or checkbox</param>
        /// <param name="htmlAttribute">自定义属性</param>
        /// <returns></returns>
        private static TagBuilder RenderRadioTd(HtmlHelper htmlHelper, string mainTagName, SelectListItem item, bool? canMultichoice = false, object htmlAttribute = null)
        {
            var tdTag = new TagBuilder("td");
            var rbValue = item.Value ?? item.Text;
            var rbName = mainTagName + "_" + rbValue;

            //generate radio
            var radioTag = new TagBuilder("input");
            radioTag.GenerateId(rbName);
            radioTag.MergeAttribute("value", rbValue);

            if (item.Selected)
                radioTag.MergeAttribute("checked", item.Selected.ToString());

            string checkType = "";
            checkType = Convert.ToBoolean(canMultichoice) ? "checkbox" : "radio";

            radioTag.Attributes.Add("type", checkType);
            radioTag.MergeAttribute("name", mainTagName);
            radioTag.MergeAttributes(new RouteValueDictionary(htmlAttribute));

            var labelTag = new TagBuilder("label");
            labelTag.MergeAttribute("for", rbName);
            labelTag.MergeAttribute("id", rbName + "_label");
            labelTag.InnerHtml = rbValue;
            tdTag.InnerHtml = radioTag + labelTag.ToString();
            return tdTag;
        }



        #endregion

        #region 私有方法
        /// <summary>
        /// 为RouteValueDictionary扩展添加class方法
        /// </summary>
        /// <param name="htmlAttributes">html属性集合</param>
        /// <param name="cssClass">样式名</param>
        /// <returns>RouteValueDictionary</returns>
        private static void AddCssClass(this IDictionary<string, object> htmlAttributes, string cssClass)
        {
            if (htmlAttributes == null)
                htmlAttributes = new Dictionary<string, object>();
            if (htmlAttributes.Any(n => n.Key.ToLower() == "class"))
                htmlAttributes["class"] += " " + cssClass;
            else
                htmlAttributes["class"] = cssClass;
        }


        /// <summary>
        /// 根据宽度类型解析出相应的cssClass值
        /// </summary>
        /// <param name="widthType">宽度类型</param>
        /// <returns>cssClass值</returns>
        private static string ResolveWidth(InputWidthType? widthType)
        {
            string cssClass = string.Empty;
            if (widthType != null)
                switch (widthType.Value)
                {
                    case InputWidthType.Short:
                        cssClass += " wh-min";
                        break;
                    case InputWidthType.Medium:
                        cssClass += " wh-middle";
                        break;
                    case InputWidthType.Long:
                        cssClass += " wh-big";
                        break;
                    case InputWidthType.Longest:
                        cssClass += " wh-bigbig";
                        break;
                }
            return cssClass;
        }

        private static MvcHtmlString CreateInput(this HtmlHelper htmlHelper, string id, string value, string className, InputWidthType? width, object htmlAttributes = null)
        {
            TagBuilder builder = new TagBuilder("input");
            builder.MergeAttribute("type", "text");
            builder.MergeAttribute("id", id);
            builder.MergeAttribute("value", value);
            builder.MergeAttribute("name", id);
            if (htmlAttributes != null)
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.AddCssClass(className);
            builder.AddCssClass("input");
            builder.AddCssClass(ResolveWidth(width));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString CreateInputFor<T, TKey>(this HtmlHelper<T> htmlHelper, Expression<Func<T, TKey>> expression, string className, InputWidthType? width, object htmlAttributes = null)
        {
            RouteValueDictionary valueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            valueDictionary.AddCssClass(className);
            valueDictionary.AddCssClass("input");
            valueDictionary.AddCssClass(ResolveWidth(width));
            return htmlHelper.TextBoxFor(expression, valueDictionary);
        }
        #endregion

    }


    /// <summary>
    /// 录入框宽度类型
    /// </summary>
    public enum InputWidthType
    {
        /// <summary>
        /// 短的
        /// </summary>
        Short,
        /// <summary>
        /// 中等的
        /// </summary>
        Medium,
        /// <summary>
        /// 长的
        /// </summary>
        Long,
        /// <summary>
        /// 最长的
        /// </summary>
        Longest
    }
}
