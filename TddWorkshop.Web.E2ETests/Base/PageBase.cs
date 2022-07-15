using OpenQA.Selenium;

namespace TddWorkshop.Web.E2ETests.Base;

public class PageBase: PageObjectBase
{
    public PageBase(IWebDriver driver, string? url) : base(driver)
    {
        if (url != null)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}