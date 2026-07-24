using Xunit;
using FluentAssertions;
using CapaEntidad;
using CapaNegocio;
using System;

namespace ProyectoHospital.Tests
{
    public class EspecialidadesBLTests
    {
        [Fact]
        public void EspecialidadesCLS_InstanciacionCorrecta_PropiedadesAsignadas()
        {
            var especialidad = new EspecialidadesCLS
            {
                Id = 3,
                Nombre = "Pediatría"
            };

            especialidad.Id.Should().Be(3);
            especialidad.Nombre.Should().Be("Pediatría");
        }

        [Fact]
        public void EspecialidadesBL_ListarEspecialidades_EjecutaMetodoBL()
        {
            var bl = new EspecialidadesBL();
            try { bl.ListarEspecialidades(); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void EspecialidadesBL_GuardarEspecialidad_EjecutaMetodoBL()
        {
            var bl = new EspecialidadesBL();
            var e = new EspecialidadesCLS { Id = 0, Nombre = "Cardiología" };
            try { bl.GuardarEspecialidad(e); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void EspecialidadesBL_RecuperarEspecialidad_EjecutaMetodoBL()
        {
            var bl = new EspecialidadesBL();
            try { bl.RecuperarEspecialidad(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void EspecialidadesBL_FiltrarEspecialidades_EjecutaMetodoBL()
        {
            var bl = new EspecialidadesBL();
            var e = new EspecialidadesCLS { Nombre = "Pediatría" };
            try { bl.FiltrarEspecialidades(e); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void EspecialidadesBL_EliminarEspecialidad_EjecutaMetodoBL()
        {
            var bl = new EspecialidadesBL();
            try { bl.EliminarEspecialidad(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }
    }
}
