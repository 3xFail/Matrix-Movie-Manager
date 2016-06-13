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
    /// Interaction logic for DoIHaveItPage.xaml
    /// </summary>
    public partial class DoIHaveItPage : Page
    {
        MainWindow m_win;
        public DoIHaveItPage()
        {
            InitializeComponent();
        }

        public DoIHaveItPage(MainWindow win)
        {
            InitializeComponent();
            m_win = win;
        }

        private void Do_I_Have_it_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            m_win.Main_Frame.Navigate(string.Empty);
        }

    }
}
