using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;

namespace Matrix_Movie_Manager.Framework
{

    //referance http://www.omdbapi.com/
    public class OMBD_Connection
    {
        const string omdb_default = "http://www.omdbapi.com/?";
        public string omdb_poster_key;
        public Movie n_movie = new Movie();
        

        public Movie GetMovie(string query, string apiKey = "")
        {
            XmlDataDocument xml = new XmlDataDocument();

            string encoded_query = HttpUtility.UrlEncode(query);

            xml.Load(omdb_default + "t=" + encoded_query + "&r=xml");

            XmlNode check = xml.DocumentElement;
            XmlNode root;

            //checks response on the api call
            if (check.Attributes["response"].Value.ToString() == "True")
            {
                //accesses the movie section of the xml node
                root = xml.DocumentElement.LastChild;

                n_movie.Title = root.Attributes["title"].Value.ToString();
                n_movie.Released = root.Attributes["released"].Value.ToString();
                n_movie.Rated = root.Attributes["rated"].Value.ToString();
                n_movie.Runtime = root.Attributes["runtime"].Value.ToString();
                n_movie.Plot = root.Attributes["plot"].Value.ToString();
                n_movie.Awards = root.Attributes["awards"].Value.ToString();
                n_movie.Poster = root.Attributes["poster"].Value.ToString();
                n_movie.Metascore = root.Attributes["metascore"].Value.ToString();
                n_movie.imdbRating = root.Attributes["imdbRating"].Value.ToString();
                n_movie.Type = root.Attributes["type"].Value.ToString();

                //these are comma delimited 
                n_movie.Genre = root.Attributes["genre"].Value.ToString();
                n_movie.Actors = root.Attributes["actors"].Value.ToString();
                n_movie.Writer = root.Attributes["writer"].Value.ToString();
                n_movie.Language = root.Attributes["language"].Value.ToString();
                n_movie.Country = root.Attributes["country"].Value.ToString();
                n_movie.Director = root.Attributes["director"].Value.ToString();


                return n_movie;
            }
            else
            {
                return null;
            }
        }
    }


}
