using CapaEntidad;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class PacienteDAL : CadenaDAL
    {
        public List<PacienteCLS> ListarPacientes()
        {
            List<PacienteCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarPacientes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<PacienteCLS>();
                        while (dr.Read())
                        {
                            PacienteCLS paciente = new PacienteCLS();
                            paciente.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            paciente.Nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                            paciente.Apellido = dr.IsDBNull(2) ? "" : dr.GetString(2);
                            paciente.FechaNacimiento = dr.GetDateTime(3);
                            paciente.Telefono = dr.IsDBNull(4) ? "" : dr.GetString(4);
                            paciente.Email = dr.IsDBNull(5) ? "" : dr.GetString(5);
                            paciente.Direccion = dr.IsDBNull(6) ? "" : dr.GetString(6);
                            lista.Add(paciente);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al listar pacientes: " + e.Message);
                }
                return lista;
            }
        }
        public PacienteCLS RecuperarPaciente(int id)
        {
            PacienteCLS paciente = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarPacientes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            paciente = new PacienteCLS();
                            paciente.Id = dr.GetInt32(0);
                            paciente.Nombre = dr.GetString(1);
                            paciente.Apellido = dr.GetString(2);
                            paciente.FechaNacimiento = dr.GetDateTime(3);
                            paciente.Telefono = dr.GetString(4);
                            paciente.Email = dr.GetString(5);
                            paciente.Direccion = dr.GetString(6);
                        }
                    }
                }
                catch (Exception e)
                {
                    paciente = null;
                    throw new Exception("Error al recuperar paciente: " + e.Message);
                }
                return paciente;
            }
        }
        public int GuardarPaciente(PacienteCLS paciente)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarPacientes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", paciente.Id);
                        cmd.Parameters.AddWithValue("@nombre", paciente.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", paciente.Apellido);
                        cmd.Parameters.AddWithValue("@fechaNacimiento", paciente.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@telefono", paciente.Telefono);
                        cmd.Parameters.AddWithValue("@email", paciente.Email);
                        cmd.Parameters.AddWithValue("@direccion", paciente.Direccion);

                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al guardar paciente: " + e.Message);
                }
            }
        }
        public int EliminarPaciente(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarPaciente", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al eliminar paciente: " + e.Message);
                }
                return 1;
            }
        }
        public List<PacienteCLS> FiltrarPacientes(PacienteCLS filtro)
        {
            List<PacienteCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarPacientes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", filtro.Nombre == null ? "" : filtro.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", filtro.Apellido == null ? "" : filtro.Apellido);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", filtro.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Telefono", filtro.Telefono == null ? "" : filtro.Telefono);
                        cmd.Parameters.AddWithValue("@Email", filtro.Email == null ? "" : filtro.Email);
                        cmd.Parameters.AddWithValue("@Direccion", filtro.Direccion == null ? "" : filtro.Direccion);

                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<PacienteCLS>();
                        while (dr.Read())
                        {
                            PacienteCLS paciente = new PacienteCLS();
                            paciente.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            paciente.Nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                            paciente.Apellido = dr.IsDBNull(2) ? "" : dr.GetString(2);
                            paciente.FechaNacimiento = dr.IsDBNull(3) ? System.DateTime.MinValue : dr.GetDateTime(3);
                            paciente.Telefono = dr.IsDBNull(4) ? "" : dr.GetString(4);
                            paciente.Email = dr.IsDBNull(5) ? "" : dr.GetString(5);
                            paciente.Direccion = dr.IsDBNull(6) ? "" : dr.GetString(6);
                            lista.Add(paciente);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al filtrar pacientes: " + e.Message);
                }
                return lista;
            }
        }
    }
}
