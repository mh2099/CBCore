//      *********    NE PAS MODIFIER CE FICHIER     *********
//      Ce fichier est régénéré par un outil de création.Modifier
// .     ce fichier peut provoquer des erreurs.
namespace Expression.Blend.SampleData.SampleDataSource
{
    using System; 
    using System.ComponentModel;

// Pour réduire de façon significative le volume des exemples de données dans votre application de production, vous pouvez définir
// la constante de compilation conditionnelle DISABLE_SAMPLE_DATA et désactiver les données échantillons lors de l'exécution.
#if DISABLE_SAMPLE_DATA
    internal class SampleDataSource { }
#else

    public class SampleDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SampleDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("/CBViewerX;component/SampleData/SampleDataSource/SampleDataSource.xaml", UriKind.RelativeOrAbsolute);
                System.Windows.Application.LoadComponent(this, resourceUri);
            }
            catch
            {
            }
        }

        private ItemCollection _Collection = new ItemCollection();

        public ItemCollection Collection
        {
            get
            {
                return this._Collection;
            }
        }
    }

    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _SerieName = string.Empty;

        public string SerieName
        {
            get
            {
                return this._SerieName;
            }

            set
            {
                if (this._SerieName != value)
                {
                    this._SerieName = value;
                    this.OnPropertyChanged("SerieName");
                }
            }
        }

        private string _SerieCategory = string.Empty;

        public string SerieCategory
        {
            get
            {
                return this._SerieCategory;
            }

            set
            {
                if (this._SerieCategory != value)
                {
                    this._SerieCategory = value;
                    this.OnPropertyChanged("SerieCategory");
                }
            }
        }

        private ComicAlbums _ComicAlbums = new ComicAlbums();

        public ComicAlbums ComicAlbums
        {
            get
            {
                return this._ComicAlbums;
            }
        }
    }

    public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
    { 
    }

    public class ComicAlbumsItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _AlbumName = string.Empty;

        public string AlbumName
        {
            get
            {
                return this._AlbumName;
            }

            set
            {
                if (this._AlbumName != value)
                {
                    this._AlbumName = value;
                    this.OnPropertyChanged("AlbumName");
                }
            }
        }

        private double _AlbumOrder = 0;

        public double AlbumOrder
        {
            get
            {
                return this._AlbumOrder;
            }

            set
            {
                if (this._AlbumOrder != value)
                {
                    this._AlbumOrder = value;
                    this.OnPropertyChanged("AlbumOrder");
                }
            }
        }

        private double _AlbumCount = 0;

        public double AlbumCount
        {
            get
            {
                return this._AlbumCount;
            }

            set
            {
                if (this._AlbumCount != value)
                {
                    this._AlbumCount = value;
                    this.OnPropertyChanged("AlbumCount");
                }
            }
        }

        private string _AlbumDate = string.Empty;

        public string AlbumDate
        {
            get
            {
                return this._AlbumDate;
            }

            set
            {
                if (this._AlbumDate != value)
                {
                    this._AlbumDate = value;
                    this.OnPropertyChanged("AlbumDate");
                }
            }
        }

        private string _AlbumScenarist = string.Empty;

        public string AlbumScenarist
        {
            get
            {
                return this._AlbumScenarist;
            }

            set
            {
                if (this._AlbumScenarist != value)
                {
                    this._AlbumScenarist = value;
                    this.OnPropertyChanged("AlbumScenarist");
                }
            }
        }

        private string _AlbumDrawer = string.Empty;

        public string AlbumDrawer
        {
            get
            {
                return this._AlbumDrawer;
            }

            set
            {
                if (this._AlbumDrawer != value)
                {
                    this._AlbumDrawer = value;
                    this.OnPropertyChanged("AlbumDrawer");
                }
            }
        }

        private string _AlbumColorist = string.Empty;

        public string AlbumColorist
        {
            get
            {
                return this._AlbumColorist;
            }

            set
            {
                if (this._AlbumColorist != value)
                {
                    this._AlbumColorist = value;
                    this.OnPropertyChanged("AlbumColorist");
                }
            }
        }

        private System.Windows.Media.ImageSource _AlbumCoverBytes = null;

        public System.Windows.Media.ImageSource AlbumCoverBytes
        {
            get
            {
                return this._AlbumCoverBytes;
            }

            set
            {
                if (this._AlbumCoverBytes != value)
                {
                    this._AlbumCoverBytes = value;
                    this.OnPropertyChanged("AlbumCoverBytes");
                }
            }
        }
    }

    public class ComicAlbums : System.Collections.ObjectModel.ObservableCollection<ComicAlbumsItem>
    { 
    }
#endif
}
