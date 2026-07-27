using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [Authorize(Roles = "Admin, Usuario")]

    public class PacientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public List<PacienteCLS> ListarPacientes()
        {
            PacientesBL objPacientesBL = new PacientesBL();
            return objPacientesBL.ListarPacientes();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public int GuardarPaciente(PacienteCLS objPacienteCLS)
        {
            if (!ModelState.IsValid)
            {
                return -1;
            }
            PacientesBL objPacientesBL = new PacientesBL();
            return objPacientesBL.GuardarPaciente(objPacienteCLS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public int EliminarPaciente(int id)
        {
            PacientesBL objPacientesBL = new PacientesBL();
            return objPacientesBL.EliminarPaciente(id);
        }

        public PacienteCLS RecuperarPaciente(int id)
        {
            PacientesBL objPacientesBL = new PacientesBL();
            return objPacientesBL.RecuperarPaciente(id);
        }

        public List<PacienteCLS> FiltrarPacientes(PacienteCLS filtro)
        {
            PacientesBL objPacientesBL = new PacientesBL();
            return objPacientesBL.FiltrarPacientes(filtro);
        }
    }
}
