using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Popcorn.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Popcorn.Repositories
{
    public class PopcornRepository
    {
        //all data provided from the following page: https://popcorntime.api-docs.io/api/welcome/introduction

        #region ConstMovieApiUrl's
        //parameters:
        // --> page     (select which page you want to display)
        // --> sort     (select which type of movies  you specificly want to display, all possible sort parameters will be hardcoded)
        // --> order    (select which order you want the movies displayed, possible values are 1 and -1 this will invert the list of possible items)
        // --> genre    (select which genre of movies you specificly want to display, all possible genres will be hardcoded)
        // --> keywords (select all movies corresponding with the submited keywords, f.e 'harry potter' will return all movies with harry potter in the title)
        //full url example: https://tv-v2.api-fetch.website/movies/1?sort=trending&order=1&genreGenre&keywords=harry%20potter

        const string MOVIEPAGE = "https://tv-v2.api-fetch.website/movies/";
        const string SPECIFICMOVIE = "https://tv-v2.api-fetch.website/movie/";

        public static readonly List<string> MOVIEGENRELIST = new List<string>() { "All", "Action", "Adventure", "Animation", "Biography", "Comedy", "Crime", "Documentary", "Drama", "Family", "Fantasy", "Film-Noir", "History", "Horror", "Music", "Musical", "Mystery", "Romance", "Sci-Fi", "Short", "Sport", "Thriller", "War", "Western" };
        public static readonly List<string> MOVIESORTLIST = new List<string>() { "Trending", "Popularity", "Last Added", "Year", "Title", "Rating" };
        #endregion

        #region ConstShowApiUrl's
        //full url example: https://tv-v2.api-fetch.website/shows/1?sort=Trending&order=1&genre=&keywords=The

        const string SHOWSPAGE = "https://tv-v2.api-fetch.website/shows/";
        const string SPECIFICSHOW = "https://tv-v2.api-fetch.website/show/";

        public static readonly List<string> SERIEGENRELIST = new List<string>() { "All", "Action", "Adventure", "Animation", "Children", "Comedy", "Crime", "Documentary", "Drama", "Family", "Fantasy", "Game Show", "Home And Garden", "Horror", "Mini Series", "Mystery", "News", "Reality", "Romance", "Science Fiction", "Soap", "Special intrest", "Sport", "Suspence", "Talk Show", "Thriller", "Western" };
        public static readonly List<string> SERIESORTLIST = new List<string>() { "Trending", "Popularity", "Updated", "Year", "Name", "Rating" };
        #endregion

        #region ConstAnimeApiUrl's
        //full url example: https://tv-v2.api-fetch.website/animes/1?sort=name&order=1&genre=&keywords=

        const string ANIMEPAGE = "https://tv-v2.api-fetch.website/animes/";
        const string SPECIFICANIME = "https://tv-v2.api-fetch.website/anime/";

        public static readonly List<string> ANIMEGENRELIST = new List<string>() { "All", "Action", "Adventure", "Cars", "Comedy", "Dementia", "Demons", "Drama", "Ecchi", "Fantasy", "Game", "Harem", "Historical", "Horror", "Josei", "Kids", "Magic", "Martial Arts", "Mecha", "Military", "Music", "Mystery", "Parody", "Police", "Psychological", "Romance", "Samurai", "School", "Sci-Fi", "Seinen", "Shoujo", "Shoujo Ai", "Shounen", "Shounen Ai", "Slice Of Life", "Space", "Sports", "Sports", "Super Power", "Supernatural", "Thriller", "Vampire" };
        public static readonly List<string> ANIMESORTLIST = new List<string>() { "Popularity", "Name", "Year" };
        public static readonly List<string> ANIMETYPELIST = new List<string>() { "All", "Movies", "TV", "OVA", "ONA" };

        //site to retrieve the images from: https://private-anon-12aa033b85-jikan.apiary-proxy.com/v3/anime/36862/pictures
        #endregion



        public static async Task<List<Movie>> GetMoviesAsync(string keyword, string sortby)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                String url = string.Format("{0}{1}?sort={3}&keywords={2}", MOVIEPAGE, 1, keyword, sortby);
                String json = await client.GetStringAsync(url);
                Debug.WriteLine("Json: " + json);
                if (json != null)
                {
                    List<Movie> Movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    Debug.WriteLine("Deserialize succesfull");
                    return Movies;
                }
                else
                {
                    Debug.WriteLine("Error, no data recieved.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Try-Catch methode failed: " + ex);
                throw ex;
            }
        }

        public static async Task<Movie> GetSingleMovieAsync(string Imbd_Id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                String url = string.Format("{0}{1}", SPECIFICMOVIE, Imbd_Id);
                String json = await client.GetStringAsync(url);
                Debug.WriteLine("Json: " + json);
                if (json != null)
                {
                    Movie Movie = JsonConvert.DeserializeObject<Movie>(json);
                    Debug.WriteLine("Deserialize succesfull");
                    return Movie;
                }
                else
                {
                    Debug.WriteLine("Error, no data recieved.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Try-Catch methode failed: " + ex);
                throw ex;
            }
        }

        public static async Task<List<Series>> GetSeriesAsync(string keyword, string sortby)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json"); //Encoding.UTF8,
                String url = string.Format("{0}{1}?sort={3}&keywords={2}", SHOWSPAGE, 1, keyword, sortby);
                String json = await client.GetStringAsync(url);
                Debug.WriteLine("Json: " + json);
                if (json != null)
                {
                    List<Series> Shows = JsonConvert.DeserializeObject<List<Series>>(json);
                    Debug.WriteLine("Deserialize succesfull");
                    return Shows;
                }
                else
                {
                    Debug.WriteLine("Error, no data recieved.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Try-Catch methode failed: " + ex);
                throw ex;
            }
        }

        public static async Task<Series> GetSingleSeriesAsync(string Imdb_Id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                String url = string.Format("{0}{1}", SPECIFICSHOW, Imdb_Id);
                String json = await client.GetStringAsync(url);
                Debug.WriteLine("Json: " + json);
                if (json != null)
                {
                    Series Show = JsonConvert.DeserializeObject<Series>(json);
                    Debug.WriteLine("Deserialize succesfull");
                    return Show;
                }
                else
                {
                    Debug.WriteLine("Error, no data recieved.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Try-Catch methode failed: " + ex);
                throw ex;
            }
        }

        public static async Task<List<Anime>> GetAnimeAsync(string keyword, string sortby)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                String url = string.Format("{0}{1}?sort={3}&keywords={2}", ANIMEPAGE, 1, keyword, sortby);
                String json = await client.GetStringAsync(url);
                Debug.WriteLine("Json: " + json);
                if (json != null)
                {
                    List<Anime> Animes = JsonConvert.DeserializeObject<List<Anime>>(json);
                    Debug.WriteLine("Deserialize succesfull");

                    foreach (Anime a in Animes)
                    {
                        a.Images = await GetAnimeThubnailsAsync(a.Id);
                    }
                    return Animes;
                }
                else
                {
                    Debug.WriteLine("Error, no data recieved.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Try-Catch methode failed: " + ex);
                throw ex;
            }
        }

        public static async Task<Anime> GetSingleAnimeAsync(string Id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                String url = string.Format("{0}{1}", SPECIFICANIME, Id);
                String json = await client.GetStringAsync(url);
                Debug.WriteLine("Json: " + json);
                if (json != null)
                {
                    Anime _Anime = JsonConvert.DeserializeObject<Anime>(json);
                    Debug.WriteLine("Deserialize succesfull");
                    return _Anime;
                }
                else
                {
                    Debug.WriteLine("Error, no data recieved.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Try-Catch methode failed: " + ex);
                throw ex;
            }
        }

        public static async Task<Thumbnail> GetAnimeThubnailsAsync(string id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                String url = string.Format("https://api.jikan.moe/v3/anime/{0}/pictures", id);
                String json = await client.GetStringAsync(url);
                Debug.WriteLine("Json: " + json);
                if (json != null)
                {
                    Thumbnail pics = new Thumbnail();
                    JObject obj = JObject.Parse(json);

                    pics.Poster = (string)obj.SelectToken("pictures[0].large");
                    try
                    {
                        pics.FanArt = (string)obj.SelectToken("pictures[1].large");
                        pics.Banner = (string)obj.SelectToken("pictures[2].large");
                    }
                    catch (Exception ex)
                    {
                        pics.FanArt = "";
                        pics.Banner = "";
                        throw ex;
                    }
                    

                    return pics;
                }
                else
                {
                    Debug.WriteLine("Error, no data recieved.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Try-Catch methode failed: " + ex);
                throw ex;
            }
        }

    }
}
