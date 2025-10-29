using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace proyectoBddescontinuado.DATOS
{
    public class clsConexion
    {
        private SqlConnection conexion;
        private string cadenaConexion = "Server=SERVIDOR;Database=BASEDATOS;User Id=USUARIO;Password=CONTRASEÑA;";

        /// <summary>
        /// Constructor que inicializa la conexión
        /// </summary>
        public clsConexion()
        {
            conexion = new SqlConnection(cadenaConexion);
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
            catch (SqlException ex)
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
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al cerrar: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Retorna la conexión actual
        /// </summary>
        public SqlConnection ObtenerConexion()
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
