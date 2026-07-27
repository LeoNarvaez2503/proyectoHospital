using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class MedicosCLS
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int EspecialidadId { get; set; }
        public string EspecialidadNombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
