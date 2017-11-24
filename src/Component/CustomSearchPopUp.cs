using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace DotNetCoreXUnit1.Component
{
    public class CustomSearchPopUp : AbstractComponent
    {
        public CustomSearchPopUp(IWebDriver driver) : base(driver)
        {
        }

        private By root = By.Id("custom-servicing-provider-search");
        private By SerachButton = By.Id("servicing-provider-search-button");
        private By LastName = By.Id("servicing-provider-provider-last-name");
        protected override IWebElement Root {
            get { return _driver.FindElement(root); }
        }

        public override void WaitUntilIsLoaded()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(root));
            _wait.Until(ExpectedConditions.ElementIsVisible(SerachButton));
        }

        public override bool IsVisible()
        {
            return Root.Displayed;
        }

        public void EnterLastName(string lastName)
        {
            E(LastName).SendKeys(lastName);
        }

        public void Search()
        {
            E(SerachButton).Click();
        }
    }
}
