using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using System;

namespace CapstoneConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("test-Type", "--ignore-certificate-errors");

            var driver = new ChromeDriver(@"\Users\gregs\Desktop\CD\CapstoneConsoleApp\CapstoneConsoleApp\bin", options);

            driver.Url = "http://www.google.com";

        }
    }
}
