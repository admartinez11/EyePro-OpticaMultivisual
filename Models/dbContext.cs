using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Models
{
    // Clase para manejar la conexión con el servidor y la base de datos
    public class dbContext
    {
        // Método estático para obtener una conexión SqlConnection
        public static SqlConnection getConnection()
        {
            try
            {
                // Nombre del servidor y base de datos a los que conectarse
                string server = "A11\\SQLEXPRESS";
                string database = "EyePro";
                // Creación de una instancia de SqlConnection utilizando la cadena de conexión especificada
                SqlConnection conexion = new SqlConnection($"Server = {server}; Database = {database}; Integrated Security = true");
                // Abre la conexión
                conexion.Open();
                // Retorna la conexión abierta
                return conexion;
            }
            catch (Exception)
            {
                // En caso de error al conectar, retorna null
                return null;
            }

        }
        
    }
}
