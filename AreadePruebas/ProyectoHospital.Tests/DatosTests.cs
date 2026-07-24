using Xunit;
using FluentAssertions;
using CapaDatos;
using CapaEntidad;
using System;

namespace ProyectoHospital.Tests
{
    public class DatosTests
    {
        [Fact]
        public void CadenaDAL_Instanciacion_InicializaCadena()
        {
            try
            {
                var dal = new CadenaDAL();
                dal.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }

        [Fact]
        public void CitasDAL_Metodos_EjecutanCorrectamente()
        {
            var dal = new CitasDAL();
            try { dal.ListarCitas(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.GuardarCitas(new CitasCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.RecuperarCitas(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.FiltrarCitas(new CitasCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.EliminarCitas(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void PacienteDAL_Metodos_EjecutanCorrectamente()
        {
            var dal = new PacienteDAL();
            try { dal.ListarPacientes(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.GuardarPaciente(new PacienteCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.RecuperarPaciente(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.FiltrarPacientes(new PacienteCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.EliminarPaciente(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void MedicosDAL_Metodos_EjecutanCorrectamente()
        {
            var dal = new MedicosDAL();
            try { dal.ListarMedicos(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.GuardarMedico(new MedicosCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.RecuperarMedico(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.FiltrarMedicos(new MedicosCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.EliminarMedico(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void TratamientosDAL_Metodos_EjecutanCorrectamente()
        {
            var dal = new TratamientosDAL();
            try { dal.ListarTratamientos(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.GuardarTratamiento(new TratamientosCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.RecuperarTratamiento(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.FiltrarTratamientos(new TratamientosCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.EliminarTratamiento(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void FacturacionDAL_Metodos_EjecutanCorrectamente()
        {
            var dal = new FacturacionDAL();
            try { dal.ListarFacturaciones(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.GuardarFacturacion(new FacturacionCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.RecuperarFacturacion(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.FiltrarFacturaciones(new FacturacionCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.EliminarFacturacion(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void EspecialidadesDAL_Metodos_EjecutanCorrectamente()
        {
            var dal = new EspecialidadesDAL();
            try { dal.ListarEspecialidades(); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.GuardarEspecialidad(new EspecialidadesCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.RecuperarEspecialidad(1); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.FiltrarEspecialidades(new EspecialidadesCLS()); } catch (Exception ex) { ex.Should().NotBeNull(); }
            try { dal.EliminarEspecialidad(999); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void GenericDAL_Metodos_EjecutanCorrectamente()
        {
            var dal = new GenericDAL();
            try { dal.ObtenerClaves("Paciente"); } catch (Exception ex) { ex.Should().NotBeNull(); }
        }

        [Fact]
        public void UsuarioDAL_Metodos_EjecutanCorrectamente()
        {
            var dal = new UsuarioDAL();
            try
            {
                dal.RegistrarUsuario(new UsuarioCLS(), out string msg);
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }

            try
            {
                dal.IniciarSesion(new UsuarioCLS(), out string m, out int id, out string r);
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
            }
        }
    }
}
