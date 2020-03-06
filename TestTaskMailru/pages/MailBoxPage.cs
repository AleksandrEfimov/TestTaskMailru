using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;


namespace TestTaskMailru
{   
    /// <summary>
    /// Страница Email.
    /// </summary>
    public class MailBoxPage
    {
        private static IWebDriver _wd;
        public NewLetter CreatingLetter;
        public ExistLetter CurrentLetter;

        /// <summary>
        /// Конструктор страницы Email.
        /// </summary>
        /// <param name="wd">WebDriver</param>
        public MailBoxPage(IWebDriver wd)
        {
            _wd = wd;
            CreatingLetter = new NewLetter(wd);
            CurrentLetter = new ExistLetter(wd);
        }

        /// <summary>
        /// Список строк с письмами..
        /// </summary>
        public List<IWebElement> lettersList => _wd.FindElements(By.CssSelector("div#app-canvas div.letter-list a.llc")).ToList();

        /// <summary>
        /// Линк "Выход".
        /// </summary>
        public IWebElement LogOut => _wd.FindElement(By.Id("PH_logoutLink"));

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
        internal List<LetterRowInFolder> Letters => 
            _wd.FindElements(By.CssSelector("div#app-canvas div.letter-list a.llc")).Select(letter => new LetterRowInFolder(letter)).ToList();


        /// <summary>
        /// Кнопка "Написать письмо"
        /// </summary>
        public IWebElement BtnWriteLetter => _wd.FindElement(By.CssSelector("div.sidebar__header span.compose-button"));

        /// <summary>
        /// Изображение осьминога при загрузке страницы.
        /// </summary>
        public IWebElement FuckingOctopusMailru => _wd.FindElement(By.Id("octopus-loader"));

        #region Папки 
        /// <summary>
        /// "Папка" папка
        /// </summary>
        private List<IWebElement> Folders => _wd.FindElements(By.CssSelector("div#app-canvas div.nav-folders a.nav__item")).ToList();

        /// <summary>
        /// Папка Входящие
        /// </summary>
        public IWebElement FldInbox => Folders.Single(d => d.GetAttribute("href").Contains("/inbox/"));
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
        /// Класс-описание формы для нового письма и действия с ним.
        /// </summary>
        public class NewLetter
        {
            IWebDriver _wd;
            internal NewLetter(IWebDriver wd)
            {
                _wd = wd;
            }

            /// <summary>
            /// Поле "Кому".
            /// </summary>
            public IWebElement ToWhom => 
                _wd.FindElement(By.XPath("//div[starts-with(@class, 'fields_container')] // input[starts-with(@class, 'container')]"));

            /// <summary>
            /// Поле "Тема".
            /// </summary>
            public IWebElement Subject => _wd.FindElement(By.XPath("//div[starts-with(@class, 'subject__container')] // input[starts-with(@class, 'container')]"));
            // public IWebElement LetterSubject => _wd.FindElement(By.Name("//input[@name='LetterSubject']"));

            /// <summary>
            /// Поле для текста письма.
            /// </summary>
            public IWebElement Body =>
                _wd.FindElement(By.XPath("//div[starts-with(@class, 'editable-container')] / div[starts-with(@class, 'editable')]"));

            /// <summary>
            /// Прикрепление файла.
            /// </summary>
            public IWebElement InputFile => _wd.FindElement(By.XPath("//input[contains(@class, 'desktopInput')]"));

            /// <summary>
            /// Кнопка Отправить.
            /// </summary>
            public IWebElement SendBtn => _wd.FindElement(By.CssSelector("div.compose-app__footer > div.compose-app__buttons > span.button2.button2_base.button2_primary.button2_hover-support.js-shortcut"));

            /// <summary>
            /// Окно успешной отправки письма.
            /// </summary>
            public IWebElement FormSentSuccess => _wd.FindElement(By.CssSelector("div.layer-sent-page"));

        }

        /// <summary>
        /// Класс-описание письма существующего в почтовом ящике.
        /// </summary>
        public class ExistLetter 
        {
            IWebDriver _wd;
            public ExistLetter(IWebDriver wd)
            {
                _wd = wd;
            }

            /// <summary>
            /// Тема.
            /// </summary>
            public IWebElement Subject => _wd.FindElement(By.CssSelector("h2.thread__subject"));

            /// <summary>
            /// Автор.
            /// </summary>
            public IWebElement Author => _wd.FindElement(By.CssSelector("div.letter__author span.letter-contact"));

            /// <summary>
            /// Адресат.
            /// </summary>
            public IWebElement Recipients => _wd.FindElement(By.CssSelector("div.letter__recipients span.letter-contact"));

            /// <summary>
            /// Текст.
            /// </summary>
            public IWebElement Body => _wd.FindElement(By.XPath("//div[contains(@id, 'BODY')]"));

        }



        /// <summary>
        /// Эквивалент строки из списка писем. Содержит адресат, тему, начало тела письма.
        /// </summary>
        internal class LetterRowInFolder
        {
            private IWebElement _row;
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="letter"></param>
            public LetterRowInFolder(IWebElement letter)
            {
                _row = letter;
            }

            /// <summary>
            /// Email кому или от кого письмо. Зависит от текущей папки.
            /// </summary>
            public string LetterEmail => _row.FindElements(By.CssSelector("span.ll-crpt"))[0].GetAttribute("title");
            
            /// <summary>
            /// Тема письма. 
            /// </summary>
            public string LetterSubject => _row.FindElements(By.CssSelector("span.llc__subject"))[0].Text;

            /// <summary>
            /// Первые слова из тела письма.
            /// </summary>            
            public string LetterFirstBodyWords => _row.FindElements(By.CssSelector("span.llc__snippet"))[0].Text;
        }

        /// <summary>
        /// Поле "Кому" на форме подтверждения отправки письма.
        /// </summary>
        public IWebElement SuccessSentFormContact => _wd.FindElement(By.XPath("//span[@class='layer-sent-page__contact-item']"));

        /// <summary>
        /// Ссылка на письмо в папке Отправленные на форме подтверждения успешной отправки письма.
        /// </summary>
        public IWebElement SuccessSentFormLink=> _wd.FindElement(By.CssSelector("a.layer__link"));

    }
}
