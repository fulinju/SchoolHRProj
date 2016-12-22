using System.Linq;
using System.Reflection;

namespace sl.validate
{
    /// <summary>
    /// 提供对实体后台验证功能
    /// </summary>
    public sealed class Model
    {
        /// <summary>
        /// 获取验证结果
        /// </summary>
        public bool Result { get; private set; }

        /// <summary>
        /// 获取提示信息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 获取验证的字段
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// 根据实体的验证规则特性进行验证
        /// </summary>
        /// <param name="model">目标实体</param>
        /// <returns></returns>
        public static Model Valid(object model)
        {
            var propertyArray = model.GetType().GetProperties();
            foreach (var p in propertyArray)
            {
                var value = p.GetValue(model, null);
                var rules = p.GetCustomAttributes(false).Select(item => item as ValidRuleBase).Where(item => item != null).OrderBy(item => item.OrderIndex);
                foreach (var rule in rules)
                {
                    if (rule.IsValid(model, value) == false)
                    {
                        return new Model { Result = false, Message = rule.GetInvalidMessage(), Property = p };
                    }
                }
            }
            return new Model { Result = true };
        }
    }
}
