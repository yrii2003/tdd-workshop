using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Versioning;
using FsCheck;

namespace TddWorkshop.Domain.Tests.Extensions;

public static class KeyValues<T>
{
    public static Arbitrary<TProperty> ToArbitrary<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is not MemberExpression mce)
        {
            throw new InvalidOperationException("expression must be of type MemberExpression");
        }

        var kva = mce.Member
            .GetCustomAttributes<KeyValuesAttribute>()
            .ToList();

        if (!kva.Any())
        {
            throw new InvalidOperationException($"KeyValuesAttribute is not defined on property {mce.Member.Name}");
        }

        var elements = kva
            .SelectMany(x => x.Objects)
            .Select(x => (TProperty)Convert.ChangeType(x, typeof(TProperty)))
            .ToArray();

        if (!elements.Any())
        {
            throw new InvalidOperationException($"KeyValues must not be empty for {mce.Member.Name}");
        }

        if (typeof(TProperty).IsNumericType())
        {
            elements = (TProperty[])AddEquivalenceClasses((dynamic)elements);
        }

        return Gen
            .Elements(elements)
            .ToArbitrary();
    }

    [RequiresPreviewFeatures]
    public static TNumber[] AddEquivalenceClasses<TNumber>(TNumber[] values)
        where TNumber : INumber<TNumber> =>
        values
            .Select(x => new[] { x - TNumber.One, x, x + TNumber.One })
            .SelectMany(x => x)
            .ToArray();

}