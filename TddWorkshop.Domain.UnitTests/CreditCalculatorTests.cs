using System.Collections;
using System.Collections.Generic;
using Bogus;
using FsCheck.Xunit;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Domain.Tests.Arbitraries;
using TddWorkshop.Domain.Tests.Extensions;
using Xunit;
using static TddWorkshop.Domain.InstantCredit.CreditGoal;
using static TddWorkshop.Domain.InstantCredit.Employment;

namespace TddWorkshop.Domain.Tests;

public class CreditCalculatorTests
{
    [Theory, ClassData(typeof(CreditCalculatorTestData))]
    public void Calculate_A(CalculateCreditRequest request, bool hasCriminalRecord, int points)
    {
        var res = CreditCalculator.Calculate(request, hasCriminalRecord);
        Assert.Equal(points, res.Points);
    }

    [Property(Arbitrary = new[] { typeof(PostiveArbitraries) })]
    public bool Calculate_IsSatisfiedEqualsPointsGreaterThan80(CalculateCreditRequest request, bool hasCriminalRecord)
    {
        var res = CreditCalculator.Calculate(request, hasCriminalRecord);
        return res.IsApproved == res.Points >= 80;
    }

    [Property(Arbitrary = new[] { typeof(PostiveArbitraries) })]
    public bool Calculate_PercentCalculatedCorrectly(CalculateCreditRequest request, bool hasCriminalRecord)
    {
        var response = CreditCalculator.Calculate(request, hasCriminalRecord);

        if (!response.IsApproved)
        {
            return response.Points < 80;
        }

        var percent = response.Points.ToInterestRate();
        return response.InterestRate == percent;
    }
}

public class CreditCalculatorTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] 
            { CreateRequest(30, ConsumerCredit, 1_000_001, Deposit.RealEstate, Employee, false), false, 100 };
        
        // yield return new object[] { CreateRequest(0, 100, false), true, 0 };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static CalculateCreditRequest CreateRequest(int age, CreditGoal goal, decimal sum,
        Deposit deposit, Employment employment, bool hasOtherCredits)
    {
        var faker = new Faker();
        return new CalculateCreditRequest(
            new PersonalInfo(age, faker.Person.FirstName, faker.Person.LastName),
            new CreditInfo(goal, sum, deposit, employment, hasOtherCredits),
            new PassportInfo("1234", "123456", faker.Date.Past(), faker.Company.CompanyName())
        );
    }
}