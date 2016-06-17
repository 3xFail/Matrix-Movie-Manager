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

namespace Matrix_Movie_Manager.UI_Elements
{
    /// <summary>
    /// Interaction logic for MovieCatalog.xaml
    /// </summary>
    public partial class MovieCatalog : Page
    {
        MainWindow m_win;
        public MovieCatalog()
        {
            InitializeComponent();
        }

        public MovieCatalog(MainWindow win)
        {
            InitializeComponent();
            m_win = win;
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            m_win.Main_Frame.Navigate(new Welcome_Page());
        }

        
    }
}
