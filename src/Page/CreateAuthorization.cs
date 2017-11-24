using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreXUnit1.Component;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace DotNetCoreXUnit1.Page
{
    class CreateAuthorization : AbstractPage
    {
        private string pagePath = "/authorizations/submission/plans/aries/create";
        private WebDriverWait _driverWait;
        private string url;
        public override string Url
        {
            get { return this.url; }
        }

        private By ServiceType = By.Id("service-type");
        private By ServiceTypeDropDown = By.CssSelector("#service-type-div .dropdown-container tbody tr:nth-child(1)");

        private By PlaceOfService = By.Id("place-of-service");
        private By PlaceOfServiceDropDown = By.CssSelector("#place-of-service-div .dropdown-container tbody tr:nth-child(1)");

        private By DischargeDate = By.Id("DischargeDate");
        private By AdmissionType = By.Id("admission-type");

        private By RequestingProvider = By.Id("requesting-provider");
        private By RequestingProviderDropDown = By.CssSelector("#requesting-provider-div .dropdown-container tbody tr:nth-child(1)");


        private By ServicingProvider = By.Id("servicing-provider");
        private CustomSearchPopUp _servicingCustomSearchPopUp;
        private By ServicingProviderResultDropDown = By.CssSelector("#servicing-provider-search-results-table tr:nth-child(2)");

        public NewAuthorizationPreScreeningPopUp AgreePopUp;

        public CreateAuthorization(Dictionary<string, string> config) : base(config)
        {
            // TODO: move this to abstract class
            this.url = config.GetValueOrDefault("hostUrl")+ pagePath;
            _driverWait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(10000));

            AgreePopUp = new NewAuthorizationPreScreeningPopUp(_driver);
            _servicingCustomSearchPopUp = new CustomSearchPopUp(_driver);
        }

        public override bool WaitUntilIsLoaded()
        {
            AgreePopUp.WaitUntilIsLoaded();
            return AgreePopUp.IsVisible();
        }

       

        public void SelectServiceType(string serviceType)
        {
            var e = _driver.FindElement(ServiceType);
            e.SendKeys(serviceType);
            _driverWait.Until(ExpectedConditions.ElementIsVisible(ServiceTypeDropDown));
            var eDropDown = _driver.FindElement(ServiceTypeDropDown);
            _driverWait.Until(ExpectedConditions.TextToBePresentInElement(eDropDown, serviceType));
            eDropDown.Click();
        }

        public void SelectPlaceOfService(string placeOfService)
        {
            var e = _driver.FindElement(PlaceOfService);
            e.SendKeys(placeOfService);
            _driverWait.Until(ExpectedConditions.ElementIsVisible(PlaceOfServiceDropDown));
            var eDropDown = _driver.FindElement(PlaceOfServiceDropDown);
            _driverWait.Until(ExpectedConditions.TextToBePresentInElement(eDropDown, placeOfService));
            eDropDown.Click();
        }

        public void TypeDischargeDateDirrectly(string date)
        {
            var e = _driver.FindElement(DischargeDate);
            e.SendKeys(date);
        }

        public void SelectAdmitionType(string type)
        {
            var e = _driver.FindElement(AdmissionType);
            var selectElement = new SelectElement(e);
            selectElement.SelectByText(type);
        }

        public void SelectRequestingProvider(string requestingProvider)
        {
            var e = _driver.FindElement(RequestingProvider);
            e.SendKeys(requestingProvider);
            _driverWait.Until(ExpectedConditions.ElementIsVisible(RequestingProviderDropDown));
            var eDropDown = _driver.FindElement(RequestingProviderDropDown);
            _driverWait.Until(ExpectedConditions.TextToBePresentInElement(eDropDown, requestingProvider));
            eDropDown.Click();
        }

        public void SearchForTheLastNameOfServicingProviderAndPickFirstFromResults(string lastName)
        {
            var e = E(ServicingProvider);
            _servicingCustomSearchPopUp.WaitUntilIsLoaded();
            _servicingCustomSearchPopUp.EnterLastName(lastName);
            _servicingCustomSearchPopUp.Search();
        }
    }
}

