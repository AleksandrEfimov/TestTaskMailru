using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;


namespace TestTaskMailru
{
    /// <summary>
    /// Класс управления настройками _webDriver. 
    /// </summary>
    internal static class ConfigWD
    {
        private static JObject _config;

        static ConfigWD()
        {
            Assembly assembly = typeof(ConfigWD).Assembly;
            var configWD = @"TestTaskMailru.manager.configWD.json";
            Stream stream = assembly.GetManifestResourceStream(configWD);
            using (StreamReader reader = new StreamReader(stream))
            {
                _config = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }
        }

        internal static TypeWD WebDriverType
        {
            get
            {
              switch (((string)_config[@"WebDriverType"]).ToLower())
              {
                    case "chrome":
                        return TypeWD.Chrome;
                    case "firefox":
                        return TypeWD.Firefox;
                    default:
                        throw new InvalidDataException("Указанный в файле конфигурации тип драйвера не поддерживается");
                }
            }
        }
        internal static TimeSpan WaitTimeout => TimeSpan.FromMilliseconds((int)_config[@"WaitTimeoutByDefault"]);
        internal static string UrlMainPage => (string)_config[@"UrlMainPage"];
        internal static string MailBoxUrl => (string)_config[@"MailBoxUrl"];

        internal static string UserLogin1 => (string)_config["user1_email"];
        internal static string UserPassword1 => (string)_config["user1_password"];
        internal static string UserLogin2 => (string)_config["user2_email"];
        internal static string UserPassword2 => (string)_config["user2_password"];



        #region Типы WebDriver`ов
        /// <summary>
        /// Доступные типы WebDriver
        /// </summary>
        public enum TypeWD
        { 
            /// <summary>
            /// ChromeDriver
            /// </summary>
            Chrome,
            /// <summary>
            /// GeckoDriver
            /// </summary>
            Firefox
        }
        #endregion
    }
}
