using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.Security.Captcha.Framework
{
    public class MaximumTriesReachedException : Exception
    {
        public MaximumTriesReachedException(string message)
            : base(message)
        {
        }
    }
}
