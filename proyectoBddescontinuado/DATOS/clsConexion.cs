using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace proyectoBddescontinuado.DATOS
{
    public class clsConexion
    {
        private MySqlConnection conexion;
        private string cadenaConexion = "Server=localhost;Database=prodDesc;User Id=root;Password=1234;";

        /// <summary>
        /// Constructor que inicializa la conexión
        /// </summary>
        public clsConexion()
        {
            conexion = new MySqlConnection(cadenaConexion);
        }

        /// <summary>
        /// Abre la conexión a la base de datos
        /// </summary>
        public bool AbrirConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al conectar: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Cierra la conexión a la base de datos
        /// </summary>
        public bool CerrarConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al cerrar: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Retorna la conexión actual
        /// </summary>
        public MySqlConnection ObtenerConexion()
        {
            return conexion;
        }

        /// <summary>
        /// Establece la cadena de conexión
        /// </summary>
        public void EstablecerCadenaConexion(string nuevaCadena)
        {
            cadenaConexion = nuevaCadena;
            conexion.ConnectionString = cadenaConexion;
        }
    }
}
