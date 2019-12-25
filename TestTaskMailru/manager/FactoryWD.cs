using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace TestTaskMailru
{
    class FactoryWD
    {
        /// <summary>
        /// Возвращает драйвер согласно указанному в конфиге.
        /// </summary>
        /// <param name="driverType"></param>
        /// <returns>экземпляр _webDriver</returns>
        public static IWebDriver InitWebDriver(ConfigWD.TypeWD driverType)
        {
            IWebDriver webDriver;
            switch (driverType)
            {
                case ConfigWD.TypeWD.Chrome:
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments("start-maximized");
                        webDriver = new ChromeDriver(chromeOptions);
                        break;
                    }
                case ConfigWD.TypeWD.Firefox:
                    {
                        webDriver = new FirefoxDriver();
                        break;
                    }

                default:
                    throw new ArgumentException($"Неизвестный тип WebDriver'а: {driverType}");
            }
            return webDriver;
            
        }
    }
}
