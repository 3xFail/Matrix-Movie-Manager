using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        private List<string> unsaved_paths;
        public SettingsPage()
        {
            InitializeComponent();
        }
        //ctor that takes a main window object to reference the main frame that is used throughout the program
        public SettingsPage(MainWindow win)
        {
            InitializeComponent();
            //contains the setting files
            m_win = win;
        }
        // returns the aplication to the main screen
        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            m_win.Main_Frame.Navigate(new Welcome_Page());
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            //store everything that was added into the settings lists in m_win then write it to the file in m_win in xml
            if(unsaved_paths.Count > 0)
            {
                foreach(string s in unsaved_paths)
                {
                    m_win.m_settings.paths.Add( s );
                }
            }
            //need a xml writer
        }

        //opens a browse panel that allows a file to be selected to be used as a search path
        private void Browse_button_Click( object sender, RoutedEventArgs e )
        {
            var dlg = new FolderBrowserDialog();
            string path = null;

            dlg.ShowNewFolderButton = true;
            DialogResult result = dlg.ShowDialog();
            if( result == System.Windows.Forms.DialogResult.OK )
            {
                path = dlg.SelectedPath;
                unsaved_paths.Add( path );
                //refresh listView
                listView.Items.Clear();
                foreach( string s in unsaved_paths )
                {
                    listView.Items.Add( s );        
                }
            }
        }
    }
}
