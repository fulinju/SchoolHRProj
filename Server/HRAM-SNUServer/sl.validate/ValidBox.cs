using System;
using System.Collections.Generic;
using System.Linq;

namespace sl.validate
{
    /// <summary>
    /// 输入框验证实体
    /// 此实体可以转换为HtmlAttributes
    /// </summary>
    public sealed class ValidBox
    {
        /// <summary>
        /// 获取字段是否是必须输入的
        /// </summary>
        private KeyValuePair<bool, string> _required;

        /// <summary>
        /// 获取验证失败时的提示语       
        /// </summary>
        private List<string> _messageList;

        /// <summary>
        /// 获取验证类型
        /// </summary>
        private List<string> _validtypeList;

        /// <summary>
        /// 验证框
        /// </summary>
        private ValidBox()
        {
            this._messageList = new List<string>();
            this._validtypeList = new List<string>();
        }

        /// <summary>
        /// 表示必须输入的验证框对象
        /// </summary>
        /// <param name="message">未输入时的提示信息</param>
        public ValidBox(string message)
            : this()
        {
            this._required = new KeyValuePair<bool, string>(true, message);
        }

        /// <summary>
        /// 表示一般验证框对象
        /// <param name="validType">验证方法和参数</param>
        /// <param name="mesage">不通过时提示信息</param>
        /// </summary>
        public ValidBox(string validType, string mesage)
            : this()
        {
            this._messageList.Add(mesage);
            this._validtypeList.Add(validType);
        }

        /// <summary>
        /// 生成JavaScript数组的字符串表达方式
        /// </summary>       
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static string MakeJsArray(params object[] param)
        {
            if (param == null) param = new object[0];

            Func<object, string> selector = item =>
            {
                if (item == null || item is string)
                {
                    return string.Format("'{0}'", item);
                }
                return item.ToString();
            };

            return "[" + string.Join(",", param.Select(selector)) + "]";
        }

        /// <summary>
        /// 验证框与运算
        /// </summary>
        /// <param name="left">左验证框</param>
        /// <param name="right">右验证框</param>
        /// <returns></returns>
        public static ValidBox operator &(ValidBox left, ValidBox right)
        {
            var newBox = new ValidBox();
            newBox._messageList.AddRange(left._messageList);
            newBox._messageList.AddRange(right._messageList);

            newBox._validtypeList.AddRange(left._validtypeList);
            newBox._validtypeList.AddRange(right._validtypeList);

            newBox._required = right._required.Key ? right._required : left._required;
            return newBox;
        }

        /// <summary>
        /// 表示生成无任何意义的空验证框
        /// </summary>
        /// <returns></returns>
        public static ValidBox Empty()
        {
            return new ValidBox();
        }

        /// <summary>
        /// 转换为Html属性对象
        /// </summary>
        /// <returns></returns>
        public object AsHtmlAttribute()
        {
            if (this._validtypeList.Count > 0)
            {
                if (this._required.Key)
                {
                    return new
                    {
                        @class = "validBox",// easyui-validatebox
                        required = "required",
                        required_message = this._required.Value,
                        message = MakeJsArray(this._messageList.ToArray()),
                        validtype = string.Join(";", this._validtypeList)
                    };
                }
                else
                {
                    return new
                    {
                        @class = "validBox",
                        message = MakeJsArray(this._messageList.ToArray()),
                        validtype = string.Join(";", this._validtypeList)
                    };
                }
            }

            if (this._required.Key)
            {
                return new { @class = "validBox", required = "required", required_message = this._required.Value };
            }

            return null;
        }


        /// <summary>
        /// 转换为Html属性对象
        /// <param name="attribute">附加的html属性</param>
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> AsHtmlAttribute(object attribute)
        {
            Action<IDictionary<string, object>, object> AddToDictionary = (dic, obj) =>
            {
                if (obj == null) return;
                var properties = obj.GetType().GetProperties();

                foreach (var p in properties)
                {
                    var key = p.Name.Replace("_", "-").ToLower();
                    var value = p.GetValue(obj, null);

                    if (dic.ContainsKey(key))
                    {
                        if (key == "class")
                        {
                            dic[key] = string.Format("{0} {1}", dic[key], value).Trim();
                        }
                    }
                    else
                    {
                        dic.Add(key, value);
                    }
                }
            };

            var dictionary = new Dictionary<string, object>();
            AddToDictionary(dictionary, this.AsHtmlAttribute());
            AddToDictionary(dictionary, attribute);
            return dictionary;
        }
    }
}