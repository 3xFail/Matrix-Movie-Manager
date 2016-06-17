using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Matrix_Movie_Manager.Framework
{

    //referance http://www.omdbapi.com/
    class OMBD_Connection
    {
        const string omdb_default = "http://www.omdbapi.com/?";
        public string omdb_poster_key;
        public Movie n_movie;
        public List<Movie> movie_list;

        public async Task<Movie> GetMovie(string query, string apiKey = "")
        {
            using (var client = new HttpClient())
            {
                string response_s;
                XmlDataDocument xml = new XmlDataDocument();
                client.BaseAddress = new Uri(omdb_default);
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(omdb_default + "t=" + query + "&r=xml");
                if (response.IsSuccessStatusCode)
                {
                    //n_movie = await response.Content.ReadAsAsync<Movie>();
                    response_s = await response.Content.ReadAsStringAsync();

                    
                    xml.Load(response_s);

                    //need to create the new movie based on the data in the xml document

                    return n_movie;
                }
                else
                {
                    return null;
                }
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


}
