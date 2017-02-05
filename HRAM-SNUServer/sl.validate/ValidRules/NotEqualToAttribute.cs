using System.Reflection;

namespace sl.validate.ValidRules
{
    /// <summary>
    /// 表示验证不要和目标ID的值一致
    /// </summary>
    public class NotEqualToAttribute : EqualToAttribute
    {
        /// <summary>
        /// 验证不要和目标ID的值一致
        /// </summary>       
        /// <param name="targetId">目标id</param>
        public NotEqualToAttribute(string targetId)
            : base(targetId)
        {
            this.Message = "输入的内容不能重复";
        }

        /// <summary>
        /// 生成验证框
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
            if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString()))
            {
                return true;
            }
            var targetProperty = model.GetType().GetProperty(TargetId, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            if (targetProperty == null)
            {
                return false;
            }
            var tagrgetValue = targetProperty.GetValue(model, null);
            return !propertyValue.Equals(tagrgetValue);
        }
    }
}
