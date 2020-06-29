using OpenQA.Selenium;

namespace Kaspersky.TestTask.Youtube
{
    public class YoutubeHomePage
    {
        IWebDriver driver;
        public YoutubeHomePage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public string HomeUrl { get { return "https://www.youtube.com/"; } }

        public IWebElement SearchQuery => driver.FindElement(By.Name("search_query"));

        public IWebElement TrendingMenu => driver.FindElement(By.CssSelector("a[href*='/feed/trending']"));

        public IWebElement GetUser(string userName) 
        { 
            return driver.FindElement(By.CssSelector(string.Format("a[href*='/user/{0}']", userName))); 
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(HomeUrl);
        }

        public void Search(string s)
        {
            SearchQuery.SendKeys(s);
            SearchQuery.SendKeys(Keys.Enter);
        }
    }
}
