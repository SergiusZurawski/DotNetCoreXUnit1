using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace DotNetCoreXUnit1.Page
{
    class PatientSearch : AbstractPage
    {
        private string pagePath = "/authorizations/submission/plans/aries/patient-search-mock";
        private WebDriverWait _driverWait;
        private string url;
        public override string Url
        {
            get { return this.url; }
        }

        private By Search = By.CssSelector("i.icon-search");

        public PatientSearch(Dictionary<string, string> config) : base(config)
        {
            this.url = config.GetValueOrDefault("hostUrl")+ pagePath;
            _driverWait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(10000));
        }

        public override bool WaitUntilIsLoaded()
        {
            _driverWait.Until(ExpectedConditions.ElementExists(Search));
            return _driver.FindElement(Search).Displayed;
        }

       

        public void ClickSearch()
        {
            _driver.FindElement(Search).Click();
        }
    }
}
