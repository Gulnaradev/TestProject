using NUnit.Framework;
using OpenQA.Selenium;

namespace Kaspersky.TestTask.Youtube
{
    public class Tests
    {
        Browser browser = new Browser();
        IWebDriver driver;

        YoutubeHomePage homePage;
        YoutubeTrendingPage trendingPage;
        VideoPage videoPage;

        [SetUp]
        public void Setup()
        {
            browser.Init_Browser();
            driver = browser.GetDriver;

            homePage = new YoutubeHomePage(driver);
            trendingPage = new YoutubeTrendingPage(driver);
            videoPage = new VideoPage(driver);
        }

        [Test]
        public void LikeNotSettedTest()
        {
            homePage.Navigate();
            homePage.TrendingMenu.Click();

            trendingPage.GetVideoByNumber(3).Click();
            videoPage.LikeElement.Click();

            var hasAttribute = Utils.IsAttributeSetted(videoPage.Menu, VideoPage.MENU_ACTIVE);

            Assert.IsTrue(hasAttribute, "Menu container must contains attribute if like was not setted");
        }


        [Test]
        public void PageRedirectTest()
        {
            homePage.Navigate();
            homePage.TrendingMenu.Click();

            trendingPage.GetVideoByNumber(7).Click();
            var partialUrl = Utils.GetPartialUrl(driver, 0, 20);
            videoPage.GoToCommentSection();

            Assert.IsTrue(videoPage.IsCommentsEnable, "Comment section was disabled");
            Assert.IsFalse(driver.Url.StartsWith(partialUrl), "Page was not redirected");
        }

        [Test]
        public void SearchResultContainsKasperskyUserTest()
        {
            var userName = "Kaspersky";
            bool isUserExists;
            homePage.Navigate();

            homePage.Search(userName);

            try
            {
                var element = homePage.GetUser(userName);
                isUserExists = true;
            }
            catch (NoSuchElementException)
            {
                isUserExists = false;
                //Log
            }

            Assert.IsTrue(isUserExists, "Can't find kaspersky user account");
        }

        [TearDown]
        public void Close_Browser()
        {
            browser.Close();
        }
    }
}