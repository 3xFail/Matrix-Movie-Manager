using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Media_Containers
{
    public abstract class Media
    {
        protected string m_name;
        protected double m_size;
        protected string m_filetype;
        
        protected string m_media_type;

        protected string m_file_path;

        public string Get_Name() { return m_name; }
        public double Get_Size() { return m_size; }
        public string Get_Filetype() { return m_filetype; }
        public string Get_File_Path() { return m_file_path; }

    }


    public class Movie : Media
    {
        Movie(string name, double size, string filetype, string filepath)
        {
            m_name = name;
            m_size = size;
            m_filetype = filetype;
            m_media_type = "Movie";
            m_file_path = filepath;
        }
        protected string  m_discirption;
        protected bool m_series;
        protected int m_series_id;
    }

    public class Tv_Show : Media
    {

        Tv_Show(string name, double size, string filetype, string filepath)
        {
            m_name = name;
            m_size = size;
            m_filetype = filetype;
            m_media_type = "Tv Show";
            m_file_path = filepath;
        }
        protected string m_discirption;
        protected int m_season_id;
        protected int m_episode_id;

    }
}
