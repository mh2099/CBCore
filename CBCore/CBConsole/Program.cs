namespace CBConsole
{
    using System;
    using CBLib.FileManage;
    using CBLib.ComicManage;

    class Program
    {
        static void Main()
        {
            var cfl1 = new ComicFileList();
            cfl1.LoadFromDirectory(@"d:\bds");         
            //cfl1.RenameComicFile();
            //cfl1.ScrapingComicInfos();
            cfl1.FindMissingAlbum(@"d:\bds", @"d:\missing_album.txt");

            //var cfl2 = new ComicFileList();
            //cfl2.LoadFromDirectory(@"e:\bd");
            //cfl2.RenameComicFile();
            //cfl2.ScrapingComicInfos();
            //cfl1.CompareWith(cfl2, @"d:\bd_compare.txt");

            //var csl = new ComicSerieList();
            //csl.LoadFromDirectory(@"d:\bds", IsLight: true);
            //csl.SaveToLiteDb(@"d:\bd.light.litedb");
            //csl.LoadFromLiteDb(@"d:\bd.light.litedb");

            Console.WriteLine("done!");
            Console.ReadKey(true);
        }
    }
}
