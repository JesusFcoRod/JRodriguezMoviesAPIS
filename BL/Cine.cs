using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezExamenMoviesApiContext contex = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[CineAdd] {cine.Latitud}, {cine.Longitud},'{cine.Descripcion}',{cine.Venta},{cine.Zona.IdZona}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezExamenMoviesApiContext contex = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[CineUpdate] {cine.IdCine}, {cine.Latitud}, {cine.Longitud},'{cine.Descripcion}',{cine.Venta}, {cine.Zona.IdZona}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int IdCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezExamenMoviesApiContext contex = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[CineDelete] {IdCine}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezExamenMoviesApiContext context = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = context.Cines.FromSqlRaw("[CineGetAll]").ToList();

                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.IdCine = item.IdCines;
                        cine.Latitud = item.Latitud.Value;
                        cine.Longitud = item.Longitud.Value;
                        cine.Descripcion = item.Descripcion;
                        cine.Venta = item.Venta.Value;

                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = item.Zona.Value;
                        cine.Zona.Descripcion = item.NombreZona;
                        

                        result.Objects.Add(cine);
                    }
                    result.Correct = true;
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezExamenMoviesApiContext contex = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = contex.Cines.FromSqlRaw($"[CineGetById] {IdCine}").AsEnumerable().FirstOrDefault();

                    ML.Cine cine = new ML.Cine();

                    cine.IdCine = query.IdCines;
                    cine.Latitud = query.Latitud.Value;
                    cine.Longitud = query.Longitud.Value;
                    cine.Descripcion = query.Descripcion;
                    cine.Venta = query.Venta.Value;

                    cine.Zona = new ML.Zona();
                    cine.Zona.IdZona = query.Zona.Value;
                    cine.Zona.Descripcion = query.NombreZona;

                    result.Object = cine;

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
