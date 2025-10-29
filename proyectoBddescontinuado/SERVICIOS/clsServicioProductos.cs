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

        /// <summary>
        /// Busca un producto y lo selecciona en el DataGridView
        /// </summary>
        public bool BuscarYSeleccionarProducto(DataGridView dgv, string codigo)
        {
        var producto = dao.ObtenerProductoPorCodigo(codigo);

        if (producto == null)
        {
            return false;
        }

        // Buscar la fila correspondiente
        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.Cells["codigoBarras"].Value.ToString() == codigo)
            {
                dgv.CurrentCell = row.Cells[0];
                row.Selected = true;
                return true;
            }
        }
        return false;
    }

        /// <summary>
  /// Descontinúa un producto y recarga el DataGridView
    /// </summary>
    public bool DescontinuarYRecargar(DataGridView dgv, int idProducto)
    {
        if (dao.DescontinuarProducto(idProducto))
        {
            RecargarDataGridView(dgv);
            return true;
        }
            return false;
    }

        /// <summary>
        /// Recarga el DataGridView con productos activos
    /// </summary>
        public void RecargarDataGridView(DataGridView dgv)
        {
            try
            {
                List<POJOS.clsProductos> productos = dao.ObtenerProductosActivos();
                DATOS.clsLlenarDataGridView.LlenarProductos(dgv, productos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recargar: " + ex.Message);
        }
    }

        /// <summary>
        /// Valida que un producto existe antes de procesarlo
        /// </summary>
        public POJOS.clsProductos ValidarProductoExistente(string codigo)
        {
            var producto = dao.ObtenerProductoPorCodigo(codigo);
            return producto;
        }

      /// <summary>
        /// Obtiene el ID del producto seleccionado en el DataGridView
      /// </summary>
    public int ObtenerIdProductoSeleccionado(DataGridView dgv)
    {
        if (dgv.SelectedRows.Count > 0)
        {
           return int.Parse(dgv.SelectedRows[0].Cells["idProducto"].Value.ToString());
        }
        return -1;
    }

        /// <summary>
        /// Obtiene la información del producto seleccionado
 /// </summary>
        public POJOS.clsProductos ObtenerProductoSeleccionado(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
            var row = dgv.SelectedRows[0];
            return new POJOS.clsProductos(
                int.Parse(row.Cells["idProducto"].Value.ToString()),
                row.Cells["nombreProducto"].Value.ToString(),
                int.Parse(row.Cells["cantidad"].Value.ToString()),
                Convert.ToDouble(row.Cells["precio"].Value.ToString().Replace("$", "").Replace(",", ".")),
                row.Cells["categoria"].Value.ToString(),
                row.Cells["oferta"].Value.ToString() == "Sí",
                row.Cells["descontinuado"].Value.ToString() == "Sí",
                row.Cells["codigoBarras"].Value.ToString()
            );
            }
            return null;
        }
    }
}
