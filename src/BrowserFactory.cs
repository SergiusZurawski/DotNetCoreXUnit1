using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using DotNetCoreXUnit1.src.DTO.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;

namespace DotNetCoreXUnit1
{
    public class BrowserFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static readonly string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static IWebDriver driver;
        private Dictionary<string, string> initConfig;
        private EnvironmentConfig envConfig;
        public static BrowserFactory Instance { get; private set; }
        private readonly string BrowserName;

        private BrowserFactory(Dictionary<string, string> initConfig, EnvironmentConfig envConfig)
        {
            Console.WriteLine("Inside BrowserFactory");
            this.initConfig = initConfig;
            this.envConfig = envConfig;
            this.BrowserName = initConfig.GetValueOrDefault("webbrowser");

            //TODO: we need this in order to close browsers, but there has to be a better way
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            
        }

        public IWebDriver Driver
        {
            get
            {
                if (Instance == null)
                {
                    throw new Exception("BrowserFactory, hasn't been initialized yet. " +
                                            "Please, call BrowserFactory.InitFactory(Dictionary<string, string> initConfig, EnvironmentConfig envConfig) " +
                                            "before using BrowserFactory.Driver");
                }
                return Instance.InitBrowser();
            }
        }



        public static BrowserFactory InitFactory(Dictionary<string, string> initConfig, EnvironmentConfig envConfig)
        {
            Instance = new BrowserFactory(initConfig, envConfig);
            return Instance;
        }

        private IWebDriver InitBrowser()
        {
            string browserForThread = Thread.CurrentThread.ManagedThreadId + BrowserName;
            switch (BrowserName)
            {
                case "FF":
                    if (!Drivers.ContainsKey(browserForThread))
                    {
                        
                        FirefoxOptions options = new FirefoxOptions();
                        options.BrowserExecutableLocation =
                            @"C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe"; //TODO: REmove hardcoded value, move to config etc
                        Drivers.Add(browserForThread, new EventFiringWebDriver(new FirefoxDriver(location, options)));

                    } 
                    return Drivers[browserForThread];  
                case "IE":
                    if (!Drivers.ContainsKey(browserForThread))
                    {
                        Drivers.Add(browserForThread, new EventFiringWebDriver(new InternetExplorerDriver(location)));

                    }
                    return Drivers[browserForThread];
                default:
                    if (!Drivers.ContainsKey(browserForThread))
                    {
                        Drivers.Add(browserForThread, new EventFiringWebDriver(new ChromeDriver(location)));

                    }
                    return Drivers[browserForThread];
            }
        }

//        public static void LoadApplication(string url)
//        {
//            Driver.Url = url;
//        }

        public void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
        }

        public void CloseWebDriver()
        {
            string browserForThread = Thread.CurrentThread.ManagedThreadId + BrowserName;
            Drivers[browserForThread].Close();
            Drivers[browserForThread].Quit();
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Instance.CloseAllDrivers();
        }
    }
}
