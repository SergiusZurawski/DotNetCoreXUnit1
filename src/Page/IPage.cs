using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreXUnit1.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace DotNetCoreXUnit1.Page
{
    public interface IPage
    {
        string Url { get; }
        void GoTo();
        void GoBack();
        void Refresh();
        string GetCurrentUrl();
        bool WaitUntilIsLoaded();
    }

    public abstract class AbstractPage : IPage {

        protected IWebDriver _driver;
        protected readonly ElementHelper _eHelper;
        public AbstractPage(Dictionary<string, string> config)
        {
            BrowserFactory.InitBrowser(config);
            _driver = BrowserFactory.Driver;
            _eHelper = new ElementHelper(_driver);
        }

        public abstract string Url { get;}

        public void GoTo()
        {
            _driver.Navigate().GoToUrl(Url);
        }

        public void GoBack()
        {
            _driver.Navigate().Back();
        }

        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }

        public string GetCurrentUrl()
        {
            return _driver.Url;
        }

        public abstract bool WaitUntilIsLoaded();

        public IWebElement E(By locator)
        {
            return _eHelper.E(locator);
        }

        public IList<IWebElement> EL(By locator)
        {
            return _eHelper.EL(locator);
        }

        public SelectElement S(By locator)
        {
            return _eHelper.S(locator);
        }
    }
}
