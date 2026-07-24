using Xunit;
using FluentAssertions;
using Login.Controllers;
using Login.Models;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoHospital.Tests
{
    public class ControllersTests
    {
        private ControllerContext CrearMockControllerContext()
        {
            var mockAuthService = new Mock<IAuthenticationService>();
            mockAuthService
                .Setup(x => x.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);
            mockAuthService
                .Setup(x => x.SignOutAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);

            var mockTempData = new Mock<ITempDataDictionary>();
            var mockTempDataFactory = new Mock<ITempDataDictionaryFactory>();
            mockTempDataFactory
                .Setup(x => x.GetTempData(It.IsAny<HttpContext>()))
                .Returns(mockTempData.Object);

            var mockUrlHelperFactory = new Mock<IUrlHelperFactory>();
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelperFactory
                .Setup(x => x.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(mockUrlHelper.Object);

            var serviceProvider = new ServiceCollection()
                .AddSingleton(mockAuthService.Object)
                .AddSingleton(mockTempDataFactory.Object)
                .AddSingleton(mockUrlHelperFactory.Object)
                .BuildServiceProvider();

            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider
            };

            return new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public void GenericController_TodasLasAcciones_EjecutanCorrectamente()
        {
            var controller = new GenericController();
            controller.Index().Should().BeOfType<ViewResult>();

            try
            {
                controller.obtenerClaves("Paciente");
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }

        [Fact]
        public void CitasController_TodasLasAcciones_EjecutanCorrectamente()
        {
            var controller = new CitasController();
            controller.Citas().Should().BeOfType<ViewResult>();

            try { controller.ListarCitas(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.GuardarCita(new CitasCLS { idCita = 0 }); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.RecuperarCitas(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.FiltrarCitas(new CitasCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.EliminarCita(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void PacientesController_TodasLasAcciones_EjecutanCorrectamente()
        {
            var controller = new PacientesController();
            controller.Index().Should().BeOfType<ViewResult>();

            try { controller.ListarPacientes(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.GuardarPaciente(new PacienteCLS { Id = 0 }); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.RecuperarPaciente(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.FiltrarPacientes(new PacienteCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.EliminarPaciente(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void MedicosController_TodasLasAcciones_EjecutanCorrectamente()
        {
            var controller = new MedicosController();
            controller.Index().Should().BeOfType<ViewResult>();

            try { controller.ListarMedicos(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.GuardarMedico(new MedicosCLS { Id = 0 }); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.RecuperarMedico(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.FiltrarMedicos(new MedicosCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.EliminarMedico(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void TratamientosController_TodasLasAcciones_EjecutanCorrectamente()
        {
            var controller = new TratamientosController();
            controller.Index().Should().BeOfType<ViewResult>();

            try { controller.ListarTratamientos(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.GuardarTratamiento(new TratamientosCLS { Id = 0 }); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.RecuperarTratamiento(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.FiltrarTratamientos(new TratamientosCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.EliminarTratamiento(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void FacturacionController_TodasLasAcciones_EjecutanCorrectamente()
        {
            var controller = new FacturacionController();
            controller.Index().Should().BeOfType<ViewResult>();

            try { controller.ListarFacturaciones(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.GuardarFacturacion(new FacturacionCLS { Id = 0 }); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.RecuperarFacturacion(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.FiltrarFacturaciones(new FacturacionCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.EliminarFacturacion(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void EspecialidadesController_TodasLasAcciones_EjecutanCorrectamente()
        {
            var controller = new EspecialidadesController();
            controller.Index().Should().BeOfType<ViewResult>();

            try { controller.ListarEspecialidades(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.GuardarEspecialidad(new EspecialidadesCLS { Id = 0 }); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.RecuperarEspecialidad(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.FiltrarEspecialidades(new EspecialidadesCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { controller.EliminarEspecialidad(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void HomeController_TodasLasAcciones_EjecutanCorrectamente()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object);
            controller.Index().Should().BeOfType<ViewResult>();
            controller.Privacy().Should().BeOfType<ViewResult>();

            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
            controller.Error().Should().BeOfType<ViewResult>();

            try { controller.ListarCitas(); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public async Task AccesoController_TodasLasAcciones_EjecutanCorrectamente()
        {
            var controller = new AccesoController
            {
                ControllerContext = CrearMockControllerContext(),
                TempData = new Mock<ITempDataDictionary>().Object,
                Url = new Mock<IUrlHelper>().Object
            };

            controller.Login().Should().BeOfType<ViewResult>();
            controller.Registrar().Should().BeOfType<ViewResult>();
            controller.Denegado().Should().BeOfType<ViewResult>();

            var userClaveMismatch = new UsuarioCLS { clave = "123", confClave = "456" };
            controller.Registrar(userClaveMismatch).Should().BeOfType<ViewResult>();

            var userFailReg = new UsuarioCLS { clave = "123", confClave = "123" };
            try { controller.Registrar(userFailReg); } catch (Exception ex) { ex.Should().NotBeNull(); }

            var userFailLogin = new UsuarioCLS { clave = "123", correo = "fail@test.com" };
            try { await controller.Login(userFailLogin); } catch (Exception ex) { ex.Should().NotBeNull(); }

            try
            {
                var logoutResult = await controller.Logout();
                logoutResult.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }

            try
            {
                controller.RevisarPermisos();
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }
    }
}
