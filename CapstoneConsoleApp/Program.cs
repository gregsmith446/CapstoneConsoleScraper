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
        }
    }
       
    // create a stock object blueprint
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

    // class to hold scraper code
    public class Scraper
    {
        // function returns a list of stocks
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

            Console.WriteLine("Gathering Portfolio Data......................");

            // print the contents of that list to the console
            Console.WriteLine("The Portfolio of stocks is as follows: ");

            //Loop iterate through portfolio of stocks, gathering data
            // current issue is the 
            for (int i = 1; i <= count; i++)
            {
                string symbol = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr["+ i +"]/td[1]/a")).GetAttribute("innerText");
                Console.WriteLine(symbol);
                string price = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr["+ i +"]/td[2]/span")).GetAttribute("innerText");
                Console.WriteLine(price);
                string change = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[" + i + "]/td[3]/span")).GetAttribute("innerText");
                Console.WriteLine(change);
                string pchange = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[" + i + "]/td[4]/span")).GetAttribute("innerText");
                Console.WriteLine(pchange);
                string volume = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[" + i + "]/td[7]/span")).GetAttribute("innerText");
                Console.WriteLine(volume);
                string marketcap = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table/tbody/tr[" + i + "]/td[13]/span")).GetAttribute("innerText");
                Console.WriteLine(marketcap);


                // for each stock entry, a new stock object is created
                // the above data is set equal to the field within the object
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


            // a database table has been created

            // connection string to SQL Server which contains database 'Scraper' and table 'dbo.Table'

            // private static readonly string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

        // the table will have a schema which will match up the stock object's fields
        // insert the stock objects into the database matching the schema
        // test that insertion worked by viewing contents of database table

        driver.Quit();

            return stockList;
        }
    }
}








