
namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示验证长度不能小于指定值
    /// </summary>
    public class MinLengthAttribute : ValidRuleBase
    {
        /// <summary>
        /// 最小长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 验证长度不能小于指定值
        /// </summary>
        /// <param name="length">最小长度</param>
        public MinLengthAttribute(int length)
        {
            this.Length = length;
            this.OrderIndex = 1;
            this.Message = "长度不能小于{0}个字";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            var validType = this.ValidTypeName + ValidBox.MakeJsArray(Length);
            return new ValidBox(validType, this.Message);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns></returns>
        public override bool IsValid(object model, object propertyValue)
        {
            var value = propertyValue == null ? null : propertyValue.ToString();
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return value.Length >= this.Length;
        }

        /// <summary>
        /// 获取错误提示信息
        /// </summary>       
        /// <returns></returns>
        public override string GetInvalidMessage()
        {
            return string.Format(this.Message, this.Length);
        }
    }
}
