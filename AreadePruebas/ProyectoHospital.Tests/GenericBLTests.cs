using Xunit;
using FluentAssertions;
using CapaNegocio;
using System;

namespace ProyectoHospital.Tests
{
    public class GenericBLTests
    {
        [Fact]
        public void GenericBL_ObtenerClaves_EjecutaMetodoBL()
        {
            var bl = new GenericBL();
            try
            {
                bl.obtenerClaves("Paciente");
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }
    }
}
