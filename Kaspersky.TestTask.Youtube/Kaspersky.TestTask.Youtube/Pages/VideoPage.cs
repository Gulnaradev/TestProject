using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Kaspersky.TestTask.Youtube
{
    public class VideoPage
    {
        IWebDriver driver;
        public VideoPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public const string MENU_ACTIVE = "menu-active";

        public bool IsCommentsEnable { get; private set; }

        public IWebElement Menu => driver.FindElement(By.CssSelector("ytd-menu-renderer.style-scope.ytd-video-primary-info-renderer"));

        public IWebElement CommentAreaHeader => driver.FindElement(By.Id("comments")).FindElement(By.Id("header"));

        public IWebElement LikeElement => driver.FindElement(By.CssSelector("#top-level-buttons > ytd-toggle-button-renderer:nth-child(1) > a"));

        private IWebElement CommentAreaBox => CommentAreaHeader.FindElement(By.Id("simple-box"));


        public void GoToCommentSection()
        {
            IsCommentsEnable = true;

            try
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(CommentAreaHeader);
                actions.Perform();

                var element = CommentAreaBox;

                element.Click();
            }
            catch (NoSuchElementException)
            {
                IsCommentsEnable = false;
            }
        }

    }
}
