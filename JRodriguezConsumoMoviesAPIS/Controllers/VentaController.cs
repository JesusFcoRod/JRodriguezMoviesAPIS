using Microsoft.AspNetCore.Mvc;

namespace JRodriguezConsumoMoviesAPIS.Controllers
{
    public class VentaController : Controller
    {
        public ActionResult Porcentajes()
        {
            int TotalVentas = BL.Venta.TotalVentas();

            for (int i=1; i<=4; i++)
            {

            }

            return View();
        }
    }
}
