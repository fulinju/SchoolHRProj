
namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示验证输入的长度范围
    /// </summary>
    public class LengthAttribute : ValidRuleBase
    {
        /// <summary>
        /// 最小长度
        /// </summary>
        public int MinLength { get; set; }
        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// 验证输入的长度范围
        /// </summary>       
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        public LengthAttribute(int minLength, int maxLength)
        {
            this.MinLength = minLength;
            this.MaxLength = maxLength;
            this.OrderIndex = 1;
            this.Message = "长度必须介于{0}到{1}个字";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            var validType = this.ValidTypeName + ValidBox.MakeJsArray(this.MinLength, this.MaxLength);
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
            string value;
            if (base.HasValue(propertyValue, out value))
            {
                var length = value.Length;
                return length >= this.MinLength && length <= this.MaxLength;
            }
            return true;
        }

        /// <summary>
        /// 获取错误提示信息
        /// </summary>       
        /// <returns></returns>
        public override string GetInvalidMessage()
        {
            return string.Format(this.Message, this.MinLength, this.MaxLength);
        }
    }
}
