using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AFT
{
    using NUnit.Framework;
    using OpenQA.Selenium.Chrome;

    // PageObjects pattern https://code.google.com/p/selenium/wiki/PageObjects

    public class LoginUiTest
    {
        [Test]
        public void IfEmailAddressIsNotValid_ShowValidationMessage()
        {
            using (var driver = new ChromeDriver())
            {
                var loginErrorMessageDisplayed = new LoginPage(driver)
                    .Navigate()
                    .SubmitButtonClick()
                    .GetErrorMessage()
                    .Displayed;

                Assert.True(loginErrorMessageDisplayed);
            }
        }

        [Test]
        public void SecondTest()
        {
            using (var driver = new ChromeDriver())
            {
                var loginPage = new LoginPage(driver);
                loginPage.Navigate();

                var loginInput = loginPage.GetLoginTextInput();
                loginInput.SendKeys("1111");

                Assert.AreEqual("1111", loginInput.GetAttribute("value"));
                
            }
        }

        [Test]
        public void CheckBoxTest()
        {
            using (var driver = new ChromeDriver())
            {
                var loginPage = new LoginPage(driver);
                loginPage.Navigate();

                var check = loginPage.GetCheckBox();
                check.Click();

                Assert.AreEqual("true", check.GetAttribute("value"));
            }

        }
    }
}