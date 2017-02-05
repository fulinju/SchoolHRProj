using System.Text.RegularExpressions;

namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示验证是否和正则表达式匹配
    /// </summary>
    public class MatchAttribute : ValidRuleBase
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegParam { get; set; }

        /// <summary>
        /// 验证是否和正则表达式匹配
        /// </summary>
        /// <param name="regParam">正则表达式</param>
        public MatchAttribute(string regParam)
        {
            this.RegParam = regParam;
            this.OrderIndex = 1;
            this.Message = "请输入正确的值";
        }

        /// <summary>
        /// 生成验证框
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            var validType = this.ValidTypeName + ValidBox.MakeJsArray(this.RegParam.Replace(@"\", @"\\"));
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
                return Regex.IsMatch(value, this.RegParam);
            }
            return true;
        }
    }
}
