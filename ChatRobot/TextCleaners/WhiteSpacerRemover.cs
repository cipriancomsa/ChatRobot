using ApplicationBoot.Annotations;
using System;

namespace ChatRobot.TextCleaners
{
    [Service("WhiteSpacerRemover", typeof(ITextCleaner))]
    public class WhiteSpacerRemover : ITextCleaner
    {
        private static readonly char[] whitespace = new char[] { ' ', '\n', '\t', '\r', '\f', '\v' };

        public string GetCleaned(string text)
        {
            return String.Join(" ", text.Split(whitespace, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
