using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;

namespace ConsoleTestingGround
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Sp Test Copies";
            //string[] path = { @"E:\Movies", @"E:\TV Shows" };
            string[] filetypes = { ".mkv", ".mp4" };
            Accessor accesser = new Accessor(path, filetypes);
            List<Movie> Movie_List = new List<Movie>();
            Movie temp= new Movie();
            OMBD_Connection con = new OMBD_Connection();


            foreach (string file in accesser.Get_All_Files())
            {
                Console.WriteLine(file);
                temp = con.GetMovie(file);
              
                Movie_List.Add(new Movie(temp));
                Console.WriteLine();
            }


            Console.WriteLine("pause");
        }
    }


    public class Accessor
    {
        //single path constructor
        public Accessor(string path)
        {
            string[] temp;

            temp = System.IO.Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (string file in temp)
            {
                all_filenames.Add(Path.GetFileName(file));
            }
        }

        //filtering ctor
        public Accessor(string path, string[] file_types)
        {
            string[] temp;

            if (file_types != null)
            {
                temp = System.IO.Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

                foreach (string file in temp)
                {
                    foreach (string type in file_types)
                    {
                        if (Path.GetFileName(file).Contains(type))
                        {
                            all_filenames.Add(Path.GetFileNameWithoutExtension(file));
                            break;
                        }
                    }

                }
            }
            else
                throw new InvalidOperationException("File type Was null");
        }

        //multi-path ctor
        public Accessor(string[] paths, string[] file_types)
        {

            if (paths != null && file_types != null)
            {
                foreach (string path in paths)
                {
                    string[] temp;

                    temp = System.IO.Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

                    foreach (string file in temp)
                    {
                        foreach (string type in file_types)
                        {
                            if (Path.GetFileName(file).Contains(type))
                            {
                                all_filenames.Add(Path.GetFileNameWithoutExtension(file));
                                break;
                            }
                        }
                    }
                }
            }
            else
                throw new InvalidOperationException("Paths was null / File types was null");
        }


        public List<string> Get_All_Files()
        {
            return all_filenames;
        }

        public List<string> all_filenames = new List<string>();

    }




    //referance http://www.omdbapi.com/
    public class OMBD_Connection
    {
        const string omdb_default = "http://www.omdbapi.com/?";
        public string omdb_poster_key;
        public Movie n_movie = new Movie();
        public List<Movie> movie_list = new List<Movie>();

        public Movie GetMovie(string query, string apiKey = "")
        {
            XmlDataDocument xml = new XmlDataDocument();

            string encoded_query = HttpUtility.UrlEncode(query);

            xml.Load(omdb_default + "t=" + encoded_query + "&r=xml");

            XmlNode check = xml.DocumentElement;
            XmlNode root;

            //checks response on the api call
            if(check.Attributes["response"].Value.ToString() == "True")
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
        public async Task<List<Movie>> GetMovieList(string query, string apiKey = "")
        {
            using (var client = new HttpClient())
            {
                string response_s;
                XmlDataDocument xml = new XmlDataDocument();
                client.BaseAddress = new Uri(omdb_default);
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(omdb_default + "s=" + query + "&r=xml");
                if (response.IsSuccessStatusCode)
                {
                    response_s = await response.Content.ReadAsStringAsync();

                    xml.Load(response_s);

                    //need to create the movies based on every node in the xml and add them to the new movie list

                    return movie_list;
                }
                else
                {
                    return null;
                }
            }
        }
    }




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


