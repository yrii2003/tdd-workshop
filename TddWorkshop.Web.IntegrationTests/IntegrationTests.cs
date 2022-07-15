using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Domain.Tests;
using TddWorkshop.Web.IntegrationTests.Base;
using Xunit;

namespace TddWorkshop.Web.IntegrationTests;

public class IntegrationTests//: IClassFixture<TddWorkshopWebApplicationFactory>
{
    private readonly TddWorkshopWebApplicationFactory _factory;

    // public IntegrationTests(TddWorkshopWebApplicationFactory factory)
    // {
    //     _factory = factory;
    // }
    
    [Theory(Skip = "Implement on Step 4"), ClassData(typeof(CreditCalculatorTestData))]
    public async Task Calculate_CreditApproved_PointsCalculatedCorrectly(CalculateCreditRequest request, 
        bool hasCriminalRecord, int points)
    {
        throw new NotImplementedException();
    }

    [Fact(Skip = "Implement on Step 4")]
    public async Task Calculate_WrongInput_ValidationFailed()
    {
        throw new NotImplementedException();
    }
}