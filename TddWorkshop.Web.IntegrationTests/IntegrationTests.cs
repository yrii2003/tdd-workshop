using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using AutoFixture;
using AutoFixture.Xunit2;
using Bogus;
using Moq;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Domain.Tests;
using TddWorkshop.Domain.Tests.Extensions;
using TddWorkshop.Web.IntegrationTests.Base;
using Xunit;
using static TddWorkshop.Domain.InstantCredit.CreditGoal;
using static TddWorkshop.Domain.InstantCredit.Employment;

namespace TddWorkshop.Web.IntegrationTests;

public class IntegrationTests: IClassFixture<TddWorkshopWebApplicationFactory>
{
    private readonly TddWorkshopWebApplicationFactory _factory;

    public IntegrationTests(TddWorkshopWebApplicationFactory factory)
    {
         _factory = factory;
    }
    
    [Theory, ClassData(typeof(CreditCalculatorTestData))]
    public async Task Calculate_CreditApproved_PointsCalculatedCorrectly(CalculateCreditRequest request, 
        bool hasCriminalRecord, int points)
    {
        _factory.CriminalCheckerMock
            .Setup(x => x.HasCriminalRecordAsync(request.PassportInfo, It.IsAny<CancellationToken>()))
            .ReturnsAsync(hasCriminalRecord);
        
        var client = _factory.CreateClient();
        var res = await client.PostAsync("Calculator/Calculate", JsonContent.Create(request));
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        var content = await res.Content.ReadFromJsonAsync<CalculateCreditRespons>();

        Assert.Equal(points, content?.Points);
        Assert.Equal(points.ToInterestRate(), content?.InterestRate);

        _factory.VerifiHas();
    }

    // [Fact]
    [Theory, ClassData(typeof(WrongDataClassData))]
    public async Task Calculate_WrongInput_ValidationFailed(CalculateCreditRequest request)
    {
       /* var fixture = new Fixture();
        fixture.Register(() => (Deposit)(-1));
        var request = fixture.Create<CalculateCreditRequest>();*/

        var res = await _factory.CreateClient().PostAsync("Calculator/Calculate", JsonContent.Create(request));

        Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
    }
}

internal class WrongDataClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var fixture = new Fixture();
        fixture.Register(() => (Deposit)(-1));

        yield return new object[] 
            {fixture.Create<CalculateCreditRequest>()};

        var fixture2 = new Fixture();
        fixture2.Register(() => "1");

        yield return new object[] // 16 points
              {fixture2.Create<CalculateCreditRequest>()};

        var fixture3 = new Fixture();
        var faker = new Faker();
        fixture3.Register(() => faker.Date.Future());

        yield return new object[] // 16 points
              {fixture3.Create<CalculateCreditRequest>()};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


}