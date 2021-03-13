using System;
using System.Collections.Generic;
using AutomatedScript.Models;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace AutomatedScript.Framework
{
    [TestFixture]
    public class TestInitialize
    {
        private RemoteWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public virtual void TestInit()
        {
            _driver = Driver;
            _wait = Wait;
        }

        [TearDown]
        public void TestCleanup()
        {
            _driver.Close();
            _driver.Dispose();
        }

        public RemoteWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    _driver = new ChromeDriver();
                    _driver.Manage().Window.Maximize();
                    _driver.Navigate().GoToUrl("https://www.amazon.com/");
                }
                return _driver;
            }
        }

        public WebDriverWait Wait
        {
            get
            {
                if (_wait == null)
                {
                    _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                }
                return _wait;
            }
        }
    }
}
