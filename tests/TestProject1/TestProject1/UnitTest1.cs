using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;



namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            baseURL = "https://src-app-stg.azurewebsites.net/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheUntitledTestCaseTest()
        {
            driver.Navigate().GoToUrl("https://src-app-stg.azurewebsites.net/");
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("j.mendezquesada18@gmail.com");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Jesus@12345");
            driver.FindElement(By.Id("send-email")).Click();
            driver.FindElement(By.LinkText("Recognize")).Click();
            driver.FindElement(By.Id("Recognitions_Case_Number")).Click();
            driver.FindElement(By.Id("Recognitions_Case_Number")).Clear();
            driver.FindElement(By.Id("Recognitions_Case_Number")).SendKeys("20654321987654");
            driver.FindElement(By.Id("Recognitions_Recognized_Eng")).Click();
            new SelectElement(driver.FindElement(By.Id("Recognitions_Recognized_Eng"))).SelectByText("Lupita Mendez (thechus18@gmail.com)");
            driver.FindElement(By.Id("Recognitions_Comment")).Click();
            driver.FindElement(By.Id("Recognitions_Comment")).Clear();
            driver.FindElement(By.Id("Recognitions_Comment")).SendKeys("Thank you for all the help");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}