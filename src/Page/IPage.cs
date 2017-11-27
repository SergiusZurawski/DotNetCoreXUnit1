﻿using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using DotNetCoreXUnit1.src.DTO.Config;
using DotNetCoreXUnit1.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
        protected readonly Actions _actions;
        protected WebDriverWait _wait;

        public string Url { get;}
        protected string Host { get; }
        protected abstract string PagePath { get;}

        protected AbstractPage()
        {
            this._driver = TestContext.Current.BrowserFactory.Driver;

            _wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(10000));
            _eHelper = new ElementHelper(_driver);
            _actions = new Actions(_driver);
            Host = @"http://" + TestContext.Current.EnvConfig.HostName + TestContext.Current.EnvConfig.DomainName;
            Url = Host + PagePath;
        }


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
