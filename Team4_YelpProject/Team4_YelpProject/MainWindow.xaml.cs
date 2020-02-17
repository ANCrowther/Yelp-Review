using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Team4_YelpProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Business
        {
            public string name { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public string business_id { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            addState();
            addGridColumns();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgre; Database = milestone1db; password = spiffy";
        }


    }
}
