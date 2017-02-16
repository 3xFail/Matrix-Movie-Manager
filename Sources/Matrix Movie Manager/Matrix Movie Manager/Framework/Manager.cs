using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Movie_Manager.Framework
{
    public class Manager
    {
        public Manager(Settings settings )
        {
            m_con = new OMBD_Connection();
            m_movie_list = new List<Movie>();
            int loc;
            int count = 0;
            
            if(settings.paths != null || settings.typenames != null )
            {
                m_accessor = new Accessor( settings.paths.ToArray(), settings.typenames.ToArray() );
                

                foreach( string file in m_accessor.Get_All_Files() )
                {
                    //fill movie list
                    m_movie_list.Add( new Movie( m_con.GetMovie( file ) ) );
                    loc = m_accessor.Get_All_Files().IndexOf(file);
                    m_movie_list[count].Path = m_accessor.Get_All_Paths()[loc];
                    count++;
                }
            }
            
            
        }

        public List<Movie> m_movie_list { get;  set; }
        public static OMBD_Connection m_con { get; set; }
        public static Accessor m_accessor { get; set; }

    }
}
