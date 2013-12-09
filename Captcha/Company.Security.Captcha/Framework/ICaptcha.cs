using System;

namespace Company.Security.Captcha.Framework
{
    /// <summary>
    /// Provides framework to build and validate captcha
    /// </summary>
    public interface ICaptcha// : ISerializable
    {
        string[] Key { get; }
        void Randomize(int length);
    }
}
