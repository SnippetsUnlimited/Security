using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.Security.Captcha.Persistence
{
    public class StaticMemoryPersistanceProvider : IPersistenceProvider
    {

        private static Dictionary<string, Dictionary<string, object>> _staticDataStore = new Dictionary<string, Dictionary<string, object>>();
        private string _sessionID = null;

        public StaticMemoryPersistanceProvider(string sessionKey)
        {
            _sessionID = sessionKey;
        }

        public string GetSessionKey()
        {
            return _sessionID;
        }

        private Dictionary<string, object> UserDataStore
        {
            get
            {
                string key = GetSessionKey();

                if (!_staticDataStore.ContainsKey(key))
                {
                    _staticDataStore.Add(key, new Dictionary<string, object>());
                }

                return _staticDataStore[key];
            }
        }

        public void SetValue<T>(string key, T value)
        {
            if (UserDataStore.ContainsKey(key))
            {
                UserDataStore[key] = value;
            }
            else
            {
                UserDataStore.Add(key, value);
            }
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            if (UserDataStore.ContainsKey(key))
            {
                return (T)UserDataStore[key];
            }
            return defaultValue;
        }

    }
}