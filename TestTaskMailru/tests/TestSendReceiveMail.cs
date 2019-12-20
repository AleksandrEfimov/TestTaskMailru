using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;


namespace TestTaskMailru
{ 
    [TestFixture]
    public class TestSendReceiveMail : BaseTests
    {
        private string _userLogin1;
        private string _userLogin2;
        private string _userPasswo1;
        private string _userPasswo2;
        private string _subject;
        private string _body;
        private MainPageMethods _mainPageMethods;
        private MailBoxPageMethods _mailBoxMethods;
        private Random rnd;

        [OneTimeSetUp]
        public override void PrepareData()
        {
            _subject = "subject" + rnd.Next(1000);
            _body = "body" + rnd.Next(1000);
        }

        [SetUp]
        public void Start()
        {
            _mainPageMethods = new MainPageMethods(_driver);
            _mailBoxMethods = new MailBoxPageMethods(_driver);
            _mainPageMethods.GoToUrl(ConfigWD.UrlMainPage);
            Assert.IsTrue(_mainPageMethods.GetUrl().Equals(ConfigWD.UrlMainPage), $"Переход на {ConfigWD.UrlMainPage} не состоялся.");
        }

        [Test(Description = "Тестирование отправки письма")]
        public void Test_01_SendReceiveEmail()
        {
            _mainPageMethods.GoToUrl(ConfigWD.UrlMainPage);
            _mainPageMethods.SignInEmailBox(ConfigWD.UserLogin1, ConfigWD.UserPassword1);
            _mailBoxMethods.OpenWriteLetterForm();
            _mailBoxMethods.FillField(ConfigWD.UserLogin2, _subject, _body);
            _mailBoxMethods.SendLetterClick();
            _mailBoxMethods.SignOut();

            _mainPageMethods.SignInEmailBox(ConfigWD.UserLogin2, ConfigWD.UserPassword2);
            _mailBoxMethods.OpenInboxFolder();
            Assert.IsTrue(_mailBoxMethods.IsLetterExistInBox(ConfigWD.UserLogin1, _subject, _body), "Тест не обнаружил во входящих письма с требуемыми отправителем, темой или телом письма");
        }




    }
}
