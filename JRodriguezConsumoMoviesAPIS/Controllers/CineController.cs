using Microsoft.AspNetCore.Mvc;

namespace JRodriguezConsumoMoviesAPIS.Controllers
{
    public class CineController : Controller
    {
        [HttpGet]
        public ActionResult CinesGetAll()
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = BL.Cine.GetAll();

            cine.Cines = result.Objects;

            return View(cine);
        }

        [HttpGet]

        public ActionResult Form(int? IdCine)
        {
            ML.Result resultZona = BL.Zona.GetAll();


            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();

            if (resultZona.Correct)
            {
                cine.Zona.Zonas = resultZona.Objects;
            }

            if (IdCine != null)
            {
                
                ML.Result result = BL.Cine.GetById(IdCine.Value);
                if (result.Correct)
                {
                    cine = (ML.Cine)result.Object;
                    cine.Zona.Zonas = resultZona.Objects;
                    return View(cine);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del cine seleccionado" + result.ErrorMessage;
                    return View("Modal");
                }
            }
            else
            {
                return View(cine);
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            if (cine.IdCine == 0)
            {
                //ADD
                result = BL.Cine.Add(cine);
                ViewBag.Message = "Cine agregado correctamente";
            }
            else
            {
                //UPDATE
                result = BL.Cine.Update(cine);
                ViewBag.Message = "Datos de Cien actualizados";
            }

            if (result.Correct)
            {
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "Error al realizar la operacion";
                return PartialView("Modal");
            }
        }

        public ActionResult Delete(int IdCine)
        {
            ML.Result result = BL.Cine.Delete(IdCine);

            if (result.Correct)
            {
                ViewBag.Message = "Cine Eliminado";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "Error al intentar eliminar el Cine" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }
    }
}
