using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class GenericDAL : CadenaDAL
    {
        public List<int> ObtenerClaves(string tabla)
        {
            List<int> lista = new List<int>();
            string nombreTabla = tabla;
            string nombreId = "Id";

            if (tabla.Equals("Pacientes", StringComparison.OrdinalIgnoreCase)) nombreTabla = "Paciente";
            else if (tabla.Equals("Medicos", StringComparison.OrdinalIgnoreCase)) nombreTabla = "Medico";
            else if (tabla.Equals("Citas", StringComparison.OrdinalIgnoreCase)) { nombreTabla = "Cita"; nombreId = "idCita"; }
            else if (tabla.Equals("Tratamientos", StringComparison.OrdinalIgnoreCase)) nombreTabla = "Tratamiento";
            else if (tabla.Equals("Especialidades", StringComparison.OrdinalIgnoreCase)) nombreTabla = "Especialidad";
            else if (tabla.Equals("Facturacion", StringComparison.OrdinalIgnoreCase)) nombreTabla = "Facturacion";
            else if (tabla.Equals("Usuarios", StringComparison.OrdinalIgnoreCase)) { nombreTabla = "Usuario"; nombreId = "idUsuario"; }

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    string query = $"SELECT {nombreId} FROM {nombreTabla} ORDER BY {nombreId} ASC";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(dr.GetInt32(0));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener claves: " + e.Message);
                }
                return lista;
            }
        }

        public List<ElementoComboCLS> ObtenerCombo(string tabla)
        {
            List<ElementoComboCLS> lista = new List<ElementoComboCLS>();
            string query = "";

            if (tabla.Equals("Pacientes", StringComparison.OrdinalIgnoreCase))
            {
                query = "SELECT Id, ISNULL(Nombre + ' ' + Apellido, 'Paciente #' + CAST(Id AS NVARCHAR)) FROM Paciente ORDER BY Id ASC";
            }
            else if (tabla.Equals("Medicos", StringComparison.OrdinalIgnoreCase))
            {
                query = "SELECT Id, ISNULL(Nombre + ' ' + Apellido, 'Médico #' + CAST(Id AS NVARCHAR)) FROM Medico ORDER BY Id ASC";
            }
            else if (tabla.Equals("Especialidades", StringComparison.OrdinalIgnoreCase))
            {
                query = "SELECT Id, Nombre FROM Especialidad ORDER BY Id ASC";
            }
            else
            {
                string nombreTabla = tabla;
                string nombreId = "Id";
                if (tabla.Equals("Citas", StringComparison.OrdinalIgnoreCase)) { nombreTabla = "Cita"; nombreId = "idCita"; }
                else if (tabla.Equals("Tratamientos", StringComparison.OrdinalIgnoreCase)) nombreTabla = "Tratamiento";
                else if (tabla.Equals("Facturacion", StringComparison.OrdinalIgnoreCase)) nombreTabla = "Facturacion";
                else if (tabla.Equals("Usuarios", StringComparison.OrdinalIgnoreCase)) { nombreTabla = "Usuario"; nombreId = "idUsuario"; }

                query = $"SELECT {nombreId}, CAST({nombreId} AS NVARCHAR) FROM {nombreTabla} ORDER BY {nombreId} ASC";
            }

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new ElementoComboCLS
                                {
                                    id = dr.GetInt32(0),
                                    texto = dr.IsDBNull(1) ? dr.GetInt32(0).ToString() : dr.GetString(1)
                                });
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener combo: " + e.Message);
                }
                return lista;
            }
        }
    }
}
