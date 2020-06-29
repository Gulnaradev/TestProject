using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Kaspersky.TestTask.Youtube
{
    public class YoutubeTrendingPage
    {
        IWebDriver driver;
        public YoutubeTrendingPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public IEnumerable<IWebElement> GetVideoContent()
        {
            return driver.FindElements(By.CssSelector("ytd-video-renderer"));
        }

        public IWebElement GetVideoByNumber(int number)
        {
           return GetVideoContent().ElementAt(number);
        }
    }
}
