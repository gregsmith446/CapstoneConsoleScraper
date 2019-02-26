using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace CapstoneConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("test-Type", "--ignore-certificate-errors");

            var driver = new ChromeDriver(@"\Users\gregs\Desktop\CD\CapstoneConsoleApp\CapstoneConsoleApp\bin", options);

            driver.Url = "https://login.yahoo.com/config/login?.src=finance&amp;.intl=us&amp;.done=https%3A%2F%2Ffinance.yahoo.com%2Fportfolios";

            IWebElement login = driver.FindElement(By.Id("login-username"));
            login.SendKeys("gregsmith446@intracitygeeks.org");
            login.Submit();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement password = driver.FindElement(By.Id("login-passwd"));
            password.SendKeys("SILICONrhode1!");
            driver.FindElement(By.Id("login-signin")).Click();
            driver.FindElement(By.Id("login-signin")).Click();

            driver.Url = "https://finance.yahoo.com/portfolio/p_0/view/v1";

            // driver.SwitchTo().Window(driver.WindowHandles.Last());

            var tabs = driver.WindowHandles; 

            foreach (var tab in tabs)
            {
                if (tabs[0] != tab)
                {
                    driver.SwitchTo().Window(tab);
                    driver.Close();
                }
            }
        }
    }
}
