using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class GenericDAL : CadenaDAL
    {
        public List<ForaneaCLS> ObtenerClaves(string tabla)
        {
            List<ForaneaCLS> lista = new List<ForaneaCLS>();
            string nombreTabla = "";
            string nombreId = "Id";
            string selectFields = "Id, Nombre";

            if (tabla.Equals("Pacientes", StringComparison.OrdinalIgnoreCase)) { nombreTabla = "Paciente"; selectFields = "Id, (Nombre + ' ' + Apellido) as Nombre"; }
            else if (tabla.Equals("Medicos", StringComparison.OrdinalIgnoreCase)) { nombreTabla = "Medico"; selectFields = "Id, (Nombre + ' ' + Apellido) as Nombre"; }
            else if (tabla.Equals("Especialidades", StringComparison.OrdinalIgnoreCase)) { nombreTabla = "Especialidad"; selectFields = "Id, Nombre"; }
            else { throw new ArgumentException("Tabla no permitida para foraneas: " + tabla); }

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    string query = $"SELECT {selectFields} FROM {nombreTabla} ORDER BY {nombreId} ASC";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new ForaneaCLS { Id = dr.GetInt32(0), Descripcion = dr.GetString(1) });
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
