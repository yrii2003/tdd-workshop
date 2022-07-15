namespace TddWorkshop.Web.E2ETests.Base;

public static class StringExtension
{
    public static string ToLowerCamelCase(this string str) =>
        string.IsNullOrEmpty(str) || str.Length < 2
            ? str.ToLowerInvariant()
            : char.ToLowerInvariant(str[0]) + str.Substring(1);
}