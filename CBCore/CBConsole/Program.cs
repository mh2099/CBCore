namespace CBConsole
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var x = CBLib.CBStorage.GetFiles(@"d:\Bds");

            Console.ReadKey(true);
        }
    }
}
