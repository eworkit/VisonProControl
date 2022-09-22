using System.Windows.Forms;
using Utilities;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Utilities.ExMethod
{
    public static class CommonEx   
    {           
        public static string ToMyString(this DateTime t)
        {
            return t.ToString("yyyy/MM/dd HH:mm:ss");
        }
        public static string ToMyString(this DateTime? t, string defaultValue = "")
        {
            if (t.HasValue)
                return t.Value.ToString("yyyy/MM/dd HH:mm:ss");
            return defaultValue;
        }
        public static string ToMyDateTimeString(this object obj, string defaultValue = "")
        {
            try
            {
                DateTime t = Convert.ToDateTime(obj);
                return t.ToMyString();
            }
            catch
            {
                return defaultValue;
            }
        }
        public static int ToInt32(this object obj, int? defaultValue =null )
        {
            if (obj == null && defaultValue.HasValue) return defaultValue.Value;
            try { return Convert.ToInt32(obj); }
            catch (Exception ex){
                if (defaultValue == null)
                    throw ex;
                return defaultValue.Value; }
        }
        public static int ToIntHex(this string s, int? defaultValue=null)
        {
            try
            {
                if (s.StartsWith("0x") || s.StartsWith("0X"))
                    s = s.Remove(0, 2);
                return Convert.ToInt32(s, 16);
            }
            catch (Exception ex) {
                if (defaultValue == null)
                    throw ex;
            }
            return defaultValue.Value;
        }
        public static uint ToUIntHex(this string s, uint? defaultValue = null)
        {
            try
            {
                if (s.StartsWith("0x") || s.StartsWith("0X"))
                    s = s.Remove(0, 2);
                return Convert.ToUInt32(s, 16);
            }
             catch (Exception ex) {
                if (defaultValue == null)
                    throw ex;
            }
            return defaultValue.Value;
        }
        public static int ToInt(this Color obj, int defaultValue = 0)
        {
            if (obj == null) return defaultValue;
            try { return (obj.B << 16) | (ushort)(((ushort)obj.G << 8) | obj.R); }
            catch { return defaultValue; }
        }

        public static uint ToUInt32(this string obj, uint defaultValue = (uint)0)
        {
            if (obj == null) return defaultValue;
            uint n = defaultValue;
            if (uint.TryParse(obj, out n))
                return n;
            return defaultValue; 
        }
        public static int ToInt32(this string obj, int defaultValue = 0)
        {
            if (obj == null) return defaultValue;
            int n = defaultValue;
            if (int.TryParse(obj, out n))
                return n;
            return defaultValue; 
        }
        public static int? ToInt32X(this string obj, int? defaultValue = null)
        {
            if (obj == null) return defaultValue;
            int n ;
            if (int.TryParse(obj, out n))
                return n;
            return defaultValue;
        }
        public static uint ToUInt32(this object obj, uint defaultValue = (uint)0)
        {
            if (obj == null) return defaultValue;
            try { return Convert.ToUInt32(obj); }
            catch { return defaultValue; }
        }
        public static long ToInt64(this object obj, long defaultValue = 0)
        {
            if (obj == null) return defaultValue;
            try { return Convert.ToInt64(obj); }
            catch { return defaultValue; }
        }
        public static ulong ToUInt64(this object obj, ulong defaultValue = 0)
        {
            if (obj == null) return defaultValue;
            try { return Convert.ToUInt64(obj); }
            catch { return defaultValue; }
        }
        public static double ObjToDouble(this object obj, int defaultValue = 0)
        {
            if (obj == null) return defaultValue;
            try { return Convert.ToDouble(obj); }
            catch (FormatException ex) { return defaultValue; }
            catch (System.InvalidCastException ex) { return defaultValue; }
        }
        public static double ToDouble(this string obj, double defaultValue = 0)
        {
            if (obj == null) return defaultValue;
            double n = defaultValue;
            if (double.TryParse(obj, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out n))
                return n;
            return defaultValue;
        }
        public static double? ToDoubleX(this string obj, double? defaultValue = null)
        {
            if (obj == null) return defaultValue;
            double n;
            if (double.TryParse(obj, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out n))
                return n;
            return defaultValue;
        }
        public static DateTime ToDateTime(this object obj, string defaultValue)
        {
            try
            {
                DateTime t = Convert.ToDateTime(obj);
                return t;
            }
            catch
            {
                DateTime t;
                DateTime.TryParse(defaultValue, out t);
                return t;
            }
        }
        public static DateTime ToDateTime(this object obj, DateTime defaultValue=default(DateTime))
        {
            try
            {
                DateTime t = Convert.ToDateTime(obj);
                return t;
            }
            catch
            {
                return defaultValue;
            }
        }
        public static DateTime ToDateTime(this string obj, DateTime defaultValue = default(DateTime))
        {
            DateTime t ;
            return DateTime.TryParse(obj, out t) ? t : defaultValue;
        }
        public static DateTime? ToDateTimeN(this object obj)
        {
            try
            {
                var t = Convert.ToDateTime(obj);
                return t;
            }
            catch
            {
                // ignored
            }
            return null;
        }
        public static void Write(this FileStream f, object value)
        {
            f.Write(SerializeHelper.rawSerialize(value), 0, System.Runtime.InteropServices.Marshal.SizeOf(value));
        }
        public static void WriteInt32(this FileStream f, int value)
        {
            f.Write(BitConverter.GetBytes(value), 0, 4);
        }
        public static void WriteInt16(this FileStream f, int value)
        {
            f.Write(BitConverter.GetBytes(value), 0, 2);
        }
        public static void WriteString(this FileStream f, string value)
        {
            f.Write(System.Text.Encoding.Default.GetBytes(value), 0, value.Length);
        }
        public static T Read<T>(this FileStream f) where T : struct
        {
            byte[] chanBytes = new byte[System.Runtime.InteropServices.Marshal.SizeOf(typeof(T))];
            f.Read(chanBytes, 0, chanBytes.Length);
            return SerializeHelper.rawDeserialize<T>(chanBytes);
        }

        public static T DeepClone<T>(this T t) where T : ICloneable
        {
            using (Stream objectStream = new MemoryStream())
            {
                try
                {
                    System.Runtime.Serialization.IFormatter formatter =
                        new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    formatter.Serialize(objectStream, t);
                    objectStream.Seek(0, SeekOrigin.Begin);
                    return (T) formatter.Deserialize(objectStream);
                }
                catch (Exception ex)
                {
                }
                return default(T);
            }
        }
    
        public static TChild CopyFromBase<TChild,TParent>(this TChild child, TParent parent) where TChild :TParent
        {
            var ParentType = typeof(TParent);

            var Properties = ParentType.GetProperties();

            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }
            return child;
        }
        public static int IndexofNoCase(this IEnumerable<string> src,string obj)
        {
            if (src == null)
                return -1;
            int i=0;
            foreach(var s in src)
            {
                if (s.EqualsNoCase(obj))
                    return i;
                i++;
            }
            return -1;
        }
        public static Tuple<int, int> IndexofNoCase(this IEnumerable<string> src, string s1,string s2)
        {
            int n1=-1, n2 = -1;
            if (src != null)
            {
                int i = 0;
                foreach (var s in src)
                {
                    if (s.EqualsNoCase(s1))
                    {
                        n1 = i;
                        if (n2 > -1)
                            break;
                    }
                    else if (s.EqualsNoCase(s2))
                    {
                        n2 = i;
                        if (n1 > -1)
                            break;
                    }
                    i++;
                }
            }
            return new Tuple<int, int>(n1, n2);
        }

        public static void Do(this EventHandler a,object o,EventArgs e)
        {
            if (a != null) a(o, e);
        }
        public static void Do(this Action a)
        {
            if (a != null) a();
        }
        public static void Do<T>(this Action<T> a, T c)
        {
            if (a != null) a(c);
        }
        public static void Do<T1, T2>(this Action<T1, T2> a, T1 c1, T2 c2)
        {
            if (a != null) a(c1, c2);
        }
        public static T Do<T>(this Func<T> a)
        {
            if (a != null) return a();
            return default(T);
        }
        public static short Hi(this int n)
        {
            return (short)(n >> 16);
        }
        public static short Low(this int n)
        {
            return (short)(n & 0xFFFF);
        }
        public static byte Hi(this short n)
        {
            return (byte)(n >> 8);
        }
        public static byte Low(this short n)
        {
            return (byte)n;
        }
      

        public static GDataValue SafeValue(this   GDataValue v)
        {
            if (v.HasValue)
                return v;
            return 0;
        }
        public static object ConvertTo(this object value, Type type)
        {
            return System.ComponentModel.TypeDescriptor.GetConverter(type).ConvertFrom(value);
        }
        public static void MergerArray(this int[] first, int[] second)
        {
            var result = new int[first.Length + second.Length];
            first.CopyTo(result, 0);
            second.CopyTo(result, first.Length);
            first = result;
        }

        public static bool In<T>(this T t, IEnumerable<T> arr,IEqualityComparer<T> comparer)
        {
            return arr.Contains(t, comparer);
        }
        public static bool In<T>(this T t, IEnumerable<T> arr)
        {
            return arr.Contains(t );
        }
        public static bool In<T>(this T t,params T[] arr)
        {
            return arr.Contains(t);
        }
        public static bool In<T>(this T t, IEqualityComparer<T> comparer, params T[] arr)
        {
            return arr.Contains(t,comparer);
        }

        public static void ThenInvoke(this bool b, Action a)
        {
            if (b)
            {
                a?.Invoke();
            }
        }
        public static void ThenInvoke(this bool? b, Action a)
        {
            if (b == true)
            {
                a?.Invoke();
            }
        }
        public static string GetDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var objs = field.GetCustomAttribute(typeof(DescriptionAttribute));
            var descriptionAttribute = (DescriptionAttribute)objs;
            return descriptionAttribute?.Description??enumValue.ToString();
        }
    }
}