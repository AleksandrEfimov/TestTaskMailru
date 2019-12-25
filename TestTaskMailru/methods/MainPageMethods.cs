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
    /// <summary>
    /// Главная страница с формой входа в электронную почту.
    /// </summary>
    class MainPageMethods : CommonMethods
    {
        MainPage _mainPage;
        CommonMethods _comMethods;

        /// <summary>
        /// Главная страница сайта с формой входа в электронную почту.
        /// </summary>
        /// <param name="wd"></param>
        public MainPageMethods(IWebDriver wd) : base(wd)
        {
            _mainPage = new MainPage(wd);
            _comMethods = new CommonMethods(wd);
        }

        /// <summary>
        /// Вход в почту.
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public bool SignInEmailBox(string login, string password)
        {
            _mainPage.SignInMailBox.loginInput?.SendKeys(login + Keys.Enter);
            WaitLong.Until(d => _mainPage.SignInMailBox.passwordInput.Displayed);
            _mainPage.SignInMailBox.passwordInput.SendKeys(password + Keys.Enter);
            WaitPageLoad("inbox");
            return _comMethods.GetUrl().Contains(ConfigWD.UrlMailBox) ? true : false;
        }
    }
}
