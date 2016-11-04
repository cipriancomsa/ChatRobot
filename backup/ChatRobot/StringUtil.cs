using System;
using System.Text.RegularExpressions;

namespace ChatRobot
{
    public static class StringUtil
    {
        private static readonly char[] whitespace = new char[] { ' ', '\n', '\t', '\r', '\f', '\v' };
        public static string Normalize(string source)
        {
            return String.Join(" ", source.Split(whitespace, StringSplitOptions.RemoveEmptyEntries));
        }

        public static string[] SplitWhitespace(this string input)
        {
            char[] whitespace = new char[] { ' ', '\t' };
            return input.Split(whitespace);
        }

    }
}
