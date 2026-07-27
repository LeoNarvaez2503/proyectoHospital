using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class EspecialidadesBL
    {
        public List<EspecialidadesCLS> ListarEspecialidades()
        {
            EspecialidadesDAL objEspecialidadesDAL = new EspecialidadesDAL();
            return objEspecialidadesDAL.ListarEspecialidades();
        }

        public int GuardarEspecialidad(EspecialidadesCLS objEspecialidadCLS)
        {
            EspecialidadesDAL objEspecialidadesDAL = new EspecialidadesDAL();
            return objEspecialidadesDAL.GuardarEspecialidad(objEspecialidadCLS);
        }

        public int EliminarEspecialidad(int id)
        {
            EspecialidadesDAL objEspecialidadesDAL = new EspecialidadesDAL();
            return objEspecialidadesDAL.EliminarEspecialidad(id);
        }

        public EspecialidadesCLS RecuperarEspecialidad(int id)
        {
            EspecialidadesDAL objEspecialidadesDAL = new EspecialidadesDAL();
            return objEspecialidadesDAL.RecuperarEspecialidad(id);
        }

        public List<EspecialidadesCLS> FiltrarEspecialidades(EspecialidadesCLS filtro)
        {
            EspecialidadesDAL objEspecialidadesDAL = new EspecialidadesDAL();
            return objEspecialidadesDAL.FiltrarEspecialidades(filtro);
        }
    }
}
