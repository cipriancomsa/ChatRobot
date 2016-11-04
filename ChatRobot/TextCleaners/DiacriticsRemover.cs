using ApplicationBoot.Annotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ChatRobot.TextCleaners
{
    [Service("DiacriticsRemover", typeof(ITextCleaner))]
    public class DiacriticsRemover : ITextCleaner
    {
        public string GetCleaned(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}
