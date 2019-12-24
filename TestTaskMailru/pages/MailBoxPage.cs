using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;


namespace TestTaskMailru
{   
    /// <summary>
    /// Страница Email.
    /// </summary>
    public class MailBoxPage : Page
    {
        protected Page _page;
        public NewLetter newLetter;
                private string _url;

        /// <summary>
        /// Конструктор страницы Email.
        /// </summary>
        /// <param name="wd">WebDriver</param>
        public MailBoxPage(IWebDriver wd) : base(wd)
        {
            _page = new Page(wd);
            _url = ConfigWD.UrlMainPage;
            newLetter = new NewLetter(wd);
        }

        /// <summary>
        /// Список строк с письмами..
        /// </summary>
        public List<IWebElement> lettersList => _page.FindElements(By.CssSelector("div#app-canvas div.letter-list a.llc"));

        /// <summary>
        /// Линк "Выход".
        /// </summary>
        public IWebElement LogOut => _page.FindElement(By.Id("PH_logoutLink"));

        /*
        /// <summary>
        /// Выбор 1го в списке письма с нужным email
        /// </summary>
        /// <param name="email">Email отправителя</param>
        /// <returns></returns>
        // public IWebElement FindLetter(string email) => lettersList.First(d => d.FindElement(By.CssSelector("span.ll-crpt") Equals(email));
        */

        /// <summary>
        /// Список писем на странице с инфомацией об отправителе, теме, начале содержания письма. 
        /// </summary>
        public List<LetterRow> Letters => 
            _page.FindElements(By.CssSelector("div#app-canvas div.letter-list a.llc")).Select(letter => new LetterRow(letter)).ToList();


        /// <summary>
        /// Кнопка "Написать письмо"
        /// </summary>
        public IWebElement BtnWriteLetter => _page.FindElement(By.CssSelector("div.sidebar__header span.compose-button"));

        /// <summary>
        /// Изображение осьминога при загрузке страницы.
        /// </summary>
        public IWebElement FuckingOctopusMailru => _page.FindElement(By.Id("octopus-loader"));

        #region Папки 
        /// <summary>
        /// "Папка" папка
        /// </summary>
        private List<IWebElement> Folders => _page.FindElements(By.CssSelector("div#app-canvas div.nav-Folders a.nav__item"));

        /// <summary>
        /// Папка Входящие
        /// </summary>
        public IWebElement FldInbox => Folders.Single(d => d.GetAttribute("href").Equals(@"/inbox/"));
        /// <summary>
        /// Папка СоцСети
        /// </summary>
        public IWebElement FldSocial => Folders.Single(d => d.GetAttribute("href").Equals(@"/social/"));
        /// <summary>
        /// Папка Рассылка
        /// </summary>
        public IWebElement FldNewsLetters => Folders.Single(d => d.GetAttribute("href").Equals(@"/newsletters/"));
        /// <summary>
        /// Папка Отправленные
        /// </summary>
        public IWebElement FldSent => Folders.Single(d => d.GetAttribute("href").Equals(@"/sent/"));
        /// <summary>
        /// Папка Черновики
        /// </summary>
        public IWebElement FldDrafts => Folders.Single(d => d.GetAttribute("href").Equals(@"/drafts/"));
        /// <summary>
        /// Папка Спам
        /// </summary>
        public IWebElement FldSpamd => Folders.Single(d => d.GetAttribute("href").Equals(@"/spam/"));

        /// <summary>
        /// Папка Корзина
        /// </summary>
        public IWebElement FldTrash => Folders.Single(d => d.GetAttribute("href").Equals(@"/trash/"));

        #endregion


        /// <summary>
        /// Создание нового письма и действия с ним.
        /// </summary>
        public class NewLetter
        {
            Page _page;
            internal NewLetter(IWebDriver wd)
            {
                _page = new Page(wd);
            }

            /// <summary>
            /// Поле "Кому".
            /// </summary>
            public IWebElement ToWhom => 
                _page.FindElement(By.XPath("//div[starts-with(@class, 'fields_container')] // input[starts-with(@class, 'container')]"));

            /// <summary>
            /// Поле "Тема".
            /// </summary>
            public IWebElement Subject => _page.FindElement(By.XPath("//div[starts-with(@class, 'subject__container')] // input[starts-with(@class, 'container')]"));
            // public IWebElement Subject => _page.FindElement(By.Name("//input[@name='Subject']"));

            /// <summary>
            /// Поле для текста письма.
            /// </summary>
            public IWebElement Body =>
                _page.FindElement(By.XPath("//div[starts-with(@class, 'editable-container')] / div[starts-with(@class, 'editable')]"));

            /// <summary>
            /// Кнопка Отправить.
            /// </summary>
            public IWebElement SendBtn => 
                _page.FindElement(By.CssSelector("div.compose-app__footer > div.compose-app__buttons > span.button2.button2_base.button2_primary.button2_hover-support.js-shortcut"));

            /// <summary>
            /// Окно успешной отправки письма.
            /// </summary>
            public IWebElement FormSentSuccess => _page.FindElement(By.CssSelector("div.layer-sent-page"));

            
        }

        /// <summary>
        /// Краткая информация по письму отображаемая в списке писем.
        /// </summary>
        public class LetterRow
        {
            private IWebElement _row;
            
            /// <summary>
            /// Превью адресата, темы и боди из строки с письмом в папке "Входящие"
            /// </summary>
            /// <param name="letter"></param>
            public LetterRow(IWebElement letter)
            {
                _row = letter;
            }

            public string Sender => _row.FindElements(By.CssSelector("span.ll-crpt"))[0].GetAttribute("title");
            public string Subject => _row.FindElements(By.CssSelector("span.llc__subject"))[0].Text;
            public string Body => _row.FindElements(By.CssSelector("span.llc__snippet"))[0].Text;
        }


    }
}
