using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace System
{
    public static class SystemExtension
    {
        /// <summary>
        /// Convert object to specified Type
        /// </summary>
        /// <typeparam name="T">desired type</typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue">default value when convert failed</param>
        /// <param name="ignoreException">true will ignore exception when convert failed. default is true</param>
        /// <returns>object of desired type</returns>
        public static T ConvertTo<T>(this object obj, T defaultValue, bool ignoreException = true)
        {
            if (ignoreException)
            {
                if (obj == null)
                    return defaultValue;
                try
                {
                    object result;
                    if (TryConventTo(obj, typeof(T), out result))
                        return (T)result;
                }
                catch { }
                return defaultValue;
            }
            return ConvertTo<T>(obj);
        }

        static object ConvertTo(JToken jToken, Type type)
        {
            if (jToken is JObject)
            {
                var jObject = (JObject)jToken;
                JToken jtype = null;
                if (jObject.TryGetValue("$type", StringComparison.OrdinalIgnoreCase, out jtype))
                {
                    var declareType = Type.GetType((jtype as JValue).Value?.ToString());
                    var newReader = jObject.Root.CreateReader();
                    if (declareType != null)
                    {
                        type = declareType;
                    }
                }
            }
            return JsonSerializer.Create(JsonConvert.DefaultSettings?.Invoke()).Deserialize(jToken.CreateReader(), type);
        }

        /// <summary>
        /// 转换为指定类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        public static object ConvertTo(this object obj, Type targetType)
        {
            if (obj != null)
            {
                object result;
                if (TryConventTo(obj, targetType, out result))
                    return result;

                throw new InvalidOperationException(Demo.Properties.Resources.CannotConvertType.FormatArgs(obj.GetType().Name, (object)targetType.Name));
            }
            return targetType.GetDefault();
        }

        static bool TryConventTo(object obj, Type targetType, out object result)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (obj is JToken)
            {
                result = ConvertTo((JToken)obj, targetType);
                return true;
            }
            if (obj is DBNull)
            {
                result = targetType.GetDefault();
                return true;
            }

            var sourceType = obj.GetType();
            if (targetType.IsAssignableFrom(sourceType))
            {
                result = obj;
                return true;
            }

            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                targetType = Nullable.GetUnderlyingType(targetType);

            if (targetType.IsEnum)
            {
                result = Enum.Parse(targetType, obj.ToString(), true);
                return true;
            }

            if (targetType == typeof(bool))//对bool特殊处理
            {
                if ("1".Equals(obj))
                {
                    result = true;
                    return true;
                }
                if ("0".Equals(obj))
                {
                    result = false;
                    return true;
                }
            }
            //处理数字类型。（空字符串转换为数字 0）
            if ((targetType.IsPrimitive || targetType == typeof(decimal)) &&
                obj is string && string.IsNullOrEmpty(obj as string))
            {
                result = 0;
                return true;
            }

            if (typeof(IConvertible).IsAssignableFrom(sourceType) &&
                typeof(IConvertible).IsAssignableFrom(targetType))
            {
                result = Convert.ChangeType(obj, targetType);
                return true;
            }

            var converter = System.ComponentModel.TypeDescriptor.GetConverter(obj);
            if (converter != null && converter.CanConvertTo(targetType))
            {
                result = converter.ConvertTo(obj, targetType);
                return true;
            }

            converter = System.ComponentModel.TypeDescriptor.GetConverter(targetType);
            if (converter != null && converter.CanConvertFrom(sourceType))
            {
                result = converter.ConvertFrom(obj);
                return true;
            }

            try
            {
                result = Convert.ChangeType(obj, targetType);
                return true;
            }
            catch { }

            result = null;
            return false;
        }

        /// <summary>
        /// Convert object to specified Type
        /// </summary>
        /// <typeparam name="T">desired type</typeparam>
        /// <param name="obj">original object</param>
        /// <returns>object of desired type</returns>
        public static T ConvertTo<T>(this object obj)
        {
            return (T)ConvertTo(obj, typeof(T));
        }

        /// <summary>
        /// 返回跟踪SQL字符串
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string ToTraceString(this IDbCommand cmd)
        {
            var content = cmd.CommandText;

            if (cmd.Parameters.Count > 0)
            {
                var pValues = cmd.Parameters.OfType<DbParameter>().Select(p =>
                {
                    var value = p.Value;
                    if (value is string)
                    {
                        value = '"' + value.ToString() + '"';
                    }
                    return p.ParameterName + "->" + value;
                });
                content += Environment.NewLine + "->" + string.Join(",", pValues);
            }
            return content;
        }

        /// <summary>
        /// 获取值，如果不存在指定键，返回T的默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hd"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetValue<T>(this HybridDictionary hd, object key, T defaultValue = default(T))
        {
            if (hd.Contains(key))
                return hd[key].ConvertTo<T>(defaultValue);
            return defaultValue;
        }

        public static string GetElementValue(this XElement element, string name)
        {
            return element?.Element(name).Value;
        }

        //public static string ToLabel(this Enum value)
        //{
        //    if (value == null)
        //        return null;
        //    return SIE.Utils.EnumViewModel.EnumToLabel(value);
        //}
    }
}