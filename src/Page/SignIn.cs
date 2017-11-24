using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace DotNetCoreXUnit1.Page
{
    class SignIn : AbstractPage
    {
        private string pagePath = "/sign-in";
        private WebDriverWait _driverWait;
        private string url;
        public override string Url
        {
            get { return this.url; }
        }

        private By UserName = By.Id("LoginPortletUsername");
        private By Password = By.Id("LoginPortletPassword");
        private By SignButton = By.Id("btnSignInSubmit");

        public SignIn(Dictionary<string, string> config) : base(config)
        {
            this.url = config.GetValueOrDefault("hostUrl")+ pagePath;
            _driverWait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(10000));
        }

        public override bool WaitUntilIsLoaded()
        {
            _driverWait.Until(ExpectedConditions.ElementExists(UserName));
            return _driver.FindElement(UserName).Displayed;
        }

       

        public void EnterCredentials(string userName, string password)
        {
            _driver.FindElement(UserName).SendKeys(userName);
            _driver.FindElement(Password).SendKeys(password);
            _driver.FindElement(SignButton).Click();
        }
    }
}
