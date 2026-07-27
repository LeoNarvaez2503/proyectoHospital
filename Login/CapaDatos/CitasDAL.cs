using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CitasDAL : CadenaDAL
    {
        public List<CitasCLS> ListarCitas()
        {
            List<CitasCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarCitas", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<CitasCLS>();
                        while (dr.Read())
                        {
                            CitasCLS citas = new CitasCLS();
                            citas.idCita = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            citas.idPaciente = dr.IsDBNull(1) ? -1 : dr.GetInt32(1);
                            citas.idMedico = dr.IsDBNull(2) ? -1 : dr.GetInt32(2);
                            citas.fecha = dr.GetDateTime(3);
                            citas.estado = dr.IsDBNull(4) ? "" : dr.GetString(4);
                            citas.PacienteNombre = dr.FieldCount > 5 && !dr.IsDBNull(5) ? dr.GetString(5) : "";
                            citas.MedicoNombre = dr.FieldCount > 6 && !dr.IsDBNull(6) ? dr.GetString(6) : "";
                            lista.Add(citas);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al listar citas: " + e.Message);
                }
                return lista;
            }
        }
        public CitasCLS RecuperarCitas(int id)
        {
            CitasCLS cita = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarCitas", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        cita = new CitasCLS();

                        while (dr.Read())
                        {
                            cita.idCita = dr.GetInt32(0);
                            cita.idPaciente = dr.GetInt32(1);
                            cita.idMedico = dr.GetInt32(2);
                            cita.fecha = dr.GetDateTime(3);
                            cita.estado = dr.GetString(4);
                        }
                    }
                }
                catch (Exception e)
                {
                    cita = null;
                    throw new Exception("Error al listar citas: " + e.Message);
                }
                return cita;
            }
        }
        public int GuardarCitas(CitasCLS cita)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarCitas", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", cita.idCita);
                        cmd.Parameters.AddWithValue("@PacienteId", cita.idPaciente);
                        cmd.Parameters.AddWithValue("@MedicoID", cita.idMedico);
                        cmd.Parameters.AddWithValue("@FechaHora", cita.fecha);
                        cmd.Parameters.AddWithValue("@Estado", cita.estado);

                        return cmd.ExecuteNonQuery();

                    }
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }
        public int EliminarCitas(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarCita", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();

                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al listar citas: " + e.Message);
                }
                return 1;
            }
        }
        public List<CitasCLS> FiltrarCitas(CitasCLS cita)
        {
            List<CitasCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {

                int pacienteId = cita.idPaciente;
                int medicoId = cita.idMedico;
                DateTime fecha = cita.fecha;
                string estado = cita.estado;

                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarCitas", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteId", pacienteId);
                        cmd.Parameters.AddWithValue("@MedicoID", medicoId);
                        cmd.Parameters.AddWithValue("@FechaHora", fecha);
                        cmd.Parameters.AddWithValue("@Estado", estado == null ? "" : estado);
                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<CitasCLS>();
                        while (dr.Read())
                        {
                            CitasCLS citas = new CitasCLS();
                            citas.idCita = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            citas.idPaciente = dr.IsDBNull(1) ? -1 : dr.GetInt32(1);
                            citas.idMedico = dr.IsDBNull(2) ? -1 : dr.GetInt32(2);
                            citas.fecha = dr.IsDBNull(3) ? System.DateTime.MinValue : dr.GetDateTime(3);
                            citas.estado = dr.IsDBNull(4) ? "" : dr.GetString(4);
                            citas.PacienteNombre = dr.FieldCount > 5 && !dr.IsDBNull(5) ? dr.GetString(5) : "";
                            citas.MedicoNombre = dr.FieldCount > 6 && !dr.IsDBNull(6) ? dr.GetString(6) : "";
                            lista.Add(citas);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al listar citas: " + e.Message);
                }
                return lista;
            }
        }

    }
}

