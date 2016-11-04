namespace Utils.Urls
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class UrlEx
    {
        public static string RemoveQueryString(this string url)
        {
            return url.Split('?').FirstOrDefault();
        }

        public static string RemoveAnchorString(this string url)
        {
            return url.Split('#').FirstOrDefault();
        }

        public static string RemoveDiacritics(this string strThis)
        {
            if (strThis == null)
                return null;

            var sb = new StringBuilder();

            foreach (char c in strThis.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static string GetRootDomain(this string url)
        {
            string[] split = url.Split('.');
            if (split.Length > 2)
                return split[split.Length - 2] + "." + split[split.Length - 1];
            return url;
        }

        public static string ConvertToLink(this string input)
        {
            //Add http:// to link url
            var urlRx = new Regex(@"(?<url>(http(s?):[/][/]|www.)([a-z]|[A-Z]|[0-9]|[/.]|[~])*)", RegexOptions.IgnoreCase);

            MatchCollection matches = urlRx.Matches(input);

            foreach (Match match in matches)
            {
                string url = match.Groups["url"].Value;
                Uri uri = new UriBuilder(url).Uri;
                input = input.Replace(url, uri.AbsoluteUri);
            }
            return input;
        }

        public static string GetHostFromUrl(this string url)
        {
            var uri = new Uri(url);
            return uri.Host;
        }
    }
}