namespace CBWinLib.Comic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using LiteDB;
    using Newtonsoft.Json;
    using CBLib.Comic;

    public static class ComicSerieIList_Xt
    {
        #region Load
        public static void LoadFromDirectory(this IList<ComicSerie> List, String CBDirectory, Boolean IsLight = false)
        {
            if (!Directory.Exists(CBDirectory)) return;

            var directories = Directory.GetDirectories(CBDirectory);

            List.Clear();

            foreach (var d in directories)
            {
                var info = Path.Combine(d, "infos.json");
                if (File.Exists(info))
                {
                    var json = File.ReadAllText(info);
                    var cs = JsonConvert.DeserializeObject<ComicSerie>(json);

                    if (IsLight)
                        foreach (var ca in cs.ComicAlbums)
                            ca.AlbumCoverBytes = null;

                    List.Add(cs);
                }
            }
        }
        public static void LoadFromLiteDb(this IList<ComicSerie> List, String LiteDbFile)
        {
            if (!File.Exists(LiteDbFile)) return;

            List.Clear();

            using (var db = new LiteDatabase(LiteDbFile))
            {
                var col = db.GetCollection<ComicSerie>("comicserie");

                List = col.FindAll().ToList(); ;
            }
        }
        #endregion
        #region Save
        public static void SaveToLiteDb(this IList<ComicSerie> List, String LiteDbFile)
        {
            using (var db = new LiteDatabase(LiteDbFile))
            {
                var col = db.GetCollection<ComicSerie>("comicserie");
                foreach (var cs in List)
                    col.Insert(cs);
            }
        }
        #endregion
    }
}