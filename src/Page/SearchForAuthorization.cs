using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace DotNetCoreXUnit1.Page
{
    class SearchForAuthorization : AbstractPage
    {
        private string pagePath = "/authorizations/submission/plans/aries";
        private WebDriverWait _driverWait;
        private string url;
        public override string Url
        {
            get { return this.url; }
        }

        private By CreateNewAuthorizationLink = By.Id("create-new-authorization");

        public SearchForAuthorization(Dictionary<string, string> config) : base(config)
        {
            this.url = config.GetValueOrDefault("hostUrl")+ pagePath;
            _driverWait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(10000));
        }

        public override bool WaitUntilIsLoaded()
        {
            _driverWait.Until(ExpectedConditions.ElementExists(CreateNewAuthorizationLink));
            return _driver.FindElement(CreateNewAuthorizationLink).Displayed;
        }

       

        public void CreateNewAuthorization()
        {
            _driver.FindElement(CreateNewAuthorizationLink).Click();
        }
    }
}
