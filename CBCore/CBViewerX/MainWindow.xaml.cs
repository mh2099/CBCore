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
            /*var list = new ObservableCollection<ComicSerie>();
            list.LoadFromDirectory(@"d:\bds", IsLight: true);
            dgComic.ItemsSource = list;*/

            var list2 = new ObservableCollection<ComicAlbum>();
            list2.LoadFromFile(@"D:\Bds\666\infos.json");
            lvAlbum.ItemsSource = list2;
        }
    }
}