namespace CBLib.ComicManage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using LiteDB;
    using Newtonsoft.Json;

    public class ComicSerieList
    {
        #region Variables
        private List<ComicSerie> _list = new List<ComicSerie>();
        #endregion
        #region Public Load
        public void LoadFromDirectory(String CBDirectory, Boolean IsLight = false)
        {
            if (!Directory.Exists(CBDirectory)) return;

            var directories = Directory.GetDirectories(CBDirectory);
            
            _list.Clear();

            foreach (var d in directories)
            {
                var info = Path.Combine(d, "infos.json");
                if(File.Exists(info))
                {
                    var json = File.ReadAllText(info);
                    var cs = JsonConvert.DeserializeObject<ComicSerie>(json);

                    if(IsLight)
                        foreach(var ca in cs.ComicAlbums)
                            ca.AlbumCoverBytes = null;

                    _list.Add(cs);
                }
            }
        }
        public void LoadFromLiteDb(String LiteDbFile)
        {
            if (!File.Exists(LiteDbFile)) return;

            _list.Clear();

            using (var db = new LiteDatabase(LiteDbFile))
            {
                var col = db.GetCollection<ComicSerie>("comicserie");

                _list = col.FindAll().ToList(); ;
            }
        }
        #endregion
        #region Save
        public void SaveToLiteDb(String LiteDbFile)
        {
            using (var db = new LiteDatabase(LiteDbFile))
            {
                var col = db.GetCollection<ComicSerie>("comicserie");
                foreach (var cs in _list)
                    col.Insert(cs);
            }
        }
        #endregion
    }
}