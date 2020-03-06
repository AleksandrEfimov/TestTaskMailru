using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Net.NetworkInformation;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestTaskMailru
{
    /// <summary>
    /// Базовый класс для тестов. Получает экземпляр webDriver.
    /// </summary>
    public abstract class BaseTests
    {
        protected IWebDriver _driver;

        [OneTimeSetUp]
        protected void BaseSetUp()
        {
            _driver = FactoryWD.InitWebDriver(ConfigWD.GetWebDriverType());
            Assert.IsNotNull(_driver, "Экземпляр WebDriver не прошёл инициализацию");
            using (Ping ping = new Ping())
            {
                PingReply pingReply;
                // Exception _ex;
                try
                {
                    pingReply = ping.Send(ConfigWD.Host, ConfigWD.PingTimeout);
                    Assert.AreEqual(IPStatus.Success, pingReply.Status, $"Тестируемый сайт не пингуется за {ConfigWD.PingTimeout}");
                }
                catch (PingException ex) 
                {
                    // _ex = ex;
                    throw new Exception("Провален тест на предварительный пинг: " + ex);
                }
            }
        }

        [OneTimeTearDown]
        protected void BaseTearDown()
        {
            _driver?.Quit();
        }

        public abstract void PrepareData();
    }
}
