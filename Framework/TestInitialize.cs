using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace AutomatedScript.Framework
{
    [TestFixture]
    public class TestInitialize
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void TestInit()
        {
            _driver = Driver;
            _wait = Wait;
        }

        [TearDown]
        public void TestCleanup()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Quit();
                }
                catch (WebDriverException)
                {
                    Console.WriteLine("Swallow exception thrown if driver process is already gone");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Swallow exception thrown if driver is already disposed or session ended");
                }
                finally
                {
                    try
                    {
                        _driver.Dispose();
                    }
                    catch { }
                    _driver = null;
                }
            }

            // Clear the wait so next test will recreate it against a fresh driver
            _wait = null;
        }

        public RemoteWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    _driver = new ChromeDriver();
                    _driver.Manage().Window.Maximize();
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
