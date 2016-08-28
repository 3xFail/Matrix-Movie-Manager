using Matrix_Movie_Manager.Framework;
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
            //this should filter the movie view grid
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            m_win.Main_Frame.Navigate(new Welcome_Page());
        }


        List<ImageSource> movie_posters_list = new List<ImageSource>();
        List<String> movie_names = new List<String>();
        


        public void LoadImages()
        {
            //should have access to full list of movies at start
            //accessable through the movie list 

            foreach (Movie movie in Manager.m_movie_list.m_movies)
            {
                this.movie_posters_list.Add(new BitmapImage(new Uri(movie.Poster)));
                this.movie_names.Add(movie.Title);
            }

            Movie_List.ItemsSource = movie_posters_list;
        }

    }
}
