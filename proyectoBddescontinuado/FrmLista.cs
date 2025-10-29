using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoBddescontinuado
{
    public partial class FrmLista : Form
    {
        private DATOS.clsDaoProductos daoProductos;
        private int productoSeleccionado = -1;

        public FrmLista()
        {
            InitializeComponent();
            daoProductos = new DATOS.clsDaoProductos();
        }

        private void FrmLista_Load(object sender, EventArgs e)
        {
            
        }

        private void btnDescontinuar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                int idProducto = int.Parse(dgvProductos.SelectedRows[0].Cells["idProducto"].Value.ToString());

                if (MessageBox.Show("¿Desea descontinuar este producto?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (daoProductos.DescontinuarProducto(idProducto))
                    {
                        MessageBox.Show("Producto descontinuado exitosamente");
                    }
                    else
                    {
                        MessageBox.Show("Error al descontinuar el producto");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para descontinuar");
            }
        }

        private void txtBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == (int)Keys.Enter)
            {
                string codigo = txtBarras.Text.Trim();
                if (!string.IsNullOrEmpty(codigo))
                {
                    POJOS.clsProductos p = daoProductos.ObtenerProductoPorCodigo(codigo);
                    if (p == null)
                    {
                        MessageBox.Show("Producto NO encontrado");
                    }
                    else
                    {
                        MessageBox.Show($"Producto encontrado: {p.nombreProducto}");

                        // Buscar y seleccionar la fila correspondiente
                        foreach (DataGridViewRow row in dgvProductos.Rows)
                        {
                            if (row.Cells["codigoBarras"].Value.ToString() == codigo)
                            {
                                dgvProductos.CurrentCell = row.Cells[0];
                                row.Selected = true;
                                break;
                            }
                        }
                    }
                }
                txtBarras.Clear();
            }
        }
    }
}
