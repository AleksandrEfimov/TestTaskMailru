using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestTaskMailru
{
    /// <summary>
    /// Базовый класс для тестов.
    /// </summary>
    public abstract class BaseTests
    {
        protected readonly Random Rnd = new Random();
        protected IWebDriver _driver;

        [SetUp]
        protected void BaseSetUp()
        {
            _driver = FactoryWD.InitWebDriver(ConfigWD.TypeWD.Chrome);
        }

        [TearDown]
        protected void BaseTearDown()
        {
            _driver?.Quit();
        }

        public abstract void PrepareData();
    }
}
