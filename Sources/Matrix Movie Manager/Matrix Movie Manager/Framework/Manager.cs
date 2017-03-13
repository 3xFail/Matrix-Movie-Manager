using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Matrix_Movie_Manager.Framework
{
    public class Manager
    {
        public Manager()
        {
            m_movie_list = new List<Movie>();
            //static_list = new List<Movie>();
            m_con = new OMBD_Connection();
            m_accessor = new Accessor();
            bgworker = new BackgroundWorker();
            bgworker.WorkerReportsProgress = true;
            bgworker.WorkerSupportsCancellation = true;
            bgworker.DoWork += bgworker_dowork;
            bgworker.RunWorkerCompleted += bgworker_runworkercompleted;
            file_count = 0;
        }

        public Manager(Manager man)
        {
            m_movie_list = man.m_movie_list;
            m_con = man.m_con;
            m_accessor = man.m_accessor;

        }
        public Manager(Settings settings)
        {
            m_con = new OMBD_Connection();
            m_movie_list = new List<Movie>();
            //static_list = new List<Movie>();
            bgworker = new BackgroundWorker();
            bgworker.WorkerReportsProgress = true;
            bgworker.WorkerSupportsCancellation = false;
            bgworker.DoWork += bgworker_dowork;
            bgworker.RunWorkerCompleted += bgworker_runworkercompleted;
            file_count = 0;
            int loc;
            int count = 0;

            if (settings.paths != null || settings.typenames != null)
            {
                m_accessor = new Accessor(settings.paths.ToArray(), settings.typenames.ToArray());

                file_count = m_accessor.Get_All_Files().Count;
                foreach (string file in m_accessor.Get_All_Files())
                {
                    //fill movie list
                    m_movie_list.Add(new Movie(m_con.GetMovie(file)));
                    loc = m_accessor.Get_All_Files().IndexOf(file);
                    m_movie_list[count].Path = m_accessor.Get_All_Paths()[loc];
                    count++;
                }
            }


        }

        private void bgworker_runworkercompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int count = 0;
            bgworker.DoWork -= bgworker_dowork;
            bgworker.RunWorkerCompleted -= bgworker_runworkercompleted;
            bgworker.ProgressChanged -= (Application.Current.MainWindow as MainWindow).MyBackgroundWorker_ProgressChanged;
            (Application.Current.MainWindow as MainWindow).MyProgressBar.Value = 0;
            (Application.Current.MainWindow as MainWindow).prog_bar.Visibility = System.Windows.Visibility.Hidden;

            //m_movie_list = ((Manager)e.Result).m_movie_list;
            //m_con = ((Manager)e.Result).m_con;
            //m_accessor = ((Manager)e.Result).m_accessor;
            //file_count = ((Manager)e.Result).file_count;

            foreach (var item in ((Manager)e.Result).m_movie_list)
            {
                m_movie_list[count].Poster = new BitmapImage(new Uri(item.Poster.ToString()));
                count++;
            }
        }

        private void bgworker_dowork(object sender, DoWorkEventArgs e)
        {
            
            int loc;
            double count = 0;
            

            Settings settings = e.Argument as Settings;

            if (settings.paths != null || settings.typenames != null)
            {
                m_accessor = new Accessor(settings.paths.ToArray(), settings.typenames.ToArray());

                file_count = m_accessor.Get_All_Files().Count;
                
                foreach (string file in m_accessor.Get_All_Files())
                {
                    //fill movie list
                    m_movie_list.Add(new Movie(m_con.GetMovie(file)));
                    loc = m_accessor.Get_All_Files().IndexOf(file);
                    m_movie_list[int.Parse(count.ToString())].Path = m_accessor.Get_All_Paths()[loc];

                    //Posters are causing problems?
                    //m_movie_list[int.Parse(count.ToString())].Poster.Freeze();

                    count++;
                    
                    bgworker.ReportProgress(int.Parse(Math.Ceiling((count / file_count) * 100).ToString()));
                }
                //static_list = m_movie_list;
                bgworker.ReportProgress(100);
                e.Result = this;
            }
        }

        //public void set_movie_lists()
        //{
        //    m_movie_list = static_list;
        //}

        public BackgroundWorker bgworker;
        public List<Movie> m_movie_list { get; set; }

        //public static List<Movie> static_list { get; set; }
        public OMBD_Connection m_con { get; set; }
        public Accessor m_accessor { get; set; }
        public double file_count { get; set; }

    }
}
