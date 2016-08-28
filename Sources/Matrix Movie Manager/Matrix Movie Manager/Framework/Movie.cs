using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Movie_Manager.Framework
{
    public class Movie
    {
        public Movie()
        {
        }

        public Movie(Movie temp)
        {
            Title = temp.Title;
            Rated = temp.Rated;
            Released = temp.Released;
            Runtime = temp.Runtime;
            Genre = temp.Genre;
            Director = temp.Director;
            Writer = temp.Writer;
            Actors = temp.Actors;
            Plot = temp.Plot;
            Language = temp.Language;
            Country = temp.Country;
            Awards = temp.Awards;
            Poster = temp.Poster;
            Metascore = temp.Metascore;
            imdbRating = temp.imdbRating;
            Type = temp.Type;
        }
        public string Title { get; set; }
        //public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        //public string imdbVotes { get; set; }
        //public string imdbID { get; set; }
        public string Type { get; set; }
        //public string Response { get; set; }
    }
}
