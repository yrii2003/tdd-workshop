using FsCheck;
using JetBrains.Annotations;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Domain.Tests.Extensions;

namespace TddWorkshop.Domain.Tests.Arbitraries;

public static class PostiveArbitraries
{
    [UsedImplicitly]
    public static Arbitrary<int> AgeGenerator() => KeyValues<PersonalInfo>.ToArbitrary(x => x.Age);

    [UsedImplicitly]
    public static Arbitrary<decimal> SumGenerator() => KeyValues<CreditInfo>.ToArbitrary(x => x.Sum);
}