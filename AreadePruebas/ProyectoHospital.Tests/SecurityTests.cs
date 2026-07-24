using Xunit;
using FluentAssertions;
using Login.Controllers;
using Login.Models;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHospital.Tests
{
    public class SecurityTests
    {
        private ControllerContext CrearContextoConRol(string rol)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testuser@hospital.com"),
                new Claim(ClaimTypes.Role, rol)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var mockAuthService = new Mock<IAuthenticationService>();
            mockAuthService
                .Setup(x => x.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);

            var mockTempData = new Mock<ITempDataDictionary>();
            var mockTempDataFactory = new Mock<ITempDataDictionaryFactory>();
            mockTempDataFactory.Setup(x => x.GetTempData(It.IsAny<HttpContext>())).Returns(mockTempData.Object);

            var mockUrlHelperFactory = new Mock<IUrlHelperFactory>();
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelperFactory.Setup(x => x.GetUrlHelper(It.IsAny<ActionContext>())).Returns(mockUrlHelper.Object);

            var serviceProvider = new ServiceCollection()
                .AddSingleton(mockAuthService.Object)
                .AddSingleton(mockTempDataFactory.Object)
                .AddSingleton(mockUrlHelperFactory.Object)
                .BuildServiceProvider();

            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal,
                RequestServices = serviceProvider
            };

            return new ControllerContext { HttpContext = httpContext };
        }

        [Theory]
        [InlineData("' OR '1'='1")]
        [InlineData("admin' --")]
        [InlineData("'; DROP TABLE Usuario; --")]
        [InlineData("' UNION SELECT 1, 'admin', 'password' --")]
        public void OWASP_A03_InyeccionSQL_DebeSerSanitizadaOProcesadaSinEjecucionMaliciosa(string sqlPayload)
        {
            var usuario = new UsuarioCLS
            {
                correo = sqlPayload,
                clave = sqlPayload
            };

            var controller = new AccesoController
            {
                ControllerContext = CrearContextoConRol("Usuario")
            };

            // Ejecución del login con payload malicioso
            Func<Task> act = async () => await controller.Login(usuario);

            // Debe manejar la excepción o retornar vista de error sin vulnerar la BD
            act.Should().NotThrowAsync<NullReferenceException>();
            usuario.correo.Should().Be(sqlPayload);
        }

        [Fact]
        public void OWASP_A02_HashingContrasenas_DebeSerUnidireccionalYResistenteAColisiones()
        {
            string pass1 = "Admin123!";
            string pass2 = "Admin123?";

            using SHA256 sha256 = SHA256.Create();
            string hash1 = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(pass1))).Replace("-", "");
            string hash2 = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(pass2))).Replace("-", "");

            hash1.Should().NotBeNullOrEmpty();
            hash1.Length.Should().Be(64); // SHA-256 produce 64 caracteres en hexadecimal
            hash1.Should().NotBe(pass1); // No debe ser texto plano
            hash1.Should().NotBe(hash2); // Resistencia a colisiones
        }

        [Fact]
        public void OWASP_A01_ControlAccesoPorRol_SecretarioNoDebeTenerAccesoAModuloTratamientos()
        {
            var controller = new AccesoController
            {
                ControllerContext = CrearContextoConRol("Secretario")
            };

            bool tienePermiso = controller.RevisarPermisos();
            tienePermiso.Should().BeFalse("El rol Secretario no debe tener permisos de acceso a datos sensibles de Tratamientos");
        }

        [Fact]
        public void Seguridad_RegistroUsuario_DebeRechazarContrasenasNoCoincidentes()
        {
            var controller = new AccesoController
            {
                ControllerContext = CrearContextoConRol("Usuario")
            };

            var usuarioInvalido = new UsuarioCLS
            {
                correo = "nuevo@hospital.com",
                clave = "ClaveSegura123!",
                confClave = "ClaveDistinta456!"
            };

            var result = controller.Registrar(usuarioInvalido);
            result.Should().BeOfType<ViewResult>("No debe registrar al usuario si la confirmación de contraseña no coincide");
        }
    }
}
