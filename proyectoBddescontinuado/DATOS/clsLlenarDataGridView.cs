using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoBddescontinuado.DATOS
{
    public class clsLlenarDataGridView
    {
        /// <summary>
     /// Llena un DataGridView con una lista de productos
    /// </summary>
    public static void LlenarProductos(DataGridView dgv, List<POJOS.clsProductos> listaProductos)
    {
        // Limpiar el DataGridView
        dgv.DataSource = null;
        dgv.Rows.Clear();

        // Configurar las columnas si no existen
        if (dgv.Columns.Count == 0)
        {
            dgv.Columns.Add("idProducto", "ID");
            dgv.Columns.Add("codigoBarras", "Código");
            dgv.Columns.Add("nombreProducto", "Nombre");
            dgv.Columns.Add("categoria", "Categoría");
            dgv.Columns.Add("cantidad", "Cantidad");
            dgv.Columns.Add("precio", "Precio");
            dgv.Columns.Add("oferta", "Oferta");
            dgv.Columns.Add("descontinuado", "Descontinuado");
        // Ajustar ancho de columnas
            dgv.Columns["idProducto"].Width = 50;
            dgv.Columns["codigoBarras"].Width = 100;
            dgv.Columns["nombreProducto"].Width = 150;
            dgv.Columns["categoria"].Width = 100;
            dgv.Columns["cantidad"].Width = 80;
            dgv.Columns["precio"].Width = 80;
            dgv.Columns["oferta"].Width = 70;
            dgv.Columns["descontinuado"].Width = 100;
        }

     // Llenar con datos
    foreach (var producto in listaProductos)
    {
        dgv.Rows.Add(
            producto.idProducto,
            producto.codigoBarras,
            producto.nombreProducto,
            producto.categoria,
            producto.cantidad,
            producto.precio.ToString("C"),
            producto.oferta ? "Sí" : "No",
            producto.descontinuado ? "Sí" : "No"
            );
        }
    }

     /// <summary>
        /// Limpia el DataGridView
        /// </summary>
    public static void LimpiarDataGridView(DataGridView dgv)
    {
       dgv.DataSource = null;
       dgv.Rows.Clear();
    }
    }
}
