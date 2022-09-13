using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Utilities.Data
{
    public class SerializeHelper
    {
        #region 可序列化对象到byte数组的相互转换
        /// <summary>
        /// 将可序列化对象转成Byte数组
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>返回相关数组</returns>
        public static byte[] ObjectToByteArray(object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            ms.Close();
            return ms.ToArray();
        }
        /// <summary>
        /// 将可序列化对象转成的byte数组还原为对象
        /// </summary>
        /// <param name="b">byte数组</param>
        /// <returns>相关对象</returns>
        public static object ByteArrayToObject(byte[] b)
        {
            if (b.Length == 0)
                return null;
            MemoryStream ms = new MemoryStream(b, 0, b.Length);
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(ms); // as datatable
        }
        #endregion

        #region 采用.net系统自带Gzip压缩类进行流压缩
        /// <summary>
        /// 压缩数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            if (data == null)
                return null;
            byte[] bData;
            MemoryStream ms = new MemoryStream();
            GZipStream stream = new GZipStream(ms, CompressionMode.Compress, true);
            stream.Write(data, 0, data.Length);
            stream.Close();
            stream.Dispose();
            //必须把stream流关闭才能返回ms流数据,不然数据会不完整
            //并且解压缩方法stream.Read(buffer, 0, buffer.Length)时会返回0
            bData = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return bData;
        }

        /// <summary>
        /// 解压数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            if (data == null)
                return null;
            byte[] bData;
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            GZipStream stream = new GZipStream(ms, CompressionMode.Decompress, true);
            byte[] buffer = new byte[1024];
            MemoryStream temp = new MemoryStream();
            int read = stream.Read(buffer, 0, buffer.Length);
            while (read > 0)
            {
                temp.Write(buffer, 0, read);
                read = stream.Read(buffer, 0, buffer.Length);
            }
            //必须把stream流关闭才能返回ms流数据,不然数据会不完整
            stream.Close();
            stream.Dispose();
            ms.Close();
            ms.Dispose();
            bData = temp.ToArray();
            temp.Close();
            temp.Dispose();
            return bData;
        }
        #endregion

        #region 采用ICSharpCode.SharpZipLib.GZip进行压缩
        /// <summary>
        /// 采用ICShareCode的Gzip类进行byte数组压缩
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        // public static byte[] ICGzipCompress(byte[] b)
        // {
        //     byte[] dataBuffer = new byte[4096];
        //     MemoryStream s = new MemoryStream();
        //     GZipOutputStream gz = new GZipOutputStream(s);
        // 
        //     StreamUtils.Copy(new MemoryStream(b), gz, dataBuffer);
        //     gz.Finish();
        //     byte[] z = new byte[gz.Length];
        //     z = s.ToArray();
        //     gz.Close();
        //     return z;
        // }
        /// <summary>
        /// 将采用ICShareCode的Gzip类压缩的byte数组解压缩
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        // public static byte[] ICGzipDecompress(byte[] b)
        // {
        //     byte[] dataBuffer = new byte[4096];
        // 
        //     MemoryStream u = new MemoryStream(b);
        //     Stream gi = new GZipInputStream(u);
        //     MemoryStream uu = new MemoryStream();
        //     StreamUtils.Copy(gi, uu, dataBuffer);
        // 
        //     byte[] z = new byte[uu.Length];
        //     z = uu.ToArray();
        //     gi.Dispose();
        //     return z;
        // 
        // }
        #endregion

        #region 采用ICSharpCode.SharpZipLib.Zip进行压缩
        /// <summary>
        /// 采用ICShareCode的Zip类进行byte数组压缩
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        // public static byte[] ICZipCompress(byte[] b)
        // {
        //     MemoryStream m = new MemoryStream();
        //     DeflaterOutputStream os = new DeflaterOutputStream(m);
        //     os.Write(b, 0, b.Length);
        //     os.Finish();
        //     byte[] z = m.ToArray();
        //     os.Dispose();
        // 
        //     return z;
        // }
        /// <summary>
        /// 将采用ICShareCode的Zip类压缩的数组解压缩
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        // public static byte[] ICZipDecompress(byte[] b)
        // {
        //     MemoryStream m = new MemoryStream();
        //     InflaterInputStream iis = new InflaterInputStream(new MemoryStream(b));
        //     StreamUtils.Copy(iis, m, new byte[4096]);
        //     byte[] z = m.ToArray();
        //     iis.Dispose();
        // 
        //     return z;
        // }
        #endregion
    }
}
