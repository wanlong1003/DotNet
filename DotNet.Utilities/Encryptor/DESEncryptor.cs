using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DotNet.Utilities.Encryptor
{
    /// <summary>
    /// DES是一种对称加密算法，常用于对数据进行加密
    /// </summary>
    public class DESEncryptor
    {
        /// <summary>
        /// 初始化向量
        /// </summary>
        private static byte[] rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString">明文</param>
        /// <param name="encryptKey">密钥(必须为8位)</param>
        /// <returns>密文</returns>
        public static string EncryptString(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(encryptString);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                        {
                            cs.Write(byteArray, 0, byteArray.Length);
                            cs.FlushFinalBlock();
                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <param name="decryptKey">密钥(必须为8位)</param>
        /// <returns>明文</returns>
        public static string DecryptString(string decryptString, string decryptKey)
        {
            try
            {
                byte[] byKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
                {
                    byte[] byteArray = Convert.FromBase64String(decryptString);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, provider.CreateDecryptor(byKey, rgbIV), CryptoStreamMode.Write))
                        {
                            cs.Write(byteArray, 0, byteArray.Length);
                            cs.FlushFinalBlock();
                            return Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch
            {
                return decryptString;
            }
        }
    }
}
