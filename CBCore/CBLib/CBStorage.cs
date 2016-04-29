namespace CBLib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using CBPortableLib;

    public static class CBStorage
    {
        public static IEnumerable<CBFile> GetFiles(String CBDirectory)
        {
            if (!Directory.Exists(CBDirectory)) return null;

            var r = new List<CBFile>();
            var directories = Directory.GetDirectories(CBDirectory);

            foreach(var d in directories)
            {
                var files = Directory.EnumerateFiles(d);

                foreach(var f in files)
                {
                    var cbFile = CBFileFactory.CBFileCreate(f);
                    if(cbFile != null) r.Add(cbFile);
                }
            }

            return r;
        }

        public static IEnumerable<CBFile> LoadFiles(String CBFile)
        {
            if (!File.Exists(CBFile)) return null;

            var json = File.ReadAllText(CBFile);
            var cbFileList = JsonConvert.DeserializeObject<IEnumerable<CBFile>>(json);
            return cbFileList;
        }

        public static void SaveFiles(String CBDirectory, String CBFile)
        {
            var cbFiles = GetFiles(CBDirectory);

            var json = JsonConvert.SerializeObject(cbFiles);
            File.WriteAllText(CBFile, json);
        }
    }
    public static class CBFileFactory
    {
        public static CBFile CBFileCreate(String Filename)
        {
            if (!File.Exists(Filename)) return null;

            var cbFile = new CBFile();
            
            cbFile.Filename = Path.GetFileNameWithoutExtension(Filename);
            cbFile.Extension = Path.GetExtension(Filename);
            cbFile.Directory = Directory.GetParent(Filename).Name;
            cbFile.CompleteDirectory = Path.GetDirectoryName(Filename);

            var regexPattern = new Regex(@"(?<index>\d{2})");
            var match = regexPattern.Match(Path.GetFileName(Filename));
            if(match.Success)
            {
                var index = match.Groups["index"].Value.ToString();
                cbFile.Index = Convert.ToByte(index);
            }

            var info = new FileInfo(Filename);
            cbFile.CreationDate = info.CreationTime;
            cbFile.Size = info.Length;

            return cbFile;
        }
    }
}