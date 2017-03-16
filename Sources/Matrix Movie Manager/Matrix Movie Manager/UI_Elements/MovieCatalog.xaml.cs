using Matrix_Movie_Manager.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Matrix_Movie_Manager.UI_Elements
{
    /// <summary>
    /// Interaction logic for MovieCatalog.xaml
    /// </summary>
    public partial class MovieCatalog : Page
    {
        MainWindow m_win;
        object previous_item_clicked;
        public MovieCatalog()
        {
            InitializeComponent();
        }

        public MovieCatalog(MainWindow win)
        {
            InitializeComponent();
            m_win = win;
            LoadImages();
            Search_Content.Focus();
            Search_Criteria.SelectedIndex = 0;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            
            m_win.Main_Frame.Navigate(new Welcome_Page());
        }


        ObservableCollection<ImageSource> movie_posters_list = new ObservableCollection<ImageSource>();
        List<String> movie_names = new List<String>();



        public void LoadImages()
        {
            //should have access to full list of movies at start
            //accessable through the movie list 

            foreach (Movie movie in m_win.m_manager.m_movie_list)
            {
                this.movie_posters_list.Add(movie.Poster);
                this.movie_names.Add(movie.Title);
            }

            Movie_List.ItemsSource = movie_posters_list;

            Movie_List.ItemsSource = m_win.m_manager.m_movie_list;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Movie_List.ItemsSource);
            view.Filter = Filter;

        }

        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(Search_Content.Text))
                return true;
            else
            {
                if (Search_Criteria.Text == "Title")
                {
                    return ((item as Movie).Title.IndexOf(Search_Content.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Search_Criteria.Text == "Actor")
                {
                    return ((item as Movie).Actors.IndexOf(Search_Content.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Search_Criteria.Text == "Genre")
                {
                    return ((item as Movie).Genre.IndexOf(Search_Content.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else {
                    return true;
                }
            }
        }


        private void Image_Click(object sender, RoutedEventArgs e)
        {
            
            if (Sidebar.Visibility == System.Windows.Visibility.Collapsed)
            {
                previous_item_clicked = ((Button)sender).DataContext;
                Movie movie = m_win.m_manager.m_movie_list.Single(m => m == ((Button)sender).DataContext);
                sb_title_content_label.Content = movie.Title;
                sb_release_date_content_label.Content = movie.Released;
                sb_runtime_content_label.Content = movie.Runtime;
                sb_genre_content_label.Content = movie.Genre;
                sb_plot_content_label.Text = movie.Plot;
                Sidebar.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Sidebar.Visibility == System.Windows.Visibility.Visible && previous_item_clicked != ((Button)sender).DataContext)
            {
                Movie movie = m_win.m_manager.m_movie_list.Single(m => m == ((Button)sender).DataContext);
                sb_title_content_label.Content = movie.Title;
                sb_release_date_content_label.Content = movie.Released;
                sb_runtime_content_label.Content = movie.Runtime;
                sb_genre_content_label.Content = movie.Genre;
                sb_plot_content_label.Text = movie.Plot;
                previous_item_clicked = ((Button)sender).DataContext;
                Sidebar.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Sidebar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void Watch_Button_Click(object sender, RoutedEventArgs e)
        {
            //string vlcpath = @"C:\Program Files\VideoLAN\VLC\vlc.exe";
            string vlcpath = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";

            if (vlcpath != null)
            {
                Movie movie = m_win.m_manager.m_movie_list.First(m => m.Title == sb_title_content_label.Content.ToString());


                Process p = new Process();
                p.StartInfo.FileName = vlcpath;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.Arguments = "\"" + movie.Path + "\"";
                p.Start();

                //p.WaitForExit();
            }
            else
                MessageBox.Show("Could Not Find VLC's Path on this system. Make sure that it is installed");
            
        }

        private void Search_Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Movie_List.ItemsSource).Refresh();
        }
    }
}
