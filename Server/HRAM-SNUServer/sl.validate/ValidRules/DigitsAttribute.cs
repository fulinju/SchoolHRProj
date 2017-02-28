using System.Text.RegularExpressions;

namespace sl.validate.ValidRules
{
    public class DigitsAttribute : ValidRuleBase
    {
        /// <summary>
        /// 验证是否为整数
        /// </summary>
        public DigitsAttribute()
        {
            Message = "请输入整数";
        }

        public override ValidBox ToValidBox()
        {
            return new ValidBox(ValidTypeName, Message);
        }

        public override bool IsValid(object model, object propertyValue)
        {
            string value;
            if (HasValue(propertyValue, out value))
            {
                string regParam = @"^\d+$";
                return Regex.IsMatch(value, regParam);
            }
            return true;
        }
    }
}