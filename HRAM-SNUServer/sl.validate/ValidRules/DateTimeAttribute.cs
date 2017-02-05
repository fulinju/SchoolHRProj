using System;

namespace sl.validate.ValidRules
{
    public class DateTimeAttribute : ValidRuleBase
    {
        public DateTimeAttribute()
        {
            Message = "日期格式不正确";
        }

        public override ValidBox ToValidBox()
        {
            return new ValidBox(ValidTypeName, Message);
        }

        public override bool IsValid(object model, object propertyValue)
        {
            try
            {
                DateTime time = Convert.ToDateTime(propertyValue);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}