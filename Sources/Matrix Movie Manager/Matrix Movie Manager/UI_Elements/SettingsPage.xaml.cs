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
using System.Xml;

namespace Matrix_Movie_Manager.UI_Elements
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public MainWindow m_win;
        private List<string> unsaved_paths = new List<string>();
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

            listView.Items.Clear();
            if (m_win.m_settings.paths != null)
            {
                foreach (string s in m_win.m_settings.paths)
                {
                    listView.Items.Add(s);
                    unsaved_paths.Add(s);
                }
            }

            if (m_win.m_settings.typenames.Count >= 1)
            {
                MKV_Check.IsChecked = m_win.m_settings.use_type[0];
                AVI_Check.IsChecked = m_win.m_settings.use_type[1];
                MP4_Check.IsChecked = m_win.m_settings.use_type[2];
            }

            var myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source = new Uri("Colors.xaml", UriKind.RelativeOrAbsolute);
            Main_Color.SelectedColor = ((SolidColorBrush)(System.Windows.Application.Current.Resources["MainColor"])).Color;
            Sec_Color.SelectedColor = ((SolidColorBrush)(System.Windows.Application.Current.Resources["SecondColor"])).Color;
            Third_Color.SelectedColor = ((SolidColorBrush)(System.Windows.Application.Current.Resources["ThirdColor"])).Color;
            Border_Color.SelectedColor = ((SolidColorBrush)(System.Windows.Application.Current.Resources["BorderColor"])).Color;
            Text_Color.SelectedColor = ((SolidColorBrush)(System.Windows.Application.Current.Resources["FontColor"])).Color;
        }
        // returns the aplication to the main screen
        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            m_win.Main_Frame.Navigate(new Welcome_Page());
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {

            //store everything that was added into the settings lists in m_win then write it to the file in m_win in xml
            XmlTextWriter w = new XmlTextWriter(m_win.settings_file_path, null);
            w.Formatting = Formatting.Indented;
            w.WriteStartElement("root", "urn:1");


            //need to change this to account for removing a path
            if (unsaved_paths.Count > 0)
            {
                w.WriteStartElement("paths");

                foreach (string s in unsaved_paths)
                {
                    m_win.m_settings.paths.Add(s);
                    w.WriteStartElement("path", "urn:1");
                    w.WriteAttributeString("loc", "urn:1", s);
                    w.WriteEndElement(); //path
                }
                w.WriteEndElement(); //paths
            }
            w.WriteStartElement("filetypes");


            w.WriteStartElement("filetype", "urn:1");
            w.WriteAttributeString("name", "urn:1", MKV_Check.Content.ToString());
            w.WriteAttributeString("value", "urn:1", MKV_Check.IsChecked.ToString());
            w.WriteEndElement(); 

            w.WriteStartElement("filetype", "urn:1");
            w.WriteAttributeString("name", "urn:1", AVI_Check.Content.ToString());
            w.WriteAttributeString("value", "urn:1", AVI_Check.IsChecked.ToString());
            w.WriteEndElement(); 

            w.WriteStartElement("filetype", "urn:1");
            w.WriteAttributeString("name", "urn:1", MP4_Check.Content.ToString());
            w.WriteAttributeString("value", "urn:1", MP4_Check.IsChecked.ToString());
            w.WriteEndElement();

            w.WriteStartElement("MainColor", "urn:1");
            w.WriteAttributeString("Color", "urn:1", Main_Color.SelectedColor.ToString());
            w.WriteEndElement();

            w.WriteStartElement("SecondColor", "urn:1");
            w.WriteAttributeString("Color", "urn:1", Sec_Color.SelectedColor.ToString());
            w.WriteEndElement();

            w.WriteStartElement("ThirdColor", "urn:1");
            w.WriteAttributeString("Color", "urn:1", Third_Color.SelectedColor.ToString());
            w.WriteEndElement();

            w.WriteStartElement("BorderColor", "urn:1");
            w.WriteAttributeString("Color", "urn:1", Border_Color.SelectedColor.ToString());
            w.WriteEndElement();

            w.WriteStartElement("FontColor", "urn:1");
            w.WriteAttributeString("Color", "urn:1", Text_Color.SelectedColor.ToString());
            w.WriteEndElement();


            w.WriteEndElement(); //filetypes
            w.WriteEndElement(); //root 
            w.Close();

            m_win.RefreshManager();

        }

        //opens a browse panel that allows a file to be selected to be used as a search path
        private void Browse_button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            string path;

            dlg.ShowNewFolderButton = true;
            DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                path = dlg.SelectedPath.ToString();
                unsaved_paths.Add(path.ToString());
                //refresh listView
                listView.Items.Clear();
                foreach (string s in unsaved_paths)
                {
                    listView.Items.Add(s);
                }
            }
        }

        
    }
}
