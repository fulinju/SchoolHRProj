using System;
using System.Linq;

namespace sl.validate
{
    /// <summary>
    /// 验证规则特性基础类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidRuleBase : Attribute
    {
        /// <summary>
        /// 排序索引
        /// 越小越优先
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 验证不通过时自定义的提示信息
        /// 不设置则使用默认提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 转换为对应的ValidBox类型
        /// </summary>
        /// <returns></returns>
        public abstract ValidBox ToValidBox();

        /// <summary>
        /// 验证是否通过
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="propertyValue">值</param>
        /// <returns></returns>
        public abstract bool IsValid(object model, object propertyValue);

        /// <summary>
        /// 获取验证不通过时的提示信息
        /// </summary>        
        /// <returns></returns>
        public virtual string GetInvalidMessage()
        {
            return this.Message;
        }

        /// <summary>
        /// 判断字段是否有值
        /// </summary>
        /// <param name="properyValue">属性值</param>
        /// <param name="value">输出值</param>
        /// <returns></returns>
        protected bool HasValue(object properyValue, out string value)
        {
            value = properyValue == null ? null : properyValue.ToString();
            return string.IsNullOrEmpty(value) == false;
        }

        /// <summary>
        /// 获取自身对应的验证类型
        /// </summary>
        protected virtual string ValidTypeName
        {
            get
            {
                var validType = this.GetType().Name.Replace("Attribute", null);
                return char.ToLower(validType.First()).ToString() + new string(validType.Skip(1).ToArray());
            }
        }
    }
}
