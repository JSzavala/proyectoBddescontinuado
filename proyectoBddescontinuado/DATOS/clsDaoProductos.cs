using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace proyectoBddescontinuado.DATOS
{
    public class clsDaoProductos
    {
        private clsConexion conexion;

        public clsDaoProductos()
        {
            conexion = new clsConexion();
        }
        /// <summary>
        /// Obtiene un producto específico por su código de barras
        /// </summary>
        public POJOS.clsProductos ObtenerProductoPorCodigo(string codigo)
        {
            POJOS.clsProductos producto = null;
            MySqlDataReader reader = null;

            try
            {
                if (conexion.AbrirConexion())
                {
                    string query = "SELECT idProducto, nombreProducto, cantidad, precio, categoria, oferta, descontinuado, codigoBarras" +
                            " FROM Productos WHERE codigoBarras = @codigo and descontinuado = false";
                    MySqlCommand comando = new MySqlCommand(query, conexion.ObtenerConexion());
                    comando.Parameters.AddWithValue("@codigo", codigo);
                    comando.CommandType = CommandType.Text;

                    reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        producto = new POJOS.clsProductos();
                        producto.idProducto = int.Parse(reader["idProducto"].ToString());
                        producto.nombreProducto = reader["nombreProducto"].ToString();
                        producto.cantidad = int.Parse(reader["cantidad"].ToString());
                        producto.precio = double.Parse(reader["precio"].ToString());
                        producto.categoria = reader["categoria"].ToString();
                        producto.oferta = bool.Parse(reader["oferta"].ToString());
                        producto.descontinuado = bool.Parse(reader["descontinuado"].ToString());
                        producto.codigoBarras = reader["codigoBarras"].ToString();
                    }

                    reader.Close();
                    conexion.CerrarConexion();
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error en la búsqueda: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                conexion.CerrarConexion();
            }
            return producto;
        }

        /// <summary>
        /// Actualiza el estado de descontinuado de un producto
        /// </summary>
        public bool DescontinuarProducto(List<string> productos)
        {
            try
            {
                if (conexion.AbrirConexion())
                {
                    int resultado = 0;
                    foreach (string codigo in productos)
                    {
                        string query = "UPDATE Productos SET descontinuado = 1 WHERE codigoBarras = @codigo";
                        MySqlCommand comando = new MySqlCommand(query, conexion.ObtenerConexion());
                        comando.Parameters.AddWithValue("@codigo", codigo);
                        comando.CommandType = CommandType.Text;
                        resultado += comando.ExecuteNonQuery();
                    }
                    conexion.CerrarConexion();

                    return resultado > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al descontinuar: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return false;
        }
    }
}
