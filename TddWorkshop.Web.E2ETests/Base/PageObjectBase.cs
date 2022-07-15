using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace TddWorkshop.Web.E2ETests.Base;

public abstract class PageObjectBase
{
    private static class Cache<T>
    {
        internal static Dictionary<string, Func<T, IWebElement>> WebElements = typeof(T)
            .GetProperties()
            .Where(x => x.CanRead && typeof(IWebElement).IsAssignableFrom(x.PropertyType))
            .ToDictionary(x => x.Name, x => (Func<T, IWebElement>)(y => (IWebElement)x.GetValue(y)!));
        
        internal static Dictionary<string, Func<T, PageObjectBase>> PageObjects = typeof(T)
            .GetProperties()
            .Where(x => x.CanRead && typeof(PageObjectBase).IsAssignableFrom(x.PropertyType))
            .ToDictionary(x => x.Name, x => (Func<T, PageObjectBase>)(y => (PageObjectBase)x.GetValue(y)!));
    }

    protected readonly IWebDriver Driver;
    
    
    public PageObjectBase(IWebDriver driver)
    {
        Driver = driver;
    }

    protected virtual string Prefix => GetType().Name;
    
    protected IWebElement ById(string name) 
        => Driver.FindElement(By.Id($"{Prefix.ToLowerCamelCase()}_{name.ToLowerCamelCase()}"));
    
    protected void FillIn(object obj)
    {
        FillIn((dynamic)this, obj);
    }
    
    private static void FillIn<TPageObject>(TPageObject pageObject, object obj)
    {
        var objProps = obj
            .GetType()
            .GetProperties()
            .Where(x => x.CanRead)
            .ToDictionary(x => x.Name, x => (Func<object, object?>)(x.GetValue));

        foreach (var element in Cache<TPageObject>.WebElements)
        {
            if (objProps.ContainsKey(element.Key))
            {
                var value = objProps[element.Key](obj);
                if (value != null)
                {
                    var webElement = element.Value(pageObject);

                    if (webElement.TagName == "input" && webElement.GetAttribute("type") != "checkbox"
                        || webElement.TagName == "textarea")
                    {
                        webElement.Clear();
                        webElement.SendKeys(PrepareValue(value));
                    }
                }
            }
        }
        
        foreach (var element in Cache<TPageObject>.PageObjects)
        {
            if (objProps.ContainsKey(element.Key))
            {
                var value = objProps[element.Key](obj);
                var poValue = (dynamic)element.Value(pageObject);
                if (poValue != null)
                {
                    FillIn(poValue, value);
                }
            }
        }
    }

    private static string? PrepareValue(object value)
    {
        if (value is DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        // Comment these lines to make MVC work
        if (value.GetType().IsEnum)
        {
            return ((int)value).ToString();
        }
        
        return value.ToString();
    }
}