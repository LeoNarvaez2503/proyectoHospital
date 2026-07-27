using CapaEntidad;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class FacturacionDAL : CadenaDAL
    {
        public List<FacturacionCLS> ListarFacturaciones()
        {
            List<FacturacionCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarFacturacion", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<FacturacionCLS>();
                        while (dr.Read())
                        {
                            FacturacionCLS factura = new FacturacionCLS();
                            factura.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            factura.PacienteId = dr.IsDBNull(1) ? -1 : dr.GetInt32(1);
                            factura.Monto = dr.IsDBNull(2) ? 0 : dr.GetDecimal(2);
                            factura.MetodoPago = dr.IsDBNull(3) ? "" : dr.GetString(3);
                            factura.FechaPago = dr.GetDateTime(4);
                            lista.Add(factura);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al listar facturaciones: " + e.Message);
                }
                return lista;
            }
        }
        public FacturacionCLS RecuperarFacturacion(int id)
        {
            FacturacionCLS factura = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarFacturacion", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            factura = new FacturacionCLS();
                            factura.Id = dr.GetInt32(0);
                            factura.PacienteId = dr.GetInt32(1);
                            factura.Monto = dr.GetDecimal(2);
                            factura.MetodoPago = dr.GetString(3);
                            factura.FechaPago = dr.GetDateTime(4);
                        }
                    }
                }
                catch (Exception e)
                {
                    factura = null;
                    throw new Exception("Error al recuperar facturación: " + e.Message);
                }
                return factura;
            }
        }
        public int GuardarFacturacion(FacturacionCLS factura)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarFacturacion", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", factura.Id);
                        cmd.Parameters.AddWithValue("@pacienteId", factura.PacienteId);
                        cmd.Parameters.AddWithValue("@monto", factura.Monto);
                        cmd.Parameters.AddWithValue("@metodoPago", factura.MetodoPago);
                        cmd.Parameters.AddWithValue("@fechaPago", factura.FechaPago);

                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al guardar facturación: " + e.Message);
                }
            }
        }
        public int EliminarFacturacion(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarFacturacion", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return -1;
                    throw new Exception("Error al eliminar facturación: " + e.Message);
                }
                return 1;
            }
        }
        public List<FacturacionCLS> FiltrarFacturaciones(FacturacionCLS filtro)
        {
            List<FacturacionCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarFacturacion", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pacienteId", filtro.PacienteId);
                        cmd.Parameters.AddWithValue("@monto", filtro.Monto);
                        cmd.Parameters.AddWithValue("@metodoPago", filtro.MetodoPago ?? "");
                        cmd.Parameters.AddWithValue("@fechaPago", filtro.FechaPago);

                        SqlDataReader dr = cmd.ExecuteReader();
                        lista = new List<FacturacionCLS>();
                        while (dr.Read())
                        {
                            FacturacionCLS factura = new FacturacionCLS();
                            factura.Id = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                            factura.PacienteId = dr.IsDBNull(1) ? -1 : dr.GetInt32(1);
                            factura.Monto = dr.IsDBNull(2) ? 0 : dr.GetDecimal(2);
                            factura.MetodoPago = dr.IsDBNull(3) ? "" : dr.GetString(3);
                            factura.FechaPago = dr.IsDBNull(4) ? System.DateTime.MinValue : dr.GetDateTime(4);
                            lista.Add(factura);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al filtrar facturaciones: " + e.Message);
                }
                return lista;
            }
        }
    }
}

