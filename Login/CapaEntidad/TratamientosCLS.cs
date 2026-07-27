using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class TratamientosCLS
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Costo { get; set; }
    }
}
