using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;


// https://stackoverflow.com/questions/37752273/how-do-i-run-specific-tests-using-dotnet-test
// https://stackoverflow.com/questions/16484839/get-name-of-running-test-in-xunit

namespace DotNetCoreXUnit1
{
    
    public class AutomateThePlanetTestsXUnit
    {
        ITestFailed eventTestFailed;
        private readonly ITestOutputHelper output;
        private readonly string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public AutomateThePlanetTestsXUnit(ITestOutputHelper output)
        {
            this.output = output;
        }

//        [Fact]
//        public void TestWithFirefoxDriver()
//        {
//            output.WriteLine("Location is here ::::::::: " + location);
//            //using (var driver = new FirefoxDriver(location))
//            FirefoxOptions options = new FirefoxOptions();
//            options.BrowserExecutableLocation = @"C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe";
//            using (var driver = new FirefoxDriver(location, options))
//            {
//                global_driver = driver;
//                var firingDriver = new EventFiringWebDriver(driver);
//                firingDriver.ExceptionThrown += firingDriver_TakeScreenshotOnException;
//                firingDriver.NavigatedForward += firingDriver_OnNavigating;
//                firingDriver.Navigated += firingDriver_OnNavigating;
//                //firingDriver.FindElementCompleted += firingDriver_OnNavigating;
//                var driver2 = firingDriver;
//                driver.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
//                var link = driver.FindElement(By.PartialLinkText("TFS Test API"));
//                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
//                ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
//                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
//                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
//                clickableElement.Click();
//            }
//        }
//
//        [Fact]
//        public void TestWithEdgeDriver()
//        {
//            using (var driver = new EdgeDriver(location))
//            {
//                driver.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
//                var link = driver.FindElement(By.PartialLinkText("TFS Test API"));
//                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
//                ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
//                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
//                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
//                clickableElement.Click();
//            }
//        }
//
//        [Fact]
//        public void TestWithChromeDriver()
//        {
//            using (var driver = new ChromeDriver(location))
//            {
//                global_driver = driver;
//                var firingDriver = new EventFiringWebDriver(driver);
//                firingDriver.ExceptionThrown += firingDriver_TakeScreenshotOnException;
//                firingDriver.NavigatedForward += firingDriver_OnNavigating;
//                firingDriver.Navigated += firingDriver_OnNavigating;
//                var driver2 = firingDriver;
//                driver2.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
//                var link = driver2.FindElement(By.PartialLinkText("TFS Test API"));
//                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
//                ((IJavaScriptExecutor)driver2).ExecuteScript(jsToBeExecuted);
//                var wait = new WebDriverWait(driver2, TimeSpan.FromSeconds(30));
//                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
//                clickableElement.Click();
//                var clickableElement1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("dfaskjdflksadfjlaksjdf")));
//                clickableElement1.Click();
//
//
//            }
//        }

      

        private IWebDriver global_driver ;
        private void firingDriver_TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {
            string name = e.GetType().FullName + DateTime.Now.ToString("yyyy-MM-dd-hhmm"); ;
//            if (!((TestOutputHelper) output).Output.Contains(name))
//            {
                string fullName = location + "\\" + name;
                global_driver.TakeScreenshot().SaveAsFile(fullName);
                output.WriteLine("Screenshot: " + fullName);
//            }

        }

        private void firingDriver_OnNavigating(object sender, WebDriverNavigationEventArgs er)
        {
            output.WriteLine("firingDriver_OnNavigating");
        }
    }
}
