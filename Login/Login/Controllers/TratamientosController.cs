using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [Authorize(Roles = "Admin")]

    public class TratamientosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public List<TratamientosCLS> ListarTratamientos()
        {
            TratamientosBL objTratamientosBL = new TratamientosBL();
            return objTratamientosBL.ListarTratamientos();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int GuardarTratamiento(TratamientosCLS objTratamientoCLS)
        {
            if (!ModelState.IsValid)
            {
                return -1;
            }
            TratamientosBL objTratamientosBL = new TratamientosBL();
            return objTratamientosBL.GuardarTratamiento(objTratamientoCLS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int EliminarTratamiento(int id)
        {
            TratamientosBL objTratamientosBL = new TratamientosBL();
            return objTratamientosBL.EliminarTratamiento(id);
        }

        public TratamientosCLS RecuperarTratamiento(int id)
        {
            TratamientosBL objTratamientosBL = new TratamientosBL();
            return objTratamientosBL.RecuperarTratamiento(id);
        }

        public List<TratamientosCLS> FiltrarTratamientos(TratamientosCLS filtro)
        {
            TratamientosBL objTratamientosBL = new TratamientosBL();
            return objTratamientosBL.FiltrarTratamientos(filtro);
        }
    }
}
