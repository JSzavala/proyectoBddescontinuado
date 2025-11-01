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
        List<string> codigosLista = new List<string>();
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
            if (dgvProductos.Rows.Count > 0)
            {
                if (MessageBox.Show("¿Desea descontinuar estos productos?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (daoProductos.DescontinuarProducto(codigosLista))
                    {
                        MessageBox.Show("Producto descontinuado exitosamente");
                        DATOS.clsLlenarDataGridView.LimpiarDataGridView(dgvProductos);
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
                        MessageBox.Show("Producto NO encontrado "+codigo);
                    }
                    else
                    {
                        DATOS.clsLlenarDataGridView.LlenarProductos(dgvProductos, p);
                        codigosLista.Add(codigo);
                    }
                }
                txtBarras.Clear();
            }
        }
    }
}
