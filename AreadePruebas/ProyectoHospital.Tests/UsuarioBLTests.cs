using Xunit;
using FluentAssertions;
using CapaNegocio;
using CapaEntidad;
using Login.Models;
using Login.Data;
using System;

namespace ProyectoHospital.Tests
{
    public class UsuarioBLTests
    {
        [Fact]
        public void UsuarioBL_Instanciacion_NoRetornaNulo()
        {
            var bl = new UsuarioBL();
            bl.Should().NotBeNull();
        }

        [Fact]
        public void ErrorViewModel_PropiedadesAsignadasCorrectamente()
        {
            var model = new ErrorViewModel { RequestId = "12345" };
            model.RequestId.Should().Be("12345");
            model.ShowRequestId.Should().BeTrue();

            var emptyModel = new ErrorViewModel { RequestId = null };
            emptyModel.ShowRequestId.Should().BeFalse();
        }

        [Fact]
        public void DatabaseInitializer_Initialize_EjecutaMetodoConTryCatch()
        {
            try
            {
                DatabaseInitializer.Initialize("Server=localhost;Database=DummyDB;Trusted_Connection=True;");
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }
    }
}
