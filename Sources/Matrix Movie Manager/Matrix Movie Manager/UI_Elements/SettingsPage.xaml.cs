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
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public MainWindow m_win;
        public SettingsPage()
        {
            InitializeComponent();
        }
        //ctor that takes a main window object to reference the main frame that is used throughout the program
        public SettingsPage(MainWindow win)
        {
            InitializeComponent();
            m_win = win;
        }
        // returns the aplication to the main screen
        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            m_win.Main_Frame.Navigate(new Welcome_Page());
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            // maybe make a config file of sorts that would handle the assignments and changes being saved and made
        }

        //adds a path location to the possible seached paths
        private void Add_button_Click(object sender, RoutedEventArgs e)
        {

        }
        //opens a browse panel that allows a file to be selected to be used as a search path
        private void Browse_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
