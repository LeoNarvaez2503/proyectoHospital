using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
