using Microsoft.AspNetCore.Mvc;

namespace JRodriguezConsumoMoviesAPIS.Controllers
{
    public class VentaController : Controller
    {
        public ActionResult Porcentajes()
        {
            //TOTAL DE VENTAS
            int TotalVentas = BL.Venta.TotalVentas();

            //TOTAL VENTAS POR ZONA
            int VentasZonaNorte = BL.Venta.TotalPorZona(1);
            int VentasZonaSur = BL.Venta.TotalPorZona(2);
            int VentasZonaEste = BL.Venta.TotalPorZona(3);
            int VentasZonaOeste = BL.Venta.TotalPorZona(4);

            //PORCENTAJES POR ZONA
            int PorcentajeZN = BL.Venta.PorcentajeZona(VentasZonaNorte,TotalVentas);
            int PorcentajeZS = BL.Venta.PorcentajeZona(VentasZonaSur,TotalVentas);
            int PorcentajeZE = BL.Venta.PorcentajeZona(VentasZonaEste,TotalVentas);
            int PorcentajeZO = BL.Venta.PorcentajeZona(VentasZonaOeste,TotalVentas);

            int[] Porcentajes = new int[4];
            Porcentajes[0] = PorcentajeZN;
            Porcentajes[1] = PorcentajeZS;
            Porcentajes[2] = PorcentajeZE;
            Porcentajes[3] = PorcentajeZO;

            ML.Cine cine = new ML.Cine();
            cine.Porcentajes = Porcentajes;

            ML.Result result = BL.Cine.GetAll();

            cine.Cines = result.Objects;

            return View(cine);
        }
    }
}
