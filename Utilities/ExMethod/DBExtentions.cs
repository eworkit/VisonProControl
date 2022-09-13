using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Utilities.ExMethod
{
    public static class DBExtentions
    {
        public static T[]  GetColumnValues<T> (this DataRowCollection Rows, int Column)
        {
            T[] arr=new T[Rows.Count];
            //int i=0;
            //foreach(DataRow r in Rows)
            //{
            //    arr[i++] = (T)r[Column];
            //}
            return arr;
        }
        public static T[] GetColumnValues<T>(this DataRowCollection Rows, string Column)
        {
            T[] arr = new T[Rows.Count];
            //int i = 0;
            //foreach (DataRow r in Rows)
            //{
            //    arr[i++] = (T)r[Column];
            //}
            return arr;
        }
    }
}
