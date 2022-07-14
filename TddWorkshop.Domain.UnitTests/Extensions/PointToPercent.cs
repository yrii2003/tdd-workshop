using System.Collections.Generic;
using System.Linq;

namespace TddWorkshop.Domain.Tests.Extensions;

public static class PointToPercent
{
    private static Dictionary<int, decimal> Percents = new()
    {
        { 80, 30 },
        { 84, 26 },
        { 88, 22 },
        { 92, 19 },
        { 96, 15 },
        { 100, 12.5m }
    };

    public static decimal? ToInterestRate(this int points)
    {
        var index = -1;
        foreach (var kv in Percents)
        {
            if (kv.Key > points)
            {
                break;
            }
            
            index++;
        }

        var percent = Percents[Percents.Keys.ElementAt(index)];
        return index != -1
            ? percent
            : null;
    }
}