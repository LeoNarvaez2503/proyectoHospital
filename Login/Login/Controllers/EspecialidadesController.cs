using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EspecialidadesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public List<EspecialidadesCLS> ListarEspecialidades()
        {
            EspecialidadesBL objEspecialidadesBL = new EspecialidadesBL();
            return objEspecialidadesBL.ListarEspecialidades();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int GuardarEspecialidad(EspecialidadesCLS objEspecialidadCLS)
        {
            if (!ModelState.IsValid)
            {
                return -1;
            }
            EspecialidadesBL objEspecialidadesBL = new EspecialidadesBL();
            return objEspecialidadesBL.GuardarEspecialidad(objEspecialidadCLS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int EliminarEspecialidad(int id)
        {
            EspecialidadesBL objEspecialidadesBL = new EspecialidadesBL();
            return objEspecialidadesBL.EliminarEspecialidad(id);
        }

        public EspecialidadesCLS RecuperarEspecialidad(int id)
        {
            EspecialidadesBL objEspecialidadesBL = new EspecialidadesBL();
            return objEspecialidadesBL.RecuperarEspecialidad(id);
        }

        public List<EspecialidadesCLS> FiltrarEspecialidades(EspecialidadesCLS filtro)
        {
            EspecialidadesBL objEspecialidadesBL = new EspecialidadesBL();
            return objEspecialidadesBL.FiltrarEspecialidades(filtro);
        }
    }
}
