using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace Face
{
    public class BaiduFace
    {
        private static string BAIDU_APPKEY
        {
            get
            {
                return ConfigurationManager.AppSettings["BAIDU_APPKEY"];
            }
        }

        /// <summary>
        /// 注册人脸
        /// </summary>
        /// <param name="username">此用户的照片</param>
        /// <param name="imageFileName"></param>
        /// <returns></returns>
        public static  string FaceRegist(string username, Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);

                // MessageBox.Show("转换成功!");
                var imgData64 = Convert.ToBase64String(arr);
                string url = "http://apis.baidu.com/idl_baidu/faceverifyservice/face_register";
                string param = "{\"params\": [{"
                    + "\"username\":\"" + username + "\","
                      + "\"cmdid\":\"1000\","
                      + "\"logid\": \"12345\","
                      + "\"appid\": \"" + BAIDU_APPKEY + "\","
                      + "\"clientip\":\"127.0.0.1\","
                      + "\"type\":\"st_groupverify\","
                      + "\"groupid\": \"0\","
                      + "\"versionnum\": \"1.0.0.1\","
                      + "\"images\": ["
                      + "\"" + imgData64 + "\""
                      + "]}],\"jsonrpc\": \"2.0\","
                      + "\"method\": \"Register\","
                      + "\"id\" : \"0\"}";
                string result = SendRequest(url, param);
                return result;
            }
        }

        /// <summary>
        /// 是否人脸
        /// </summary>
        /// <param name="username">此用户的照片</param>
        /// <param name="imageFileName"></param>
        /// <returns></returns>
        public static string ValidateFace(string username, Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);

                // MessageBox.Show("转换成功!");
                var imgData64 = Convert.ToBase64String(arr);
                string url = "http://apis.baidu.com/idl_baidu/faceverifyservice/face_verify";
                string param = "{\"params\": [{"
                  + "\"username\":\"" + username + "\","
                     + "\"cmdid\":\"1000\","
                     + "\"logid\": \"12345\","
                       + "\"appid\": \"" + BAIDU_APPKEY + "\","
                     + "\"clientip\":\"127.0.0.1\","
                     + "\"type\":\"st_groupverify\","
                     + "\"groupid\": \"0\","
                     + "\"versionnum\": \"1.0.0.1\","
                     + "\"images\": ["
                     + "\"" + imgData64 + "\""
                     + "]}],\"jsonrpc\": \"2.0\","
                     + "\"method\": \"Verify\","
                     + "\"id\" : \"0\"}";

                return SendRequest(url, param);
            }

        }

        /// <summary>
        /// 识别人脸
        /// </summary>
        /// <param name="imageFileName"></param>
        /// <returns></returns>
        public static string IdentifyFace(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                var imgData64 = Convert.ToBase64String(arr);
                string url = "http://apis.baidu.com/idl_baidu/faceverifyservice/face_recognition";

                string param = "{\"params\": [{"
                    + "\"cmdid\":\"2002\","
                    + "\"logid\": \"12345\","
                    + "\"appid\": \"" + BAIDU_APPKEY + "\","
                    + "\"clientip\":\"127.0.0.1\","
                    + "\"type\":\"st_groupverify\","
                    + "\"groupid\": \"0\","
                    + "\"versionnum\": \"1.0.0.1\","
                    + "\"images\": ["
                    + "\"" + imgData64 + "\""
                    + "]}],\"jsonrpc\": \"2.0\","
                    + "\"method\": \"Verify\","
                    + "\"id\" : \"0\"}";

                return SendRequest(url, param);
            }
        }

        /// <summary>
        /// 发送HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="param">请求的参数</param>
        /// <returns>请求结果</returns>
        private static string SendRequest(string url, string param)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            // 添加header
            request.Headers.Add("apikey", "a6eab24b4e36be0b5fdbae93cc92bfae");
            request.ContentType = "application/x-www-form-urlencoded";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }

    }


}
