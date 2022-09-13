using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.ExMethod;

namespace VisionControl
{
    internal class ByteConverter
    {
        public static byte[] ToSocketBytes(string text, bool hex)
        {
            return hex ?
             text.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => (byte)x.ToIntHex()).ToArray() :
                 System.Text.Encoding.UTF8.GetBytes(text);
        }
        public static string ToSocketString(byte[] data, bool hex)
        {
            return  hex ?
                string.Join(" ", data.Select(x => x.ToString("X2"))) :
                System.Text.ASCIIEncoding.UTF8.GetString(data);
        }
    }
}
