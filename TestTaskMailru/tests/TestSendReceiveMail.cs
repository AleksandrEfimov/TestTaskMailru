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
        private string _userName1;
        private string _userFam1;
        private MainPageMethods _mainPageMethods;
        private MailBoxPageMethods _mailBoxMethods;

        [OneTimeSetUp]
        public override void PrepareData()
        {
            _subject = "subject" + Guid.NewGuid();
            _body = "body" + Guid.NewGuid();
            _userLogin1 = ConfigWD.EmailUser1;
            _userPassword1 = ConfigWD.PasswordUser1;
            _userName1 = ConfigWD.NameUser1;
            _userFam1 = ConfigWD.FamilyUser1;
            _userLogin2 = ConfigWD.EmailUser2;
            _userPassword2 = ConfigWD.PasswordUser2;
            _mainPageUrl = ConfigWD.Host;
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
            //_mailBoxMethods.WaitDisappearsOctopus();
            _mailBoxMethods.OpenWriteLetterForm();
            _mailBoxMethods.FillField(_userLogin2, _subject, _body);
            _mailBoxMethods.ToSendLetterClick();

            Assert.IsTrue(_mailBoxMethods.SuccessSentContactEmail().Contains( _userLogin2), "Email получателя не совпадает со значением на форме 'Письмо отправлено'");
            _mailBoxMethods.ClickLinkSentLetter();
            Assert.IsTrue(_mailBoxMethods.IsOpenSentFolder(), "После клика по форме 'Письмо отправлено' не было перехода в папку 'Отправленные'.");
            Assert.AreEqual(_mailBoxMethods.SubjectEmail(), _subject, "Тема письма не совпадает с указанным в письме.");
            Assert.AreEqual(_mailBoxMethods.RecipientsEmail(), _userLogin2, "Email получателя не совпадает с указанным в письме.");
            Assert.AreEqual(_mailBoxMethods.BodyEmail(), _body, "Тело пиьсма не совпадает с указанным в письме.");
            //Assert.IsTrue(_mailBoxMethods.IsLetterExistInFolder(_userLogin2, _subject, _body), "Письмо не было обнаружено в папке 'Отправленные'");
            _mailBoxMethods.SignOut();

            _mainPageMethods.SignInEmailBox(_userLogin2,_userPassword2);
            _mailBoxMethods.OpenInboxFolder();
            Assert.IsTrue(_mailBoxMethods.IsLetterExistInLettersList(_userLogin1, _subject, _body), "Тест не обнаружил во входящих письма с требуемыми отправителем, темой или телом письма");
        }
    }
}
