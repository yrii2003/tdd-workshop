using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Web.E2ETests.Base;

namespace TddWorkshop.Web.E2ETests.Pages;

public class FormPage : PageBase
{
    public FormPage(IWebDriver driver, string url) : base(driver, url)
    {
        PersonalInfo = new PersonalInfo(driver);
        CreditInfo = new CreditInfo(driver);
        PassportInfo = new PassportInfo(driver);
        CreditResult = new CreditResult(driver);
    }

    protected IWebElement SubmitButton => Driver.FindElement(By.CssSelector("[type=\"submit\"]"));

    public PersonalInfo PersonalInfo { get; }

    public CreditInfo CreditInfo { get; }

    public PassportInfo PassportInfo { get; }
    
    protected CreditResult CreditResult { get; }

    public FormPage FillIn(CalculateCreditRequest request)
    {
        base.FillIn(request);
        return this;
    }

    public CreditResult Submit(CalculateCreditRequest request)
    {
        return FillIn(request).Submit();
    }
    
    public CreditResult Submit()
    {
        SubmitButton.Click();
        return new CreditResult(Driver);
    }
}

public class CreditResult: PageObjectBase
{
    public CreditResult(IWebDriver driver) : base(driver) { }

    public IWebElement InterestRate => Driver.FindElement(By.Id("interest-rate"), 10);
}

public class PersonalInfo : PageObjectBase
{
    public PersonalInfo(IWebDriver driver) : base(driver) { }

    public IWebElement Age => ById(nameof(Age));

    public IWebElement FirstName => ById(nameof(FirstName));

    public IWebElement LastName => ById(nameof(LastName));
}

public class CreditInfo : PageObjectBase
{
    public CreditInfo(IWebDriver driver) : base(driver) { }

    public IWebElement Sum => ById(nameof(Sum));

    public IWebElement Goal => ById(nameof(Goal));

    public IWebElement Deposit => ById(nameof(Deposit));

    public IWebElement Employment => ById(nameof(Employment));

    public IWebElement HasOtherCredits => ById(nameof(HasOtherCredits));
}

public class PassportInfo : PageObjectBase
{
    public PassportInfo(IWebDriver driver) : base(driver) { }
    
    public IWebElement Series => ById(nameof(Series));

    public IWebElement Number => ById(nameof(Number));

    public IWebElement IssueDate => ById(nameof(IssueDate));

    public IWebElement IssuedBy => ById(nameof(IssuedBy));

}