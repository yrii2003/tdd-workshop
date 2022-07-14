using System.Threading;
using AutoFixture.Xunit2;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Domain.Tests;
using TddWorkshop.Domain.Tests.Extensions;
using TddWorkshop.Web.E2ETests.Base;
using TddWorkshop.Web.E2ETests.Pages;
using Xunit;

namespace TddWorkshop.Web.E2ETests;

public class E2eTests: IClassFixture<WebDriverFixture>
{
    private readonly WebDriverFixture _fixture;

    public E2eTests(WebDriverFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory, ClassData(typeof(CreditCalculatorTestData))]
    public void CreditCalculatorForm_SendForm_ResponseReceived(CalculateCreditRequest request, 
        bool hasCriminalRecord, int points)
    {
        var page = _fixture.CreatePage<FormPage>();
        var result = page.Submit(request);
        Assert.Equal(points.ToInterestRate().ToString(), result.InterestRate.Text);
    }
}