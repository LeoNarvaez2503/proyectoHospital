using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MedicosBL
    {
        public List<MedicosCLS> ListarMedicos()
        {
            MedicosDAL objMedicosDAL = new MedicosDAL();
            return objMedicosDAL.ListarMedicos();
        }

        public int GuardarMedico(MedicosCLS objMedicoCLS)
        {
            MedicosDAL objMedicosDAL = new MedicosDAL();
            return objMedicosDAL.GuardarMedico(objMedicoCLS);
        }

        public int EliminarMedico(int id)
        {
            MedicosDAL objMedicosDAL = new MedicosDAL();
            return objMedicosDAL.EliminarMedico(id);
        }

        public MedicosCLS RecuperarMedico(int id)
        {
            MedicosDAL objMedicosDAL = new MedicosDAL();
            return objMedicosDAL.RecuperarMedico(id);
        }

        public List<MedicosCLS> FiltrarMedicos(MedicosCLS filtro)
        {
            MedicosDAL objMedicosDAL = new MedicosDAL();
            return objMedicosDAL.FiltrarMedicos(filtro);
        }
    }
}
