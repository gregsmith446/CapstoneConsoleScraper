using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
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

            // using JSExecutor, switch to popup + close, then switch back to portfolio page
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            IList<string> tabs = new List<string>(driver.WindowHandles);
            driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
            driver.SwitchTo().Window(tabs[0]);

            driver.Url = "https://finance.yahoo.com/portfolio/p_0/view/v1";

            driver.Manage().Window.Maximize();

            /*

            // scrape data!!
            IWebElement PortfolioTable = driver.FindElement(By.ClassName("W(100%)"));

            // Find all rows in the table and assign to a list
            IList<IWebElement> rows = new List<IWebElement>(PortfolioTable.FindElements(By.TagName("tr")));
            String strRowData = "";

            // loop through rows in table to only get columns
            for (int j = 1; j < rows.Count; j++)
            {
                // During the loop, get the columns from a particular row and set = 1stTdElem, a list
                List<IWebElement> lstTdElem = new List<IWebElement>(rows[j].FindElements(By.TagName("td")));

                if (lstTdElem.Count > 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        strRowData = strRowData + lstTdElem[i].Text + ",";
                    }
                }
                else
                {
                    // To print the data into the console and add comma between text
                    Console.WriteLine(rows[0].Text.Replace(" ", ","));
                }
            }

            // Print the data to the console
            System.Console.WriteLine(strRowData);
            */
        }
    }
}

// the * symbol is the wildcard or select all

// table headers example: symbol, price, change, change %. currency, market time, volume
//*[@id="pf-detail-table"]/div[1]/table/thead/tr/th[*]

// all of column 1
//*[@id="pf-detail-table"]/div[1]/table/tbody/tr[*]/td[*]/a

// all of row 1
//*[@id="pf-detail-table"]/div[1]/table/tbody/tr[1]/td[*]/span









