using Matrix_Movie_Manager.Framework;
using Matrix_Movie_Manager.UI_Elements;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.IO;
using System.Text;


namespace Matrix_Movie_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main_Frame.Navigate(new Welcome_Page());
            m_settings = new Settings();
            m_settings.paths = new List<string>();
            m_settings.types = new List<string>();
            StringBuilder output = new StringBuilder();

            if(File.Exists(settings_file_path))
            {
                string xml = File.ReadAllText(settings_file_path);
                if(xml.Length > 0)
                {
                    //parse as xml
                    using( XmlReader reader = XmlReader.Create( new StringReader( xml ) ) )
                    {
                        reader.ReadToFollowing( "path" );

                        if( reader.MoveToFirstAttribute() != false )
                        {
                            m_settings.paths.Add( reader.Value );

                            while( reader.MoveToNextAttribute() != true )
                            {
                                m_settings.paths.Add( reader.Value );
                            }
                        }
                        reader.ReadToFollowing( "filetypes" );


                        if( reader.MoveToFirstAttribute() != false )
                        {
                            m_settings.types.Add( reader.Value );

                            while( reader.MoveToNextAttribute() != true )
                            {
                                m_settings.types.Add( reader.Value );
                            }
                        }

                    }
                }
                else
                {
                    //settings file was empty
                    //this is a freash startup
                    m_settings.paths = null;
                    m_settings.types = null;
                }
            }
            m_manager = new Manager(m_settings);

        }

        private void My_Movies_Butt_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate( new MovieCatalog(this));
        }

        private void Do_Have_Butt_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate(new DoIHaveItPage(this));
        }

        private void Search_Butt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Suggest_Butt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Butt_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate(new SettingsPage(this));
        }

        private string settings_file_path = "..\\Data_Files\\settings.xml";
        public Settings m_settings;
        public static Manager m_manager;

    }
    public struct Settings
    {
        public List<string> paths { get; set; }
        public List<string> types { get; set; }
    };
}
