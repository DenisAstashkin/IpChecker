using System.Windows;
using GMap.NET;
using GMap.NET.MapProviders;

namespace IpChecker
{    
    public partial class Map : Window
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public Map()
        {
            InitializeComponent();
        }
        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = AccessMode.ServerAndCache;
            mapView.MapProvider = GoogleMapProvider.Instance;
            mapView.MinZoom = 2;
            mapView.MaxZoom = 20;
            mapView.Zoom = 13; 
            mapView.Position = new PointLatLng(lat, lon);
            mapView.MouseWheelZoomType = MouseWheelZoomType.ViewCenter; 
            mapView.CanDragMap = true;             
            mapView.ShowCenter = true;
        }
    }
}