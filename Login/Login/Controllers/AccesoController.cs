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
using System.Text.RegularExpressions;

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

        private bool EsEntradaSegura(string entrada)
        {
            if (string.IsNullOrEmpty(entrada)) return true;
            string lower = entrada.ToLowerInvariant();
            if (lower.Contains("<script") || lower.Contains("</script>") ||
                lower.Contains("javascript:") || lower.Contains("onerror=") ||
                lower.Contains("onload=") || lower.Contains("<iframe") ||
                lower.Contains("<img") || lower.Contains("<svg") ||
                lower.Contains("<object") || lower.Contains("<embed") ||
                entrada.Contains("<") || entrada.Contains(">"))
            {
                return false;
            }
            return true;
        }

        private bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (!EsEntradaSegura(email)) return false;
            // Exigir formato de correo válido con regex y sin caracteres de scripts
            string patronEmail = @"^[^@\s<>'""\(\)]+@[^@\s<>'""\(\)]+\.[^@\s<>'""\(\)]+$";
            return Regex.IsMatch(email, patronEmail, RegexOptions.IgnoreCase);
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioCLS objUser)
        {
            if (string.IsNullOrWhiteSpace(objUser.correo) || string.IsNullOrWhiteSpace(objUser.clave) || string.IsNullOrWhiteSpace(objUser.confClave))
            {
                ViewData["mensajeRegistro"] = "Por favor, completa todos los campos para registrarte";
                ViewData["isRegister"] = true;
                objUser.clave = "";
                objUser.confClave = "";
                return View("Login", objUser);
            }

            if (!EsEmailValido(objUser.correo) || !EsEntradaSegura(objUser.clave))
            {
                ViewData["mensajeRegistro"] = "El correo electrónico contiene caracteres o formatos no permitidos por seguridad";
                ViewData["isRegister"] = true;
                objUser.clave = "";
                objUser.confClave = "";
                return View("Login", objUser);
            }

            if (objUser.clave != objUser.confClave)
            {
                ViewData["mensajeRegistro"] = "Las contraseñas no coinciden";
                ViewData["isRegister"] = true;
                objUser.clave = "";
                objUser.confClave = "";
                return View("Login", objUser);
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
                if (string.IsNullOrWhiteSpace(mensaje))
                {
                    mensaje = "Error al registrar usuario";
                }
                ViewData["mensajeRegistro"] = mensaje;
                ViewData["isRegister"] = true;
                objUser.clave = "";
                objUser.confClave = "";
                return View("Login", objUser);
            }
        }

        private string Encriptar(string cadena)
        {
            StringBuilder builder = new StringBuilder();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] result = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(cadena ?? ""));
                foreach (byte b in result)
                    builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioCLS objUser)
        {
            if (string.IsNullOrWhiteSpace(objUser.correo) || string.IsNullOrWhiteSpace(objUser.clave))
            {
                ViewData["mensaje"] = "Por favor, ingresa tu correo y contraseña";
                ViewData["isRegister"] = false;
                objUser.clave = "";
                return View(objUser);
            }

            if (!EsEmailValido(objUser.correo) || !EsEntradaSegura(objUser.clave))
            {
                ViewData["mensaje"] = "El correo o contraseña contiene caracteres o formatos no permitidos por seguridad";
                ViewData["isRegister"] = false;
                objUser.clave = "";
                return View(objUser);
            }

            objUser.clave = Encriptar(objUser.clave);
            string mensaje;
            int idUsuario;
            string rol;
            UsuarioDAL objUserDAL = new UsuarioDAL();
            bool exito = objUserDAL.IniciarSesion(objUser, out mensaje, out idUsuario, out rol);
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
                if (string.IsNullOrWhiteSpace(mensaje))
                {
                    mensaje = "Correo o contraseña incorrectos";
                }
                ViewData["mensaje"] = mensaje;
                ViewData["isRegister"] = false;
                objUser.clave = "";
                return View(objUser);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login", "Acceso");
        }

        public bool RevisarPermisos()
        {
            return User.IsInRole("Admin");
        }
    }
}
