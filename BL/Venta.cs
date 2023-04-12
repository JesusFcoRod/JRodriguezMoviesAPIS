using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Venta
    {
        public static int TotalVentas()
        {
            ML.Result result = new ML.Result();
            ML.Cine cine = new ML.Cine();
            int Total = 0;

            try
            {
                using (DL.JrodriguezExamenMoviesApiContext contex = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = contex.Cines.FromSqlRaw("[CineGetAll]").ToList();
                    int TamList = query.Count();
                    int[] Ventas = new int[TamList];

                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        cine = new ML.Cine();
                        cine.Venta = item.Venta.Value;
                        result.Objects.Add(cine);

                        Ventas[TamList - 1] = int.Parse(cine.Venta.ToString());
                        TamList--;
                    }

                    Total = Ventas.Sum();
                }

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return Total;
        }

        public static int TotalPorZona(int IdZona)
        {
            int TotalZona = 0;
            try
            {
                using (DL.JrodriguezExamenMoviesApiContext context = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = context.Cines.FromSqlRaw($"[CineGetByZona] {IdZona}").ToList();
                    int TamList = query.Count();
                    int[] VentasZona = new int[TamList];

                    foreach (var item in query)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = item.Zona.Value;
                        cine.Zona.Descripcion = item.Descripcion;

                        VentasZona[TamList - 1] = int.Parse(cine.Venta.ToString());
                        TamList--;

                    }
                    TotalZona = VentasZona.Sum();
                }

            }
            catch (Exception Ex)
            {
                return 0;
            }
            return TotalZona;
        }
    }
}
