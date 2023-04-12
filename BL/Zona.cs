using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezExamenMoviesApiContext contex = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = contex.Zonas.FromSqlRaw("[ZonaGetAll]").ToList();

                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Zona zona = new ML.Zona();
                        zona.IdZona = item.IdZona;
                        zona.Descripcion = item.Descripcion;

                        result.Objects.Add(zona);
                    }
                    result.Correct = true;
                }
            }
            catch(Exception Ex)
            {
                result.Ex = Ex;
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }
    }
}
