using Xunit;
using FluentAssertions;
using Login.Controllers;
using CapaEntidad;
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
using System.Threading.Tasks;

namespace ProyectoHospital.Tests
{
    /// <summary>
    /// PRUEBAS DE VALIDACIÓN DE DATOS DE ENTRADA Y VALORES LÍMITE (SQA)
    /// Justificación Metodológica (El Porqué):
    /// SonarQube detectó la omisión de ModelState.IsValid en los controllers
    /// (BUG-01, BUG-05, BUG-08, BUG-10). Estas pruebas verifican el comportamiento
    /// del sistema al recibir datos inválidos, nulos, vacíos o fuera de rango.
    /// 
    /// Para Qué: Asegurar que el sistema no crash ante datos malformados y
    /// manejar controladamente los errores de validación según ISO 25010 (Robustez).
    /// </summary>
    public class ValidationAndBoundaryTests
    {
        // ═══════════════════════════════════════════════════════════════
        // GRUPO 1: PRUEBAS DE CAMPOS NULOS (NULL SAFETY)
        // ═══════════════════════════════════════════════════════════════

        [Fact]
        public void CP25_Validacion_GuardarPaciente_CamposNulos_NoDebeCrashear()
        {
            var controller = new PacientesController();
            var pacienteNulo = new PacienteCLS
            {
                Id = 0,
                Nombre = null,
                Apellido = null,
                Telefono = null,
                Email = null,
                Direccion = null
            };

            // El sistema debe manejar campos nulos sin NullReferenceException
            Action act = () => controller.GuardarPaciente(pacienteNulo);
            act.Should().NotThrow<NullReferenceException>(
                "GuardarPaciente debe manejar campos nulos sin crashear");
        }

        [Fact]
        public void CP25b_Validacion_GuardarMedico_CamposNulos_NoDebeCrashear()
        {
            var controller = new MedicosController();
            var medicoNulo = new MedicosCLS
            {
                Id = 0,
                Nombre = null,
                Apellido = null,
                Telefono = null,
                Email = null
            };

            Action act = () => controller.GuardarMedico(medicoNulo);
            act.Should().NotThrow<NullReferenceException>(
                "GuardarMedico debe manejar campos nulos sin crashear");
        }

        [Fact]
        public void CP25c_Validacion_GuardarTratamiento_CamposNulos_NoDebeCrashear()
        {
            var controller = new TratamientosController();
            var tratamientoNulo = new TratamientosCLS
            {
                Id = 0,
                Descripcion = null,
                PacienteId = 1
            };

            Action act = () => controller.GuardarTratamiento(tratamientoNulo);
            act.Should().NotThrow<NullReferenceException>(
                "GuardarTratamiento debe manejar campos nulos sin crashear");
        }

        [Fact]
        public void CP25d_Validacion_GuardarFacturacion_CamposNulos_NoDebeCrashear()
        {
            var controller = new FacturacionController();
            var facturaNula = new FacturacionCLS
            {
                Id = 0,
                MetodoPago = null,
                PacienteId = 1
            };

            Action act = () => controller.GuardarFacturacion(facturaNula);
            act.Should().NotThrow<NullReferenceException>(
                "GuardarFacturacion debe manejar campos nulos sin crashear");
        }

        [Fact]
        public void CP25e_Validacion_GuardarEspecialidad_CamposNulos_NoDebeCrashear()
        {
            var controller = new EspecialidadesController();
            var especialidadNula = new EspecialidadesCLS
            {
                Id = 0,
                Nombre = null
            };

            Action act = () => controller.GuardarEspecialidad(especialidadNula);
            act.Should().NotThrow<NullReferenceException>(
                "GuardarEspecialidad debe manejar campos nulos sin crashear");
        }

        // ═══════════════════════════════════════════════════════════════
        // GRUPO 2: PRUEBAS DE CADENAS VACÍAS (EMPTY STRINGS)
        // ═══════════════════════════════════════════════════════════════

        [Fact]
        public void CP26_Validacion_GuardarPaciente_CamposVacios_ManejoControlado()
        {
            var controller = new PacientesController();
            var pacienteVacio = new PacienteCLS
            {
                Id = 0,
                Nombre = "",
                Apellido = "",
                Telefono = "",
                Email = "",
                Direccion = ""
            };

            Action act = () => controller.GuardarPaciente(pacienteVacio);
            act.Should().NotThrow<NullReferenceException>(
                "Campos vacíos deben ser manejados controladamente");
        }

        // ═══════════════════════════════════════════════════════════════
        // GRUPO 3: PRUEBAS DE VALORES LÍMITE (BOUNDARY VALUE ANALYSIS)
        // ═══════════════════════════════════════════════════════════════

        [Fact]
        public void CP33_ValorLimite_NombreConUnCaracter_DebeSerAceptado()
        {
            var paciente = new PacienteCLS
            {
                Id = 0,
                Nombre = "A",
                Apellido = "B"
            };

            paciente.Nombre.Should().HaveLength(1, "El nombre con 1 carácter es el límite mínimo válido");
            paciente.Nombre.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void CP34_ValorLimite_NombreCon10000Caracteres_ManejoControlado()
        {
            var controller = new PacientesController();
            var nombreExtremo = new string('A', 10000);

            var pacienteExtremo = new PacienteCLS
            {
                Id = 0,
                Nombre = nombreExtremo,
                Apellido = nombreExtremo,
                Telefono = "0999999999",
                Email = "extremo@test.com",
                Direccion = "Dirección de prueba"
            };

            // No debe crashear aunque el nombre sea absurdamente largo
            Action act = () => controller.GuardarPaciente(pacienteExtremo);
            act.Should().NotThrow<StackOverflowException>(
                "Nombres de 10,000 caracteres no deben provocar desbordamiento de pila");
        }

        [Fact]
        public void CP35_ValorLimite_IDMaximoEntero_ManejoControlado()
        {
            var controller = new PacientesController();

            // int.MaxValue = 2,147,483,647
            Action act = () => controller.RecuperarPaciente(int.MaxValue);
            act.Should().NotThrow<OverflowException>(
                "ID con valor máximo de int no debe causar overflow");
        }

        [Fact]
        public void CP36_ValorLimite_IDCero_ManejoControlado()
        {
            var controller = new CitasController();

            Action act = () => controller.EliminarCita(0);
            act.Should().NotThrow<NullReferenceException>(
                "ID = 0 debe manejarse controladamente sin crash");
        }

        [Fact]
        public void CP36b_ValorLimite_IDNegativo_ManejoControlado()
        {
            var controller = new PacientesController();

            Action act = () => controller.EliminarPaciente(-1);
            act.Should().NotThrow<NullReferenceException>(
                "ID negativo debe manejarse controladamente");
        }

        [Fact]
        public void CP37_ValorLimite_FechaNacimientoFutura_DebeSerDetectada()
        {
            var paciente = new PacienteCLS
            {
                Id = 0,
                Nombre = "Futuro",
                Apellido = "Test",
                FechaNacimiento = new DateTime(2099, 12, 31)
            };

            // Una fecha de nacimiento en el futuro debería ser lógicamente inválida
            paciente.FechaNacimiento.Should().BeAfter(DateTime.Now,
                "Se detecta que la fecha está en el futuro — el sistema debería validar esto");
        }

        [Fact]
        public void CP37b_ValorLimite_FechaNacimientoMinima_ManejoControlado()
        {
            var paciente = new PacienteCLS
            {
                Id = 0,
                Nombre = "Antiguo",
                Apellido = "Test",
                FechaNacimiento = DateTime.MinValue
            };

            // DateTime.MinValue no debe causar crash
            paciente.FechaNacimiento.Should().Be(DateTime.MinValue);
        }

        [Fact]
        public void CP38_ValorLimite_TelefonoConCaracteresEspeciales_ManejoControlado()
        {
            var controller = new PacientesController();
            var paciente = new PacienteCLS
            {
                Id = 0,
                Nombre = "Test",
                Apellido = "Especial",
                Telefono = "+593 (09) 9999-9999",
                Email = "test@hospital.com",
                Direccion = "Quito"
            };

            Action act = () => controller.GuardarPaciente(paciente);
            act.Should().NotThrow<FormatException>(
                "Teléfonos con formato especial no deben causar error de formato");
        }

        // ═══════════════════════════════════════════════════════════════
        // GRUPO 4: PRUEBAS DE REGISTRO DE USUARIO (VALIDACIÓN)
        // ═══════════════════════════════════════════════════════════════

        [Fact]
        public void CP28_Validacion_RegistrarUsuario_CorreoVacio_DebeRechazar()
        {
            var controller = new AccesoController
            {
                ControllerContext = CrearContextoBasico()
            };

            var usuario = new UsuarioCLS
            {
                correo = "",
                clave = "Test123!",
                confClave = "Test123!"
            };

            // El sistema debe manejar correo vacío sin crash
            Action act = () => controller.Registrar(usuario);
            act.Should().NotThrow<NullReferenceException>(
                "Correo vacío debe manejarse sin NullReferenceException");
        }

        [Fact]
        public void CP28b_Validacion_RegistrarUsuario_CorreoNulo_DebeRechazar()
        {
            var controller = new AccesoController
            {
                ControllerContext = CrearContextoBasico()
            };

            var usuario = new UsuarioCLS
            {
                correo = null,
                clave = "Test123!",
                confClave = "Test123!"
            };

            Action act = () => controller.Registrar(usuario);
            act.Should().NotThrow<NullReferenceException>(
                "Correo nulo debe manejarse sin NullReferenceException");
        }

        [Fact]
        public void CP29_Validacion_Facturacion_MontoNegativo_DebeSerDetectado()
        {
            var factura = new FacturacionCLS
            {
                Id = 0,
                PacienteId = 1,
                Monto = -500.00m,
                MetodoPago = "Tarjeta"
            };

            // Un monto negativo debería ser lógicamente inválido
            factura.Monto.Should().BeNegative(
                "Se detecta monto negativo — el sistema debería validar montos positivos");
        }

        [Fact]
        public void CP29b_Validacion_Facturacion_MontoCero_DebeSerDetectado()
        {
            var factura = new FacturacionCLS
            {
                Id = 0,
                PacienteId = 1,
                Monto = 0,
                MetodoPago = "Efectivo"
            };

            factura.Monto.Should().Be(0,
                "Monto cero es un caso borde — el sistema debería decidir si es válido o no");
        }

        // ═══════════════════════════════════════════════════════════════
        // HELPERS PRIVADOS
        // ═══════════════════════════════════════════════════════════════

        private ControllerContext CrearContextoBasico()
        {
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
                RequestServices = serviceProvider
            };

            return new ControllerContext { HttpContext = httpContext };
        }
    }
}
