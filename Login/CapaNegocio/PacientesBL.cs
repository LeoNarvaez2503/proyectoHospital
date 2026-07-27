using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class PacientesBL
    {
        public List<PacienteCLS> ListarPacientes()
        {
            PacienteDAL objPacientesDAL = new PacienteDAL();
            return objPacientesDAL.ListarPacientes();
        }

        public int GuardarPaciente(PacienteCLS objPacienteCLS)
        {
            PacienteDAL objPacientesDAL = new PacienteDAL();
            return objPacientesDAL.GuardarPaciente(objPacienteCLS);
        }

        public int EliminarPaciente(int id)
        {
            PacienteDAL objPacientesDAL = new PacienteDAL();
            return objPacientesDAL.EliminarPaciente(id);
        }

        public PacienteCLS RecuperarPaciente(int id)
        {
            PacienteDAL objPacientesDAL = new PacienteDAL();
            return objPacientesDAL.RecuperarPaciente(id);
        }

        public List<PacienteCLS> FiltrarPacientes(PacienteCLS filtro)
        {
            PacienteDAL objPacientesDAL = new PacienteDAL();
            return objPacientesDAL.FiltrarPacientes(filtro);
        }
    }
}
