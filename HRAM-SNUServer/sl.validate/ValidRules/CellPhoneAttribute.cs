using System.Text.RegularExpressions;

namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示验证是手机格式
    /// </summary>
    public class CellPhoneAttribute : ValidRuleBase
    {

        /// <summary>
        /// 验证是邮箱格式
        /// </summary>
        public CellPhoneAttribute()
        {
            this.Message = "请输入正确的手机";
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
                return Regex.IsMatch(value, @"^1(?:[38]\d|4[57]|5[01256789])\d{8}$");
            }
            return true;
        }
    }
}
