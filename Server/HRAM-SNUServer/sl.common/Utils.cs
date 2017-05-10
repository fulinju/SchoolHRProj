using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace sl.common
{
    public class Utils
    {
        #region 对象转换处理

        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            if (expression != null)
            {
                return IsNumeric(expression.ToString());
            }
            return false;

        }

        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(string expression)
        {
            if (expression != null)
            {
                string str = expression;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(object expression)
        {
            if (expression != null)
            {
                return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
        /// 将字符串转换为数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串数组</returns>
        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[',']);
        }

        /// <summary>
        /// 将数组转换为字符串
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="speater">分隔符</param>
        /// <returns>String</returns>
        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            if (expression != null)
            {
                return StrToBool(expression, defValue);
            }
            return defValue;
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression, "true", true) == 0)
                {
                    return true;
                }
                else if (string.Compare(expression, "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjToInt(object expression, int defValue)
        {
            if (expression != null)
            {
                return StrToInt(expression.ToString(), defValue);
            }
            return defValue;
        }

        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string expression, int defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            {
                return defValue;
            }

            int rv;
            if (Int32.TryParse(expression, out rv))
            {
                return rv;
            }
            return Convert.ToInt32(StrToFloat(expression, defValue));
        }

        /// <summary>
        /// Object型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjToFloat(object expression, float defValue)
        {
            if (expression != null)
            {
                return StrToFloat(expression.ToString(), defValue);
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string expression, float defValue)
        {
            if ((expression == null) || (expression.Length > 10))
            {
                return defValue;
            }
            float intValue = defValue;
            if (expression != null)
            {
                bool IsFloat = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    float.TryParse(expression, out intValue);
                }
            }
            return intValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str, DateTime defValue)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime))
                {
                    return dateTime;
                }
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str)
        {
            return StrToDateTime(str, DateTime.Now);
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj)
        {
            return StrToDateTime(obj.ToString());
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj, DateTime defValue)
        {
            return StrToDateTime(obj.ToString(), defValue);
        }

        #endregion

        #region 删除最后结尾的一个逗号

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        #endregion

        #region 删除最后结尾的指定字符后的字符

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }

        #endregion

        #region 生成指定长度的字符串

        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }

            return ReturnStr;
        }

        #endregion

        #region 生成日期随机码

        /// <summary>
        /// 生成日期随机码
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        public static string GetDateCode_yyyyMMdd()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 获取随机字符串。
        /// </summary>
        /// <param name="len">字符串的长度</param>
        /// <param name="includeNums">包含数字</param>
        /// <returns></returns>
        public static string RandomStr(int len, bool includeNums)
        {
            if (len < 0)
                return string.Empty;
            const string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string abc123 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz9876543210";

            string charList = includeNums ? abc123 : abc;
            int maxChar = charList.Length;

            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                byte[] guid = Guid.NewGuid().ToByteArray();
                Random rand = new Random((int)DateTime.Now.Ticks + i + guid[0] + guid[guid.Length - 1]);
                sb.Append(charList[rand.Next(0, maxChar)]);
            }
            return sb.ToString();
        }

        #endregion

        #region 截取字符长度

        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="inputString">字符</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string CutString(string inputString, int len)
        {
            /*ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++) {
                if ((int)s[i] == 63) {
                    tempLen += 2;
                } else {
                    tempLen += 1;
                }

                try {
                    tempString += inputString.Substring(i, 1);
                } catch {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号 
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "…";
            return tempString;*/
            string temp = inputString;
            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= len)
            {
                return temp;
            }
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= len - 3)
                {
                    return temp + "";
                }
            }
            return "";
        }

        public static string CutString_z(string stringToSub, int length)
        {
            if (IsNullOrEmpty(stringToSub))
            {
                return "";
            }
            if (Regex.Replace(stringToSub, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= length)
            {
                return stringToSub;
            }
            Regex regex = new Regex(@"[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            foreach (char t in stringChar)
            {
                if (regex.IsMatch((t).ToString()))
                {
                    nLength += 2;
                }
                else
                {
                    nLength = nLength + 1;
                }

                if (nLength <= length)
                {
                    sb.Append(t);
                }
                else
                {
                    break;
                }
            }

            return sb + "..";
        }

        #endregion

        #region 清除HTML标记

        public static string DropHTML(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring))
                return string.Empty;
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        public static string RemoveHtmlTag(string strhtml)
        {
            string stroutput = strhtml;
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
            stroutput = regex.Replace(stroutput, "");
            return stroutput;

        }


        #endregion

        #region 清除HTML标记且返回相应的长度

        public static string DropHTML(string Htmlstring, int strLen)
        {
            return CutString(DropHTML(Htmlstring), strLen);
        }

        #endregion

        #region TXT代码转换成HTML格式

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="Input">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把TXT代码转换成HTML格式
        public static String ToHtml(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br />");
            sb.Replace("\n", "<br />");
            sb.Replace("\t", " ");
            //sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }

        #endregion

        #region HTML代码转换成TXT格式

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把HTML代码转换成TXT格式
        public static String ToTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }

        #endregion

        #region 检查危险字符

        /// <summary>
        /// 检查危险字符
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string Filter(string sInput)
        {
            if (string.IsNullOrEmpty(sInput))
            {
                return "";
            }
            string sInput1 = sInput.ToLower();
            string output = sInput;
            const string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            output = output.Replace("'", "''");
            return output;
        }

        #endregion

        #region 检查过滤设定的危险字符

        /// <summary> 
        /// 检查过滤设定的危险字符
        /// </summary> 
        /// <param name="InText">要过滤的字符串 </param> 
        /// <returns>如果参数存在不安全字符，则返回true </returns> 
        public static bool SqlFilter(string word, string InText)
        {
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 过滤特殊字符

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Htmls(string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                string ihtml = Input.ToLower();
                ihtml = ihtml.Replace("<script", "&lt;script");
                ihtml = ihtml.Replace("script>", "script&gt;");
                ihtml = ihtml.Replace("<%", "&lt;%");
                ihtml = ihtml.Replace("%>", "%&gt;");
                ihtml = ihtml.Replace("<$", "&lt;$");
                ihtml = ihtml.Replace("$>", "$&gt;");
                return ihtml;
            }
            return string.Empty;
        }

        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }

        #endregion

        #region 获得配置文件节点XML文件的绝对路径

        public static string GetXmlMapPath(string xmlName)
        {
            return GetMapPath(ConfigurationManager.AppSettings[xmlName]);
        }

        #endregion

        #region 获得当前绝对路径

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        #endregion

        #region 操作cookie(new)

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            /* if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                 return HttpContext.Current.Request.Cookies[strName].Value.ToString();*/
            if (HttpContext.Current.Request.Cookies[strName] != null)
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
                if (httpCookie != null)
                {
                    return httpCookie.Value;
                }
            }
            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="key">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            /*if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return HttpContext.Current.Request.Cookies[strName][key].ToString();*/
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
            if (httpCookie != null && (httpCookie[key] != null))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
                if (cookie != null)
                {
                    return cookie[key].ToString();
                }
            }
            return "";
        }

        public static int GetCookiesCount(string name)
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[name];
            if (httpCookie != null)
            {
                return httpCookie.Values.Count;
            }
            return 0;
        }

        #endregion

        #region 检测对象是否为空

        #region 重载1

        /// <summary>
        /// 检测对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        #endregion

        #region 重载2

        /// <summary>
        /// 检测对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(object data)
        {
            return IsNullOrEmpty<object>(data);
        }

        #endregion

        #region 重载3

        /// <summary>
        /// 检测字符串是否为空，为空返回true
        /// </summary>
        /// <param name="text">要检测的字符串</param>
        public static bool IsNullOrEmpty(string text)
        {
            //检测是否为null
            if (text == null)
            {
                return true;
            }

            //检测字符串空值
            if (string.IsNullOrEmpty(text.ToString().Trim()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion

        #region 检查是否为IP地址

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        #endregion

        #region 检测是否有Sql危险字符

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        #endregion

        #region 文件操作

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        public static bool DeleteFile(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return false;
            }
            string fullpath = GetMapPath(_filepath);
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除上传的文件(及缩略图)
        /// </summary>
        /// <param name="_filepath"></param>
        public static void DeleteUpFile(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return;
            }
            string thumbnailpath = _filepath.Substring(0, _filepath.LastIndexOf("/")) + "mall_" +
                                   _filepath.Substring(_filepath.LastIndexOf("/") + 1);
            string fullTPATH = GetMapPath(_filepath); //宿略图
            string fullpath = GetMapPath(_filepath); //原图
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
            if (File.Exists(fullTPATH))
            {
                File.Delete(fullTPATH);
            }
        }

        /// <summary>
        /// 返回文件大小KB
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        /// <returns>int</returns>
        public static int GetFileSize(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return 0;
            }
            string fullpath = GetMapPath(_filepath);
            if (File.Exists(fullpath))
            {
                FileInfo fileInfo = new FileInfo(fullpath);
                return ((int)fileInfo.Length) / 1024;
            }
            return 0;
        }

        /// <summary>
        /// 返回文件扩展名，不含“.”
        /// </summary>
        /// <param name="_filepath">文件名称</param>
        /// <returns>string</returns>
        public static string GetFileExt(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return "";
            }
            if (_filepath.LastIndexOf(".") > 0)
            {
                return _filepath.Substring(_filepath.LastIndexOf(".") + 1); //文件扩展名，不含“.”
            }
            return "";
        }

        /// <summary>
        /// 获得远程字符串
        /// </summary>
        public static string GetDomainStr(string key, string uriPath)
        {
            string result = CacheHelper.Get(key) as string;
            if (result == null)
            {
                System.Net.WebClient client = new System.Net.WebClient();
                try
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    result = client.DownloadString(uriPath);
                }
                catch
                {
                    result = "暂时无法连接!";
                }
                CacheHelper.Insert(key, result, 60);
            }

            return result;
        }

        #endregion

        #region URL处理

        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// URL字符解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }

        /// <summary>
        /// 组合URL参数
        /// </summary>
        /// <param name="_url">页面地址</param>
        /// <param name="_keys">参数名称</param>
        /// <param name="_values">参数值</param>
        /// <returns>String</returns>
        public static string CombUrlTxt(string _url, string _keys, params string[] _values)
        {
            StringBuilder urlParams = new StringBuilder();
            try
            {
                string[] keyArr = _keys.Split(new char[] { '&' });
                for (int i = 0; i < keyArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_values[i]) && _values[i] != "0")
                    {
                        _values[i] = UrlEncode(_values[i]);
                        urlParams.Append(string.Format(keyArr[i], _values) + "&");
                    }
                }
                if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.IndexOf("?") == -1)
                    urlParams.Insert(0, "?");
            }
            catch
            {
                return _url;
            }
            return _url + DelLastChar(urlParams.ToString(), "&");
        }

        #endregion

        #region 获得文章中的图片

        /// <summary> 
        /// 取得HTML中所有图片的 URL。 
        /// </summary> 
        /// <param name="sHtmlText">HTML代码</param> 
        /// <returns>图片的URL列表</returns> 
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            Regex regImg =
                new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>",
                          RegexOptions.IgnoreCase);
            // 搜索匹配的字符串 
            if (!string.IsNullOrEmpty(sHtmlText))
            {
                MatchCollection matches = regImg.Matches(sHtmlText);
                int i = 0;
                string[] sUrlList = new string[matches.Count];
                // 取得匹配项列表 
                foreach (Match match in matches)
                    sUrlList[i++] = match.Groups["imgUrl"].Value;
                return sUrlList;
            }
            return new[] { "" };
        }

        /// <summary>
        /// 去除script
        /// </summary>
        public static string loseScript(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }
            else
            {
                System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>",
                                                                                                       System.Text.RegularExpressions.
                                                                                                           RegexOptions.IgnoreCase);
                html = regex1.Replace(html, ""); //过滤<script></script>标记
                return html;
            }
        }

        /// <summary>
        /// 去除iframe
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string loseIframe(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }
            else
            {
                System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>",
                                                                                                       System.Text.RegularExpressions.
                                                                                                           RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>",
                                                                                                       System.Text.RegularExpressions.
                                                                                                           RegexOptions.IgnoreCase);
                html = regex1.Replace(html, ""); //过滤<script></script>标记
                html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
                return html;
            }
        }

        /// <summary>
        /// 替换表情
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ReplaceSmaile(object content)
        {
            if (content == null)
            {
                return string.Empty;
            }
            else
            {
                string Input = content.ToString();
                if (string.IsNullOrEmpty(Input))
                {
                    return string.Empty;
                }
                string s = string.Empty;
                Match match = Regex.Match(Input, "\\[em\\:[^\\:\\]\\[em\\:><,/]*\\:\\]", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
                while (match.Success)
                {
                    s = match.Value;
                    Input = Input.Replace(s, "<img src=\"/template/images/face/" + s + ".gif\"   align=\"middle\" style=\"border:0\" />");
                    match = match.NextMatch();
                }
                string rList = Input.Replace("[em:", string.Empty);
                rList = rList.Replace(":]", string.Empty);
                return rList;
            }
        }

        #endregion

        #region 日期操作
        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }
        #region 返回时间差

        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            {
            }
            return dateDiff;
        }

        #endregion

        /// <summary>
        /// 得到数据库默认最小日期
        /// </summary>
        /// <returns></returns>
        public static DateTime getdefaultDate()
        {
            return SqlDateTime.MinValue.Value;
        }

        /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        public static string GetDate(DateTime date)
        {
            if (CommonValidate.IsDateTime(date.ToString()))
            {
                return date.ToString("yyyy-MM-dd");
            }
            else
            {
                return string.Empty;
            }
        }
        public static string GetDate(DateTime? date)
        {
            if (date.HasValue)
            {
                try
                {
                    return Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                }
                catch
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }
        public static string DefaultDate(DateTime date)
        {
            if (date == SqlDateTime.MinValue.Value)
            {
                return string.Empty;
            }
            else
            {
                return date.ToString("yyyy-MM-dd");
            }
        }

        //获取时间月，日
        public static string GetMonthDay(DateTime date)
        {
            if (date == SqlDateTime.MinValue.Value)
            {
                return string.Empty;
            }
            else
            {
                return date.ToString("MM-dd");
            }
        }
        public static DateTime InsertDefaultDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return SqlDateTime.MinValue.Value;
            }
            else
            {
                return ObjectToDateTime(date, DateTime.Now);
            }
        }
        #endregion

        #region 返回字符串真实长度, 1个汉字长度为2
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
        #endregion

        #region 返回URL中结尾的文件名
        /// <summary>
        /// 返回URL中结尾的文件名
        /// </summary>		
        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strs1 = url.Split(new char[] { '/' });
            return strs1[strs1.Length - 1].Split(new char[] { '?' })[0];
        }
        #endregion

        #region 格式化字节数字符串
        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
            {
                return ((double)(bytes / 1073741824)).ToString("0") + "G";
            }

            if (bytes > 1048576)
            {
                return ((double)(bytes / 1048576)).ToString("0") + "M";
            }

            if (bytes > 1024)
            {
                return ((double)(bytes / 1024)).ToString("0") + "K";
            }

            return bytes.ToString() + "Bytes";
        }
        #endregion

        #region 移除Html标记
        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            if (string.IsNullOrEmpty(content))
                return string.Empty;
            return Regex.Replace(content, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
        }
        #endregion

        #region 从HTML中获取文本,保留br,p,img
        /// <summary>
        /// 从HTML中获取文本,保留br,p,img
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static string GetTextFromHTML(string HTML)
        {
            Regex regEx = new Regex(@"</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);
            return regEx.Replace(HTML, "");
        }
        #endregion

        #region 备份文件
        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 备份文件,当目标文件存在时覆盖
        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }
        #endregion

        #region 恢复文件
        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    else
                    {
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                    }
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }
        #endregion

        #region 存储文件
        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }
        #endregion

        #region 获取ajax形式的分页链接
        /// <summary>
        /// 获取ajax形式的分页链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="callback">回调函数</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns></returns>
        public static string GetAjaxPageNumbers(int curPage, int countPage, string callback, int extendPage)
        {
            string pagetag = "page";
            int startPage = 1;
            int endPage = 1;

            string t1 = "<a href=\"###\" onclick=\"" + string.Format(callback, "&" + pagetag + "=1");
            string t2 = "<a href=\"###\" onclick=\"" + string.Format(callback, "&" + pagetag + "=" + countPage);

            t1 += "\">&laquo;</a>";
            t2 += "\">&raquo;</a>";

            if (countPage < 1)
                countPage = 1;
            if (extendPage < 3)
                extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span>");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append("<a href=\"###\" onclick=\"");
                    s.Append(string.Format(callback, pagetag + "=" + i));
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            s.Append(t2);

            return s.ToString();
        }
        #endregion

        #region 获取站点根目录URL
        /// <summary>
        /// 获取站点根目录URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootUrl(string forumPath)
        {
            int port = HttpContext.Current.Request.Url.Port;
            return string.Format("{0}://{1}{2}{3}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host.ToString(),
                                 (port == 80 || port == 0) ? "" : ":" + port, forumPath);
        }
        #endregion

        #region 根据字符串获取枚举值
        /// <summary>
        /// 根据字符串获取枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">字符串枚举值</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static T GetEnum<T>(string value, T defValue)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentException)
            {
                return defValue;
            }
        }
        #endregion

        #region 将8位日期型整型数据转换为日期字符串数据
        /// <summary>
        /// 将8位日期型整型数据转换为日期字符串数据
        /// </summary>
        /// <param name="date">整型日期</param>
        /// <param name="chnType">是否以中文年月日输出</param>
        /// <returns></returns>
        public static string FormatDate(int date, bool chnType)
        {
            string dateStr = date.ToString();

            if (date <= 0 || dateStr.Length != 8)
                return dateStr;

            if (chnType)
                return dateStr.Substring(0, 4) + "年" + dateStr.Substring(4, 2) + "月" + dateStr.Substring(6) + "日";
            return dateStr.Substring(0, 4) + "-" + dateStr.Substring(4, 2) + "-" + dateStr.Substring(6);
        }
        #endregion

        #region 格式化日期
        public static string FormatDate(int date)
        {
            return FormatDate(date, false);
        }
        #endregion

        #region 得到下一个日期
        /// <summary>
        /// 得到下一个日期
        /// </summary>
        /// <param name="daycount">天数</param>
        /// <returns></returns>
        public static DateTime GetNextDate(int daycount)
        {
            DateTime dt = DateTime.Now.AddDays(daycount);
            return dt;
        }
        #endregion

        #region 获取UUID
        public static string UUID()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        #endregion

        #region 闭合html标签
        /// <summary>
        /// 闭合html标签
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CloseHTML(string str)
        {
            string[] HtmlTag = new string[] { "p", "div", "span", "table", "ul", "font", "b", "u", "i", "a", "h1", "h2", "h3", "h4", "h5", "h6" };

            for (int i = 0; i < HtmlTag.Length; i++)
            {
                int OpenNum = 0, CloseNum = 0;
                Regex re = new Regex("<" + HtmlTag[i] + "[^>]*" + ">", RegexOptions.IgnoreCase);
                MatchCollection m = re.Matches(str);
                OpenNum = m.Count;
                re = new Regex("</" + HtmlTag[i] + ">", RegexOptions.IgnoreCase);
                m = re.Matches(str);
                CloseNum = m.Count;

                for (int j = 0; j < OpenNum - CloseNum; j++)
                {
                    str += "</" + HtmlTag[i] + ">";
                }
            }

            return str;
        }
        #endregion

        #region 获取颜色代码
        public static string GetColorString(string old, string key)
        {
            char[] sp = { ' ', '|', '\'', ';', ':', '-', '(', ')' };
            string[] arrkey = key.Split(sp);
            foreach (string temp in arrkey)
            {
                if (temp != "")
                {
                    Match m = Regex.Match(old, temp, RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        string str_m = m.Value;
                        old = old.Replace(str_m, "<font color=#ff3300>" + str_m + "</font>");
                    }
                }

            }
            return old;
        }
        #endregion

        #region 删除文件
        public static void DeleteFiles(string carPicture)
        {
            if (!string.IsNullOrEmpty(carPicture))
            {
                string[] files = carPicture.Split(',');
                foreach (string file in files)
                {
                    DeleteFile(file);
                }
            }
        }
        #endregion

        #region 获取当前页面的主域，如www.qq.com主域是qq.com
        /// <summary>
        /// 获取当前页面的主域，如www.qq.com主域是qq.com
        /// </summary>
        public static string ServerDomain
        {
            get
            {
                string urlHost = HttpContext.Current.Request.Url.Host.ToLower();
                string[] urlHostArray = urlHost.Split(new[] { '.' });
                if ((urlHostArray.Length < 3) || CommonValidate.IsIP(urlHost))
                {
                    return urlHost;
                }
                string urlHost2 = urlHost.Remove(0, urlHost.IndexOf(".", StringComparison.Ordinal) + 1);
                if ((urlHost2.StartsWith("com.") || urlHost2.StartsWith("net.")) || (urlHost2.StartsWith("org.") || urlHost2.StartsWith("gov.")))
                {
                    return urlHost;
                }
                return urlHost2;
            }
        }
        #endregion

        #region 获取最小时间
        public static DateTime GetMinDateTime()
        {
            return SqlDateTime.MinValue.Value;
        }
        #endregion
    }
}
