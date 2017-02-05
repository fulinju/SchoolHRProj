using System.Text.RegularExpressions;

namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示验证是邮箱格式
    /// </summary>
    public class EmailAttribute : ValidRuleBase
    {

        /// <summary>
        /// 验证是邮箱格式
        /// </summary>
        public EmailAttribute()
        {
            this.Message = "请输入正确的邮箱";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return new ValidBox(this.ValidTypeName, this.Message);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="propertyValue">实体属性的值</param>
        /// <returns></returns>
        public override bool IsValid(object model, object propertyValue)
        {
            string value;
            if (base.HasValue(propertyValue, out value))
            {
                return Regex.IsMatch(value, @"^\w+(\.\w*)*@\w+\.\w+$");
            }
            return true;
        }
    }
}
