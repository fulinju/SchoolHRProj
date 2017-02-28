
namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示验值要在一定的区间中
    /// </summary>
    public class RangeAttribute : ValidRuleBase
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// 验值要在一定的区间中
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public RangeAttribute(int minValue, int maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Message = "值要在区间[{0},{1}]内的整数";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            var validType = this.ValidTypeName + ValidBox.MakeJsArray(this.MinValue, this.MaxValue);
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
            var value = 0;
            if (int.TryParse(propertyValue.ToString(), out value))
            {
                return value >= this.MinValue && value <= this.MaxValue;
            }
            return false;
        }

        /// <summary>
        /// 获取错误提示信息
        /// </summary>     
        /// <returns></returns>
        public override string GetInvalidMessage()
        {
            return string.Format(this.Message, this.MinValue, this.MaxValue);
        }
    }
}
