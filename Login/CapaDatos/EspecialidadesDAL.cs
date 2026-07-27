using CapaEntidad;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class EspecialidadesDAL : CadenaDAL
    {
        public List<EspecialidadesCLS> ListarEspecialidades()
        {
            List<EspecialidadesCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarEspecialidades", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<EspecialidadesCLS>();
                        while (dr.Read())
                        {
                            EspecialidadesCLS especialidad = new EspecialidadesCLS();
                            especialidad.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            especialidad.Nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                            lista.Add(especialidad);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al listar especialidades: " + e.Message);
                }
                return lista;
            }
        }
        public EspecialidadesCLS RecuperarEspecialidad(int id)
        {
            EspecialidadesCLS especialidad = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarEspecialidades", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            especialidad = new EspecialidadesCLS();
                            especialidad.Id = dr.GetInt32(0);
                            especialidad.Nombre = dr.GetString(1);
                        }
                    }
                }
                catch (Exception e)
                {
                    especialidad = null;
                    throw new Exception("Error al recuperar especialidad: " + e.Message);
                }
                return especialidad;
            }
        }
        public int GuardarEspecialidad(EspecialidadesCLS especialidad)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarEspecialidades", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", especialidad.Id);
                        cmd.Parameters.AddWithValue("@nombre", especialidad.Nombre);

                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al guardar especialidad: " + e.Message);
                }
            }
        }
        public int EliminarEspecialidad(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarEspecialidad", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al eliminar especialidad: " + e.Message);
                }
                return 1;
            }
        }
        public List<EspecialidadesCLS> FiltrarEspecialidades(EspecialidadesCLS filtro)
        {
            List<EspecialidadesCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarEspecialidades", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", filtro.Nombre ?? "");

                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<EspecialidadesCLS>();
                        while (dr.Read())
                        {
                            EspecialidadesCLS especialidad = new EspecialidadesCLS();
                            especialidad.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            especialidad.Nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                            lista.Add(especialidad);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al filtrar especialidades: " + e.Message);
                }
                return lista;
            }
        }
    }
}
