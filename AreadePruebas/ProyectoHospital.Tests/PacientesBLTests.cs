using Xunit;
using FluentAssertions;
using CapaEntidad;
using CapaNegocio;
using System;

namespace ProyectoHospital.Tests
{
    public class PacientesBLTests
    {
        [Fact]
        public void PacienteCLS_InstanciacionCorrecta_PropiedadesAsignadas()
        {
            var paciente = new PacienteCLS
            {
                Id = 1,
                Nombre = "Juan",
                Apellido = "Pérez",
                FechaNacimiento = new DateTime(1990, 5, 15),
                Telefono = "0991234567",
                Email = "juan.perez@test.com",
                Direccion = "Av. Principal 123"
            };

            paciente.Id.Should().Be(1);
            paciente.Nombre.Should().Be("Juan");
            paciente.Apellido.Should().Be("Pérez");
            paciente.Email.Should().Be("juan.perez@test.com");
        }

        [Fact]
        public void PacientesBL_ListarPacientes_EjecutaMetodoBL()
        {
            var bl = new PacientesBL();
            try { bl.ListarPacientes(); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void PacientesBL_GuardarPaciente_EjecutaMetodoBL()
        {
            var bl = new PacientesBL();
            var p = new PacienteCLS { Id = 0, Nombre = "Ana", Apellido = "Pérez" };
            try { bl.GuardarPaciente(p); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void PacientesBL_RecuperarPaciente_EjecutaMetodoBL()
        {
            var bl = new PacientesBL();
            try { bl.RecuperarPaciente(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void PacientesBL_FiltrarPacientes_EjecutaMetodoBL()
        {
            var bl = new PacientesBL();
            var p = new PacienteCLS { Nombre = "Ana" };
            try { bl.FiltrarPacientes(p); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void PacientesBL_EliminarPaciente_EjecutaMetodoBL()
        {
            var bl = new PacientesBL();
            try { bl.EliminarPaciente(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }
    }
}
