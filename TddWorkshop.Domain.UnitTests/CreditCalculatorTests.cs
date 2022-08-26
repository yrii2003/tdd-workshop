using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Bogus;
using Bogus.DataSets;
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
    public void Calculate_PointsCalculatedCorrectly(
        CalculateCreditRequest request, bool hasCriminalRecord, int points)
    {
        // var expected = 100;

        /*var request = new CalculateCreditRequest(
            new PersonalInfo(34, "U", "S"),
            new CreditInfo(ConsumerCredit, 2_000_000, Deposit.RealEstate, Employee),
            new PassportInfo("1234", "123456", DateTime.Now, "Smfr"));*/


        var res = CreditCalculator.Calculate(request, hasCriminalRecord);

        Assert.Equal(points, res.Points);
    }

    [Theory, AutoData]
    public void Calculate_AutoData_PercentsCalculatedCorrectly(
        CalculateCreditRequest request, bool hasCriminalRecord)
    {
        var res = CreditCalculator.Calculate(request, hasCriminalRecord);
        Assert.Equal(res.Points.ToInterestRate(), res.InterestRate);
    }

    [Property(Arbitrary = new[] { typeof(PostiveArbitraries) })]
    public bool Calculate_PercentsCalculatedCorrectly(
       CalculateCreditRequest request, bool hasCriminalRecord)
    {
        var res = CreditCalculator.Calculate(request, hasCriminalRecord);
        return res.Points.ToInterestRate() == res.InterestRate;
    }


    [Property(Arbitrary = new[] { typeof(PostiveArbitraries) })]
    public bool Respons_PercentsCalculatedCorrectly(
      CalculateCreditRespons response)
    {

        return response.Points.ToInterestRate() == response.InterestRate;
    }

    [Property(Arbitrary = new[] { typeof(PostiveArbitraries) })]
    public bool Respons_Greater80_IsApproved(
      CalculateCreditRespons response)
    {

        return response.IsApproved == response.Points > 80;
    }

    [Fact]
    public void Calculate_WrongEnum()
    {
        var faker = new Faker();
        var request = new CalculateCreditRequest(
            new PersonalInfo(33, faker.Person.FirstName, faker.Person.LastName),
                new CreditInfo(ConsumerCredit, 2_000_000, Deposit.Guarantor, (Employment)(-1), false),
            new PassportInfo("1234", "123456", faker.Date.Past(2), faker.Company.CompanyName()));

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            CreditCalculator.Calculate(request, false);
        });
    }
}

public class CreditCalculatorTestData : IEnumerable<object[]>
{
    public static readonly CalculateCreditRequest Maximum =
        CreateRequest(30, ConsumerCredit, 1_000_001, Deposit.RealEstate, Employee, false);
    
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] // 100 points - 12,5%
            { Maximum, false, 100 };

        yield return new object[] // 85 points - 26%
            { CreateRequest(30, ConsumerCredit, 1_000_001, Deposit.RealEstate, Employee, false), true, 85 };

        yield return new object[] // 16 points
            { CreateRequest(21, RealEstate, 5_000_001, Deposit.None, Unemployed, true), true, 16 };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static CalculateCreditRequest CreateRequest(int age, CreditGoal goal, decimal sum,
        Deposit deposit, Employment employment, bool hasOtherCredits)
    {
        var faker = new Faker();
        return new CalculateCreditRequest(
            new PersonalInfo(age, faker.Person.FirstName, faker.Person.LastName),
            new CreditInfo(goal, sum, deposit, employment, hasOtherCredits),
            new PassportInfo("1234", "123456", faker.Date.Past(2), faker.Company.CompanyName())
        );
    }
}