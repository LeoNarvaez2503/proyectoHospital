using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
using CapaEntidad;
using CapaDatos;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Login.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Login()
        {
            return View();

        }
        public IActionResult Registrar()
        {
            return View();
        }
        public IActionResult Denegado()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(UsuarioCLS objUser)
        {
            if (objUser.clave != objUser.confClave)
            {
                ViewData["mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            objUser.clave = Encriptar(objUser.clave);

            UsuarioDAL objUserDAL = new UsuarioDAL();
            bool registrado = objUserDAL.RegistrarUsuario(objUser, out string mensaje);

            if (registrado)
            {
                TempData["mensajeExito"] = "¡Registro exitoso! Por favor, inicia sesión.";
                return RedirectToAction("Login");
            }
            else
            {
                ViewData["mensaje"] = mensaje;
                return View("Login");
            }

        }

        private string Encriptar(string cadena)
        {
            StringBuilder builder = new StringBuilder();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] result = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(cadena));
                foreach (byte b in result)
                    builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioCLS objUser)
        {
            objUser.clave = Encriptar(objUser.clave);
            string mensaje;
            int idUsuario;
            string rol;
            UsuarioDAL objUserDAL = new UsuarioDAL();
            bool exito = objUserDAL.IniciarSesion(objUser, out mensaje,out idUsuario, out rol);
            if (exito)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, objUser.correo),
                    new Claim(ClaimTypes.Role, rol)
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", principal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["mensaje"] = mensaje;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Cerrar la sesión del usuario usando el esquema "CookieAuth"
            await HttpContext.SignOutAsync("CookieAuth");

            // Redirigir al usuario a la página de inicio de sesión
            return RedirectToAction("Login", "Acceso");
        }
        public bool RevisarPermisos()
        {
            return User.IsInRole("Admin");
        }
    }
}
