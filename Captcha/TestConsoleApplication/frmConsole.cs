using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Company.Security.Captcha.Framework;
using Company.Security.Captcha.Persistence;
using System.IO;

namespace TestConsoleApplication
{
    public partial class frmConsole : Form
    {
        public frmConsole()
        {
            InitializeComponent();
        }


        private Image GetCaptchaImage1()
        {
            var persistence = new StaticMemoryPersistanceProvider("1234567");
            var generator = new CaptchaGenerator(persistence);
            generator.CaseInsensitive = false;

            var captcha = generator.GenerateCaptcha<SingleWordImageCaptcha>();
            return captcha.Image;

            //Use this code to return image stream instead of image
            //MemoryStream stream = new MemoryStream();
            //captcha.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //stream.Position = 0;
        }

        private Image GetCaptchaImage2()
        {
            var persistence = new StaticMemoryPersistanceProvider("1234567");
            var generator = new CaptchaGenerator(persistence);
            generator.CaseInsensitive = false;

            var captcha = generator.GenerateCaptcha<MultiWordImageCaptcha>();
            return captcha.Image;

            //Use this code to return image stream instead of image
            //MemoryStream stream = new MemoryStream();
            //captcha.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //stream.Position = 0;
        }

        private bool ValidateInput(string[] input)
        {
            var persistence = new StaticMemoryPersistanceProvider("1234567");
            var generator = new CaptchaGenerator(persistence);
            generator.CaseInsensitive = false;
            return generator.ValidateInput(input);
        }


        private void btnGenerateNew_Click(object sender, EventArgs e)
        {
            var captchaImage = this.GetCaptchaImage1();
            pbCaptcha.BackgroundImage = captchaImage;
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            string[] input = txtEntry.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (this.ValidateInput(input))
            {
                lblMessage.Text = "Success!!";
            }
            else
            {
                lblMessage.Text = "Error!!";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var captchaImage = this.GetCaptchaImage2();
            pbCaptcha.BackgroundImage = captchaImage;
        }
    }
}
