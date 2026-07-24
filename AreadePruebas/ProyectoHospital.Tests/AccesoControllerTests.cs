using Xunit;
using FluentAssertions;
using CapaEntidad;
using Login.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoHospital.Tests
{
    public class AccesoControllerTests
    {
        [Fact]
        public void Registrar_ContrasenasNoCoinciden_RetornaVistaConMensajeError()
        {
            // Arrange
            var controller = new AccesoController();
            var usuario = new UsuarioCLS
            {
                correo = "test@hospital.com",
                clave = "Password123!",
                confClave = "PasswordDiferente!"
            };

            // Act
            var result = controller.Registrar(usuario) as ViewResult;

            // Assert
            result.Should().NotBeNull();
            controller.ViewData["mensaje"].Should().Be("Las contraseñas no coinciden");
        }

        [Fact]
        public void Denegado_RetornaVistaAccesoDenegado()
        {
            // Arrange
            var controller = new AccesoController();

            // Act
            var result = controller.Denegado();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}
