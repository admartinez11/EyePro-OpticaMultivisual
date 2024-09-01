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
                string server = "sql8020.site4now.net";
                string database = "db_aac9bb_eyepro";
                string userId = "db_aac9bb_eyepro_admin";
                string Password = "EyePro123";
                SqlConnection conexion = new SqlConnection($"Server = {server}; DataBase = {database}; User Id = {userId}; Password = {Password}");
                conexion.Open();
                return conexion;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"{ex.Message} Código de error: EC-001 \nNo fue posible conectarse a la base de datos, favor verifique las credenciales o que tenga acceso al sistema.", "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

    }
}
