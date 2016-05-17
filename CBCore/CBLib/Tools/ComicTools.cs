namespace CBLib.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class ComicTools
    {
        public static HashSet<String> GetComicExtensions()
        {
            return new HashSet<String>(StringComparer.OrdinalIgnoreCase) { ".cbz", ".cbr", ".cb7", ".cba", ".cbt", ".pdf" };
        }

        public static Byte GetOrder(this String FileName)
        {
            var regexPattern = new Regex(@".{0,} - (?<order>\d{2})\..{0,}");
            var match = regexPattern.Match(FileName);
            if(match.Success)
                return Byte.Parse(match.Groups["order"].Value.ToString());
            return 0;
        }

        public static String GetRealName(this String DirectoryName)
        {
            var regexPattern = new Regex(@"(?<word>(\w|\s|-|'|,|\.)+)\s{0,1}(\((?<article>(The|Le|La|L'|Les))\)){0,1}");
            var match = regexPattern.Match(DirectoryName);
            if (match.Success)
            {
                var article = match.Groups["article"].Value.ToString().Trim();
                var word = match.Groups["word"].Value.ToString().Trim();

                if (article.Length > 0)
                {
                    if (!article.Contains("L'"))
                        article = $"{article} ";
                    word = $"{Char.ToLower(word[0])}{word.Substring(1)}";
                }

                return $"{article}{word}";
            }

            return DirectoryName;
        }
    }
}