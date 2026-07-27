using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class TratamientosBL
    {
        public List<TratamientosCLS> ListarTratamientos()
        {
            TratamientosDAL objTratamientosDAL = new TratamientosDAL();
            return objTratamientosDAL.ListarTratamientos();
        }

        public int GuardarTratamiento(TratamientosCLS objTratamientoCLS)
        {
            TratamientosDAL objTratamientosDAL = new TratamientosDAL();
            return objTratamientosDAL.GuardarTratamiento(objTratamientoCLS);
        }

        public int EliminarTratamiento(int id)
        {
            TratamientosDAL objTratamientosDAL = new TratamientosDAL();
            return objTratamientosDAL.EliminarTratamiento(id);
        }

        public TratamientosCLS RecuperarTratamiento(int id)
        {
            TratamientosDAL objTratamientosDAL = new TratamientosDAL();
            return objTratamientosDAL.RecuperarTratamiento(id);
        }

        public List<TratamientosCLS> FiltrarTratamientos(TratamientosCLS filtro)
        {
            TratamientosDAL objTratamientosDAL = new TratamientosDAL();
            return objTratamientosDAL.FiltrarTratamientos(filtro);
        }
    }
}
