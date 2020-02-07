using System;
using System.Collections.Generic;

namespace Dr.TrainsCli.Extensions
{
    public static class StringArrayExtensions
    {
        public static bool TryGetOptionValue(this string[] str, string optionName, string optionAlias, out string? value, string? defaultValue)
        {
            var idx = Array.FindIndex(str, s => s == optionName || s == optionAlias) + 1;

            if(idx > 0 && idx < str.Length)
            {
                value = str[idx];
                return true;
            }

            value = defaultValue;
            return false;
        }

        public static bool ContainsCommand(this string[] str, string commandName)
            => ContainsOption(str, commandName, null);

        public static bool ContainsOption(this string[] str, string optionName, string? optionAlias)
        {
            var idx = Array.FindIndex(str, s => s.ToLower() == optionName.ToLower() || s.ToLower() == optionAlias!.ToLower());
            return (idx >= 0);
        }
    }
}
