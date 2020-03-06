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

        public CommonMethods CommonMethod;
        public MailBoxPage MailBoxPage;
        public MainPage MainPage;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="wd">webDriver</param>
        public MailBoxPageMethods(IWebDriver wd)
        {
            CommonMethod = new CommonMethods(wd);
            MailBoxPage = new MailBoxPage(wd);
            MainPage = new MainPage(wd);
        }

        /// <summary>
        /// Deprecated. Пример прогресса во времени. %)
        /// Ожидание исчезновения осьминога.
        /// </summary>
        /// <returns></returns>
        public void WaitDisappearsOctopus()
        {
            // CommonMethod.WaitLong.Until(d => !MailBoxPage.FuckingOctopusMailru.Displayed);
            try
            {
                CommonMethod.WaitIgnoreStaleNoSuch.Until(d => !MailBoxPage.FuckingOctopusMailru.Displayed);
            }
            catch (WebDriverTimeoutException)
            {
            }
        }


        #region 'Новое письмо'.
        /// <summary>
        /// Открывает форму заполнения полей нового письма.
        /// </summary>
        /// <returns><see cref="true"/>если открылась, <see cref="false"/> если нет.</returns>
        public bool OpenWriteLetterForm()
        {
            CommonMethod.WaitPageReady();
            CommonMethod.Wait.Until(d => MailBoxPage.BtnWriteLetter.Displayed);
            // на случай вылезания виджета.
            CommonMethod.SmartClick(MailBoxPage.BtnWriteLetter);
            return CommonMethod.WaitIgnoreStaleNoSuch.Until(d => MailBoxPage.CreatingLetter.Subject.Displayed);
        }

        /// <summary>
        /// Заполнение полей на форме написания письма.
        /// </summary>
        /// <param name="addresser">Кому.</param>
        /// <param name="subject">Тема.</param>
        /// <param name="body">Тело письма.</param>
        public void FillField(string addresser = "", string subject = "", string body = "")
        {
            if (addresser != string.Empty)
                MailBoxPage.CreatingLetter.ToWhom.Clear();
            MailBoxPage.CreatingLetter.ToWhom.SendKeys(addresser);
            if (subject != string.Empty)
                MailBoxPage.CreatingLetter.Subject.Clear();
            MailBoxPage.CreatingLetter.Subject.SendKeys(subject);
            if (body != string.Empty)
                MailBoxPage.CreatingLetter.Body.Clear();
            MailBoxPage.CreatingLetter.Body.SendKeys(body);
        }

        /// <summary>
        /// Клик по кнопке "Отправить"
        /// </summary>
        /// <returns></returns>
        public bool ToSendLetterClick()
        {
            MailBoxPage.CreatingLetter.SendBtn.Click();
            var res = CommonMethod.Wait.Until(d => MailBoxPage.CreatingLetter.FormSentSuccess.Displayed);
            // CommonMethod.WaitLong.Until( d => !MailBoxPage.newLetter.FormSentSuccess.Enabled);
            return res;
        }

        /// <summary>
        /// Email контакта на форме успешной отправки.
        /// </summary>
        /// <returns></returns>
        public string SuccessSentContactEmail()
        {
            return MailBoxPage.SuccessSentFormContact.Text;
        }

        /// <summary>
        /// Переход к отправленнному письму через линк на форме успешной отправки.
        /// </summary>
        public void ClickLinkSentLetter()
        {
            CommonMethod.SmartClick(MailBoxPage.SuccessSentFormLink);
        }
        #endregion

        #region 'Навигация по папкам'.
        /// <summary>
        /// Переход в папку Входящие.
        /// </summary>
        /// <returns></returns>
        public bool OpenInboxFolder()
        {
            MailBoxPage.FldInbox.Click();
            //CommonMethod.WaitPageReady();
            return IsOpenInboxFolder();
        }

        /// <summary>
        /// Открыта ли папка Отправленные? 
        /// </summary>
        public bool IsOpenInboxFolder()
        {
            return CommonMethod.Wait.Until(d => CommonMethod.GetUrl().EndsWith("/inbox/"));
        }

        /// <summary>
        /// Переход в папку Отправленные.
        /// </summary>
        public bool OpenSentFolder()
        {
            MailBoxPage.FldInbox.Click();
            return IsOpenSentFolder();
        }

        /// <summary>
        /// Открыта ли папка Отправленные?
        /// </summary>
        public bool IsOpenSentFolder()
        {
            CommonMethod.WaitPageReady();
            return CommonMethod.Wait.Until(d => CommonMethod.GetUrl().Contains("/sent/"));
        }
        #endregion

        #region 'Работа с письмами'
        /// <summary>
        /// Наличие письма с заданными параметрами в списке писем.
        /// </summary>
        /// <param name="email">Email контакта из письма</param>
        /// <param name="subject">Тема письма</param>
        /// <param name="body">Начало письма</param>
        /// <returns>Результат сравнения переданных параметров и полей писем в текущей папке</returns>
        public bool IsLetterExistInLettersList(string email = "", string subject = "", string body = "")
        {
            var letter = MailBoxPage.Letters.First(d => d.LetterSubject.Equals(subject));
            if (letter != null)
                return (letter.LetterEmail.Contains(email) && letter.LetterFirstBodyWords.Contains(body));
            else
                return false;
        }

        /// <summary>
        /// Тема письма.
        /// </summary>
        public string SubjectEmail()
        {
            return MailBoxPage.CurrentLetter.Subject.Text;
        }

        /// <summary>
        /// Email получателя.
        /// </summary>
        public string RecipientsEmail()
        {
            return MailBoxPage.CurrentLetter.Recipients.Text;
        }

        /// <summary>
        /// Автор письма.
        /// </summary>
        public string AuthorEmail()
        {
            return MailBoxPage.CurrentLetter.Author.Text;
        }
        
        /// <summary>
        /// Тело письма.
        /// </summary>
        public string BodyEmail()
        {
            return MailBoxPage.CurrentLetter.Body.Text;
        }

        #endregion

        /// <summary>
        /// Выход из аккаунта MailBoxPage.
        /// </summary>
        /// <returns></returns>
        internal bool SignOut()
        {
            MailBoxPage.LogOut.Click();
            CommonMethod.Wait.Until(d => d.Url.EndsWith("from=logout"));
            // CommonMethod.WaitPageReady(ConfigWD.Host);
            return CommonMethod.Wait.Until(d => MainPage.LoginForm.EnterMailBoxLnk.Displayed);
        }

        
    }
}
