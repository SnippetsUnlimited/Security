using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.Security.Captcha.Persistence
{
    /// <summary>
    /// Provides framework to persist data. 
    /// </summary>
    public interface IPersistenceProvider
    {
        /// <summary>
        /// Returns user key for data store.
        /// </summary>
        /// <returns></returns>
        string GetSessionKey();

        /// <summary>
        /// Saves user data.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="key">Key for the data.</param>
        /// <param name="value">Data to be saved.</param>
        void SetValue<T>(string key, T value);

        /// <summary>
        /// Returns user data.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="key">Key for the data.</param>
        /// <param name="defaultValue">default value to be returned if the data does not exist.</param>
        /// <returns></returns>
        T GetValue<T>(string key, T defaultValue);
    }
}
