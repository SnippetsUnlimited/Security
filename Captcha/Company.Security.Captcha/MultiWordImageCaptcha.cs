using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Company.Security.Captcha.Extensions;
using System.Drawing;

namespace Company.Security.Captcha.Framework
{
    public class MultiWordImageCaptcha : Captcha
    {
        private int _Seed = 1;

        public MultiWordImageCaptcha()
        {
        }

        public override void Randomize(int length)
        {
            string value1 = this.GetRandomWord(length, _Seed);
            string value2 = this.GetRandomWord(length, _Seed);
            this.Key = new string[] { value1, value2 };

            int width = 400;
            int height = 150;
            int fontSize = 80;
            string text = this.Key[0] + this.Key[1];
            string fontFamily = "Ariel";
            string fontWhiteList = "";

            this.Image = CaptchaImageLibrary.CreateImage(width, height, fontSize, text, fontFamily, FontWrapFactor.Low, BackgroundNoiseLevel.Low, LineNoiseLevel.Low, fontWhiteList);
        }

        public override string[] Key { get; protected set; }

        public Image Image { get; set; }


    }
}
