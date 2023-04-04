namespace JRodriguezConsumoMoviesAPIS.Models
{
    public class Movie
    {
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public  int idMovie { get; set; }
        public string Descripcion { get; set; }
        public bool favorite { get; set; }
        public int media_id {  get; set; }
        public string media_type { get; set; }
        public List<object> Movies { get; set; }

    }
}
