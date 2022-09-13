using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Utilities.Data
{
    public static class DataEx
    {
        /// <summary>
        /// 交换指定两个行号的DataRow
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public static DataTable SwapRow(this DataTable dt, int index1, int index2)
        {
//             DataRow dr = dt.NewRow();
//             dr.ItemArray = dt.Rows[index1].ItemArray;
//             dt.Rows[index1].ItemArray = dt.Rows[index2].ItemArray;
//             dt.Rows[index2].ItemArray = dr.ItemArray;
//             return dt;
            return dt.SwapRow(dt.Rows[index1], dt.Rows[index2]);
        }
        public static DataTable SwapRow(this DataTable dt, DataRow dr1, DataRow dr2)
        {
            DataRow dr = dt.NewRow();
            dr.ItemArray = dr1.ItemArray;
            dr1.ItemArray = dr2.ItemArray;
            dr2.ItemArray = dr.ItemArray;
            return dt;
        }
        /// <summary>
        /// 将指定索引的DataRow移动到其它位置
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="srcIndex">移动前的位置</param>
        /// <param name="destIndex">移动后的位置</param>
        /// <returns></returns>
        public static DataTable MoveRow(this DataTable dt, int srcIndex, int destIndex)
        {
            return dt.MoveRow(dt.Rows[srcIndex], destIndex);
        }
        public static DataTable MoveRow(this DataTable dt, DataRow dr, int destIndex)
        {
            DataRow drTmp = dt.NewRow();
            drTmp.ItemArray = dr.ItemArray;
            dt.Rows.Remove(dr);
            if (destIndex > dt.Rows.Count - 1)
                dt.Rows.Add(drTmp);
            else
                dt.Rows.InsertAt(drTmp, destIndex);
            return dt;
        }
    }
}
