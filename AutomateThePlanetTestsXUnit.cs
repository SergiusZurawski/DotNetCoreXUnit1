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

namespace DotNetCoreXUnit1
{
    
    public class AutomateThePlanetTestsXUnit
    {
        ITestFailed eventTestFailed;
        private readonly ITestOutputHelper output;
        private string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public AutomateThePlanetTestsXUnit(ITestOutputHelper output)
        {
            this.output = output;
            this.eventTestFailed = eventTestFailed;
        }

        [Fact]
        public void TestWithFirefoxDriver()
        {
        
            using (var driver = new FirefoxDriver(location))
            {
                global_driver = driver;
                var firingDriver = new EventFiringWebDriver(driver);
                firingDriver.ExceptionThrown += firingDriver_TakeScreenshotOnException;
                firingDriver.NavigatedForward += firingDriver_OnNavigating;
                firingDriver.Navigated += firingDriver_OnNavigating;
                //firingDriver.FindElementCompleted += firingDriver_OnNavigating;
                var driver2 = firingDriver;
                driver.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
                var link = driver.FindElement(By.PartialLinkText("TFS Test API"));
                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
                clickableElement.Click();
                var ss = driver.GetScreenshot();
                ss.SaveAsFile("ss1.png");
                output.WriteLine("screenshot {0}", location+"/"+ "ss1.png");
            }
        }

        [Fact]
        public void TestWithEdgeDriver()
        {
            using (var driver = new EdgeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
                var link = driver.FindElement(By.PartialLinkText("TFS Test API"));
                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void TestWithChromeDriver()
        {
            using (var driver = new ChromeDriver(location))
            {
                global_driver = driver;
                var firingDriver = new EventFiringWebDriver(driver);
                firingDriver.ExceptionThrown += firingDriver_TakeScreenshotOnException;
                firingDriver.NavigatedForward += firingDriver_OnNavigating;
                firingDriver.Navigated += firingDriver_OnNavigating;
                //firingDriver.FindElementCompleted += firingDriver_OnNavigating;
                var driver2 = firingDriver;
                driver2.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
                var ss = driver2.GetScreenshot();
                ss.SaveAsFile("chromeUniqName.png");
                output.WriteLine("Change screenshot {0}", location + "/" + "chromeUniqName.png");
                output.WriteLine("Still inside");
                var link = driver2.FindElement(By.PartialLinkText("TFS Test API"));
                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                ((IJavaScriptExecutor)driver2).ExecuteScript(jsToBeExecuted);
                var wait = new WebDriverWait(driver2, TimeSpan.FromMinutes(1));
                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
                clickableElement.Click();
                var clickableElement1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("dfaskjdflksadfjlaksjdf")));
                clickableElement1.Click();


            }
        }
        private IWebDriver global_driver ;
        private void firingDriver_TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {
            output.WriteLine("we are HERE !!!!! firingDriver_TakeScreenshotOnException");
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
            string path = location + "/Exception-" + timestamp + ".png";
            global_driver.TakeScreenshot().SaveAsFile(path);
            output.WriteLine("<screenshot xlink:type=\"simple\" xlink: href = \"{0}\" xlink: show = \"new\" > ", path);
        }

        private void firingDriver_OnNavigating(object sender, WebDriverNavigationEventArgs er)
        {
            output.WriteLine("firingDriver_OnNavigating");
        }
    }
}
