using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CitasCLS
    {
        public int idCita { get; set; }
        public int idPaciente { get; set; }
        public int idMedico { get; set; }
        public string PacienteNombre { get; set; }
        public string MedicoNombre { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
    }
}
