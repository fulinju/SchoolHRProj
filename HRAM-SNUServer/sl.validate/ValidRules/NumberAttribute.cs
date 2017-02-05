using System.Text.RegularExpressions;

namespace sl.validate.ValidRules
{
    public class NumberAttribute : ValidRuleBase
    {
        /// <summary>
        /// 验证是否为数字
        /// </summary>
        public NumberAttribute()
        {
            Message = "请输入正确的数字";
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
                string regParam = @"^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$";
                return Regex.IsMatch(value, regParam);
            }
            return true;
        }
    }
}