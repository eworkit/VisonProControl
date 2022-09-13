using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.ExMethod
{
    public static class CollectionEx
    { 
        public static void Foreach<T>(this IList lst,Action<T> a)
        {
            foreach(T i in lst)
            {
                a(i);
            }
        }
        public static T Find<T>(this IEnumerable<T> obj, Predicate<T> predicate)
        {
            foreach (var i in obj)
                if (predicate(i))
                    return i;
            return default(T);
        }
        public static void RemoveRange<T>(this IList<T> obj,IList<T> target)
        {
            foreach (var t in target)
                obj.Remove(t);
        }
        public static int RemoveAll<T>(this IList<T> obj, Predicate<T> match)
        {
            List<T> lst = obj as List<T>;
            if (lst != null)
                return lst.RemoveAll(match);
            var removeobjs = obj.Where(p => match(p)).ToList();
            int n = 0;
            foreach (var t in removeobjs)
            {
                if (obj.Remove(t))
                    n++;
            }
            return n;
        }
        /// <summary>
        /// 判断两个List的元素是否相同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="one"></param>
        /// <param name="another"></param>
        /// <returns></returns>
        public static bool ListEquals<T>(this IEnumerable<T> one, IEnumerable<T> another,IEqualityComparer<T> compare=null)
        {
            if (one == null)
            {
                if (another == null || !another.Any())
                    return true;
                return false;
            }
            else if (another == null)
            {
                if (!one.Any())
                    return true;
            }
            if (one.Count() != another.Count()) return false;
            return !(one.Except(another, compare)).Any();
        }
        public static bool HasElement<T>(this IEnumerable<T> s)
        {
            return s != null && s.Any();
        }
        /// <summary>
        /// 将ICollection元素复制到Array中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c">要复制的元素</param>
        /// <param name="arrayIndex">array中从零开始的索引，将从此处开始复制</param>
        /// <returns>复制后的数组</returns>
        public static T[] Copy<T>(this ICollection<T> c, int arrayIndex = 0)// where T:new()
        {
            if (c == null)
                return null;
            if (c.Count == 0)
                return new T[0];
            var t = new T[c.Count];
            c.CopyTo(t, arrayIndex);
            return t;
        }
        public static IList<T> Clone<T>(this ICollection<T> collection) where T:ICloneable
        {
            if (collection == null)
                return null;
            return collection.Select(c => (T) c.Clone()).ToList();
        }
        public static IEnumerable<T> TakeEnd<T>(this IEnumerable<T> source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            int c = source.Count();
            if (count >= c)
                return source;
            return source.Skip(source.Count() - count).ToList();
        }

        /// <summary>
        /// 扩展Add方法，如果给定的key存在，则更新值，否则Add
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="d"></param>
        /// <param name="k"></param>
        /// <param name="v"></param>
        /// <returns>0：更新，1：新增</returns>
        public static int AddX<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey k, TValue v)
        {
            if (d.ContainsKey(k))
            {
                d[k] = v;
                return 0;
            }
            d.Add(k, v);
            return 1;
        }
        public static void AddRangeOverride<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd)
        {
            dicToAdd.ForEach(x => dic[x.Key] = x.Value);
        }

        public static void AddRangeNewOnly<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd)
        {
            dicToAdd.ForEach(x => { if (!dic.ContainsKey(x.Key)) dic.Add(x.Key, x.Value); });
        }

        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd)
        {
            dicToAdd.ForEach(x => dic.Add(x.Key, x.Value));
        }
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd,Predicate<KeyValuePair<TKey,TValue>> where)
        {
            dicToAdd.ForEach(x => { if (where(x)) dic.Add(x.Key, x.Value); });
        }

        public static bool ContainsKeys<TKey, TValue>(this Dictionary<TKey, TValue> dic, IEnumerable<TKey> keys)
        {
            bool result = false;
            keys.ForEach(x => { result = dic.ContainsKey(x); return result; }); 
            return result;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static bool ForEach<T>(this IEnumerable<T> source,Predicate<T> breakPredicate)
        {
            foreach (var item in source)
            {
                if (breakPredicate(item))
                    return true;
            }
            return false;
        }
        public static void RemoveAll<K, V>(this IDictionary<K, V> dict, Predicate<KeyValuePair<K,V>> match)
        {
            foreach (var key in dict.ToArray().Where(key => match( key)))
                dict.Remove(key); 
        } 
    }
}
