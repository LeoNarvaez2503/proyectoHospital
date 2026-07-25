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
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoHospital.Tests
{
    /// <summary>
    /// PRUEBAS DE AUTORIZACIÓN, ROLES, IDOR Y CSRF (OWASP A01:2021 & A07:2021)
    /// Justificación Metodológica:
    /// Verifica la presencia y configuración adecuada de atributos [Authorize] y [ValidateAntiForgeryToken]
    /// en todos los controladores de la aplicación, así como el control de acceso basado en roles (RBAC).
    /// </summary>
    public class SessionAndSecurityTests
    {
        // ═══════════════════════════════════════════════════════════════
        // CONTROL DE ACCESO BASADO EN ROLES (RBAC) - ATRIBUTOS [AUTHORIZE]
        // ═══════════════════════════════════════════════════════════════

        [Fact]
        public void CP19_RBAC_PacientesController_DebeTenerAtributoAuthorizeConRolesValidos()
        {
            var authAttribute = typeof(PacientesController).GetCustomAttribute<AuthorizeAttribute>();
            authAttribute.Should().NotBeNull("PacientesController debe estar protegido con el atributo [Authorize]");
            authAttribute!.Roles.Should().Contain("Admin", "Debe incluir el rol Admin");
            authAttribute.Roles.Should().Contain("Usuario", "Debe incluir el rol Usuario");
        }

        [Fact]
        public void CP19b_RBAC_TratamientosController_DebeRestringirAccesoSoloAAdmin()
        {
            var authAttribute = typeof(TratamientosController).GetCustomAttribute<AuthorizeAttribute>();
            authAttribute.Should().NotBeNull("TratamientosController debe estar protegido con [Authorize]");
            authAttribute!.Roles.Should().Be("Admin", "El módulo de Tratamientos solo debe ser accesible por el rol Admin");
        }

        [Fact]
        public void CP19c_RBAC_MedicosController_DebeRestringirAccesoSoloAAdmin()
        {
            var authAttribute = typeof(MedicosController).GetCustomAttribute<AuthorizeAttribute>();
            authAttribute.Should().NotBeNull("MedicosController debe estar protegido con [Authorize]");
            authAttribute!.Roles.Should().Be("Admin", "El módulo de Médicos solo debe ser accesible por el rol Admin");
        }

        [Fact]
        public void CP19d_RBAC_EspecialidadesController_DebeRestringirAccesoSoloAAdmin()
        {
            var authAttribute = typeof(EspecialidadesController).GetCustomAttribute<AuthorizeAttribute>();
            authAttribute.Should().NotBeNull("EspecialidadesController debe estar protegido con [Authorize]");
            authAttribute!.Roles.Should().Be("Admin", "El módulo de Especialidades solo debe ser accesible por el rol Admin");
        }

        [Fact]
        public void CP19e_RBAC_FacturacionController_DebePermitirAdminYUsuario()
        {
            var authAttribute = typeof(FacturacionController).GetCustomAttribute<AuthorizeAttribute>();
            authAttribute.Should().NotBeNull("FacturacionController debe estar protegido con [Authorize]");
            authAttribute!.Roles.Should().Contain("Admin");
            authAttribute.Roles.Should().Contain("Usuario");
        }

        [Fact]
        public void CP19f_RBAC_CitasController_DebePermitirAdminYUsuario()
        {
            var authAttribute = typeof(CitasController).GetCustomAttribute<AuthorizeAttribute>();
            authAttribute.Should().NotBeNull("CitasController debe estar protegido con [Authorize]");
            authAttribute!.Roles.Should().Contain("Admin");
            authAttribute.Roles.Should().Contain("Usuario");
        }

        // ═══════════════════════════════════════════════════════════════
        // EVALUACIÓN DE VULNERABILIDAD CSRF (FALTA DE TOKENS ANTIFORGERY)
        // ═══════════════════════════════════════════════════════════════

        [Fact]
        public void CP55_CSRF_AccesoController_RegistrarPost_DebeTenerTokenAntiforgery()
        {
            var method = typeof(AccesoController).GetMethod("Registrar", new[] { typeof(UsuarioCLS) });
            var antiforgeryAttr = method?.GetCustomAttribute<ValidateAntiForgeryTokenAttribute>();
            antiforgeryAttr.Should().NotBeNull("La acción Registrar (POST) DEBE incluir [ValidateAntiForgeryToken] para prevenir ataques CSRF");
        }

        [Fact]
        public void CP56_CSRF_AccesoController_LoginPost_DebeTenerTokenAntiforgery()
        {
            var method = typeof(AccesoController).GetMethod("Login", new[] { typeof(UsuarioCLS) });
            var antiforgeryAttr = method?.GetCustomAttribute<ValidateAntiForgeryTokenAttribute>();
            antiforgeryAttr.Should().NotBeNull("La acción Login (POST) DEBE incluir [ValidateAntiForgeryToken] para prevenir ataques CSRF");
        }

        // ═══════════════════════════════════════════════════════════════
        // EVALUACIÓN DE PRUEBAS DE AUTORIZACIÓN HORIZONTAL (IDOR)
        // ═══════════════════════════════════════════════════════════════

        [Fact]
        public void CP22_IDOR_PacientesController_RecuperarPaciente_AccesoConIDInexistente()
        {
            var controller = new PacientesController();
            
            // Un ID que no pertenece al usuario autenticado no debe causar fallos de puntero nulo no manejados
            Action act = () => controller.RecuperarPaciente(999999);
            act.Should().NotThrow<NullReferenceException>("El IDOR no debe provocar fallos sin capturar en el servidor");
        }

        [Fact]
        public void CP23_IDOR_CitasController_RecuperarCitas_AccesoConIDInexistente()
        {
            var controller = new CitasController();

            Action act = () => controller.RecuperarCitas(999999);
            act.Should().NotThrow<NullReferenceException>();
        }

        [Fact]
        public void CP24_IDOR_TratamientosController_RecuperarTratamiento_AccesoConIDInexistente()
        {
            var controller = new TratamientosController();

            Action act = () => controller.RecuperarTratamiento(999999);
            act.Should().NotThrow<NullReferenceException>();
        }
    }
}
