using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DotNet.Utilities.Encryptor
{
    /// <summary>
    /// MD5
    ///   MD5是一种摘要算法，由于其是单向（不能解密）所以并不能称为一种加密算法，其主要用途是验证数据是否被修改
    /// </summary>
    public class MD5Encryptor
    {
        /// <summary>
        /// 计算字符串的MD5
        /// </summary>
        /// <param name="inputString">明文</param>
        /// <returns>MD5</returns>
        public static string String(string inputString)
        {
            byte[] byteArr = Encoding.Default.GetBytes(inputString);
            using (MemoryStream stream = new MemoryStream(byteArr))
            {
                return Stream(stream);
            }
        }

        /// <summary>
        /// 计算Stream的MD5
        /// </summary>
        /// <param name="inputStream">原始流</param>
        /// <returns>MD5</returns>
        public static string Stream(Stream inputStream)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] byteArr = md5.ComputeHash(inputStream);
                StringBuilder sb = new StringBuilder(32);
                for (int i = 0; i < byteArr.Length; i++)
                {
                    sb.Append(byteArr[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string File(string fileName)
        {
            using (FileStream file = new FileStream(fileName, FileMode.Open))
            {
                return Stream(file);
            }
        }

        public static string File(string fileName, bool isBig)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            int bufferSize = 1048576; // 缓冲区大小，1MB
            byte[] buff = new byte[bufferSize];

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                long offset = 0;
                while (offset < fs.Length)
                {
                    long readSize = bufferSize;
                    if (offset + readSize > fs.Length)
                    {
                        readSize = fs.Length - offset;
                    }

                    fs.Read(buff, 0, Convert.ToInt32(readSize)); // 读取一段数据到缓冲区

                    if (offset + readSize < fs.Length) // 不是最后一块
                    {
                        md5.TransformBlock(buff, 0, Convert.ToInt32(readSize), buff, 0);
                    }
                    else // 最后一块
                    {
                        md5.TransformFinalBlock(buff, 0, Convert.ToInt32(readSize));
                    }

                    offset += bufferSize;
                }

                fs.Close();
                byte[] result = md5.Hash;
                StringBuilder sb = new StringBuilder(32);
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }
                return sb.ToString();
            }


        }
    }
}
