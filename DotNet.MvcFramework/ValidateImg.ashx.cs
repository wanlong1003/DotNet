using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace DotNet.MvcFramework
{
    /// <summary>
    /// 生成验证码
    /// </summary>
    public class ValidateImg : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ClearContent();
            context.Response.ContentType = "image/jpeg";
            string code = CreateCode(5);
            context.Session["ValidateCode"] = code;
            Bitmap image = CreateImage(code);
            image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /*产生验证码*/
        public string CreateCode(int codeLength)
        {
            string so = "1,2,3,4,5,6,7,8,9,0";
            string[] strArr = so.Split(',');
            string code = "";
            Random rand = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                code += strArr[rand.Next(0, strArr.Length)];
            }
            return code;
        }

        /*产生验证图片*/
        public Bitmap CreateImage(string code)
        {

            Bitmap image = new Bitmap(90, 30);

            Graphics g = Graphics.FromImage(image);
            WebColorConverter ww = new WebColorConverter();
            g.Clear(Color.White);

            Random random = new Random();
            ////画图片的背景噪音线
            for (int i = 0; i < 25; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            Font font = new Font("Arial", 18, FontStyle.Bold | FontStyle.Italic);
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);

            g.DrawString(code, font, brush, 0, 0);

            ////画图片的前景噪音点
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            //画图片的边框线
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            try
            {
                return image;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                g.Dispose();
            }
        }

    }
}