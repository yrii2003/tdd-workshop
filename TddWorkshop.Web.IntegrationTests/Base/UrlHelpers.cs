using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TddWorkshop.Web.IntegrationTests.Base;

public static class UrlHelpers
{
    public static string ToQueryString(this object request, string prefix = "")
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var properties = request
            .GetType()
            .GetProperties()
            .Where(x => x.CanRead)
            .Where(x => x.GetValue(request, null) != null)
            .ToDictionary(x => x.Name, x => x.GetValue(request, null));

        if (!properties.Any())
        {
            return "";
        }

        var values = properties
            .Where(x => IsValueTypeOrString(x.Value))
            .Select(x => $"{prefix}{x.Key}={x.Value}")
            .ToList();

        var valStr = values.Any()
            ? values.Aggregate((c, n) => $"{c}&{n}")
            : "";

        var subValues = properties
            .Where(x => IsRefTypeAndNotString(x.Value))
            .Select(x => x.Value?.ToQueryString(x.Key + "."))
            .ToList();

        var subValuesStr = subValues.Any()
            ? subValues.Aggregate((c, n) => $"{c}&{n}")
            : "";

        return string.Join("&", valStr, subValuesStr).Trim('&');
    }

    private static bool IsValueTypeOrString(object? value)
    {
        var type = value?.GetType();
        if (type == null)
        {
            return false;
        }

        return type.IsValueType == true || typeof(string) == type;
    }

    private static bool IsRefTypeAndNotString(object? value)
    {
        var type = value?.GetType();
        if (type == null)
        {
            return false;
        }

        return type.IsValueType == false && typeof(string) != type;
    }
}