namespace CBWinLib.File
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class FileIList_Xt
    {
        public static void LoadFromDirectory(this IList<String> List, String CBDirectory)
        {
            if (!Directory.Exists(CBDirectory)) return;

            var directories = Directory.GetDirectories(CBDirectory);

            List.Clear();

            foreach (var d in directories)
                List.Add(new DirectoryInfo(d).Name);
        }
    }
}
