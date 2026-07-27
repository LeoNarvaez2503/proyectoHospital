using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CitasBL
    {
        public List<CitasCLS> ListarCitas()
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.ListarCitas();
        }
        public int GuardarCita(CitasCLS objCitasCLS)
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.GuardarCitas(objCitasCLS);
        }
        public int EliminarCita(int id)
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.EliminarCitas(id);
        }
        public CitasCLS RecuperarCitas(int idCita)
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.RecuperarCitas(idCita);
        }
        public List<CitasCLS> FiltrarCitas(CitasCLS objCitasCLS)
        {
            CitasDAL objCitasDAL = new CitasDAL();
            return objCitasDAL.FiltrarCitas(objCitasCLS);
        }
    }
}
