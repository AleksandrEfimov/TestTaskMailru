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
        private string _userPassword1;
        private string _userPassword2;
        private string _mainPageUrl;
        private string _mailPageUrl;
        private string _subject;
        private string _body;
        private MainPageMethods _mainPageMethods;
        private MailBoxPageMethods _mailBoxMethods;
        private Random rnd;

        [OneTimeSetUp]
        public override void PrepareData()
        {
            rnd = new Random();
            _subject = "subject" + rnd.Next(1000);
            _body = "body" + rnd.Next(1000);
            _userLogin1 = ConfigWD.UserLogin1;
            _userPassword1 = ConfigWD.UserPassword1;
            _userLogin2 = ConfigWD.UserLogin2;
            _userPassword2 = ConfigWD.UserPassword2;
            _mainPageUrl = ConfigWD.UrlMainPage;
            _mailPageUrl = ConfigWD.UrlMailBox;
        }

        [SetUp]
        public void Start()
        {
            _mainPageMethods = new MainPageMethods(_driver);
            _mailBoxMethods = new MailBoxPageMethods(_driver);
            _mainPageMethods.GoToUrl(_mainPageUrl);
            Assert.IsTrue(_mainPageMethods.GetUrl().Contains(_mainPageUrl), $"Переход на {_mainPageUrl} не состоялся.");
            
        }

        [Test(Description = "Тестирование отправки письма")]
        public void Test_01_SendReceiveEmail()
        {
            _mainPageMethods.SignInEmailBox(_userLogin1, _userPassword1);
            _mailBoxMethods.WaitDisappearsOctopus();
            _mailBoxMethods.OpenWriteLetterForm();
            _mailBoxMethods.FillField(_userLogin2, _subject, _body);
            _mailBoxMethods.SendLetterClick();
            _mailBoxMethods.SignOut();

            _mainPageMethods.SignInEmailBox(_userLogin2,_userPassword2);
            _mailBoxMethods.OpenInboxFolder();
            Assert.IsTrue(_mailBoxMethods.IsLetterExistInBox(_userLogin1, _subject, _body), "Тест не обнаружил во входящих письма с требуемыми отправителем, темой или телом письма");
        }




    }
}
