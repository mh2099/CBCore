namespace CBLib.FileManage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Newtonsoft.Json;
    using ComicManage;

    public static class ComicFileList
    {
        #region Variables
        private static List<ComicFile> _list = new List<ComicFile>();
        #endregion
        #region Get
        public static void LoadFromFile(String OutputFile)
        {
            if (!File.Exists(OutputFile)) return;

            var json = File.ReadAllText(OutputFile);
            _list = JsonConvert.DeserializeObject<List<ComicFile>>(json);
        }
        public static void GetFiles(String CBDirectory)
        {
            if (!Directory.Exists(CBDirectory)) return;

            var directories = Directory.GetDirectories(CBDirectory);
            var extensions = new HashSet<String>(StringComparer.OrdinalIgnoreCase) { ".cbz", ".cbr", ".pdf" };

            _list.Clear();

            foreach (var d in directories)
            {
                var files = Directory.EnumerateFiles(d).Where(a => extensions.Contains(Path.GetExtension(a)));

                foreach (var f in files)
                {
                    var cbFile = ComicFile.Create(f);
                    if (cbFile != null) _list.Add(cbFile);
                }
            }
        }
        #endregion
        #region Set
        public static void SaveToFile(String OutputFile)
        {
            var json = JsonConvert.SerializeObject(_list, Formatting.Indented);
            File.WriteAllText(OutputFile, json);
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Create a list of all bds
        /// </summary>
        /// <param name="OutputFile"></param>
        public static void CreateList(String OutputFile)
        {
            var sb = new StringBuilder();

            foreach (var cbfile in _list)
                sb.AppendLine($"{cbfile.Realname} : {cbfile.FileName}");

            File.WriteAllText(OutputFile, sb.ToString());
        }
        /// <summary>
        /// Rename all cbz, cbr and pdf in a good format name
        /// </summary>
        /// <param name="ShowOnly"></param>
        public static void Rename(Boolean ShowOnly = false)
        {
            foreach (var cbfile in _list)
            {
                if (cbfile.BdIndex > -1)
                {
                    var oldFilename = Path.Combine(cbfile.FileFullDirectory, $"{cbfile.FileName}{cbfile.FileExtension}");
                    var newFilename = Path.Combine(cbfile.FileFullDirectory, $"{cbfile.Realname}{cbfile.FileExtension}");

                    if (ShowOnly)
                        Console.WriteLine($"{oldFilename} --> {newFilename}");
                    else
                        File.Move(oldFilename, newFilename);
                }
                else
                {
                    var filename = Path.Combine(cbfile.FileFullDirectory, $"{cbfile.FileName}{cbfile.FileExtension}");
                    if (ShowOnly)
                        Console.WriteLine($"keep: {filename}");
                }
            }
        }
        /// <summary>
        /// Set informations (scrap from bedetheque) to a json file (in the album directory)
        /// </summary>
        public static void SetInfos()
        {
            Double i = 0;
            var series = _list.GroupBy(a => a.FileDirectory).Select(a => a.Key);

            foreach (var serie in series)
            {
                var directory = _list.Where(a => a.FileDirectory == serie).Select(a => a.FileFullDirectory).FirstOrDefault();
                var filename = Path.Combine(directory, "infos.json");

                if (!File.Exists(filename))
                {
                    var cs = ComicScrapper.GetSerie(serie);

                    var json = JsonConvert.SerializeObject(cs);

                    File.WriteAllText(filename, json);
                }

                i++;

                Double value = i / series.Count() * 100d;

                Console.Clear();
                Console.Write($"serie: {value.ToString("0.00")}% {serie}");
            }

        }
        #endregion
    }
}