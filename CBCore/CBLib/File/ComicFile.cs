namespace CBLib.File
{
    using System;
    using Newtonsoft.Json;

    public class ComicFile
    {
        [JsonProperty(PropertyName = "name")]
        public String FileName { get; set; }
        [JsonProperty(PropertyName = "extension")]
        public String FileExtension { get; set; }
        [JsonProperty(PropertyName = "directory")]
        public String FileDirectory { get; set; }
        [JsonProperty(PropertyName = "fulldirectory")]
        public String FileFullDirectory { get; set; }
        [JsonProperty(PropertyName = "creationdate")]
        public DateTime FileCreationDate { get; set; }
        [JsonProperty(PropertyName = "size")]
        public Int64 FileSize { get; set; }

        [JsonProperty(PropertyName = "index")]
        public SByte BdIndex { get; set; } = -1;
        [JsonProperty(PropertyName = "hs")]
        public Boolean BdHS { get; set; }
        
        public String Realname { get; set; }
    }
}