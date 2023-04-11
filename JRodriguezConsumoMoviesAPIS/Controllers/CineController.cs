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
            ML.Cine cine = new ML.Cine();

            if (IdCine != null)
            {
                return View(cine);
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
    }
}
