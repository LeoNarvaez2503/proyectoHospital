using CapaNegocio;
using CapaEntidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [Authorize(Roles = "Admin, Usuario")]
    public class GenericController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<ForaneaCLS> obtenerClaves(string tabla)
        {
            GenericBL objGenericBL = new GenericBL();
            return objGenericBL.obtenerClaves(tabla);
        }
    }
}
