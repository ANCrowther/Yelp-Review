using System;
using System.Windows;

namespace Map
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myMap.Loaded += MyMap_Loaded;
        }

        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            //set map to lat and long Everett
            myMap.Center =
            new Geopoint(new BasicGeoposition()
            {
                Latitude = 48.0090,
                Longitude = -122.2021
            });

            myMap.ZoomLevel = 16;
        }
    }
