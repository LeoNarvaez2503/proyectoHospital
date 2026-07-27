using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [Authorize(Roles = "Admin")]

    public class MedicosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public List<MedicosCLS> ListarMedicos()
        {
            MedicosBL objMedicosBL = new MedicosBL();
            return objMedicosBL.ListarMedicos();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int GuardarMedico(MedicosCLS objMedicoCLS)
        {
            if (!ModelState.IsValid)
            {
                foreach (var val in ModelState.Values) {
                    foreach (var err in val.Errors) {
                        Console.WriteLine("ModelState Error: " + err.ErrorMessage);
                    }
                }
                return -1;
            }
            MedicosBL objMedicosBL = new MedicosBL();
            return objMedicosBL.GuardarMedico(objMedicoCLS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int EliminarMedico(int id)
        {
            MedicosBL objMedicosBL = new MedicosBL();
            return objMedicosBL.EliminarMedico(id);
        }

        public MedicosCLS RecuperarMedico(int id)
        {
            MedicosBL objMedicosBL = new MedicosBL();
            return objMedicosBL.RecuperarMedico(id);
        }

        public List<MedicosCLS> FiltrarMedicos(MedicosCLS filtro)
        {
            MedicosBL objMedicosBL = new MedicosBL();
            return objMedicosBL.FiltrarMedicos(filtro);
        }
    }
}
