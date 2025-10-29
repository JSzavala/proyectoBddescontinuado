using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

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
        /// Obtiene todos los productos de la base de datos
        /// </summary>
        public List<POJOS.clsProductos> ObtenerTodosProductos()
        {
            List<POJOS.clsProductos> listaProductos = new List<POJOS.clsProductos>();
            SqlDataReader reader = null;

        try
        {
            if (conexion.AbrirConexion())
            {
                string query = "SELECT idProducto, nombreProducto, cantidad, precio, categoria, oferta, descontinuado, codigoBarras FROM Productos";
                SqlCommand comando = new SqlCommand(query, conexion.ObtenerConexion());
                comando.CommandType = CommandType.Text;

                reader = comando.ExecuteReader();

            while (reader.Read())
            {
                POJOS.clsProductos producto = new POJOS.clsProductos();
                producto.idProducto = int.Parse(reader["idProducto"].ToString());
                producto.nombreProducto = reader["nombreProducto"].ToString();
                producto.cantidad = int.Parse(reader["cantidad"].ToString());
                producto.precio = double.Parse(reader["precio"].ToString());
                producto.categoria = reader["categoria"].ToString();
                producto.oferta = bool.Parse(reader["oferta"].ToString());
                producto.descontinuado = bool.Parse(reader["descontinuado"].ToString());
                producto.codigoBarras = reader["codigoBarras"].ToString();

            listaProductos.Add(producto);
            }

            reader.Close();
            conexion.CerrarConexion();
        }
    }
    catch (SqlException ex)
    {
        System.Windows.Forms.MessageBox.Show("Error en la consulta: " + ex.Message);
    }
    finally
    {
        if (reader != null && !reader.IsClosed)
        reader.Close();
        conexion.CerrarConexion();
    }
    return listaProductos;
}

        /// <summary>
        /// Obtiene un producto específico por su código de barras
        /// </summary>
public POJOS.clsProductos ObtenerProductoPorCodigo(string codigo)
    {
        POJOS.clsProductos producto = null;
        SqlDataReader reader = null;

    try
    {
        if (conexion.AbrirConexion())
                {
            string query = "SELECT idProducto, nombreProducto, cantidad, precio, categoria, oferta, descontinuado, codigoBarras FROM Productos WHERE codigoBarras = @codigo";
            SqlCommand comando = new SqlCommand(query, conexion.ObtenerConexion());
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
        catch (SqlException ex)
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
    public bool DescontinuarProducto(int idProducto)
    {
        try
        {
            if (conexion.AbrirConexion())
            {
                string query = "UPDATE Productos SET descontinuado = 1 WHERE idProducto = @id";
                SqlCommand comando = new SqlCommand(query, conexion.ObtenerConexion());
                comando.Parameters.AddWithValue("@id", idProducto);
                comando.CommandType = CommandType.Text;

                int resultado = comando.ExecuteNonQuery();
                conexion.CerrarConexion();

                return resultado > 0;
            }
        }
        catch (SqlException ex)
        {
            System.Windows.Forms.MessageBox.Show("Error al descontinuar: " + ex.Message);
        }
        finally
        {
            conexion.CerrarConexion();
        }
            return false;
    }

        /// <summary>
        /// Obtiene todos los productos no descontinuados
     /// </summary>
public List<POJOS.clsProductos> ObtenerProductosActivos()
    {
        List<POJOS.clsProductos> listaProductos = new List<POJOS.clsProductos>();
        SqlDataReader reader = null;

        try
         {
            if (conexion.AbrirConexion())
             {
                string query = "SELECT idProducto, nombreProducto, cantidad, precio, categoria, oferta, descontinuado, codigoBarras FROM Productos WHERE descontinuado = 0";
                SqlCommand comando = new SqlCommand(query, conexion.ObtenerConexion());
                comando.CommandType = CommandType.Text;

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    POJOS.clsProductos producto = new POJOS.clsProductos();
                    producto.idProducto = int.Parse(reader["idProducto"].ToString());
                    producto.nombreProducto = reader["nombreProducto"].ToString();
                    producto.cantidad = int.Parse(reader["cantidad"].ToString());
                    producto.precio = double.Parse(reader["precio"].ToString());
                    producto.categoria = reader["categoria"].ToString();
                    producto.oferta = bool.Parse(reader["oferta"].ToString());
                    producto.descontinuado = bool.Parse(reader["descontinuado"].ToString());
                    producto.codigoBarras = reader["codigoBarras"].ToString();

                    listaProductos.Add(producto);
                }

                reader.Close();
                conexion.CerrarConexion();
            }
        }
        catch (SqlException ex)
        {
            System.Windows.Forms.MessageBox.Show("Error en la consulta: " + ex.Message);
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();
                conexion.CerrarConexion();
        }
    return listaProductos;
        }
    }
}
