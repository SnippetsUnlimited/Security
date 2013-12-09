using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Company.Security.Captcha.Persistence;

namespace Company.Security.Captcha.Framework
{
    public class CaptchaGenerator
    {
        private const string numberoftries = "numberoftries";
        private const string strength = "strength";
        private const string captchasecret = "captchasecret";
        private const string caseinsensitive = "caseinsensitive";
        private const string allowednumberoftries = "allowednumberoftries";
        
        private IPersistenceProvider _PersistenceProvider = null;

        public CaptchaGenerator(IPersistenceProvider persistenceProvider)
        {
            _PersistenceProvider = persistenceProvider;
        }

        public IPersistenceProvider PersistenceProvider
        {
            get
            {
                return _PersistenceProvider;
            }
        }

        public bool CaseInsensitive
        {
            get
            {
                return _PersistenceProvider.GetValue<bool>(caseinsensitive, false);
            }
            set
            {
                _PersistenceProvider.SetValue<bool>(caseinsensitive, value);
            }
        }

        public int NumberOfTries
        {
            get
            {
                return _PersistenceProvider.GetValue<int>(numberoftries, 0);
            }
            protected set
            {
                _PersistenceProvider.SetValue<int>(numberoftries, value);
            }
        }

        public int AllowedNumberOfTries
        {
            get
            {
                return _PersistenceProvider.GetValue<int>(allowednumberoftries, 5);
            }
            protected set
            {
                _PersistenceProvider.SetValue<int>(allowednumberoftries, value);
            }
        }

        public int[] StrengthLevels
        {
            get
            {
                return new int[] { 4, 4, 4, 6, 6, 6 };
            }
        }

        public int CurrentStrength
        {
            get
            {
                if (NumberOfTries >= StrengthLevels.Length)
                {
                    return StrengthLevels[StrengthLevels.Length - 1];
                }
                return StrengthLevels[this.NumberOfTries];
            }
        }

        public T GenerateCaptcha<T>()
            where T: ICaptcha, new()
        {
            T captcha = new T();
            captcha.Randomize(this.CurrentStrength);
            _PersistenceProvider.SetValue<string[]>(captchasecret, captcha.Key);
            return captcha;
        }

        public bool ValidateInput(string[] input)
        {
            this.NumberOfTries++;

            if (this.AllowedNumberOfTries < this.NumberOfTries)
            {
                throw new MaximumTriesReachedException("Maximum allowed tries reached.");
            }

            string[] originalKey = _PersistenceProvider.GetValue<string[]>(captchasecret, null);

            if (originalKey != null && input != null && originalKey.Length == input.Length)
            {
                for (int x = 0; x < originalKey.Length; x++)
                {
                    if (originalKey[x] == null || input[x] == null || string.Compare(originalKey[x], input[x], this.CaseInsensitive) != 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }














    }
}
