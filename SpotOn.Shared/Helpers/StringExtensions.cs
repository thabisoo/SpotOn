using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.Shared.Helpers
{
    public static class StringExtensions
    {
        public static string Truncate(string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= length ? value : $"{value.Substring(0, length)}... More";
        }
    }
}
