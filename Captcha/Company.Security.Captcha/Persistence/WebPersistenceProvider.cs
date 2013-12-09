using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.Security.Captcha.Persistence
{
    public class WebPersistenceProvider : IPersistenceProvider
    {
        public WebPersistenceProvider()
        {
        }

        public string GetSessionKey()
        {
            return System.Web.HttpContext.Current.Session.SessionID;
        }

        public void SetValue<T>(string key, T value)
        {
            System.Web.HttpContext.Current.Session[key] = value;
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            if (System.Web.HttpContext.Current.Session[key] == null)
            {
                System.Web.HttpContext.Current.Session[key] = defaultValue;
            }
            return (T)System.Web.HttpContext.Current.Session[key];
        }
    }
}
