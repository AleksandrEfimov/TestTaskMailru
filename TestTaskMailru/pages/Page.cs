using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;


namespace TestTaskMailru
{
    public class Page
    {
        internal IWebDriver _webDriver;
        internal WebDriverWait wait;
        internal WebDriverWait waitShort;
        internal WebDriverWait waitLong;

        public Page(IWebDriver driver)
        {
            _webDriver = driver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public IWebElement FinderWithTime(By loc, int timeout = 1)
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeout));
            var el = _webDriver.FindElements(loc)[0];
            if (wait.Until(d => el.Displayed))
                return el;
            else return null;
        }

        /// <summary>
        /// Поиск элемента.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public IWebElement? FindElement(By loc) 
        {
            try
            {
                return _webDriver.FindElement(loc);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Поиск списка элементов.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public List<IWebElement> FindElements(By loc) => _webDriver.FindElements(loc).ToList();
        

    }
}
