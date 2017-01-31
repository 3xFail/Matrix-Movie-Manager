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
            m_settings.typenames = new List<string>();
            m_settings.use_type = new List<bool>();
            StringBuilder output = new StringBuilder();
            

            settings_file_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + settings_file;

            if (File.Exists(settings_file_path))
            {
                string xml = File.ReadAllText(settings_file_path);
                if(xml.Length > 0)
                {
                    //parse as xml
                    using( XmlReader reader = XmlReader.Create( new StringReader( xml ) ) )
                    {

                        while(reader.Read())
                        {
                            
                                switch (reader.Name)
                                {
                                    case "root":
                                        break;
                                    case "paths":
                                        break;
                                    case "path":
                                        m_settings.paths.Add(reader["d3p1:loc"].ToString());
                                        break;
                                    case "filetypes":
                                        break;
                                    case "filetype":
                                        m_settings.typenames.Add(reader["d3p1:name"].ToString());

                                        if (reader["d3p1:value"].Trim().ToUpper() == "TRUE")
                                            m_settings.use_type.Add(true);
                                        else
                                            m_settings.use_type.Add(false);

                                        break;
                                }
                            
                        }

                    }
                }
                else
                {
                    //settings file was empty
                    //this is a freash startup
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

        public string settings_file_path;
        private string settings_file = "\\Data_Files\\settings.xml";
        public Settings m_settings;
        public Manager m_manager;

    }
    public struct Settings
    {
        public List<string> paths { get; set; }
        public List<string> typenames { get; set; }
        public List<bool> use_type { get; set; }

    };
}
