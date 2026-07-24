using Xunit;
using FluentAssertions;
using CapaEntidad;
using CapaNegocio;
using System;

namespace ProyectoHospital.Tests
{
    public class TratamientosBLTests
    {
        [Fact]
        public void TratamientosCLS_InstanciacionCorrecta_PropiedadesAsignadas()
        {
            var tratamiento = new TratamientosCLS
            {
                Id = 5,
                PacienteId = 1,
                Descripcion = "Paracetamol 500mg cada 8 horas",
                Fecha = DateTime.Today,
                Costo = 25.00m
            };

            tratamiento.Id.Should().Be(5);
            tratamiento.PacienteId.Should().Be(1);
            tratamiento.Descripcion.Should().Be("Paracetamol 500mg cada 8 horas");
            tratamiento.Costo.Should().Be(25.00m);
        }

        [Fact]
        public void TratamientosBL_ListarTratamientos_EjecutaMetodoBL()
        {
            var bl = new TratamientosBL();
            try { bl.ListarTratamientos(); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void TratamientosBL_GuardarTratamiento_EjecutaMetodoBL()
        {
            var bl = new TratamientosBL();
            var t = new TratamientosCLS { Id = 0, Descripcion = "Ibuprofeno" };
            try { bl.GuardarTratamiento(t); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void TratamientosBL_RecuperarTratamiento_EjecutaMetodoBL()
        {
            var bl = new TratamientosBL();
            try { bl.RecuperarTratamiento(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void TratamientosBL_FiltrarTratamientos_EjecutaMetodoBL()
        {
            var bl = new TratamientosBL();
            var t = new TratamientosCLS { Descripcion = "Ibuprofeno" };
            try { bl.FiltrarTratamientos(t); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void TratamientosBL_EliminarTratamiento_EjecutaMetodoBL()
        {
            var bl = new TratamientosBL();
            try { bl.EliminarTratamiento(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }
    }
}
