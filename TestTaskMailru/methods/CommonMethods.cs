using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestTaskMailru
{
    class CommonMethods
    {
        private Page _page;
        public IWebDriver WebDriver;
        public IJavaScriptExecutor Js;
        public WebDriverWait Wait;
        public WebDriverWait WaitIgnoreStaleNoSuch;
        public WebDriverWait WaitIgnoreStaleNoSuchShort;

        public CommonMethods(IWebDriver wd)
        {
            WebDriver = wd;
            Wait = new WebDriverWait(WebDriver, ConfigWD.WaitTimeout);
            WaitIgnoreStaleNoSuch = new WebDriverWait(WebDriver, ConfigWD.WaitTimeout);
            WaitIgnoreStaleNoSuch.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
            WaitIgnoreStaleNoSuchShort = new WebDriverWait(WebDriver, ConfigWD.WaitTimeout);
            WaitIgnoreStaleNoSuchShort.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
            Js = (IJavaScriptExecutor)WebDriver;
            _page = new Page(wd);
        }

        /// <summary>
        /// Ожидание загрузки страницы, 
        /// </summary>
        /// <param name="url"></param>
        public void WaitPageReady(string url = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    Wait.Until(driver => driver.Url.Contains(url));
                }
                Wait.Until(driver => Js.ExecuteScript("return document.readyState").ToString().Equals("complete"));
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception($"Страница {url} полностью не загрузилась за {Wait.Timeout.Seconds} секунд.");
            }
        }
        
        // <summary>
        /// Получить Url текущей страницы.
        /// </summary>
        /// <returns></returns>
        public string GetUrl() => WebDriver.Url;

        /// <summary>
        /// Перейти на переданный url.
        /// </summary>
        /// <param name="url"></param>
        public void GoToUrl(string url) => WebDriver.Navigate().GoToUrl("http://" +url);

        /// <summary>
        /// Клик с "докликом".
        /// </summary>
        /// <param name="element">IWebElement ссылка на элемент</param>
        public void SmartClick(IWebElement element)
        {
            try
            {
                Wait.Until(driver => element.Enabled && element.Displayed);
                element.Click();
            }
            catch (ElementClickInterceptedException _ex)
            {
                Js.ExecuteScript("arguments[0].click();", element);
            }
        }

        /// <summary>
        /// Закрытие окошек-уведомлений "Браузер Atom", "Телефон", "Уведомления о новых письмах в браузере".
        /// В случае тестовой среды, они скорее всего были бы отключены.
        /// </summary>
        public void CloseNotifyIfExist()
        {
            if(_page.NotifyAddPhoneCloseCross.Count > 0)
            {
                _page.NotifyAddPhoneCloseCross[0].Click();
            }
            if (_page.NotifyANewLetterCloseCross.Count > 0)
            {
                _page.NotifyANewLetterCloseCross[0].Click();
            }
            if ( _page.NotifyAtomBrowserCloseCross.Count > 0)
            {
                _page.NotifyAtomBrowserCloseCross[0].Click();
            }
        }

        
    }
}
