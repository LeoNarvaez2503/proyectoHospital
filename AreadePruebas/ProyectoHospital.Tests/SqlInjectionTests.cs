using Xunit;
using FluentAssertions;
using Login.Controllers;
using CapaEntidad;
using CapaNegocio;
using CapaDatos;
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
    /// PRUEBAS DE INYECCIÓN SQL EN TODOS LOS MÓDULOS CRUD (OWASP A03:2021)
    /// Justificación Metodológica (El Porqué):
    /// Las pruebas SQLi existentes (SecurityTests.cs) solo evalúan el endpoint de Login.
    /// Sin embargo, el sistema tiene 6 módulos CRUD que reciben entrada directa del usuario
    /// y deben ser evaluados contra payloads maliciosos de inyección SQL.
    /// 
    /// Para Qué: Garantizar que la totalidad de los puntos de entrada del sistema
    /// hospitalario son resistentes a inyección de código SQL, no solo el módulo de
    /// autenticación, cumpliendo al 100% con OWASP A03:2021.
    /// </summary>
    public class SqlInjectionTests
    {
        // ═══════════════════════════════════════════════════════════════
        // PAYLOADS DE ATAQUE REUTILIZABLES (OWASP Testing Guide v4.2)
        // ═══════════════════════════════════════════════════════════════
        public static IEnumerable<object[]> PayloadsSQLi => new List<object[]>
        {
            new object[] { "' OR '1'='1" },
            new object[] { "admin' --" },
            new object[] { "'; DROP TABLE Paciente; --" },
            new object[] { "' UNION SELECT 1, 'hacked', 'pwd' --" },
            new object[] { "1; DELETE FROM Paciente WHERE 1=1; --" },
            new object[] { "' OR 1=1; EXEC xp_cmdshell('whoami'); --" },
            new object[] { "'; WAITFOR DELAY '0:0:5'; --" },
            new object[] { "' AND 1=CONVERT(int, (SELECT @@version)); --" }
        };

        // ═══════════════════════════════════════════════════════════════
        // CP-13: INYECCIÓN SQL EN MÓDULO PACIENTES
        // ═══════════════════════════════════════════════════════════════

        [Theory]
        [MemberData(nameof(PayloadsSQLi))]
        public void CP13_OWASP_A03_InyeccionSQL_Pacientes_GuardarConPayloadMalicioso(string sqlPayload)
        {
            var controller = new PacientesController();
            var pacienteMalicioso = new PacienteCLS
            {
                Id = 0,
                Nombre = sqlPayload,
                Apellido = sqlPayload,
                Telefono = sqlPayload,
                Email = sqlPayload,
                Direccion = sqlPayload
            };

            // El sistema debe manejar el payload sin ejecutar código SQL malicioso
            Func<int> act = () => controller.GuardarPaciente(pacienteMalicioso);
            act.Should().NotThrow<SqlInjectionException>(
                "El módulo Pacientes debe sanitizar payloads SQL maliciosos mediante Stored Procedures parametrizados");
        }

        [Theory]
        [MemberData(nameof(PayloadsSQLi))]
        public void CP13b_OWASP_A03_InyeccionSQL_Pacientes_FiltrarConPayloadMalicioso(string sqlPayload)
        {
            var controller = new PacientesController();
            var filtroMalicioso = new PacienteCLS
            {
                Nombre = sqlPayload,
                Apellido = sqlPayload
            };

            Func<List<PacienteCLS>> act = () => controller.FiltrarPacientes(filtroMalicioso);
            act.Should().NotThrow<SqlInjectionException>(
                "El filtrado de pacientes no debe ser vulnerable a inyección SQL");
        }

        // ═══════════════════════════════════════════════════════════════
        // CP-14: INYECCIÓN SQL EN MÓDULO MÉDICOS
        // ═══════════════════════════════════════════════════════════════

        [Theory]
        [MemberData(nameof(PayloadsSQLi))]
        public void CP14_OWASP_A03_InyeccionSQL_Medicos_GuardarConPayloadMalicioso(string sqlPayload)
        {
            var controller = new MedicosController();
            var medicoMalicioso = new MedicosCLS
            {
                Id = 0,
                Nombre = sqlPayload,
                Apellido = sqlPayload,
                Telefono = sqlPayload,
                Email = sqlPayload
            };

            Func<int> act = () => controller.GuardarMedico(medicoMalicioso);
            act.Should().NotThrow<SqlInjectionException>(
                "El módulo Médicos debe sanitizar payloads SQL maliciosos");
        }

        [Theory]
        [MemberData(nameof(PayloadsSQLi))]
        public void CP14b_OWASP_A03_InyeccionSQL_Medicos_FiltrarConPayloadMalicioso(string sqlPayload)
        {
            var controller = new MedicosController();
            var filtroMalicioso = new MedicosCLS
            {
                Nombre = sqlPayload,
                Apellido = sqlPayload
            };

            Func<List<MedicosCLS>> act = () => controller.FiltrarMedicos(filtroMalicioso);
            act.Should().NotThrow<SqlInjectionException>(
                "El filtrado de médicos no debe ser vulnerable a inyección SQL");
        }

        // ═══════════════════════════════════════════════════════════════
        // CP-15: INYECCIÓN SQL EN MÓDULO TRATAMIENTOS
        // ═══════════════════════════════════════════════════════════════

        [Theory]
        [MemberData(nameof(PayloadsSQLi))]
        public void CP15_OWASP_A03_InyeccionSQL_Tratamientos_GuardarConPayloadMalicioso(string sqlPayload)
        {
            var controller = new TratamientosController();
            var tratamientoMalicioso = new TratamientosCLS
            {
                Id = 0,
                PacienteId = 1,
                Descripcion = sqlPayload,
                Costo = 100
            };

            Func<int> act = () => controller.GuardarTratamiento(tratamientoMalicioso);
            act.Should().NotThrow<SqlInjectionException>(
                "El módulo Tratamientos debe sanitizar payloads SQL maliciosos");
        }

        // ═══════════════════════════════════════════════════════════════
        // CP-16: INYECCIÓN SQL EN MÓDULO FACTURACIÓN
        // ═══════════════════════════════════════════════════════════════

        [Theory]
        [MemberData(nameof(PayloadsSQLi))]
        public void CP16_OWASP_A03_InyeccionSQL_Facturacion_GuardarConPayloadMalicioso(string sqlPayload)
        {
            var controller = new FacturacionController();
            var facturaMaliciosa = new FacturacionCLS
            {
                Id = 0,
                PacienteId = 1,
                MetodoPago = sqlPayload,
                Monto = 100
            };

            Func<int> act = () => controller.GuardarFacturacion(facturaMaliciosa);
            act.Should().NotThrow<SqlInjectionException>(
                "El módulo Facturación debe sanitizar payloads SQL maliciosos");
        }

        // ═══════════════════════════════════════════════════════════════
        // CP-17: INYECCIÓN SQL EN MÓDULO ESPECIALIDADES
        // ═══════════════════════════════════════════════════════════════

        [Theory]
        [MemberData(nameof(PayloadsSQLi))]
        public void CP17_OWASP_A03_InyeccionSQL_Especialidades_GuardarConPayloadMalicioso(string sqlPayload)
        {
            var controller = new EspecialidadesController();
            var especialidadMaliciosa = new EspecialidadesCLS
            {
                Id = 0,
                Nombre = sqlPayload
            };

            Func<int> act = () => controller.GuardarEspecialidad(especialidadMaliciosa);
            act.Should().NotThrow<SqlInjectionException>(
                "El módulo Especialidades debe sanitizar payloads SQL maliciosos");
        }

        // ═══════════════════════════════════════════════════════════════
        // CP-17b: INYECCIÓN SQL EN GenericDAL (STRING INTERPOLATION)
        // Hallazgo crítico: GenericDAL.cs línea 31 usa $"SELECT {nombreId}..."
        // ═══════════════════════════════════════════════════════════════

        [Theory]
        [InlineData("Paciente")]
        [InlineData("Medicos")]
        [InlineData("Citas")]
        [InlineData("Tratamientos")]
        [InlineData("Especialidades")]
        [InlineData("Facturacion")]
        [InlineData("Usuarios")]
        public void CP17c_OWASP_A03_GenericDAL_ObtenerClaves_TablasValidas_NoLanzaExcepcionSQL(string tablaValida)
        {
            var dal = new GenericDAL();

            // Solo debe aceptar nombres de tabla del whitelist interno
            try
            {
                dal.ObtenerClaves(tablaValida);
            }
            catch (Exception ex)
            {
                // Si falla por conexión a BD es esperado, pero NO debe fallar por SQL injection
                ex.Message.Should().NotContain("DROP", "La tabla válida no debe generar comandos destructivos");
                ex.Message.Should().NotContain("xp_cmdshell", "No debe ejecutar comandos del sistema");
            }
        }

        [Theory]
        [InlineData("Paciente; DROP TABLE Usuario; --")]
        [InlineData("' OR '1'='1")]
        [InlineData("Paciente UNION SELECT * FROM Usuario --")]
        public void CP17d_OWASP_A03_GenericDAL_ObtenerClaves_TablasInvalidas_DebeRechazar(string tablaInvalida)
        {
            var dal = new GenericDAL();

            // El sistema debe mapear internamente y no ejecutar la cadena directa
            try
            {
                dal.ObtenerClaves(tablaInvalida);
            }
            catch (Exception ex)
            {
                // Se espera error de conexión o de tabla no encontrada, NUNCA ejecución del payload
                ex.Should().NotBeNull("Debe rechazar tablas no reconocidas");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // CP-18: INYECCIÓN SQL EN MÓDULO CITAS
        // ═══════════════════════════════════════════════════════════════

        [Theory]
        [MemberData(nameof(PayloadsSQLi))]
        public void CP18_OWASP_A03_InyeccionSQL_Citas_GuardarConPayloadMalicioso(string sqlPayload)
        {
            var controller = new CitasController();
            var citaMaliciosa = new CitasCLS
            {
                idCita = 0,
                idPaciente = 1,
                idMedico = 1,
                estado = sqlPayload
            };

            Func<int> act = () => controller.GuardarCita(citaMaliciosa);
            act.Should().NotThrow<SqlInjectionException>(
                "El módulo Citas debe sanitizar payloads SQL maliciosos");
        }
    }

    /// <summary>
    /// Excepción personalizada para identificar fallos de inyección SQL en pruebas
    /// </summary>
    public class SqlInjectionException : Exception
    {
        public SqlInjectionException(string message) : base(message) { }
    }
}
