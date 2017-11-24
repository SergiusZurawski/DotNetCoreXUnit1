using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreXUnit1.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace DotNetCoreXUnit1.Component
{
    public interface IComponent
    {
        void WaitUntilIsLoaded();
        bool IsVisible();
    }

    public abstract class AbstractComponent : IComponent
    {

        protected IWebDriver _driver;
        protected WebDriverWait _wait;
        protected readonly ElementHelper _eHelper;
        public AbstractComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(10000));
            _eHelper = new ElementHelper(_driver);
        }

        protected abstract IWebElement Root { get;}
        public abstract void WaitUntilIsLoaded();
        public abstract bool IsVisible();

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
