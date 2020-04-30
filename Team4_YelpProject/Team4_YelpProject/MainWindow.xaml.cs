//---REFERENCES---
// The following channel by Code Advocate has a 12 part 'Beginners guide to MVVM Pattern using WPF in C#' playlist that
//  heavily influenced how this project was made. Code Advocate took the time to not only demonstrate writing a MVVM code,
//  also took the time to explain the hows and whys for binding and layout.
// https://www.youtube.com/channel/UCt3V3nyiz-uOCjSuBJlMl1w
//
// The following Microsoft Docs page basically showed how to bind a treeview, did not complicate the matter talking about
//  the objects being bound. Extremely helpful as it did not clutter the explaination with weird ways of making classes
//  for binding purposes.
// https://docs.microsoft.com/en-us/archive/blogs/mikehillberg/treeview-and-hierarchicaldatatemplate-step-by-step
//---END REFERENCES---

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
