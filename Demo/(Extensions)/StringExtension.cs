﻿using System.Collections.Generic;
using System.Linq;

namespace System
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 使用<see cref="StringComparison.OrdinalIgnoreCase"/>比较两个字符串是否相等。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="target">对比的字符串</param>
        /// <returns></returns>
        public static bool CIEquals(this string str, string target)
        {
            return string.Equals(str, target, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="str">The string to test</param>
        /// <returns>true if the value parameter is null or System.String.Empty, or if value consists exclusively of white-space characters.</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Indicates whether the specified string is null or an System.String.Empty string.
        /// </summary>
        /// <param name="str">The string to test.</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断字符串是否不为Null且不为<see cref="String.Empty "/>
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str">带格式化的字符串</param>
        /// <param name="args">格式化参数</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatArgs(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>
        /// 使用指定的分隔符连接字符串.
        /// </summary>
        /// <param name="arr">字符串集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns>连接的字符串</returns>
        public static string Concat(this IEnumerable<string> arr, string separator)
        {
            if (arr?.Any() != true)
                return string.Empty;
            var str = "";
            foreach (var s in arr)
                str += s + separator;
            return str.Remove(str.Length - separator.Length);
        }
    }
}
