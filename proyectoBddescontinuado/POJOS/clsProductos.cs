using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoBddescontinuado.POJOS
{
    public class clsProductos
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public string categoria { get; set; }
        public bool oferta { get; set; }
        public bool descontinuado { get; set; }
        public string codigoBarras { get; set; }

        /// <summary>
        /// Constructor sin parámetros
        /// </summary>
        public clsProductos()
        {
        }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public clsProductos(int id, string nombre, int cant, double prec, string cat, bool ofert, bool desc, string codigo = "")
        {
            idProducto = id;
            nombreProducto = nombre;
            cantidad = cant;
            precio = prec;
            categoria = cat;
            oferta = ofert;
            descontinuado = desc;
            codigoBarras = codigo;
        }
    }
}
