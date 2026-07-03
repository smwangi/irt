using System.Text.RegularExpressions;

namespace Irt.Application
{
    public static partial class Extensions
    {
        extension(string str)
        {
            public string ToCamelCase()
            {
                if (string.IsNullOrEmpty(str))
                {
                    return str;
                }

                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }

            public string ToSnakeCase()
                => MyRegex2().Replace(MyRegex1().Replace(MyRegex().Replace(str, "$1_$2"), "$1_$2"), "_")
                        .ToLower();
        }

        [GeneratedRegex(@"([\p{Lu}]+)([\p{Lu}][\p{Ll}])")]
        private static partial Regex MyRegex();
        [GeneratedRegex(@"([\p{Ll}\d])([\p{Lu}])")]
        private static partial Regex MyRegex1();
        [GeneratedRegex(@"[-\s]")]
        private static partial Regex MyRegex2();
    }
}
