using JRodriguezConsumoMoviesAPIS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace JRodriguezConsumoMoviesAPIS.Controllers
{
    public class MovieController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public MovieController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult GetAllPeliculasPopulares()
        {
            Models.Movie movie = new Models.Movie();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlApi"];
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("movie/popular?api_key=0f1d91837fc21381512e96c7c4129494&language=es-ES");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();

                    movie.Movies = new List<object>();
                    foreach (var resultItem in resultJSON.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.idMovie = resultItem.id;
                        movieItem.Descripcion = resultItem.overview;
                        movieItem.Titulo = resultItem.title;
                        movieItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;

                        movie.Movies.Add(movieItem);
                    }
                }          
            }
            return View(movie);
        }

        [HttpGet]
        public IActionResult GetAllPeliculasFavoritas()
        {
            Models.Movie movie = new Models.Movie();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlApi"];
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("account/18699426/favorite/movies?api_key=0f1d91837fc21381512e96c7c4129494&session_id=9af6555c8bb924b4ce48f86e34e91f4ad8993076&language=es-ES");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();

                    movie.Movies = new List<object>();
                    foreach (var resultItem in resultJSON.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.idMovie = resultItem.id;
                        movieItem.Descripcion = resultItem.overview;
                        movieItem.Titulo = resultItem.title;
                        movieItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;

                        movie.Movies.Add(movieItem);
                    }
                }
            }
            return View(movie);
        }

        [HttpGet]
        public ActionResult AddFavoritos(int? IdPelicula, int? valFav)
        {
            Models.Movie movie = new Models.Movie();
            movie.media_id = IdPelicula.Value;
            movie.media_type = "movie";

            if (valFav == null)
            {
                movie.favorite = true;
            }
            else
            {
                movie.favorite = false;
            }

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlApi"];
                client.BaseAddress = new Uri(urlApi);

                var postTask = client.PostAsJsonAsync<Models.Movie>("account/18699426/favorite?api_key=0f1d91837fc21381512e96c7c4129494&session_id=9af6555c8bb924b4ce48f86e34e91f4ad8993076",movie);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Accion realizada";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error!";
                    return PartialView("Modal");
                }

            }
                
        }

        [HttpGet]
        public ActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]

        public ActionResult LoginUsuario(ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.GetByUserName(usuario);

            if (result.Correct)
            {
                ML.Usuario usuarioUnboxing = (ML.Usuario)result.Object;
                if (usuario.Password == usuarioUnboxing.Password)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.Message = "La contraseña es incorrecta";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "El Nombre de Usuario es incorrecta o no existe";
                return PartialView("Modal");
            }
        }

    }
}
