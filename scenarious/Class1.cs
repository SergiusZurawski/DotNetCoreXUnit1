using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace DotNetCoreXUnit1
{
    public class Class1
    {

        private readonly string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        [Fact]
        public void PassingTest()
        {
                var firingDriver = new EventFiringWebDriver(new ChromeDriver(location));
                var driver2 = firingDriver;
                driver2.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
                var ss = driver2.GetScreenshot();
                ss.SaveAsFile("chromeUniqName.png");
                var link = driver2.FindElement(By.PartialLinkText("TFS Test API"));
                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                ((IJavaScriptExecutor)driver2).ExecuteScript(jsToBeExecuted);
                var wait = new WebDriverWait(driver2, TimeSpan.FromMinutes(1));
                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
                clickableElement.Click();
                driver2.Quit();
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}