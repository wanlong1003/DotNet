using System.Net.Mail;

namespace DotNet.Utilities.Email
{
    /// <summary>
    /// 电子邮件帮助类
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="smtp">SMTP服务器地址</param>
        /// <param name="from">发件人地址</param>
        /// <param name="pwd">发件人密码</param>
        /// <param name="to">收件人地址</param>
        /// <param name="title">主题</param>
        /// <param name="body">正文</param>
        /// <param name="paths">附件列表</param>
        public static void SendMail(string smtp, string from, string pwd, string to, string title, string body, params string[] paths)
        {
            //创建smtpclient对象
            SmtpClient client = new SmtpClient();
            client.Host = smtp;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pwd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            //创建mailMessage对象
            MailMessage message = new MailMessage(from, to);
            message.Subject = title;

            //正文默认格式为html
            message.Body = body;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            //添加附件
            if (paths != null && paths.Length > 0)
            {
                foreach (string path in paths)
                {
                    Attachment data = new Attachment(path, System.Net.Mime.MediaTypeNames.Application.Octet);
                    message.Attachments.Add(data);
                }
            }
            client.Send(message);
        }

    }
}