using System.Windows;
using Microsoft.Maps.MapControl.WPF;

namespace Map
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double latitude = 48.4090;
        public double longitude = -122.4021;

        public MainWindow()
        {
            InitializeComponent();
            //DelegateEventHandlers();
        }

        private void DelegateEventHandlers()
        {
            /*    UNDER CONSTRUCTION    */
        }

        private void centerOnMe_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            LatLocation.Content = "___";
            LongLocation.Content = "___";
            LatLocation.Content = latitude.ToString();
            LongLocation.Content = longitude.ToString();

            Location currentLocation = new Location(latitude, longitude);
            Pushpin pin = new Pushpin();
            pin.Location = currentLocation;

            myMap.Children.Add(pin);
            myMap.Center = currentLocation;
        }
    }
}
