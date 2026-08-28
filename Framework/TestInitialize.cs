using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

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
            try
            {
                var outcome = TestContext.CurrentContext.Result.Outcome.Status;
                if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed && _driver != null)
                {
                    try
                    {
                        var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                        var fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

                        // Use GitHub Actions workspace if available
                        var repoRoot = Environment.GetEnvironmentVariable("GITHUB_WORKSPACE")
                                       ?? TestContext.CurrentContext.WorkDirectory;

                        var screenshotsDir = Path.Combine(repoRoot, "TestResults", "Screenshots");
                        Directory.CreateDirectory(screenshotsDir);

                        var filePath = Path.Combine(screenshotsDir, fileName);
                        File.WriteAllBytes(filePath, screenshot.AsByteArray);

                        Console.WriteLine($"Screenshot saved: {filePath}");
                        TestContext.AddTestAttachment(filePath, "Failure screenshot");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to capture screenshot: {ex.Message}");
                    }
                }
            }
            finally
            {
                if (_driver != null)
                {
                    try { _driver.Quit(); } catch { }
                    try { _driver.Dispose(); } catch { }
                    _driver = null;
                }
                _wait = null;
            }
        }

        public IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    if (Environment.GetEnvironmentVariable("CI") == "true")
                    {
                        var options = new ChromeOptions();
                        options.AddArgument("--headless=new");
                        options.AddArgument("--disable-gpu");
                        options.AddArgument("--window-size=1920,1080");
                        options.AddArgument("--no-sandbox");
                        options.AddArgument("--disable-dev-shm-usage");

                        _driver = new ChromeDriver(options);
                        _driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
                    }
                    else
                    {
                        _driver = new ChromeDriver();
                        _driver.Manage().Window.Maximize();
                    }
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