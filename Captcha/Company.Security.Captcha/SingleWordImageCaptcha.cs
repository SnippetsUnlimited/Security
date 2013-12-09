using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Company.Security.Captcha.Extensions;
using System.Drawing;

namespace Company.Security.Captcha.Framework
{
    public class SingleWordImageCaptcha : Captcha
    {
        private int _Seed = 1;

        public SingleWordImageCaptcha()
        {
        }

        public override void Randomize(int length)
        {
            string value = this.GetRandomWord(length, _Seed);
            this.Key = new string[] { value };

            int width = 400;
            int height = 150;
            int fontSize = 80;
            string text = this.Key[0];
            string fontFamily = "Ariel";
            string fontWhiteList = "";

            this.Image = CaptchaImageLibrary.CreateImage(width, height, fontSize, text, fontFamily, FontWrapFactor.Low, BackgroundNoiseLevel.Low, LineNoiseLevel.Low, fontWhiteList);
        }

        public override string[] Key { get; protected set; }

        public Image Image { get; set; }

    }
}
