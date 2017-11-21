using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;

namespace DotNetCoreXUnit1.src
{
    class BrowserFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static readonly string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static IWebDriver driver;

        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public static void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "FF":
                    if (Driver == null)
                    {
                        driver = new EventFiringWebDriver(new FirefoxDriver(location));
                        Drivers.Add("FF", Driver);
                    }
                    break;

                case "IE":
                    if (Driver == null)
                    {
                        driver = new EventFiringWebDriver(new InternetExplorerDriver(location));
                        Drivers.Add("IE", Driver);
                    }
                    break;

                default:
                    if (Driver == null)
                    {
                        driver = new EventFiringWebDriver(new ChromeDriver(location));
                        Drivers.Add("Chrome", Driver);
                    }
                    break;
            }
        }

        public static void LoadApplication(string url)
        {
            Driver.Url = url;
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
        }
    }
}
