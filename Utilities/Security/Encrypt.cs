using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utilities.Security
{
    public class DESEncrypt
    {
        //static readonly string key = "or7kj0sv";
        private static byte[] byKey = { 0xA2, 0x5B, 0xDF, 0x79, 0x30, 0xC1, 0x4E, 0xCA };
        private static byte[] byIV = { 0xA2, 0x5B, 0xDF, 0x79, 0x30, 0xC1, 0x4E, 0xCA };
        public static string Encode(string data)
        {
            //  string s = System.Text.ASCIIEncoding.ASCII.GetString(byKey);
            //  byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(s);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            cryptoProvider.Mode = CipherMode.ECB;
            cryptoProvider.Padding = PaddingMode.Zeros;
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        public static string Decode(string data)
        {
            // byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 8));           
            try
            {
                var byEnc = Convert.FromBase64String(data);
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                cryptoProvider.Mode = CipherMode.ECB;
                cryptoProvider.Padding = PaddingMode.Zeros;
                MemoryStream ms = new MemoryStream(byEnc);
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cst);
                return sr.ReadToEnd().TrimEnd('\0');
            }
            catch
            {
                return null;
            }
        }
    }
}
