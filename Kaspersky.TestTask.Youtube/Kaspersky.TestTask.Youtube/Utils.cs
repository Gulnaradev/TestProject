using OpenQA.Selenium;

namespace Kaspersky.TestTask.Youtube
{
    public static class Utils
    {
        public static string GetPartialUrl(IWebDriver driver, int startIndex, int lastIndex)
        {
            return driver.Url.Substring(0, 20);
        }

        public static bool IsAttributeSetted(IWebElement element, string attribute)
        {
            var result = false;

            if (element.GetAttribute(attribute) != null)
            {
                result = true;
            };

            return result;
        }
    }
}
