

namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示要求必须输入
    /// </summary>
    public class RequiredAttribute : ValidRuleBase
    {
        /// <summary>
        /// 要求必须输入
        /// </summary>
        public RequiredAttribute()
        {
            this.OrderIndex = -1;
            this.Message = "该项为必填项";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return new ValidBox(this.Message);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="propertyValue">属性的值</param>
        /// <returns></returns>
        public override bool IsValid(object model, object propertyValue)
        {
            return propertyValue != null && propertyValue.ToString().Length > 0;
        }
    }
}
