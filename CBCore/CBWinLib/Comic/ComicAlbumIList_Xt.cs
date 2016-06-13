namespace CBWinLib.Comic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using CBLib.Comic;

    public static class ComicAlbumIList_Xt
    {
        public static void LoadFromFile(this IList<ComicAlbum> List, String ComicInfoFile, Boolean IsLight = false)
        {
            if (!File.Exists(ComicInfoFile)) return;

            List.Clear();

            var json = File.ReadAllText(ComicInfoFile);
            var cs = JsonConvert.DeserializeObject<ComicSerie>(json);

            if (IsLight)
                foreach (var ca in cs.ComicAlbums)
                    ca.AlbumCoverBytes = null;

            foreach(var album in cs.ComicAlbums)
                List.Add(album);
        }
    }
}