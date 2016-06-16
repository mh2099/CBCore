namespace CBLib.Comic
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json;

    public class ComicAlbum : INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "name")]
        public String AlbumName { get; set; }
        [JsonProperty(PropertyName = "order")]
        public Byte AlbumOrder { get; set; }
        [JsonProperty(PropertyName = "count")]
        public Byte AlbumCount { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime AlbumDate { get; set; }
        [JsonProperty(PropertyName = "scenarist")]
        public String AlbumScenarist { get; set; }
        [JsonProperty(PropertyName = "drawer")]
        public String AlbumDrawer { get; set; }
        [JsonProperty(PropertyName = "colorist")]
        public String AlbumColorist { get; set; }
        [JsonProperty(PropertyName = "collection")]
        public String AlbumCollection { get; set; }
        [JsonProperty(PropertyName = "editor")]
        public String AlbumEditor { get; set; }
        [JsonProperty(PropertyName = "isbn")]
        public String AlbumIsbn { get; set; }
        [JsonProperty(PropertyName = "cover")]
        public String AlbumCover { get; set; }
        [JsonProperty(PropertyName = "cover_bytes")]
        public Byte[] AlbumCoverBytes { get; set; }
        [JsonProperty(PropertyName = "note")]
        public Single Note { get; set; }
        [JsonProperty(PropertyName = "note_count")]
        public Single NoteCount { get; set; }
        [JsonProperty(PropertyName = "filename")]
        public String Filename { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T Storage, T value, [CallerMemberName] String PropertyName = null)
        {
            if (Equals(Storage, value))
                return false;

            Storage = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] String PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}