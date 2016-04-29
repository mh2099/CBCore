namespace CBPortableLib
{
    using System;

    public class CBFile
    {
        public String Filename { get; set; }
        public String Extension { get; set; }
        public String Directory { get; set; }
        public String CompleteDirectory { get; set; }
        public Byte Index { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 Size { get; set; }
    }
}