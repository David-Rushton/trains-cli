using System;
using System.Collections.Generic;

namespace trains_cli.Extensions
{
    public static class StringArrayExtensions
    {
        public static bool TryGetOptionValue(this string[] str, string optionName, string optionAlias, out string value)
        {
            string defaultReturn = null;
            var idx = Array.FindIndex(str, s => s == optionName || s == optionAlias) + 1;

            if(idx > 0 && idx < str.Length)
            {
                value = str[idx];
                return true;
            }

            value = defaultReturn;
            return false;
        }

        public static bool ContainsCommand(this string[] str, string commandName)
            => ContainsOption(str, commandName, null);

        public static bool ContainsOption(this string[] str, string optionName, string optionAlias)
        {
            var idx = Array.FindIndex(str, s => s == optionName || s == optionAlias);
            return (idx > 0);
        }
    }
}
