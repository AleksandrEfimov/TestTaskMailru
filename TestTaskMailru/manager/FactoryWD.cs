using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TestTaskMailru
{
    abstract class FactoryWD
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
                        chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                        //Stopwatch sw = new Stopwatch();
                        //sw.Start();
                        webDriver = new ChromeDriver(chromeOptions);
                        //sw.Stop();
                        break;
                    }
                case ConfigWD.TypeWD.Firefox:
                    {
                        FirefoxOptions ffOptions = new FirefoxOptions();
                        ffOptions.AddArguments("start-maximized");
                        ffOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                        webDriver = new FirefoxDriver(ffOptions);
                        break;
                    }

                default:
                    throw new ArgumentException($"Неизвестный тип WebDriver'а: {driverType}");
            }

            webDriver.Manage().Timeouts().ImplicitWait = ConfigWD.WaitImplicit;
            webDriver.Manage().Timeouts().PageLoad = ConfigWD.WaitPageLoad;
            webDriver.Manage().Timeouts().AsynchronousJavaScript = ConfigWD.WaitTimeout;

            return webDriver;
        }
    }
}
