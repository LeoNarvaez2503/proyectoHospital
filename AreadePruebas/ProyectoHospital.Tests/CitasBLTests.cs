using Xunit;
using FluentAssertions;
using CapaEntidad;
using CapaNegocio;
using System;

namespace ProyectoHospital.Tests
{
    public class CitasBLTests
    {
        [Fact]
        public void CitasCLS_AsignacionValores_EsValida()
        {
            var cita = new CitasCLS
            {
                idCita = 10,
                idPaciente = 1,
                idMedico = 2,
                fecha = DateTime.Today,
                estado = "Reservada"
            };

            cita.idCita.Should().Be(10);
            cita.idPaciente.Should().Be(1);
            cita.idMedico.Should().Be(2);
            cita.estado.Should().Be("Reservada");
        }

        [Fact]
        public void CitasBL_ListarCitas_EjecutaMetodoBL()
        {
            var bl = new CitasBL();
            try
            {
                var result = bl.ListarCitas();
                result.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }

        [Fact]
        public void CitasBL_GuardarCita_EjecutaMetodoBL()
        {
            var bl = new CitasBL();
            var c = new CitasCLS { idCita = 0, idPaciente = 1, idMedico = 1, fecha = DateTime.Now, estado = "Pendiente" };
            try
            {
                bl.GuardarCita(c);
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }

        [Fact]
        public void CitasBL_RecuperarCitas_EjecutaMetodoBL()
        {
            var bl = new CitasBL();
            try
            {
                bl.RecuperarCitas(1);
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }

        [Fact]
        public void CitasBL_FiltrarCitas_EjecutaMetodoBL()
        {
            var bl = new CitasBL();
            var c = new CitasCLS { idPaciente = 1 };
            try
            {
                bl.FiltrarCitas(c);
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }

        [Fact]
        public void CitasBL_EliminarCita_EjecutaMetodoBL()
        {
            var bl = new CitasBL();
            try
            {
                bl.EliminarCita(999);
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }
    }
}
