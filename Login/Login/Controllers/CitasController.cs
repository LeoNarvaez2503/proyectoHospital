using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using CapaDatos;
using CapaNegocio;
using Microsoft.AspNetCore.Authorization;

namespace Login.Controllers
{
    [Authorize (Roles ="Admin, Usuario")]
    public class CitasController : Controller
    {
        public IActionResult Citas()
        {
            return View();
        }
        public List<CitasCLS> ListarCitas()
        {
            CitasBL objCitasBL = new CitasBL();
            return objCitasBL.ListarCitas();
        }
        public int GuardarCita(CitasCLS objCitasCLS)
        {
            CitasBL objCitasBL = new CitasBL();
            return objCitasBL.GuardarCita(objCitasCLS);
        }

        public int EliminarCita(int id)
        {
            CitasBL objCitasBL = new CitasBL();
            return objCitasBL.EliminarCita(id);
        }

        public CitasCLS RecuperarCitas(int id)
        {
            CitasBL objCitasBL = new CitasBL();
            return objCitasBL.RecuperarCitas(id);
        }

        public List<CitasCLS> FiltrarCitas(CitasCLS objCitasCLS)
        {
            CitasBL objCitasBL = new CitasBL();
            return objCitasBL.FiltrarCitas(objCitasCLS);
        }
    }
}
