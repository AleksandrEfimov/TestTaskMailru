using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;


// namespace TestTaskMailru.manager
namespace TestTaskMailru
{
    /// <summary>
    /// Базовый класс управления WebDriver.
    /// </summary>
    class Driver
    {

        /// <summary>
        /// Driver.
        /// </summary>
        public readonly IWebDriver WebDriver;

        /// <summary>
        /// JavaScript executer.
        /// </summary>
        public readonly IJavaScriptExecutor Js;

        /// <summary>
        ///  Явное ожидание для основных операций. 
        /// </summary>
        public readonly WebDriverWait Wait;


        public Driver()
        {
            WebDriver = GetDriver();
            Js = (IJavaScriptExecutor)WebDriver;
            Wait = new WebDriverWait(WebDriver, ConfigWD.WaitTimeout);
         }

        /// <summary>
        /// Возвращает экземпляр _webDriver
        /// </summary>
        /// <returns></returns>
        public static IWebDriver GetDriver()
        {
            switch (ConfigWD.WebDriverType)
            {
                case ConfigWD.TypeWD.Chrome:
                    return new ChromeDriver();
                case ConfigWD.TypeWD.Firefox:
                    return new FirefoxDriver();
                default:
                    throw new Exception("Не поддерживваемый тип драйвера");
            }
        }


       
    }
}
