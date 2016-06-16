namespace CBViewerX
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using CBLib.Comic;
    using CBWinLib.Comic;
    using CBWinLib.File;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            var list = new ObservableCollection<ComicSerie>();
            /*list.LoadFromFiles(new[] {
                    @"D:\Bds\4 princes de Ganahan (Les)\infos.json",
                    @"D:\Bds\666\infos.json",
                    @"D:\Bds\6666\infos.json",
                    @"D:\Bds\Berceuse assassine\infos.json",
                    @"D:\Bds\Blacksad\infos.json"
                });*/
            list.LoadFromDirectory(@"d:\bds"); //, IsLight: true);
            listBox.ItemsSource = list;
        }
    }
}