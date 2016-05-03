namespace CBConsole
{
    using System;
    using CBLib.FileManage;
    using CBLib.ComicManage;

    class Program
    {
        static void Main()
        {
            ComicFileList.GetFiles(@"d:\bds");
            ComicFileList.SetInfos();
            //CBFileList.CreateList(@"d:\bds\out.txt");
            //ComicFileList.Rename();
            
            Console.WriteLine("done!");
            Console.ReadKey(true);
        }
    }
}
