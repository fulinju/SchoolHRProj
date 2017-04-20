using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using sl.validate;
using sl.validate.ValidRules;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Html验证扩展类
    /// </summary>
    public static class HtmlHeplerExtend
    {
        #region 生成前端验证规则
        /// <summary>
        /// 生成前端验证规则
        /// </summary>
        /// <param name="html">html助手</param>
        /// <returns></returns>
        public static ValidBox Valid(this HtmlHelper html)
        {
            return ValidBox.Empty();
        }
        #endregion

        #region 验证必须输入
        /// <summary>
        /// 验证必须输入
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="message">提示信息</param>        
        /// <returns></returns>
        public static ValidBox Required(this ValidBox box, string message = null)
        {
            var newBox = new RequiredAttribute { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证输入是URL
        /// <summary>
        /// 验证输入是URL
        /// </summary>
        /// <param name="box">验证框</param>        
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox Url(this ValidBox box, string message = null)
        {
            var newBox = new UrlAttribute { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证输入是否是Email
        /// <summary>
        /// 验证输入是否是Email
        /// </summary>
        /// <param name="box">验证框</param>      
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox Email(this ValidBox box, string message = null)
        {
            var newBox = new EmailAttribute { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证输入是否是手机
        /// <summary>
        /// 验证输入是否是手机
        /// </summary>
        /// <param name="box">验证框</param>      
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox CellPhone(this ValidBox box, string message = null)
        {
            var newBox = new CellPhoneAttribute { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证输入的长度
        /// <summary>
        /// 验证输入的长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox Length(this ValidBox box, int minLength, int maxLength, string message = null)
        {
            var newBox = new LengthAttribute(minLength, maxLength) { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion


        #region 验证输入的最小长度
        /// <summary>
        /// 验证输入的最小长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="length">最小长度</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox MinLength(this ValidBox box, int length, string message = null)
        {
            var newBox = new MinLengthAttribute(length) { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证输入的最大长度
        /// <summary>
        /// 验证输入的最大长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="length">最大长度</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox MaxLength(this ValidBox box, int length, string message = null)
        {
            var newBox = new MaxLengthAttribute(length) { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证输入的值的范围
        /// <summary>
        /// 验证输入的值的范围
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox Range(this ValidBox box, int minValue, int maxValue, string message = null)
        {
            var newBox = new RangeAttribute(minValue, maxValue) { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证和正则表达式是否匹配
        /// <summary>
        /// 验证和正则表达式是否匹配
        /// </summary>
        /// <param name="box">验证框</param>        
        /// <param name="regParam">表达式</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox Match(this ValidBox box, string regParam, string message = null)
        {
            var newBox = new MatchAttribute(regParam) { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证输入和正则表达式不匹配
        /// <summary>
        /// 验证输入和正则表达式不匹配
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="regParam">表达式</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox NotMatch(this ValidBox box, string regParam, string message = null)
        {
            var newBox = new NotMatchAttribute(regParam) { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证输入是否和目标ID元素的值相等
        /// <summary>
        /// 验证输入是否和目标ID元素的值相等
        /// </summary>
        /// <param name="box">验证框</param>      
        /// <param name="targetID">目标元素ID</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox EqualTo(this ValidBox box, string targetID, string message = null)
        {
            var newBox = new EqualToAttribute(targetID) { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证输入是否和目标ID元素的值不相等
        /// <summary>
        /// 验证输入是否和目标ID元素的值不相等
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="targetID">目标元素ID</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static ValidBox NotEqualTo(this ValidBox box, string targetID, string message = null)
        {
            var newBox = new NotEqualToAttribute(targetID) { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 远程验证输入值
        /// <summary>
        /// 远程验证输入值
        /// </summary>
        /// <param name="box">提示信息</param>
        /// <param name="url">远程地址</param>
        /// <param name="targetID">提交的目标元素的ID</param>
        /// <returns></returns>
        public static ValidBox Remote(this ValidBox box, string url, params string[] targetID)
        {
            var newBox = new sl.validate.ValidRules.RemoteAttribute(url, targetID).ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证是否为整数
        /// <summary>
        /// 验证是否为整数
        /// </summary>
        /// <param name="box"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ValidBox Digits(this ValidBox box, string message = null)
        {
            var newBox = new DigitsAttribute { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证是否为整数
        /// <summary>
        /// 验证是否为整数
        /// </summary>
        /// <param name="box"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ValidBox Number(this ValidBox box, string message = null)
        {
            var newBox = new NumberAttribute { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region  验证下拉框，当值为0或者-1时则不通过
        /// <summary>
        /// 验证下拉框，当值为0或者-1时则不通过
        /// </summary>
        /// <param name="box"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ValidBox DropDown(this ValidBox box, string message = null)
        {
            var newBox = new DropDownAttribute { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 验证是否为日期
        /// <summary>
        /// 验证是否为日期
        /// </summary>
        /// <param name="box"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ValidBox DateTime(this ValidBox box, string message = null)
        {
            var newBox = new DateTimeAttribute { Message = message }.ToValidBox();
            return box & newBox;
        }
        #endregion

        #region 获取表达式对应属性的验证框描述
        /// <summary>
        /// 获取表达式对应属性的验证框描述
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <typeparam name="TKey">键</typeparam>
        /// <param name="html">Html</param>
        /// <param name="keySelector">属性选择表达式</param>
        /// <returns></returns>
        private static ValidBox GetExpressionValidBox<T, TKey>(this HtmlHelper<T> html, Expression<Func<T, TKey>> keySelector)
        {
            // 解析表达式
            var body = keySelector.Body as MemberExpression;
            if (body == null || body.Member.DeclaringType.IsAssignableFrom(typeof(T)) == false || body.Expression.NodeType != ExpressionType.Parameter)
            {
                return null;
            }
            // 获取自定义属性
            var attributeArray = typeof(T).GetProperty(body.Member.Name).GetCustomAttributes(false);
            
            if (attributeArray.Length == 0)
            {
                return null;
            }

            // 过滤获取验证属性
            var validBoxAttributeArray = attributeArray.Select(item => item as ValidRuleBase).Where(item => item != null).OrderBy(item => item.OrderIndex);
            if (!validBoxAttributeArray.Any())
            {
                return null;
            }

            // 生成验证规则
            var validBox = validBoxAttributeArray.First().ToValidBox();
            if (validBoxAttributeArray.Count() > 1)
            {
                var others = validBoxAttributeArray.Skip(1);
                foreach (var item in others)
                {
                    validBox = validBox & item.ToValidBox();
                }
            }
            return validBox;
        }
        #endregion

        #region 依据实体属性的特性生成前端验证规则
        /// <summary>
        /// 依据实体属性的特性生成前端验证规则
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <typeparam name="TKey">键</typeparam>
        /// <param name="html">Html</param>
        /// <param name="keySelector">属性选择表达式</param>      
        /// <returns></returns>
        public static object ValidFor<T, TKey>(this HtmlHelper<T> html, Expression<Func<T, TKey>> keySelector)
        {
            return html.GetExpressionValidBox(keySelector).AsHtmlAttribute();
        }
        #endregion

        #region 依据实体属性的特性生成前端验证规则
        /// <summary>
        /// 依据实体属性的特性生成前端验证规则
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <typeparam name="TKey">键</typeparam>
        /// <param name="html">Html</param>
        /// <param name="keySelector">属性选择表达式</param>    
        /// <param name="attribute">附加的html属性</param>
        /// <returns></returns>
        public static IDictionary<string, object> ValidFor<T, TKey>(this HtmlHelper<T> html, Expression<Func<T, TKey>> keySelector, object attribute)
        {
            return html.GetExpressionValidBox(keySelector).AsHtmlAttribute(attribute);
        }
        #endregion
    }
}