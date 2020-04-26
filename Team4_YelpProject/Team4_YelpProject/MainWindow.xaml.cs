using System.Windows;
using Team4_YelpProject.ViewModel;

namespace Team4_YelpProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        YelpViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new YelpViewModel();
            this.DataContext = ViewModel;
        }
    }
}
