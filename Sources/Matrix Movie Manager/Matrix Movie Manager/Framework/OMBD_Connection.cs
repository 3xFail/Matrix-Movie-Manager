using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

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
            string encoded_query = "";
            int result = 0;
            StringBuilder sb = new StringBuilder();
            XmlDataDocument xml = new XmlDataDocument();

            
            string[] title_words = Regex.Split(query, @" ");
            int.TryParse(title_words.Last().ToString(), out result);

            if(result >= 1930 && result <= int.Parse(DateTime.Now.Year.ToString()))
            {
                foreach (string item in title_words)
                {
                    if (item == title_words.Last())
                    {

                    }
                    else
                    {
                        sb.Append(item);
                        sb.Append(" ");
                    }
                }
                encoded_query = HttpUtility.UrlEncode(sb.ToString());

                xml.Load(omdb_default + "t=" + encoded_query + "&y=" + title_words.Last() + "&r=xml");
            }
            else
            {
                encoded_query = HttpUtility.UrlEncode(query);
                xml.Load(omdb_default + "t=" + encoded_query + "&r=xml");
            }

            



            XmlNode check = xml.DocumentElement;
            XmlNode root;

            //checks response on the api call
            if (check.Attributes["response"] != null)
            {
                //accesses the movie section of the xml node
                root = xml.DocumentElement.LastChild;

                n_movie.Title = root.Attributes["title"].Value.ToString();
                n_movie.Released = root.Attributes["released"].Value.ToString();
                n_movie.Rated = root.Attributes["rated"].Value.ToString();
                n_movie.Runtime = root.Attributes["runtime"].Value.ToString();
                n_movie.Plot = root.Attributes["plot"].Value.ToString();
                n_movie.Awards = root.Attributes["awards"].Value.ToString();

                if (root.Attributes["poster"].Value.ToString() != "N/A")
                {
                    n_movie.Poster = new BitmapImage(new Uri(root.Attributes["poster"].Value.ToString()));
                    //n_movie.Poster.Freeze();
                }
                else
                {
                    n_movie.Poster = new BitmapImage(new Uri(@"\Data_Files\Images\images.png", UriKind.Relative));
                    //n_movie.Poster.Freeze();
                }
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
                n_movie.Title = query;
                n_movie.Released = "blank";
                n_movie.Rated = "blank";
                n_movie.Runtime = "blank";
                n_movie.Plot = "blank";
                n_movie.Awards = "blank";
                n_movie.Poster = new BitmapImage(new Uri(@"\Data_Files\Images\images.png", UriKind.Relative));
                //n_movie.Poster.Freeze();
                n_movie.Metascore = "blank";
                n_movie.imdbRating = "blank";
                n_movie.Type = "blank";

                //these are comma delimited 
                n_movie.Genre = "blank";
                n_movie.Actors = "blank";
                n_movie.Writer = "blank";
                n_movie.Language = "blank";
                n_movie.Country = "blank";
                n_movie.Director = "blank";
                return n_movie;
            }
        }
    }


}
