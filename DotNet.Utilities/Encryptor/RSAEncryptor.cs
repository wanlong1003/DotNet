using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utilities.Encryptor
{
    /// <summary>
    /// RSA是一种【非对称加密算法】，由于其使用时只能是由公钥加密私钥解密所以也被称为【公钥加密算法】
    ///   主要用途：
    ///   1. 数据加密（公钥加密私钥解密）
    ///   2. 数据验证（私钥签名公钥验证）
    /// </summary>
    public class RSAEncryptor
    {
        /// <summary>
        /// 生成公钥和私钥
        /// </summary>
        /// <param name="privateKey">生成的私钥</param>
        /// <param name="publicKey">生成的公钥</param>
        public static void CreateKey(out string privateKey, out string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            privateKey = rsa.ToXmlString(true);  //私钥
            publicKey = rsa.ToXmlString(false);  //公钥
        }

        /// <summary> 
        /// RSA 公钥加密
        /// </summary> 
        /// <param name="plaintext" >明文</param> 
        /// <param name="publicKey" >公钥</param> 
        /// <returns>Base64密文</returns>
        public static string Encrypt(string plaintext, string publicKey)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(plaintext), publicKey));
        }

        /// <summary>
        /// RSA 公钥加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] plaintext, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            return rsa.Encrypt(plaintext, false);
        }

        /// <summary> 
        /// RSA 私钥解密
        /// </summary> 
        /// <param name="ciphertext">Base64密文</param> 
        /// <param name="privateKey">私钥</param> 
        /// <returns>明文</returns>
        public static string Decrypt(string ciphertext, string privateKey)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(ciphertext), privateKey));
        }

        /// <summary> 
        /// RSA 私钥解密
        /// </summary> 
        /// <param name="ciphertext">密文</param> 
        /// <param name="privateKey">私钥</param> 
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] ciphertext, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            return rsa.Decrypt(ciphertext, false);
        }

        /// <summary>
        /// RSA 私钥签名 
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>密文</returns>
        public static string Sign(string plaintext, string privateKey)
        {
            return Convert.ToBase64String(Sign(Encoding.UTF8.GetBytes(plaintext), "SHA1"));
        }

        /// <summary>
        /// RSA 私钥签名
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>密文</returns>
        public static byte[] Sign(byte[] plaintext, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            return rsa.SignData(plaintext, "SHA1");
        }


        /// <summary>
        /// RSA 公钥验证签名
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="ciphertext">密文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>是否验证通过</returns>
        public static bool Verify(string plaintext, string ciphertext, string publicKey)
        {
            return Verify(Encoding.UTF8.GetBytes(plaintext), Convert.FromBase64String(ciphertext), publicKey);
        }

        /// <summary>
        /// RSA 公钥验证签名
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="ciphertext">密文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>是否验证通过</returns>
        public static bool Verify(byte[] plaintext, byte[] ciphertext, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            return rsa.VerifyData(plaintext, "SHA1", ciphertext);
        }

    }
}
