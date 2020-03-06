using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;


namespace TestTaskMailru
{
    public class MainPage
    {
        private IWebDriver _wd;
        /// <summary>
        /// Реализует вход в почту.
        /// </summary>
        public LoginMailBoxForm LoginForm;

        /// <summary>
        /// Главная страница.
        /// </summary>
        /// <param name="wd"></param>
        public MainPage(IWebDriver wd)
        {
            _wd = wd;
            LoginForm = new LoginMailBoxForm(wd);
        }

        /// <summary>
        /// Класс описание формы для логина.
        /// </summary>
        public class LoginMailBoxForm
        {
            private IWebDriver _wd;
            public LoginMailBoxForm(IWebDriver wd)
            {
                _wd = wd;
            }

            /// <summary>
            /// Поле для ввода логина.
            /// </summary>
            public IWebElement loginInput => _wd.FindElement(By.Id("mailbox:login"));

            /// <summary>
            /// Кнопка перехода к полю Пароль и Входа.
            /// </summary>
            public IWebElement loginSubmitBtn => _wd.FindElement(By.Id("mailbox:submit"));

            /// <summary>
            /// Поле для ввода пароля
            /// </summary>
            public IWebElement passwordInput => _wd.FindElement(By.Id("mailbox:password"));

            /// <summary>
            /// Линк Вход
            /// </summary>
            public IWebElement EnterMailBoxLnk => _wd.FindElement(By.Id("PH_authLink"));

            /// <summary>
            /// Линк Выход
            /// </summary>
            public IWebElement LogOutLink => _wd.FindElement(By.Id("PH_logoutLink"));

            /// <summary>
            /// Список доступных доменов.
            /// </summary>
            public IWebElement DomainList => _wd.FindElement(By.Id("mailbox:domain"));
        }
    }
}
