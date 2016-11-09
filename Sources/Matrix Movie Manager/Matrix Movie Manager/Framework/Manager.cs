using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Movie_Manager.Framework
{
    public class Manager
    {
        public Manager()
        {
            //need to make some sort of saved settings file/class that can passed in
            //file/class needs to include
            //path or paths
            //file types

            //m_accessor = new Accessor(settings.paths, settings.file_types);
            m_con = new OMBD_Connection();

            foreach (string file in m_accessor.Get_All_Files())
            {
                //fill movie list
                m_movie_list.m_movies.Add(new Movie(m_con.GetMovie(file)));
            }
        }

        public static MovieList m_movie_list { get;  set; }
        public static OMBD_Connection m_con { get; set; }
        public static Accessor m_accessor { get; set; }

    }
}
