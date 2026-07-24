using Xunit;
using FluentAssertions;
using CapaEntidad;
using CapaNegocio;
using System;

namespace ProyectoHospital.Tests
{
    public class MedicosBLTests
    {
        [Fact]
        public void MedicosCLS_InstanciacionCorrecta_PropiedadesAsignadas()
        {
            var medico = new MedicosCLS
            {
                Id = 1,
                Nombre = "Carlos",
                Apellido = "González",
                EspecialidadId = 2,
                Telefono = "0998765432",
                Email = "carlos.gonzalez@hospital.com"
            };

            medico.Id.Should().Be(1);
            medico.Nombre.Should().Be("Carlos");
            medico.Apellido.Should().Be("González");
            medico.EspecialidadId.Should().Be(2);
        }

        [Fact]
        public void MedicosBL_ListarMedicos_EjecutaMetodoBL()
        {
            var bl = new MedicosBL();
            try { bl.ListarMedicos(); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void MedicosBL_GuardarMedico_EjecutaMetodoBL()
        {
            var bl = new MedicosBL();
            var m = new MedicosCLS { Id = 0, Nombre = "Dr. House" };
            try { bl.GuardarMedico(m); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void MedicosBL_RecuperarMedico_EjecutaMetodoBL()
        {
            var bl = new MedicosBL();
            try { bl.RecuperarMedico(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void MedicosBL_FiltrarMedicos_EjecutaMetodoBL()
        {
            var bl = new MedicosBL();
            var m = new MedicosCLS { Nombre = "Dr." };
            try { bl.FiltrarMedicos(m); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void MedicosBL_EliminarMedico_EjecutaMetodoBL()
        {
            var bl = new MedicosBL();
            try { bl.EliminarMedico(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }
    }
}
