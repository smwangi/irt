using System.Text.RegularExpressions;

namespace Irt.Application
{
    public static partial class Extensions
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string ToSnakeCase(this string str)
            => MyRegex2().Replace(MyRegex1().Replace(MyRegex().Replace(str, "$1_$2"), "$1_$2"), "_")
                    .ToLower();
        [GeneratedRegex(@"([\p{Lu}]+)([\p{Lu}][\p{Ll}])")]
        private static partial Regex MyRegex();
        [GeneratedRegex(@"([\p{Ll}\d])([\p{Lu}])")]
        private static partial Regex MyRegex1();
        [GeneratedRegex(@"[-\s]")]
        private static partial Regex MyRegex2();
    }
}