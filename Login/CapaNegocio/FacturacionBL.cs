using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class FacturacionBL
    {
        public List<FacturacionCLS> ListarFacturaciones()
        {
            FacturacionDAL objFacturacionDAL = new FacturacionDAL();
            return objFacturacionDAL.ListarFacturaciones();
        }

        public int GuardarFacturacion(FacturacionCLS objFacturacionCLS)
        {
            FacturacionDAL objFacturacionDAL = new FacturacionDAL();
            return objFacturacionDAL.GuardarFacturacion(objFacturacionCLS);
        }

        public int EliminarFacturacion(int id)
        {
            FacturacionDAL objFacturacionDAL = new FacturacionDAL();
            return objFacturacionDAL.EliminarFacturacion(id);
        }

        public FacturacionCLS RecuperarFacturacion(int id)
        {
            FacturacionDAL objFacturacionDAL = new FacturacionDAL();
            return objFacturacionDAL.RecuperarFacturacion(id);
        }

        public List<FacturacionCLS> FiltrarFacturaciones(FacturacionCLS filtro)
        {
            FacturacionDAL objFacturacionDAL = new FacturacionDAL();
            return objFacturacionDAL.FiltrarFacturaciones(filtro);
        }
    }
}
