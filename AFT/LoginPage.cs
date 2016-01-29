using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

internal class LoginPage
{
    private ChromeDriver driver;

    public LoginPage(ChromeDriver driver)
    {
        this.driver = driver;
    }

    public LoginPage Navigate()
    {
        driver.Navigate().GoToUrl("http://localhost:49333/Account/Login");
        return this;
    }

    public IWebElement GetErrorMessage()
    {
        return driver.FindElementByXPath("//*[@id='loginForm']/form/div[1]/div/span/span");
    }

    public ReadOnlyCollection<IWebElement> GetAllErrorMessages()
    {
        return driver.FindElementsByXPath("//*[@id='loginForm']/form/div[1]/div/span/span");
    }

    public LoginPage SubmitButtonClick()
    {
        var submitButton = driver.FindElementByXPath("//*[@id='loginForm']/form/div[4]/div/input");
        submitButton.Click();
        return this;
    }

    public IWebElement GetCheckBox()
    {
        var check = driver.FindElementByXPath("//*[@id='loginForm']/form/div[3]/div/div/input");
        return check;
    }

    public IWebElement GetLoginTextInput()
    {
        return driver.FindElementByXPath("//*[@id='loginForm']/form/div[1]/div/input");
    }
}