namespace Team4_YelpProject.View
{
    using System.Windows;
    using Team4_YelpProject.Model;
    using Team4_YelpProject.ViewModel;

    /// <summary>
    /// Interaction logic for BusinessTipsView.xaml
    /// </summary>
    public partial class BusinessTipsView : Window
    {
        TipsViewModel ViewModel;

        public BusinessTipsView(Business B, YelpUser U)
        {
            InitializeComponent();
            ViewModel = new TipsViewModel(B, U);
            this.DataContext = ViewModel;
        }
    }
}
