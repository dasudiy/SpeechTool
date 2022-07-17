using System;
using System.Collections.Generic;
using System.Text;

namespace TTSTool.Classes
{
    public static class ConvertExtension
    {
        private static Dictionary<Type, Func<object, object>> dict = new Dictionary<Type, Func<object, object>>();

        static ConvertExtension()
        {
            dict.Add(typeof(sbyte), WrapValueConvert(Convert.ToSByte));
            dict.Add(typeof(byte), WrapValueConvert(Convert.ToByte));
            dict.Add(typeof(short), WrapValueConvert(Convert.ToInt16));
            dict.Add(typeof(int), WrapValueConvert(Convert.ToInt32));
            dict.Add(typeof(long), WrapValueConvert(Convert.ToInt64));
            dict.Add(typeof(ushort), WrapValueConvert(Convert.ToUInt16));
            dict.Add(typeof(uint), WrapValueConvert(Convert.ToUInt32));
            dict.Add(typeof(ulong), WrapValueConvert(Convert.ToUInt64));
            dict.Add(typeof(double), WrapValueConvert(Convert.ToDouble));
            dict.Add(typeof(float), WrapValueConvert(Convert.ToSingle));
            dict.Add(typeof(decimal), WrapValueConvert(Convert.ToDecimal));
            dict.Add(typeof(bool), WrapValueConvert(Convert.ToBoolean));
            dict.Add(typeof(Guid), f => new Guid(f.ToString()));
            dict.Add(typeof(DateTime), f => Convert.ToDateTime(f));

            dict.Add(typeof(sbyte?), (o) => { return !o.IsSByte() ? null : WrapValueConvert(Convert.ToSByte)(o); });
            dict.Add(typeof(byte?), (o) => { return !o.IsByte() ? null : WrapValueConvert(Convert.ToByte)(o); });
            dict.Add(typeof(short?), (o) => { return !o.IsShort() ? null : WrapValueConvert(Convert.ToInt16)(o); });
            dict.Add(typeof(int?), (o) => { return !o.IsInt() ? null : WrapValueConvert(Convert.ToInt32)(o); });
            dict.Add(typeof(long?), (o) => { return !o.IsLong() ? null : WrapValueConvert(Convert.ToInt64)(o); });
            dict.Add(typeof(ushort?), (o) => { return !o.IsUShort() ? null : WrapValueConvert(Convert.ToUInt16)(o); });
            dict.Add(typeof(uint?), (o) => { return !o.IsUInt() ? null : WrapValueConvert(Convert.ToUInt32)(o); });
            dict.Add(typeof(ulong?), (o) => { return !o.IsULong() ? null : WrapValueConvert(Convert.ToUInt64)(o); });
            dict.Add(typeof(double?), (o) => { return !o.IsDouble() ? null : WrapValueConvert(Convert.ToDouble)(o); });
            dict.Add(typeof(float?), (o) => { return !o.IsFloat() ? null : WrapValueConvert(Convert.ToSingle)(o); });
            dict.Add(typeof(decimal?), (o) => { return !o.IsDecimal() ? null : WrapValueConvert(Convert.ToDecimal)(o); });
            dict.Add(typeof(bool?), (o) => { return !o.IsBool() ? null : WrapValueConvert(Convert.ToBoolean)(o); });
            dict.Add(typeof(Guid?), (o) => { if (!o.IsGuid()) { return null; } return new Guid(o.ToString()); });
            dict.Add(typeof(DateTime?), (o) => { if (!o.IsDateTime()) { return null; } return Convert.ToDateTime(o); });
            dict.Add(typeof(string), Convert.ToString);
        }

        private static Func<object, object> WrapValueConvert<T>(Func<object, T> input) where T : struct
        {
            return i =>
            {
                if (i == null || i is DBNull) { return null; }
                return input(i);
            };
        }

        public static bool CanConvertTo(this object obj, Type targetType)
        {
            return dict.ContainsKey(targetType);
        }

        public static T To<T>(this object obj)
        {
            return (T)To(obj, typeof(T));
        }

        public static T To<T>(this object obj, T defaultValue)
        {
            try
            {
                object value;
                if (TryConvertTo(obj, typeof(T), out value))
                {
                    return (T)value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool TryConvertTo<T>(this object obj, out T value)
        {
            object v;
            var ret = TryConvertTo(obj, typeof(T), out v);
            if (ret)
            {
                value = (T)v;
            }
            else
            {
                value = default(T);
            }
            return ret;
        }

        public static bool TryConvertTo(this object obj, Type targetType, out object value)
        {
            value = null;
            //if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            //{
            //    if (obj == null)
            //    {
            //        value = null;
            //        return true;
            //    }
            //    else
            //    {
            //        return TryConvertTo(obj, targetType.GetGenericArguments().First(), out value);
            //    }
            //}


            if (obj != null)
            {
                try
                {
                    if (obj.GetType() == targetType || targetType.IsAssignableFrom(obj.GetType()))
                    {
                        value = obj;
                    }
                    else if (dict.ContainsKey(targetType))
                    {
                        value = dict[targetType](obj);
                    }
                    else if (targetType.IsEnum)
                    {
                        value = Enum.Parse(targetType, obj.ToString(), true);
                    }
                    else if (targetType.IsArray && obj is string)
                    {
                        if (obj.ToString().StartsWith("[") && obj.ToString().EndsWith("]")) { return false; }
                        var elementType = targetType.GetElementType();
                        var items = obj.ToString().Split(',');
                        var target = Array.CreateInstance(elementType, items.Length);
                        for (var i = 0; i < items.Length; i++)
                        {
                            target.SetValue(To(items[i], elementType), i);
                        }

                        value = target;
                    }
                    else if (obj.GetType() == typeof(Newtonsoft.Json.Linq.JObject) || obj.GetType() == typeof(Newtonsoft.Json.Linq.JArray))
                    {
                        //Newtonsoft.Json.Linq.JObject jobj = (Newtonsoft.Json.Linq.JObject)obj;
                        value = obj.GetType().GetMethod("ToObject", new Type[] { }).MakeGenericMethod(targetType).Invoke(obj, null);
                    }
                    else
                    {
                        value = Convert.ChangeType(obj, targetType);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                if (!targetType.IsValueType)
                {
                    value = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static object To(this object obj, Type targetType)
        {
            object value;
            if (TryConvertTo(obj, targetType, out value))
            {
                return value;
            }
            else
            {
                throw new NotImplementedException($"无法将{obj?.ToString()}({obj?.GetType().Name})转换为{targetType.Name}");
            }
        }
    }
}
