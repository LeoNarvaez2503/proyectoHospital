using CapaEntidad;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class MedicosDAL : CadenaDAL
    {
        public List<MedicosCLS> ListarMedicos()
        {
            List<MedicosCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarMedicos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<MedicosCLS>();
                        while (dr.Read())
                        {
                            MedicosCLS medico = new MedicosCLS();
                            medico.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            medico.Nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                            medico.Apellido = dr.IsDBNull(2) ? "" : dr.GetString(2);
                            medico.EspecialidadId = dr.IsDBNull(3) ? -1 : dr.GetInt32(3);
                            medico.Telefono = dr.IsDBNull(4) ? "" : dr.GetString(4);
                            medico.Email = dr.IsDBNull(5) ? "" : dr.GetString(5);
                            medico.EspecialidadNombre = dr.FieldCount > 6 && !dr.IsDBNull(6) ? dr.GetString(6) : "";
                            lista.Add(medico);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al listar médicos: " + e.Message);
                }
                return lista;
            }
        }
        public MedicosCLS RecuperarMedico(int id)
        {
            MedicosCLS medico = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarMedicos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            medico = new MedicosCLS();
                            medico.Id = dr.GetInt32(0);
                            medico.Nombre = dr.GetString(1);
                            medico.Apellido = dr.GetString(2);
                            medico.EspecialidadId = dr.GetInt32(3);
                            medico.Telefono = dr.GetString(4);
                            medico.Email = dr.GetString(5);
                        }
                    }
                }
                catch (Exception e)
                {
                    medico = null;
                    throw new Exception("Error al recuperar médico: " + e.Message);
                }
                return medico;
            }
        }
        public int GuardarMedico(MedicosCLS medico)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarMedicos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", medico.Id);
                        cmd.Parameters.AddWithValue("@nombre", medico.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", medico.Apellido);
                        cmd.Parameters.AddWithValue("@especialidadId", medico.EspecialidadId);
                        cmd.Parameters.AddWithValue("@telefono", medico.Telefono);
                        cmd.Parameters.AddWithValue("@email", medico.Email);

                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al guardar médico: " + e.Message);
                }
            }
        }
        public int EliminarMedico(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarMedico", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al eliminar médico: " + e.Message);
                }
                return 1;
            }
        }
        public List<MedicosCLS> FiltrarMedicos(MedicosCLS filtro)
        {
            List<MedicosCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarMedicos", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", filtro.Nombre ?? "");
                        cmd.Parameters.AddWithValue("@apellido", filtro.Apellido ?? "");
                        cmd.Parameters.AddWithValue("@especialidadId", filtro.EspecialidadId);
                        cmd.Parameters.AddWithValue("@telefono", filtro.Telefono ?? "");
                        cmd.Parameters.AddWithValue("@email", filtro.Email ?? "");

                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<MedicosCLS>();
                        while (dr.Read())
                        {
                            MedicosCLS medico = new MedicosCLS();
                            medico.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            medico.Nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                            medico.Apellido = dr.IsDBNull(2) ? "" : dr.GetString(2);
                            medico.EspecialidadId = dr.IsDBNull(3) ? -1 : dr.GetInt32(3);
                            medico.Telefono = dr.IsDBNull(4) ? "" : dr.GetString(4);
                            medico.Email = dr.IsDBNull(5) ? "" : dr.GetString(5);
                            medico.EspecialidadNombre = dr.FieldCount > 6 && !dr.IsDBNull(6) ? dr.GetString(6) : "";
                            lista.Add(medico);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al filtrar médicos: " + e.Message);
                }
                return lista;
            }
        }
    }
}

