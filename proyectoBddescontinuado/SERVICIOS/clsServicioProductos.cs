using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoBddescontinuado.SERVICIOS
{
    /// <summary>
    /// Servicio para operaciones comunes de la aplicación
    /// </summary>
    public class clsServicioProductos
    {
        private DATOS.clsDaoProductos dao;

        public clsServicioProductos()
        {
            dao = new DATOS.clsDaoProductos();
        }

        
    }
}
