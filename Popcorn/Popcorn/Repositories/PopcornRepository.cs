using Newtonsoft.Json;
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
        //all movie related api urls
        #region ConstMovieApiUrl's
        //clarification MOVIEPAGE
        //-----------------------
        //parameters:
        // --> page     (select which page you want to display)
        // --> sort     (select which type of movies  you specificly want to display, all possible sort parameters will be hardcoded)
        // --> order    (select which order you want the movies displayed, possible values are 1 and -1 this will invert the list of possible items)
        // --> genre    (select which genre of movies you specificly want to display, all possible genres will be hardcoded)
        // --> keywords (select all movies corresponding with the submited keywords, f.e 'harry potter' will return all movies with harry potter in the title)
        //full url example: https://tv-v2.api-fetch.website/movies/1?sort=trending&order=1&genreGenre&keywords=harry%20potter
        const string MOVIEPAGE = "https://tv-v2.api-fetch.website/movies/";
        //clarification SPECIFICMOVIE
        //---------------------------
        //parameters:
        // --> imdb_id (selects the movie with the corresponding imdb_id)
        //full url example: https://tv-v2.api-fetch.website/movie/tt8879926
        const string SPECIFICMOVIE = "https://tv-v2.api-fetch.website/movie/";
        // clarification MOVIEGENRELIST
        //---------------------------
        //since there is no data provided related to the possible genre's I decided to hardcode them.
        private static readonly List<string> MOVIEGENRELIST = new List<string>() { "All", "Action", "Adventure", "Animation", "Biography", "Comedy", "Crime", "Documentary", "Drama", "Family", "Fantasy", "Film-Noir", "History", "Horror", "Music", "Musical", "Mystery", "Romance", "Sci-Fi", "Short", "Sport", "Thriller", "War", "Western" };
        // clarification MOVIESORTLIST
        //---------------------------qqqqqqqqqqq
        //since there is no data provided related to the possible Sorts I decided to hardcode them.
        private static readonly List<string> MOVIESORTLIST = new List<string>() { "Trending", "Popularity", "Last Added", "Year", "Title", "Rating" };
        #endregion

        //all show related api urls
        #region ConstShowApiUrl's
        //clarification ANIMEPAGE
        //-----------------------
        //parameters:
        // --> page     (select which page you want to display)
        // --> sort     (select which type of show  you specificly want to display, all possible sort parameters will be hardcoded)
        // --> order    (select which order you want the shows displayed, possible values are 1 and -1 this will invert the list of possible items)
        // --> genre    (select which genre of show you specificly want to display, all possible genres will be hardcoded)
        // --> keywords (select all shows corresponding with the submited keywords, f.e 'orange' will return all movies with orange in the title)
        //full url example: https://tv-v2.api-fetch.website/shows/1?sort=Trending&order=1&genre=&keywords=The big
        const string SHOWSPAGE = "https://tv-v2.api-fetch.website/shows/";
        //parameters:
        // --> imdb_id (selects the movie with the corresponding id)
        //full url example: https://tv-v2.api-fetch.website/show/tt0898266?id=tt0898266
        //or                https://tv-v2.api-fetch.website/show/tt0898266
        //they messed something up with theire api logic, however it still works
        const string SPECIFICSHOW = "https://tv-v2.api-fetch.website/show/";
        // clarification SHOWGENRELIST
        //---------------------------
        //since there is no data provided related to the possible genre's I decided to hardcode them.
        private static readonly List<string> SHOWGENRELIST = new List<string>() { "All", "Action", "Adventure", "Animation", "Children", "Comedy", "Crime", "Documentary", "Drama", "Family", "Fantasy", "Game Show", "Home And Garden", "Horror", "Mini Series", "Mystery", "News", "Reality", "Romance", "Science Fiction", "Soap", "Special intrest", "Sport", "Suspence", "Talk Show", "Thriller", "Western" };
        // clarification SHOWSSORTLIST
        //---------------------------
        //since there is no data provided related to the possible Sorts I decided to hardcode them.
        private static readonly List<string> SHOWSSORTLIST = new List<string>() { "Trending", "Popularity", "Updated", "Year", "Name", "Rating" };
        #endregion

        //all anime related api urls
        #region ConstAnimeApiUrl's

        //clarification ANIMEPAGE
        //-----------------------
        //parameters:
        // --> page     (select which page you want to display)
        // --> sort     (select which type of anime  you specificly want to display, all possible sort parameters will be hardcoded)
        // --> order    (select which order you want the anime's displayed, possible values are 1 and -1 this will invert the list of possible items)
        // --> genre    (select which genre of anime you specificly want to display, all possible genres will be hardcoded)
        // --> keywords (select all anime's corresponding with the submited keywords, f.e 'orange' will return all movies with orange in the title)
        //full url example: https://tv-v2.api-fetch.website/animes/1?sort=name&order=1&genre=&keywords=
        const string ANIMEPAGE = "https://tv-v2.api-fetch.website/animes/";
        //clarification SPECIFICMOVIE
        //---------------------------
        //parameters:
        // --> imdb_id (selects the movie with the corresponding id)
        //full url example: https://tv-v2.api-fetch.website/anime/11844
        const string SPECIFICANIME = "https://tv-v2.api-fetch.website/anime/";
        // clarification SHOWGENRELIST
        //---------------------------
        //since there is no data provided related to the possible genre's I decided to hardcode them.
        private static readonly List<string> ANIMEGENRELIST = new List<string>() { "All", "Action", "Adventure", "Cars", "Comedy", "Dementia", "Demons", "Drama", "Ecchi", "Fantasy", "Game", "Harem", "Historical", "Horror", "Josei", "Kids", "Magic", "Martial Arts", "Mecha", "Military", "Music", "Mystery", "Parody", "Police", "Psychological", "Romance", "Samurai", "School", "Sci-Fi", "Seinen", "Shoujo", "Shoujo Ai", "Shounen", "Shounen Ai", "Slice Of Life", "Space", "Sports", "Sports", "Super Power", "Supernatural", "Thriller", "Vampire" };
        // clarification ANIMESORTLIST
        //---------------------------
        //since there is no data provided related to the possible Sorts I decided to hardcode them.
        private static readonly List<string> ANIMESORTLIST = new List<string>() { "Popularity", "Name", "Year" };
        // clarification ANIMETYPELIST
        //---------------------------
        //since there is no data provided related to the possible Sorts I decided to hardcode them.
        private static readonly List<string> ANIMETYPELIST = new List<string>() { "All", "Movies", "TV", "OVA", "ONA" };
        //possible site to retrieve the images from: https://myanimelist.net of https://kitsu.io/anime/one-punch-man
        #endregion

        public static async Task<List<Movie>> GetTrendingMoviesAsync(string keyword)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                String url = string.Format("{0}{1}?sort=trending&keywords={2}", MOVIEPAGE, 1, keyword);
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
                Debug.WriteLine("Try-Catch methode failed");
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
                Debug.WriteLine("Try-Catch methode failed");
                throw ex;
            }
        }

        public static async Task<List<Series>> GetTrendingSeriesAsync(string keyword)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                String url = string.Format("{0}{1}?sort=trending&keywords={2}", SHOWSPAGE, 1, keyword);
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
                Debug.WriteLine("Try-Catch methode failed");
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
                Debug.WriteLine("Try-Catch methode failed");
                throw ex;
            }
        }

        public static async Task<List<Anime>> GetTrendingAnimeAsync(string keyword)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                String url = string.Format("{0}{1}?sort=trending&keywords={2}", ANIMEPAGE, 1, keyword);
                String json = await client.GetStringAsync(url);
                Debug.WriteLine("Json: " + json);
                if (json != null)
                {
                    List<Anime> Animes = JsonConvert.DeserializeObject<List<Anime>>(json);
                    Debug.WriteLine("Deserialize succesfull");
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
                Debug.WriteLine("Try-Catch methode failed");
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
                Debug.WriteLine("Try-Catch methode failed");
                throw ex;
            }
        }

    }
}
