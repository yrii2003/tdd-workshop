using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TddWorkshop.Web.E2ETests.Base;

public class WebDriverFixture: IDisposable
{
    private const string BaseUrl = "https://localhost:5001";
    private readonly IWebDriver _driver;
    private bool _disposed;

    public WebDriverFixture()
    {
        try
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(BaseUrl);
        }
        catch (WebDriverException e)
        {
            _driver?.Dispose();
            throw;
        }

    }

    ~WebDriverFixture()
    {
        if (!_disposed)
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _driver.Dispose();
        }

        _disposed = true;
    }

    public void GoToUrl(string url)
    {
        _driver.Navigate().GoToUrl(url);
    }

    public T CreatePage<T>(string? url = null)
        where T: PageBase
    {
        return (T)Activator.CreateInstance(typeof(T), _driver, url)!;
    }
}