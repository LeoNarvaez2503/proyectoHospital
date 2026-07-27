using CapaEntidad;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class TratamientosDAL : CadenaDAL
    {
        public List<TratamientosCLS> ListarTratamientos()
        {
            List<TratamientosCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarTratamientos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<TratamientosCLS>();
                        while (dr.Read())
                        {
                            TratamientosCLS tratamiento = new TratamientosCLS();
                            tratamiento.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            tratamiento.PacienteId = dr.IsDBNull(1) ? -1 : dr.GetInt32(1);
                            tratamiento.Descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2);
                            tratamiento.Fecha = dr.GetDateTime(3);
                            tratamiento.Costo = dr.IsDBNull(4) ? 0 : dr.GetDecimal(4);
                            tratamiento.PacienteNombre = dr.FieldCount > 5 && !dr.IsDBNull(5) ? dr.GetString(5) : "";
                            lista.Add(tratamiento);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al listar tratamientos: " + e.Message);
                }
                return lista;
            }
        }
        public TratamientosCLS RecuperarTratamiento(int id)
        {
            TratamientosCLS tratamiento = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarTratamientos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            tratamiento = new TratamientosCLS();
                            tratamiento.Id = dr.GetInt32(0);
                            tratamiento.PacienteId = dr.GetInt32(1);
                            tratamiento.Descripcion = dr.GetString(2);
                            tratamiento.Fecha = dr.GetDateTime(3);
                            tratamiento.Costo = dr.GetDecimal(4);
                        }
                    }
                }
                catch (Exception e)
                {
                    tratamiento = null;
                    throw new Exception("Error al recuperar tratamiento: " + e.Message);
                }
                return tratamiento;
            }
        }
        public int GuardarTratamiento(TratamientosCLS tratamiento)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarTratamientos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", tratamiento.Id);
                        cmd.Parameters.AddWithValue("@pacienteId", tratamiento.PacienteId);
                        cmd.Parameters.AddWithValue("@descripcion", tratamiento.Descripcion);
                        cmd.Parameters.AddWithValue("@fecha", tratamiento.Fecha);
                        cmd.Parameters.AddWithValue("@costo", tratamiento.Costo);

                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al guardar tratamiento: " + e.Message);
                }
            }
        }
        public int EliminarTratamiento(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarTratamiento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al eliminar tratamiento: " + e.Message);
                }
                return 1;
            }
        }
        public List<TratamientosCLS> FiltrarTratamientos(TratamientosCLS filtro)
        {
            List<TratamientosCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarTratamientos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pacienteId", filtro.PacienteId);
                        cmd.Parameters.AddWithValue("@descripcion", filtro.Descripcion ?? "");
                        cmd.Parameters.AddWithValue("@fecha", filtro.Fecha);

                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<TratamientosCLS>();
                        while (dr.Read())
                        {
                            TratamientosCLS tratamiento = new TratamientosCLS();
                            tratamiento.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            tratamiento.PacienteId = dr.IsDBNull(1) ? -1 : dr.GetInt32(1);
                            tratamiento.Descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2);
                            tratamiento.Fecha = dr.IsDBNull(3) ? System.DateTime.MinValue : dr.GetDateTime(3);
                            tratamiento.Costo = dr.IsDBNull(4) ? 0 : dr.GetDecimal(4);
                            tratamiento.PacienteNombre = dr.FieldCount > 5 && !dr.IsDBNull(5) ? dr.GetString(5) : "";
                            lista.Add(tratamiento);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al filtrar tratamientos: " + e.Message);
                }
                return lista;
            }
        }
    }
}

