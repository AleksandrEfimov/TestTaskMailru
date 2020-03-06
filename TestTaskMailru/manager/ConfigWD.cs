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
    public class ConfigWD
    {
        private static ConfigJSON _cfgJson;

        /// <summary>
        /// Конструктор.
        /// </summary>
        static ConfigWD()
        {
            var configWD = @"wd.config";
            if (File.Exists(configWD))
            {
                using (StreamReader r = new StreamReader(configWD))
                {
                    var json = r.ReadToEnd();
                    _cfgJson = JsonConvert.DeserializeObject<ConfigJSON>(json);
                }
            }
        }

        /// <summary>
        /// Класс описывающий конфигурационный файл.
        /// </summary>
        protected class ConfigJSON
        {
            public string WebDriverType { get; set; }
            public int WaitTimeoutByDefault { get; set; }
            public int WaitTimeoutShort { get; set; }
            public int WaitTimeoutLong { get; set; }
            public int WaitImplicit { get; set; }
            public int WaitPageLoad { get; set; }
            public int PingTimeout { get; set; }
            public string Host { get; set; }
            public string MailBoxUrl { get; set; }
            public string PathChromeBrowser { get; set; }
            public string PathFireFoxBrowser { get; set; }
            public string Email_user1 { get; set; }
            public string Password_user1 { get; set; }
            public string Name_user1 { get; set; }
            public string Family_user1 { get; set; }
            public string Email_user2 { get; set; }
            public string Password_user2 { get; set; }
        }

        /// <summary>
        /// Тип драйвера заданный в конфиге.
        /// </summary>
        /// <returns></returns>
        public static TypeWD GetWebDriverType()
        {
            // switch (_cfgJson.WebDriverType.ToLower())
            switch (_cfgJson.WebDriverType.ToLower())
            {
                case "chrome":
                    {
                        return TypeWD.Chrome;
                    }
                case "firefox":
                    {
                        return TypeWD.Firefox;
                    }
                default:
                    {
                        throw new InvalidDataException("Указанный в файле конфигурации тип драйвера не поддерживается");
                    }
            }
        }
        internal static TimeSpan WaitTimeout => TimeSpan.FromSeconds(_cfgJson.WaitTimeoutByDefault);
        internal static TimeSpan WaitTimeoutLong => TimeSpan.FromSeconds(_cfgJson.WaitTimeoutLong);
        internal static TimeSpan WaitImplicit => TimeSpan.FromSeconds(_cfgJson.WaitImplicit);
        internal static TimeSpan WaitPageLoad => TimeSpan.FromSeconds(_cfgJson.WaitPageLoad);
        internal static int PingTimeout => _cfgJson.PingTimeout;

        internal static string Host => _cfgJson.Host;
        internal static string UrlMailBox => _cfgJson.MailBoxUrl;

        internal static string EmailUser1 => _cfgJson.Email_user1;
        internal static string PasswordUser1 => _cfgJson.Password_user1;
        internal static string NameUser1 => _cfgJson.Name_user1;
        internal static string FamilyUser1 => _cfgJson.Family_user1;
        internal static string EmailUser2 => _cfgJson.Email_user2;
        internal static string PasswordUser2 => _cfgJson.Password_user2;
               
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
