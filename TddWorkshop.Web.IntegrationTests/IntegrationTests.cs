using System.Net;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Domain.Tests;
using TddWorkshop.Web.IntegrationTests.Base;
using Xunit;

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
        var res = await _factory
            .HttpClient
            .PostAsync("Calculator/Calculate", JsonContent.Create(request));
        
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        var content = await res.Content.ReadFromJsonAsync<CalculateCreditResponse>();
        
        Assert.Equal(points, content?.Points);
        _factory.CriminalCheckerMock.Verify(x => x.HasCriminalRecord(
                It.IsAny<PersonalInfo>(), 
                It.IsAny<CancellationToken>()),
            Times.Exactly(1));
    }
}