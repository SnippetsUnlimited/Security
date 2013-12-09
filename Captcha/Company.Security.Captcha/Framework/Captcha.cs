using System;

namespace Company.Security.Captcha.Framework
{
    public abstract class Captcha : ICaptcha
    {
        public abstract string[] Key { get; protected set; }

        public abstract void Randomize(int length);
    }
}
