using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Company.Security.Captcha.Extensions
{
    public static class CaptchaHelper
    {
        private static int _SecondarySeed = 0;

        public static string GetRandomWord(this Company.Security.Captcha.Framework.Captcha instance, int length, int seed)
        {
            var stream = System.Reflection.Assembly.GetAssembly(typeof(CaptchaHelper)).GetManifestResourceStream("Company.Security.Captcha.Resources.SamplingString.txt");
            var reader = new System.IO.StreamReader(stream);
            string text = reader.ReadToEnd();
            int textLength = text.Length;

            CaptchaHelper._SecondarySeed += seed;
            CaptchaHelper._SecondarySeed = _SecondarySeed % 9999999;
            var r = new Random(CaptchaHelper._SecondarySeed);

            int randomStart = r.Next() % (textLength - length);
            string captchaString = text.Substring(randomStart, length);

            return captchaString;
        }


    }
}
