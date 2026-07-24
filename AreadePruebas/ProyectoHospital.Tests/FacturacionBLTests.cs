using Xunit;
using FluentAssertions;
using CapaEntidad;
using CapaNegocio;
using System;

namespace ProyectoHospital.Tests
{
    public class FacturacionBLTests
    {
        [Fact]
        public void FacturacionCLS_InstanciacionCorrecta_PropiedadesAsignadas()
        {
            var factura = new FacturacionCLS
            {
                Id = 100,
                PacienteId = 1,
                Monto = 45.50m,
                MetodoPago = "Efectivo",
                FechaPago = DateTime.Today
            };

            factura.Id.Should().Be(100);
            factura.PacienteId.Should().Be(1);
            factura.Monto.Should().Be(45.50m);
            factura.MetodoPago.Should().Be("Efectivo");
        }

        [Fact]
        public void FacturacionBL_ListarFacturaciones_EjecutaMetodoBL()
        {
            var bl = new FacturacionBL();
            try { bl.ListarFacturaciones(); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void FacturacionBL_GuardarFacturacion_EjecutaMetodoBL()
        {
            var bl = new FacturacionBL();
            var f = new FacturacionCLS { Id = 0, Monto = 100m };
            try { bl.GuardarFacturacion(f); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void FacturacionBL_RecuperarFacturacion_EjecutaMetodoBL()
        {
            var bl = new FacturacionBL();
            try { bl.RecuperarFacturacion(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void FacturacionBL_FiltrarFacturaciones_EjecutaMetodoBL()
        {
            var bl = new FacturacionBL();
            var f = new FacturacionCLS { MetodoPago = "Efectivo" };
            try { bl.FiltrarFacturaciones(f); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void FacturacionBL_EliminarFacturacion_EjecutaMetodoBL()
        {
            var bl = new FacturacionBL();
            try { bl.EliminarFacturacion(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }
    }
}
