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
            _userLogin1 = ConfigWD.cfg.user1_email;
            _userPassword1 = ConfigWD.cfg.user1_password;
            _userLogin2 = ConfigWD.cfg.user2_email;
            _userPassword2 = ConfigWD.cfg.user2_password;
            _mainPageUrl = ConfigWD.cfg.MainPageUrl;
            _mailPageUrl = ConfigWD.cfg.MailBoxUrl;
        }

        [SetUp]
        public void Start()
        {
            _mainPageMethods = new MainPageMethods(_driver);
            _mailBoxMethods = new MailBoxPageMethods(_driver);
            _mainPageMethods.GoToUrl(ConfigWD.cfg.MainPageUrl);
            Assert.IsTrue(_mainPageMethods.GetUrl().Contains(ConfigWD.cfg.MainPageUrl), $"Переход на {ConfigWD.cfg.MainPageUrl} не состоялся.");
            
        }

        [Test(Description = "Тестирование отправки письма")]
        public void Test_01_SendReceiveEmail()
        {
            _mainPageMethods.GoToUrl(ConfigWD.cfg.MainPageUrl);
            _mainPageMethods.SignInEmailBox(ConfigWD.cfg.user1_email, ConfigWD.cfg.user1_password);
            _mailBoxMethods.WaitDisappearsOctopus();
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
