using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;


namespace TestTaskMailru
{
    class MainPage : Page
    {

        protected Page _page;

        private string _url;

        /// <summary>
        /// Главная страница.
        /// </summary>
        /// <param name="wd"></param>
        MainPage(IWebDriver wd) : base(wd)
        {
            _page = new Page(wd);
            _url = ConfigWD.MainPageUrl;
        }

        /// <summary>
        /// Класс описание формы для логина.
        /// </summary>
        class LoginMailBox
        {
            private Page _page;
            LoginMailBox(IWebDriver wd)
            {
                _page = new Page(wd);
            }

            /// <summary>
            /// Поле для ввода логина.
            /// </summary>
            public IWebElement loginInput => _page.FindElement(By.Id("mailbox:login"));

            /// <summary>
            /// Кнопка перехода к полю Пароль и Входа.
            /// </summary>
            public IWebElement loginSubmitBtn => _page.FindElement(By.Id("mailbox:submit"));

            /// <summary>
            /// Поле для ввода пароля
            /// </summary>
            public IWebElement passwordInput => _page.FindElement(By.Id("mailbox:password"));

            /// <summary>
            /// Линк Вход
            /// </summary>
            public IWebElement EnterMailBoxLnk => _page.FindElement(By.Id("PH_authLink"));

            /// <summary>
            /// Линк Выход
            /// </summary>
            public IWebElement LogOutLink => _page.FindElement(By.Id("PH_logoutLink"));

            /// <summary>
            /// Список доступных доменов.
            /// </summary>
            public IWebElement DomainList => _page.FindElement(By.Id("mailbox:domain"));
                 
        }

      
    }
}
