using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.ExMethod
{
    public static class StringEx
    {
        /// <summary>
        /// 比较两个字符串是否相等，忽略大小写
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EqualsNoCase(this string s, string value)
        {
            if (s == null)
            {
                if (value == null)
                    return true;
                return false;
            }
            return s.Equals(value, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// 判断指定的字符串是否在给定的字符串中
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EqualsAny(this string s, params string[] value)
        {
            if (s == null)
            {
                if (value == null)
                    return true;
                if (value.Length == 0)
                    return true;
                return false;
            }
            return value.Contains(s);
        }
        /// <summary>
        /// 判断指定的字符串是否在给定的字符串中,忽略大小写
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EqualsIgNoreAny(this string s, params string[] value)
        {
            if (s == null)
            {
                if (value == null)
                    return true;
                if (value.Length == 0)
                    return true;
                return false;
            }
            foreach (var v in value)
            {
                if (s.EqualsNoCase(v))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 指示指定的字符串是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <param name="IgnoreSpace">是否忽略空格</param>
        /// <returns></returns>
        public static bool IsEmpty(this string s, bool IgnoreSpace = true)
        {
            if (IgnoreSpace)
                return string.IsNullOrWhiteSpace(s);
            return string.IsNullOrEmpty(s);
        }

        private static string ReplaceNoCase(this string realaceValue, string oldValue, string newValue)
        {
            return System.Text.RegularExpressions.Regex.Replace(realaceValue, oldValue, newValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
        public static string ToStr(this object obj, string defaultValue = "")
        {
            return obj == null ? defaultValue : obj.ToString();
        }
        public static string Merge(this IEnumerable<char> source)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in source)
                sb.Append(s);
            return sb.ToString();
        }
        public static string Merge(this IEnumerable<char> source,char merge)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in source)
                sb.Append(s).Append(merge); 
            return sb.ToString().TrimEnd(merge);
        }
        public static string Merge(this IEnumerable<string> sArr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in sArr)
                sb.Append(s);
            return sb.ToString();
        }
        /// <summary>
        /// 将字符串集合以给定字符合并为一个字符串。
        /// </summary>
        /// <param name="sArr">要合并的字符串集合</param>
        /// <param name="mergeChar">连接字符</param>
        /// <returns>合并后的字符串</returns>
        public static string Merge(this IEnumerable<string> sArr, char mergeChar)
        {
            if (sArr.Count() == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var s in sArr)
                sb.Append(s).Append(mergeChar);
            return sb.ToString().TrimEnd(mergeChar);
        }
        /// <summary>
        /// 将字符串集合以给定字符合并为一个字符串。
        /// </summary>
        /// <param name="sArr">要合并的字符串集合</param>
        /// <param name="mergeChar">连接字符</param>
        /// <returns>合并后的字符串</returns>
        public static string Merge(this IEnumerable<string> sArr, string merge, bool TrimEnd = true)
        {
            if (sArr == null)
                return string.Empty;
            int Len = sArr.Count();
            if (Len == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            int n = 0;
            if (TrimEnd)
            {
                foreach (var s in sArr)
                    if (n++ == Len - 1)
                        sb.Append(s);
                    else
                        sb.Append(s).Append(merge);
            }
            else foreach (var s in sArr)
                    sb.Append(s).Append(merge);
            return sb.ToString();
        }
        public static string ToStrTrim(this object obj)
        {
            return obj == null ? string.Empty : obj.ToString().Trim();
        } /// <summary>
        /// 检索子字符串。子字符串从指定的位置开始到指定字符串在此实例中的第一个匹配项的索引结束。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string SubString(this string s, int start, string seperator)
        {
            int n = s.IndexOf(seperator);
            if (n <= 0)
                return s;
            if (n <= start)
                return "";
            return s.Substring(start, n);
        }
        /// <summary>
        /// 获取指定字符串左边从0开始、指定长度的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(this string s, int len)
        {
            if (s == null)
                return s;
            if (len > s.Length)
                if (len >= s.Length)
                    return s;
            return s.Substring(0, len);
        }
        /// <summary>
        ///  获取指定字符串右边从0开始、指定长度的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(this string s, int len)
        {
            if (s == null)
                return s;
            if (len >= s.Length)
                return s;
            return s.Substring(s.Length - len, len);
        }
        public static string PatchEnd(this string s, string patch)
        {
            if (!s.EndsWith(patch))
                s += patch;
            return s;
        }
        public static string PatchStart(this string s, string patch)
        {
            if (!s.StartsWith(patch))
                s = patch+s;
            return s;
        }
        public  static string GetLocalNumber(this string s)
        {
            if (MyApp.IsDefaultDecimalSeparator)
                return s;
            return s.Replace(".", MyApp.DecimalSeparator);
        }
        public static string GetDefaultNumber(this string s)
        {
            if (MyApp.IsDefaultDecimalSeparator)
                return s;
            return s.Replace(MyApp.DecimalSeparator, ".");
        } 
    }
}
