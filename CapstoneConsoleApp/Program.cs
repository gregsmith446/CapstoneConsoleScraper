using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CapstoneConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // create instance of scraper class
            var runScraper = new Scraper();

            // using class, access the actual scraping code, which returns a list
            runScraper.Scrape();

            // print the contents of that list to the console
            Console.WriteLine("The Portfolio of stocks is as follows: ");
        }
    }
        
    public class Stock
    {
        public string Symbol { get; set; }
        public string Price { get; set; }
        public string Change { get; set; }
        public string PChange { get; set; }
        public string Volume { get; set; }
        public string MarketCap { get; set; }
        public System.DateTime ScrapeTime { get; set; }
    }

    public class Scraper
    {
        public List<Stock> Scrape()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("test-Type", "--ignore-certificate-errors", "--disable-gpu", "window-size=1920,1080");

            IWebDriver driver = new ChromeDriver(@"\Users\gregs\Desktop\CD\CapstoneConsoleApp\CapstoneConsoleApp\bin", options);

            driver.Navigate().GoToUrl("https://login.yahoo.com/config/login?.src=finance&amp;.intl=us&amp;.done=https%3A%2F%2Ffinance.yahoo.com%2Fportfolios");
            driver.Manage().Window.Maximize();

            IWebElement username = driver.FindElement(By.Id("login-username"));
            username.SendKeys("gregsmith446@intracitygeeks.org");
            username.SendKeys(Keys.Return);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IWebElement password = driver.FindElement(By.Id("login-passwd"));
            password.SendKeys("SILICONrhode1!");
            IWebElement loginButton = driver.FindElement(By.Id("login-signin"));
            loginButton.SendKeys(Keys.Return);

            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IWebElement list = driver.FindElement(By.TagName("tbody"));
            ReadOnlyCollection<IWebElement> items = list.FindElements(By.TagName("tr"));
            int count = items.Count;

            Console.WriteLine("There are " + count + " stocks in the list.");

            // create list to store stock data type
            List<Stock> stockList = new List<Stock>();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var testpath = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[1]/td[1]/a")).GetAttribute("innerText");
            Console.WriteLine(testpath);

            //Loop iterate through portfolio of stocks, gathering data
            // current issue is the 
            for (int i = 1; i <= count; i++)
            {
                var symbol = driver.FindElement(By.XPath("//*[@id=\"pf - detail - table\"]/div[1]/table/tbody/tr[" + i + "]/td[1]/a")).GetAttribute("innerText");
                var price = driver.FindElement(By.XPath("//*[@id=\"pf - detail - table\"]/div[1]/table/tbody/tr[" + i + "]/td[2]/a")).GetAttribute("innerText");
                var change = driver.FindElement(By.XPath("//*[@id=\"pf - detail - table\"]/div[1]/table/tbody/tr[" + i + "]/td[3]/a")).GetAttribute("innerText");
                var pchange = driver.FindElement(By.XPath("//*[@id=\"pf - detail - table\"]/div[1]/table/tbody/tr[" + i + "]/td[4]/a")).GetAttribute("innerText");
                var volume = driver.FindElement(By.XPath("//*[@id=\"pf - detail - table\"]/div[1]/table/tbody/tr[" + i + "]/td[7]/a")).GetAttribute("innerText");
                var marketcap = driver.FindElement(By.XPath("//*[@id=\"pf - detail - table\"]/div[1]/table/tbody/tr[" + i + "]/td[13]/a")).GetAttribute("innerText");

                // for each stock entry, a new stock is created
                Stock newStock = new Stock();
                newStock.Symbol = symbol;
                newStock.Price = price;
                newStock.Change = change;
                newStock.PChange = pchange;
                newStock.Volume = volume;
                newStock.MarketCap = marketcap;

                // that stock is then added to the list of stocks
                stockList.Add(newStock);
            }

            driver.Quit();

            foreach (object stock in stockList)
            {
                Console.WriteLine(stock);
            }

            return stockList;
        }
    }
}








