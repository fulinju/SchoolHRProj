namespace System.Web.Mvc
{
    /// <summary>
    /// 拓展MVC，判断请求类型
    /// </summary>
    public static class HttpContextExtension
    {
        public static bool IsPost(this HttpRequestBase request)
        {
            return "POST" == request.HttpMethod;
        }

        public static bool IsGet(this HttpRequestBase request)
        {
            return "GET" == request.HttpMethod;
        }
    }
}