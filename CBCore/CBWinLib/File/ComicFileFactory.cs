namespace CBWinLib.File
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using CBLib.File;
        
    public static class ComicFileFactory
    {
        public static ComicFile Create(String Filename)
        {
            if (!File.Exists(Filename)) return null;

            var cbFile = new ComicFile();

            cbFile.FileName = Path.GetFileNameWithoutExtension(Filename);
            cbFile.FileExtension = Path.GetExtension(Filename);
            cbFile.FileDirectory = Directory.GetParent(Filename).Name;
            cbFile.FileFullDirectory = Path.GetDirectoryName(Filename);

            var info = new FileInfo(Filename);
            cbFile.FileCreationDate = info.CreationTime;
            cbFile.FileSize = info.Length;

            var regexPattern = new Regex(@"(?<index>\d{2}$)");
            var match = regexPattern.Match(Path.GetFileNameWithoutExtension(Filename));
            if (match.Success)
            {
                var index = match.Groups["index"].Value.ToString();
                cbFile.BdIndex = Convert.ToSByte(index);
            }

            var regexPattern2 = new Regex(@"HS\s{0,1}\d{2}$");
            var match2 = regexPattern2.Match(Path.GetFileNameWithoutExtension(Filename));
            if (match2.Success) cbFile.BdHS = true;

            var regexPattern3 = new Regex(@"(?<word>(\w|\s|-|'|,|\.)+)\s{0,1}(\((?<article>(The|Le|La|L'|Les))\)){0,1}");
            var match3 = regexPattern3.Match(Directory.GetParent(Filename).Name);
            if (match3.Success)
            {
                var article = match3.Groups["article"].Value.ToString().Trim();
                var word = match3.Groups["word"].Value.ToString().Trim();
                var hs = cbFile.BdHS ? "HS " : String.Empty;

                if (article.Length > 0)
                {
                    if (!article.Contains("L'"))
                        article = $"{article} ";
                    word = $"{Char.ToLower(word[0])}{word.Substring(1)}";
                }

                cbFile.Realname = $"{article}{word} - {hs}{cbFile.BdIndex.ToString("00")}";
            }

            return cbFile;
        }
    }
}