using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using DotNetCoreXUnit1.Component;
using DotNetCoreXUnit1.Page;
using DotNetCoreXUnit1.scenarious;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace DotNetCoreXUnit1
{
    [Collection("SharedVariableCollection")]
    public class Class2 : IDisposable
    {
        private readonly ITestOutputHelper output;
        private readonly LaunchSettingsFixture _fixture;
        private readonly TestContext context = TestContext.Current; 
        public Class2(ITestOutputHelper output, LaunchSettingsFixture fixture)
        {
            this.output = output;
            this._fixture = fixture;
        }

        [Fact]
        public void EnvironmentVariables()
        {
            Thread.Sleep(5000);
            TestContext tx = TestContext.Current;
            tx.WriteLog();
            tx.WritToLog("" + Thread.CurrentThread.ManagedThreadId);
            Environment.SetEnvironmentVariable("testenv1", "horray");
            var v = Environment.GetEnvironmentVariable("testenv");
            var v1 = Environment.GetEnvironmentVariable("testenv1");
            output.WriteLine( "Variable is: " + v);
            output.WriteLine("Variable1 is: " + v1);
            Assert.Equal(1, 2);
        }
        
        [Fact]
        public void TestDriverFactory()
        {
            SearchForAuthorization createNew = new SearchForAuthorization();
            createNew.GoTo();
            SignIn signIn = new SignIn();
            signIn.WaitUntilIsLoaded();
            signIn.EnterCredentials(context.EnvConfig.UserCredentials.GetValueOrDefault("defaultUser").UserName, context.EnvConfig.UserCredentials.GetValueOrDefault("defaultUser").Password);
            createNew.WaitUntilIsLoaded();
            createNew.CreateNewAuthorization();
            PatientSearch patientSearch = new PatientSearch();
            patientSearch.WaitUntilIsLoaded();
            patientSearch.ClickSearch();
            CreateAuthorization createAuthorization = new CreateAuthorization();
            createAuthorization.WaitUntilIsLoaded();
            createAuthorization.AgreePopUp.Close();
            createAuthorization.SelectServiceType("Chemotherapy");
            createAuthorization.SelectPlaceOfService("Home");
//            createAuthorization.TypeDischargeDateDirrectly("11/23/2017");
            createAuthorization.CopyAdmissionDateToDischargeDate();
            createAuthorization.SelectAdmitionType("Elective");
            createAuthorization.SelectRequestingProvider("Berks Family Care");
            createAuthorization.SearchForTheLastNameOfServicingProviderAndPickFirstFromResults("Ahtaridis");
            createAuthorization.SearchForAdditionalProvider("Univ Of Penn Gastroenterology");
            createAuthorization.SetDiagnoses("001");
            createAuthorization.SetServiceLine("123123", "2", "Month(s)", "2", "Month(s)");
//            createAuthorization.UploadAttachment("");
            createAuthorization.AddNotes("Some notes");
            createAuthorization.Submit();
            context.BrowserFactory.CloseWebDriver();
        }


        public void Dispose()
        {
            context.BrowserFactory.CloseWebDriver();
        }

    }
}