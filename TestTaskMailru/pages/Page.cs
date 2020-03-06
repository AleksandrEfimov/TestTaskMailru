using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace TestTaskMailru
{
    public class Page
    {
        protected IWebDriver _webDriver;
        internal WebDriverWait wait;

        public Page(IWebDriver driver)
        {
            _webDriver = driver;
            wait = new WebDriverWait(driver, ConfigWD.WaitTimeout);
        }

        /// <summary>
        /// Сообщение с предложением устанвоки почтового Виджета.
        /// </summary>
        public IWebElement Bubble => _webDriver.FindElement(By.Id("bubble-home-install"));

        /// <summary>
        /// Крестик окошка "Уведомлять о новых письмах".
        /// </summary>
        public IList<IWebElement> NotifyANewLetterCloseCross => _webDriver.FindElements(By.XPath("//*/div[@data-test-id='close']"));

        /// <summary>
        /// Крестик окошка "Добавить номера телефона".
        /// </summary>
        public IList<IWebElement> NotifyAddPhoneCloseCross => _webDriver.FindElements(By.XPath("//*/div[@data-test-id='cross']"));
        
        /// <summary>
        /// Крестик уведомления "Браузер Атом"
        /// </summary>
        public IList<IWebElement> NotifyAtomBrowserCloseCross => _webDriver.FindElements(By.XPath("//*/span[@id='bubble-home-close']"));
    }
}
