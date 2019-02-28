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

            driver.Manage().Window.Maximize();

            IWebElement login = driver.FindElement(By.Id("login-username"));
            login.SendKeys("gregsmith446@intracitygeeks.org");
            login.Submit();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement password = driver.FindElement(By.Id("login-passwd"));
            password.SendKeys("SILICONrhode1!");
            driver.FindElement(By.Id("login-signin")).SendKeys(Keys.Return);
            // driver.FindElement(By.Id("login-signin")).Click();

            // using JSExecutor, switch to popup + close, then switch back to portfolio page
            /*
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            IList<string> tabs = new List<string>(driver.WindowHandles);
            driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
            driver.SwitchTo().Window(tabs[0]);
            */

            driver.Url = "https://finance.yahoo.com/portfolio/p_0/view/v1";

            // you are now on the portfolio page 

            // instantiate an IWebElement object named Portfolio Table
            IWebElement PortfolioTable = driver.FindElement(By.Id("pf-detail-table"));

            // instantiate a LIST called rows and assign the table rows to a list
            IList<IWebElement> rows = new List<IWebElement>(PortfolioTable.FindElements(By.TagName("tr")));

            // set row data = nothing - will be used to hold all the row data
            String strRowData = "";

            // loop through rows in table to only get columns
            for (int j = 1; j < rows.Count; j++)
            {
                // During the loop, instantiate a LIST called 1stTdElement that will hold table data
                List<IWebElement> lstTdElem = new List<IWebElement>(rows[j].FindElements(By.TagName("td")));

                // loop thtrough each table data section, adding all the data from the rows
                if (lstTdElem.Count > 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        strRowData = strRowData + lstTdElem[i].Text + ",";
                    }
                }
                else
                {
                    // print the data into the console and add a comma between text
                    Console.WriteLine(rows[0].Text.Replace(" ", ","));
                }
            }

            // Print the completed data to the console
            System.Console.WriteLine(strRowData);
        }
    }
}

// these are XPaths

// the * symbol is the wildcard or select all

// table headers example: symbol, price, change, change %. currency, market time, volume
//*[@id="pf-detail-table"]/div[1]/table/thead/tr/th[*]

// all of column 1
//*[@id="pf-detail-table"]/div[1]/table/tbody/tr[*]/td[*]/a

// all of row 1
//*[@id="pf-detail-table"]/div[1]/table/tbody/tr[1]/td[*]/span









