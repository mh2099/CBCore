namespace CBWinLib.Dev
{
    using System;
    using System.Collections.Generic;
    using CBLib.Comic;
    using Comic;

    public static class Generator
    {
        public static void GenerateComicAlbumList()
        {
            var list = new List<ComicAlbum>();
            list.LoadFromFile(@"D:\Bds\666\infos.json");
            list.SaveToXAML(@"d:\ca_666.xml", @"d:\ca_666.xaml");
        }

        public static void GenerateComicSerieList()
        {
            var list = new List<ComicSerie>();
            list.LoadFromFiles(new[] { @"D:\Bds\666\infos.json", @"D:\Bds\Alix Senator\infos.json", @"D:\Bds\Ars Magna\infos.json" });            
            list.SaveToXAML(@"d:\cs_666AlixArs.xml", @"d:\cs_666AlixArs.xaml");
        }
    }
}