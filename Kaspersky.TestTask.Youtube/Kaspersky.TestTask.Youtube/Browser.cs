using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaspersky.TestTask.Youtube
{
    public class Browser
    {
        IWebDriver webDriver;

        public void Init_Browser()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public string Title
        {
            get { return webDriver.Title; }
        }

        public void Close()
        {
            webDriver.Quit();
        }

        public IWebDriver GetDriver
        {
            get { return webDriver; }
        }

    }
}
