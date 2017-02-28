using System;

namespace sl.common
{
    public class ValueUtility
    {
        public static T ChangeType<T>(object text, T defaultValue)
        {
            if (text == null) return defaultValue;
            try
            {
                return (T)Convert.ChangeType(text, typeof(T));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
