using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras;

namespace TestTaskMailru
{
    /// <summary>
    /// Методы работы с элементами страницы email.
    /// </summary>
    class MailBoxPageMethods
    {

        public CommonMethods Common;
        public MailBoxPage MailBox;
        public MainPage _mainPage;
        private IWebDriver _webDriver;
        private WebDriverWait _wait;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="wd">webDriver</param>
        public MailBoxPageMethods(IWebDriver wd)
        {
            _webDriver = wd;
            _wait = new WebDriverWait(wd, TimeSpan.FromSeconds(10));
            Common = new CommonMethods(wd);
            MailBox = new MailBoxPage(wd);
            _mainPage = new MainPage(wd);
        }

        /// <summary>
        /// Открывает форму для написания письма.
        /// </summary>
        /// <returns><see cref="true"/>если открылась, <see cref="false"/> если нет.</returns>
        public bool OpenWriteLetterForm()
        {
            Common.WaitLong.Until(d => MailBox.BtnWriteLetter.Displayed);
            MailBox.BtnWriteLetter.Click();
            Thread.Sleep(5000);
            // return Common.WaitLong.Until(d => MailBox.newLetter.Subject.Displayed);
            return true;
        }

        /// <summary>
        /// Ожидание исчезновения осьминога.
        /// </summary>
        /// <returns></returns>
        public void WaitDisappearsOctopus()
        {
            // Common.WaitLong.Until(d => !MailBox.FuckingOctopusMailru.Displayed);
            _wait.Until(d => !MailBox.FuckingOctopusMailru.Displayed);
        }

        /// <summary>
        /// Метода заполнения формы написания письма.
        /// </summary>
        /// <param name="addresser">Кому.</param>
        /// <param name="subject">Тема.</param>
        /// <param name="body">Тело письма.</param>
        public void FillField(string addresser = "", string subject = "", string body = "")
        {
            if (addresser != string.Empty)
                MailBox.newLetter.ToWhom?.SendKeys(addresser);
            if (subject != string.Empty)
                MailBox.newLetter.Subject?.SendKeys(subject);
            if (body != string.Empty)
                MailBox.newLetter.Body?.SendKeys(body);
        }

        /// <summary>
        /// Выход из аккаунта MailBox.
        /// </summary>
        /// <returns></returns>
        internal bool SignOut()
        {
            MailBox.LogOut.Click();
            //Common.WaitPageLoad(ConfigWD.cfg.MainPageUrl);
            return Common.Wait.Until(d => _mainPage.SignInMailBox.EnterMailBoxLnk.Displayed);
        }

        /// <summary>
        /// Клик по кнопке "Отправить"
        /// </summary>
        /// <returns></returns>
        public bool SendLetterClick()
        {
            MailBox.newLetter.SendBtn.Click();
            // var res = Common.WaitLong.Until(d => MailBox.newLetter.FormSentSuccess.Displayed);
            // Common.WaitLong.Until( d => !MailBox.newLetter.FormSentSuccess.Enabled);
            return true;
        }

        public bool OpenInboxFolder()
        {
            MailBox.FldInbox.Click();
            //Common.WaitPageLoad();
            return Common.Wait.Until(d => Common.GetUrl().Contains("/inbox/"));
        }

        public bool IsLetterExistInBox(string addresser = "", string subject = "", string body = "")
        { 
            var letter = MailBox.Letters.First(d => d.Sender.Equals(addresser));
            if (letter != null)
                return letter.Subject.Equals(subject) && letter.Body.Contains(body);
            else 
                return false;
        }
    }
}
