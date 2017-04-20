using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Face
{
    public partial class MainForm : Form
    {
        private string key;
        private string secret;
        private bool isRun;
        private int sleep;

        public MainForm()
        {
            InitializeComponent();
            key = System.Configuration.ConfigurationManager.AppSettings["key"];
            secret = System.Configuration.ConfigurationManager.AppSettings["secret"];
            isRun = false;
            sleep = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                label1.Text = open.FileName;
                pictureBox1.Image = new Bitmap(open.FileName);
                Compare(pictureBox1.Image, pictureBox2.Image);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                label2.Text = open.FileName;
                pictureBox2.Image = new Bitmap(open.FileName);
                Compare(pictureBox1.Image, pictureBox2.Image);
            }
        }

        private void Compare(Image img1, Image img2)
        {
            var url = "https://api-cn.faceplusplus.com/facepp/v3/compare";
            if (img1 != null && img2 != null)
            {
                var form = new NameValueCollection();
                form.Add("api_key", key);
                form.Add("api_secret", secret);
                form.Add("image_base64_1", GetImgBase64(img1));
                form.Add("image_base64_2", GetImgBase64(img2));

                using (var client = new WebClient())
                {
                    var result = client.UploadValues(url, form);
                    var str = Encoding.UTF8.GetString(result);
                    WriteLog(str);
                }
            }
        }

        delegate void DelegateWriteLog(string msg);
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new DelegateWriteLog(WriteLog), msg);
            }
            else
            {
                richTextBox1.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "]["+Thread.CurrentThread.ManagedThreadId+"]" + msg + "\r\n");
                richTextBox1.ScrollToCaret();
            }
        }

        /// <summary>
        /// 对图片进行Base64编码，返回编码后的字符串
        /// </summary>
        /// <param name="img"></param>
        /// <returns>Base64编码</returns>
        private string GetImgBase64(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                return Convert.ToBase64String(arr);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isRun)
            {
                isRun = false;
            }
            else
            {
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("请选择图片文件路径");
                    return;
                }

                sleep = (int)numericUpDown2.Value;
                for (var i = 0; i < numericUpDown1.Value; i++)
                {
                    Thread thread = new Thread(Exec);
                    thread.IsBackground = true;
                    thread.Start();
                }
                isRun = true;
            }
            if (isRun)
            {
                button3.Text = "停止压力测试";
            }
            else
            {
                button3.Text = "启动压力测试";
            }
        }
        
        private void Exec()
        {
            var path = textBox1.Text;
            var images = Directory.GetFiles(path);
            Random random = new Random();

            while (isRun)
            {
                try
                {
                    var img1 = new Bitmap(images[random.Next(0, images.Length)]);
                    var img2 = new Bitmap(images[random.Next(0, images.Length)]);
                    
                    Compare(img1, img2);

                    //百度测试 旧接口
                    //string regist_Face = BaiduFace.FaceRegist("123", img1);
                    //WriteLog(regist_Face);
                    //string asdad = BaiduFace.IdentifyFace(img2);
                    //WriteLog(asdad);

                    Thread.Sleep(sleep);

                }
                catch(Exception ex)
                {
                    WriteLog(ex.Message);
                }               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.SelectedPath;
            }
        }
    }
}