namespace CBLib.ComicManage
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ComicSerie
    {
        [JsonProperty(PropertyName = "name")]
        public String SerieName { get; set; }
        [JsonProperty(PropertyName = "id")]
        public UInt16 SerieId { get; set; }
        [JsonProperty(PropertyName = "serie_url")]
        public String SerieUrl { get; set; }
        [JsonProperty(PropertyName = "albums_url")]
        public String AlbumsUrl { get; set; }
        [JsonProperty(PropertyName = "category")]
        public String SerieCategory { get; set; }
        [JsonProperty(PropertyName = "synopsis")]
        public String SerieSynopsis { get; set; }
        [JsonProperty(PropertyName = "finished")]
        public Boolean SerieFinished { get; set; }
        [JsonProperty(PropertyName = "urls")]
        public List<String> AlbumUrls { get; set; } = new List<String>();
        [JsonProperty(PropertyName = "albums")]
        public List<ComicAlbum> ComicAlbums { get; set; } = new List<ComicAlbum>();
    }
}