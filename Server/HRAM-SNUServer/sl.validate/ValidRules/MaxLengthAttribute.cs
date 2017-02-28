
namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示验证长度不能大于指定值
    /// </summary>
    public class MaxLengthAttribute : ValidRuleBase
    {
        /// <summary>
        /// 最大长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 验证长度不能大于指定值
        /// </summary>
        /// <param name="length">最大长度</param>
        public MaxLengthAttribute(int length)
        {
            this.Length = length;
            this.OrderIndex = 1;
            this.Message = "长度不能超过{0}个字";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            var validTpe = this.ValidTypeName + ValidBox.MakeJsArray(this.Length);
            return new ValidBox(validTpe, this.Message);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns></returns>
        public override bool IsValid(object model, object propertyValue)
        {
            string value;
            if (base.HasValue(propertyValue, out value))
            {
                return value.Length <= this.Length;
            }
            return true;
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
