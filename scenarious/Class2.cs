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
    public class Class2
    {
        private readonly ITestOutputHelper output;
        private readonly LaunchSettingsFixture _fixture;
        public Class2(ITestOutputHelper output, LaunchSettingsFixture fixture)
        {
            this.output = output;
            this._fixture = fixture;
        }

        [Fact]
        public void EnvironmentVariables()
        {

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
            SearchForAuthorization createNew = new SearchForAuthorization(_fixture._browserConfig);
            createNew.GoTo();
            SignIn signIn = new SignIn(_fixture._browserConfig);
            signIn.WaitUntilIsLoaded();
            signIn.EnterCredentials(_fixture._config.UserCredentials.GetValueOrDefault("defaultUser").UserName, _fixture._config.UserCredentials.GetValueOrDefault("defaultUser").Password);
            createNew.WaitUntilIsLoaded();
            createNew.CreateNewAuthorization();
            PatientSearch patientSearch = new PatientSearch(_fixture._browserConfig);
            patientSearch.WaitUntilIsLoaded();
            patientSearch.ClickSearch();
            CreateAuthorization createAuthorization = new CreateAuthorization(_fixture._browserConfig);
            createAuthorization.WaitUntilIsLoaded();
            createAuthorization.AgreePopUp.Close();
            createAuthorization.SelectServiceType("Chemotherapy");
            createAuthorization.SelectPlaceOfService("Home");
            createAuthorization.TypeDischargeDateDirrectly("11/23/2017");
            createAuthorization.SelectAdmitionType("Elective");
            createAuthorization.SelectRequestingProvider("Berks Family Care");
            createAuthorization.SearchForTheLastNameOfServicingProviderAndPickFirstFromResults("George");


        }


    }
}