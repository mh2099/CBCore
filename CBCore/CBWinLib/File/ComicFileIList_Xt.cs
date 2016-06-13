namespace CBWinLib.File
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using CBLib.Comic;
    using CBLib.File;
    using CBLib.Tools;
    using Comic;

    public static class ComicFileIList_Xt
    {
        #region Load
        public static void LoadFromJsonFile(this IList<ComicFile> List, String CBJsonFile)
        {
            if (!File.Exists(CBJsonFile)) return;

            var json = File.ReadAllText(CBJsonFile);
            List = JsonConvert.DeserializeObject<IList<ComicFile>>(json);
        }
        public static void LoadFromDirectory(this IList<ComicFile> List, String CBDirectory)
        {
            if (!Directory.Exists(CBDirectory)) return;

            var directories = Directory.GetDirectories(CBDirectory);

            List.Clear();

            foreach (var d in directories)
            {
                var files = Directory.EnumerateFiles(d).Where(a => ComicTools.GetComicExtensions().Contains(Path.GetExtension(a)));

                foreach (var f in files)
                {
                    var cbFile = ComicFileFactory.Create(f);
                    if (cbFile != null) List.Add(cbFile);
                }
            }
        }
        #endregion
        #region Save
        public static void SaveToJsonFile(this IList<ComicFile> List, String CBJsonFile)
        {
            var json = JsonConvert.SerializeObject(List, Formatting.Indented);
            File.WriteAllText(CBJsonFile, json);
        }
        #endregion
        #region Functions
        /// <summary>
        /// Compare this ComicFile list with anotherone
        /// </summary>
        /// <param name="ComicFiles"></param>
        public static void CompareWith(this IList<ComicFile> List, IList<ComicFile> ListToCompare, String ResultFile)
        {
            var sb = new StringBuilder();

            var seriesA = List.GroupBy(a => a.FileDirectory).Select(a => a.Key);
            var seriesB = ListToCompare.GroupBy(a => a.FileDirectory).Select(a => a.Key);

            sb.AppendLine("----- serieA (non présente en B) -----");
            foreach (var serieA in seriesA)
            {
                if (seriesB.Contains(serieA))
                {
                    foreach (var albumA in List.Where(a => a.FileDirectory == serieA))
                        if (!ListToCompare.Any(a => a.FileName == albumA.FileName))
                            sb.AppendLine($"{serieA}, file: {albumA.FileName}");
                }
                else
                    sb.AppendLine(serieA);
            }

            sb.AppendLine("----- serieB (non présente en A) -----");
            foreach (var serieB in seriesB)
            {
                if (seriesA.Contains(serieB))
                {
                    foreach (var albumB in ListToCompare.Where(a => a.FileDirectory == serieB))
                        if (!List.Any(a => a.FileName == albumB.FileName))
                            sb.AppendLine($"{serieB}, file: {albumB.FileName}");
                }
                else
                {
                    sb.AppendLine(serieB);
                }
            }

            File.WriteAllText(ResultFile, sb.ToString());
        }
        /// <summary>
        /// Create a list of all bds
        /// </summary>
        /// <param name="ComicListFile"></param>
        public static void CreateComicList(this IList<ComicFile> List, String ComicListFile)
        {
            var sb = new StringBuilder();

            foreach (var cbfile in List)
                sb.AppendLine($"{cbfile.Realname} : {cbfile.FileName}");

            File.WriteAllText(ComicListFile, sb.ToString());
        }
        /// <summary>
        /// Rename all cbz, cbr and pdf in a good format name
        /// </summary>
        /// <param name="ShowOnly"></param>
        public static void RenameComicFile(this IList<ComicFile> List, Boolean ShowOnly = false)
        {
            foreach (var cbfile in List)
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
        /// Find all missing albums and export it in a text file
        /// </summary>
        public static void FindMissingAlbum(this IList<ComicFile> List, String CBDirectory, String ExportFile)
        {
            var sb = new StringBuilder();

            Double i = 0;

            var series = List.GroupBy(a => a.FileDirectory).Select(a => a.Key);

            foreach (var serie in series)
            {
                var directory = List.Where(a => a.FileDirectory == serie).Select(a => a.FileFullDirectory).FirstOrDefault();
                var filename = Path.Combine(directory, "infos.json");

                if (File.Exists(filename))
                {
                    var json = File.ReadAllText(filename);
                    var cs = JsonConvert.DeserializeObject<ComicSerie>(json);

                    var files = Directory.EnumerateFiles(directory).Where(a => ComicTools.GetComicExtensions().Contains(Path.GetExtension(a)));
                    var fNumber = files.Select(a => a.GetOrder());

                    foreach (var album in cs.ComicAlbums)
                        if (album.AlbumOrder > 0)
                            if (!fNumber.Contains(album.AlbumOrder))
                                sb.AppendLine($"{serie.GetRealName()} - {album.AlbumOrder.ToString("00")}");
                }

                i++;

                Double value = i / series.Count() * 100d;

                Console.Clear();
                Console.Write($"serie: {value.ToString("0.00")}% {serie}");
            }

            File.WriteAllText(ExportFile, sb.ToString());

            Console.Clear();
        }
        /// <summary>
        /// Set informations (scrap from bedetheque) to a json file (in the album directory)
        /// </summary>
        public static void ScrapingComicInfos(this IList<ComicFile> List)
        {
            Double i = 0;
            var series = List.GroupBy(a => a.FileDirectory).Select(a => a.Key);

            foreach (var serie in series)
            {
                var directory = List.Where(a => a.FileDirectory == serie).Select(a => a.FileFullDirectory).FirstOrDefault();
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

            Console.Clear();
        }
        #endregion
    }
}