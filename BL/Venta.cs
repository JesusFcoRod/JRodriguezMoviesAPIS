using Microsoft.EntityFrameworkCore;
using ML;
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
           ML.Cine cine = new ML.Cine();
            int TotalVentas = 0;
            try
            {
                using (DL.JrodriguezExamenMoviesApiContext context = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = context.Cines.FromSqlRaw($"[VentasPorZona] {IdZona}").ToList();

                    int TamList = query.Count();
                    int[] Ventas = new int[TamList];

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            Ventas[TamList - 1] = int.Parse(item.Venta.Value.ToString());
                            TamList--;

                        }
                        TotalVentas = Ventas.Sum();
                    }                   
                }

            }
            catch (Exception Ex)
            {
                return TotalVentas;
            }
            return TotalVentas;
        }

        public static int PorcentajeZona(int VentaZona, int TotalVentas)
        {
            int Porcentaje = (VentaZona * 100) / TotalVentas;
            return Porcentaje;
        }
    }
}
