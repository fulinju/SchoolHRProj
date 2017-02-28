using System.Reflection;

namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示验证是否和目标ID的值一致
    /// </summary>
    public class EqualToAttribute : ValidRuleBase
    {
        /// <summary>
        /// 匹配的目标ID
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// 验证是否和目标ID的值一致
        /// </summary>        
        /// <param name="targetId">目标id</param>
        public EqualToAttribute(string targetId)
        {
            this.TargetId = targetId;
            this.Message = "两次输入的字符不一至";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            var validType = this.ValidTypeName + ValidBox.MakeJsArray(this.TargetId);
            return new ValidBox(validType, this.Message);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="propertyValue">属性的值</param>
        /// <returns></returns>
        public override bool IsValid(object model, object propertyValue)
        {
            string value;
            if (base.HasValue(propertyValue, out value))
            {
                var targetProperty = model.GetType().GetProperty(TargetId, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (targetProperty == null)
                {
                    return false;
                }
                var tagrgetValue = targetProperty.GetValue(model, null);
                if (tagrgetValue == null)
                {
                    return false;
                }

                return value == tagrgetValue.ToString();
            }
            return true;
        }
    }
}
