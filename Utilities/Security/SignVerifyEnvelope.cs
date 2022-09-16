using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.IO;

namespace Utilities.EzLicense
{
    public class SignVerifyEnvelope
    { 
        public SignVerifyEnvelope()
        {
        //    RSACryptoServiceProvider Key = new RSACryptoServiceProvider();
        //    string pubKey = Key.ToXmlString(false);
        //    Key.FromXmlString(pubKey);
        //    CreateSomeXml("test.xml");
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load("test.xml");
        //    SignedXml signedXml = new SignedXml(xmlDoc);
        //    signedXml.SigningKey = Key;
        //    Reference reference = new Reference();
        //    reference.Uri = "";
        //    XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
        //    reference.AddTransform(env);
        //    signedXml.AddReference(reference);
        //    signedXml.ComputeSignature();
        //    XmlElement xmlDigitalSignature = signedXml.GetXml();
        //    xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        //    xmlDoc.Save("license.xml");

             // Generate a signing key. 
            return;
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = "";
            RSACryptoServiceProvider Key = new RSACryptoServiceProvider(cspParams);
            string s =  IteRegist.getRNum();
            // Create an XML file to sign.
            CreateSomeXml("Sign.xml");

            // Sign the XML that was just created and save it in a 
            // new file.
            SignXmlFile("Sign.xml", "Lisense.xml", Key);
           

            // Verify the signature of the signed XML.
            bool result = VerifyXmlFile("");

            // Display the results of the signature verification to \
            // the console.
            if(result)
            {
               // Console.WriteLine("The XML signature is valid.");
            }
            else
            {
              //  Console.WriteLine("The XML signature is not valid.");
            }

        }
        public static bool  SignFile(string keyContainerName)
        {
              CspParameters cspParams = new CspParameters();
                cspParams.KeyContainerName = GetHash(keyContainerName);
                RSACryptoServiceProvider Key = new RSACryptoServiceProvider(cspParams);
                string s =  IteRegist.getRNum();
                CreateSomeXml("Sign.xml");

                // Sign the XML that was just created and save it in a 
                // new file.
                SignXmlFile("Sign.xml", "Lisense.xml", Key);
                return true;
        }
        
        // Sign an XML file and save the signature in a new file.
        public static void SignXmlFile(string FileName, string SignedFileName, RSA Key)
        {
            // Create a new XML document.
            XmlDocument doc = new XmlDocument();

            // Format the document to ignore white spaces.
            doc.PreserveWhitespace = false;

            // Load the passed XML file using it's name.
            doc.Load(new XmlTextReader(FileName));

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(doc);

            // Add the key to the SignedXml document. 
            signedXml.SigningKey = Key;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";
         
            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);


            // Add an RSAKeyValue KeyInfo (optional; helps recipient find key to validate).
            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new RSAKeyValue((RSA)Key));
            signedXml.KeyInfo = keyInfo;

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));


            if (doc.FirstChild is XmlDeclaration)
            {
                doc.RemoveChild(doc.FirstChild);
            }

            // Save the signed XML document to a file specified
            // using the passed string.
            XmlTextWriter xmltw = new XmlTextWriter(SignedFileName, new UTF8Encoding(false));
            doc.WriteTo(xmltw);
            xmltw.Close();
        }
        public static Boolean VerifyXmlFile( string KeyContainerName)
        {
            // Create a new XML document.
            try
            {String Name="Lisense.xml";
                XmlDocument xmlDocument = new XmlDocument();

                // Format using white spaces.
                xmlDocument.PreserveWhitespace = true;

                // Load the passed XML file into the document. 
                xmlDocument.Load(Name);

                // Create a new SignedXml object and pass it
                // the XML document class.
                SignedXml signedXml = new SignedXml(xmlDocument);

                // Find the "Signature" node and create a new
                // XmlNodeList object.
                XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Signature");

                // Load the signature node.
                signedXml.LoadXml((XmlElement)nodeList[0]);

                // Check the signature and return the result.

                CspParameters cspParams = new CspParameters();
                cspParams.KeyContainerName = KeyContainerName;
                RSACryptoServiceProvider Key = new RSACryptoServiceProvider(cspParams);
                return signedXml.CheckSignature(Key);
            }
            catch {}
            return false;
        }

        public static void CreateSomeXml(string FileName)
        {
            // Create a new XmlDocument object.
            XmlDocument document = new XmlDocument();

            // Create a new XmlNode object.
            XmlNode node = document.CreateNode(XmlNodeType.Element, "", "APP", "Sign");

            // Add some text to the node.
            node.InnerText = "signed.";

            // Append the node to the document.
            document.AppendChild(node);

            // Save the XML document to the file name specified.
            XmlTextWriter xmltw = new XmlTextWriter(FileName, new UTF8Encoding(false));
            document.WriteTo(xmltw);
            xmltw.Close();
        }




        /// <summary>
        /// 生成公私钥
        /// </summary>
        /// <param name="PrivateKeyPath"></param>
        /// <param name="PublicKeyPath"></param>
        public void RSAKey(string PrivateKeyPath, string PublicKeyPath)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                this.CreatePrivateKeyXML(PrivateKeyPath, provider.ToXmlString(true));
                this.CreatePublicKeyXML(PublicKeyPath, provider.ToXmlString(false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        /// <summary>
        /// 对原始数据进行MD5加密
        /// </summary>
        /// <param name="m_strSource">待加密数据</param>
        /// <returns>返回机密后的数据</returns>
        public static string GetHash(string m_strSource)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create("MD5");
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            byte[] inArray = algorithm.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="m_strEncryptString">MD5加密后的数据</param>
        /// <returns>RSA公钥加密后的数据</returns>
        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPublicKey);
                byte[] bytes = new UnicodeEncoding().GetBytes(m_strEncryptString);
                str2 = Convert.ToBase64String(provider.Encrypt(bytes, false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="m_strDecryptString">待解密的数据</param>
        /// <returns>解密后的结果</returns>
        public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPrivateKey);
                byte[] rgb = Convert.FromBase64String(m_strDecryptString);
                byte[] buffer2 = provider.Decrypt(rgb, false);
                str2 = new UnicodeEncoding().GetString(buffer2);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }
        /// <summary>
        /// 对MD5加密后的密文进行签名
        /// </summary>
        /// <param name="p_strKeyPrivate">私钥</param>
        /// <param name="m_strHashbyteSignature">MD5加密后的密文</param>
        /// <returns></returns>
        public string SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature)
        {
            byte[] rgbHash = Convert.FromBase64String(m_strHashbyteSignature);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("MD5");
            byte[] inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="p_strKeyPublic">公钥</param>
        /// <param name="p_strHashbyteDeformatter">待验证的用户名</param>
        /// <param name="p_strDeformatterData">注册码</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {
            try
            {
                byte[] rgbHash = Convert.FromBase64String(p_strHashbyteDeformatter);
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(p_strKeyPublic);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("MD5");
                byte[] rgbSignature = Convert.FromBase64String(p_strDeformatterData);
                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        ///// <summary>
        ///// 获取硬盘ID
        ///// </summary>
        ///// <returns>硬盘ID</returns>
        //public string GetHardID()
        //{
        //    string HDInfo = "";
        //    ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
        //    ManagementObjectCollection moc1 = cimobject1.GetInstances();
        //    foreach (ManagementObject mo in moc1)
        //    {
        //        HDInfo = (string)mo.Properties["Model"].Value;
        //    }
        //    return HDInfo;
        //}
        /// <summary>
        /// 读注册表中指定键的值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>返回键值</returns>
        private string ReadReg(string key)
        {
            string temp = "";
            try
            {
                RegistryKey myKey = Microsoft.Win32.Registry.LocalMachine;
                RegistryKey subKey = myKey.OpenSubKey(@"SOFTWARE/JX/Register");

                temp = subKey.GetValue(key).ToString();
                subKey.Close();
                myKey.Close();
                return temp;
            }
            catch (Exception)
            {
                throw;//可能没有此注册项;
            }

        }
        /// <summary>
        /// 创建注册表中指定的键和值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        private void WriteReg(string key, string value)
        {
            try
            {
                RegistryKey rootKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");
                rootKey.SetValue(key, value);
                rootKey.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建公钥文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="publickey"></param>
        public void CreatePublicKeyXML(string path, string publickey)
        {
            try
            {
                FileStream publickeyxml = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(publickeyxml);
                sw.WriteLine(publickey);
                sw.Close();
                publickeyxml.Close();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 创建私钥文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="privatekey"></param>
        public void CreatePrivateKeyXML(string path, string privatekey)
        {
            try
            {
                FileStream privatekeyxml = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(privatekeyxml);
                sw.WriteLine(privatekey);
                sw.Close();
                privatekeyxml.Close();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 读取公钥
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadPublicKey(string path)
        {
            StreamReader reader = new StreamReader(path);
            string publickey = reader.ReadToEnd();
            reader.Close();
            return publickey;
        }
        /// <summary>
        /// 读取私钥
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadPrivateKey(string path)
        {
            StreamReader reader = new StreamReader(path);
            string privatekey = reader.ReadToEnd();
            reader.Close();
            return privatekey;
        }
        /// <summary>
        /// 初始化注册表，程序运行时调用，在调用之前更新公钥xml
        /// </summary>
        /// <param name="path">公钥路径</param>
        public void InitialReg(string path)
        {
            Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");
            Random ra = new Random();
            string publickey = this.ReadPublicKey(path);
            if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE/JX/Register").ValueCount <= 0)
            {
                this.WriteReg("RegisterRandom", ra.Next(1, 100000).ToString());
                this.WriteReg("RegisterPublicKey", publickey);
            }
            else
            {
                this.WriteReg("RegisterPublicKey", publickey);
            }
        } 




    }



    
}
