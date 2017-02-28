namespace sl.validate.ValidRules
{
    public class DropDownAttribute : ValidRuleBase
    {

        public DropDownAttribute()
        {
            Message = "请选择一个下拉项";
        }

        public override ValidBox ToValidBox()
        {
            return new ValidBox(ValidTypeName, Message);
        }

        public override bool IsValid(object model, object propertyValue)
        {
            if (propertyValue != null)
            {
                string value = propertyValue.ToString();
                if (value != "" && value != "-1" && value != "0")
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}