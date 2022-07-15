using System.Collections.Generic;
using System.Linq;

namespace TddWorkshop.Domain.Tests.Extensions;

public static class PointToPercent
{
    public static decimal? ToInterestRate(this int points)
    {
        return points switch
        {
            < 80 => null,
            < 84 => 30,
            < 88 => 26,
            < 92 => 22,
            < 96 => 19,
            < 100 => 15,
            100 => 12.5m,
            _ => null
        };
    }
}