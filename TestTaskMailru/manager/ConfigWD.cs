using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestTaskMailru
{
    /// <summary>
    /// Класс управления настройками _webDriver. 
    /// </summary>
    internal static class ConfigWD
    {
        public static ConfigJSON cfg;

        static ConfigWD()
        {
            var configWD = @"configWD.json";
            if (File.Exists(configWD))
            {
                using (StreamReader r = new StreamReader(configWD))
                {
                    string json = r.ReadToEnd();
                    cfg = JsonConvert.DeserializeObject<ConfigJSON>(json);
                }
            }
        }

        public class ConfigJSON
        {
            public string WebDriverType { get; set; }
            public int WaitTimeoutByDefault { get; set; }
            public int WaitTimeoutShort { get; set; }
            public int WaitTimeoutLong { get; set; }
            public string MainPageUrl { get; set; }
            public string MailBoxUrl { get; set; }
            public string ChromeBrowserPath { get; set; }
            public string FireFoxPath { get; set; }
            public string user1_email { get; set; }
            public string user1_password { get; set; }
            public string user2_email { get; set; }
            public string user2_password { get; set; }
        }
                                    
        public static TypeWD GetWebDriverType()
        {
            get :
            {
              switch (cfg.WebDriverType.ToLower())
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
        internal static TimeSpan WaitTimeout => TimeSpan.FromSeconds((int)cfg.WaitTimeoutByDefault);
        internal static TimeSpan WaitTimeoutLong => TimeSpan.FromSeconds((int)cfg.WaitTimeoutLong);
        internal static string UrlMainPage => (string)cfg.MainPageUrl;
        internal static string MailBoxUrl => (string)cfg.MailBoxUrl;

        internal static string UserLogin1 => (string)cfg.user1_email;
        internal static string UserPassword1 => (string)cfg.user1_password;
        internal static string UserLogin2 => (string)cfg.user2_email;
        internal static string UserPassword2 => (string)cfg.user2_password;

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
