using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
    /// Interaction logic for NewMovieSearch.xaml
    /// </summary>
    public partial class NewMovieSearch : Page
    {
        MainWindow m_win;
        public NewMovieSearch()
        {
            InitializeComponent();
        }

        public NewMovieSearch(MainWindow window)
        {
            InitializeComponent();
            m_win = window;
            textBox.Focus();
        }

        private void search_button_Click(object sender, RoutedEventArgs e)
        {
            string query = HttpUtility.UrlEncode(textBox.Text);
            Process.Start("http://www.imdb.com/find?ref_=nv_sr_fn&q=" + query + "&s=all");
            textBox.Text = "";
        }
    }
}
