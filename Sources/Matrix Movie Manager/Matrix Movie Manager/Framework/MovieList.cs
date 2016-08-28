using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Movie_Manager.Framework
{
    public class MovieList
    {
        public MovieList()
        {

        }
        public MovieList(Movie n_movie)
        {
            m_movies.Add(new Movie(n_movie));
        }

        public List<Movie> m_movies { get; set; }

    }
}
