namespace CBLib.Tools
{
    using System;
    using System.Text.RegularExpressions;

    public static class ComicTools
    {
        public static String GetRealName(this String DirectoryName)
        {
            var regexPattern3 = new Regex(@"(?<word>(\w|\s|-|'|,|\.)+)\s{0,1}(\((?<article>(The|Le|La|L'|Les))\)){0,1}");
            var match3 = regexPattern3.Match(DirectoryName);
            if (match3.Success)
            {
                var article = match3.Groups["article"].Value.ToString().Trim();
                var word = match3.Groups["word"].Value.ToString().Trim();

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