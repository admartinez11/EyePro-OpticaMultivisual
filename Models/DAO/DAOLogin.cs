using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpticaMultivisual.Models.DTO;
using OpticaMultivisual.Models;
using OpticaMultivisual.Controllers.Helper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Runtime.Remoting.Messaging;

namespace OpticaMultivisual.Models.DAO
{
    // La clase DAOLogin hereda de DTOLogin y se encarga de manejar la lógica de acceso a datos para el inicio de sesión
    public class DAOLogin : DTOLogin
    {
        SqlCommand Command = new SqlCommand();

        public int ValidarLogin()
        {
            try
            {
                Command.Connection = getConnection();

                // Primero, verificar si el usuario existe
                string queryUserExists = "SELECT COUNT(*) FROM ViewLogin WHERE username = @username";
                SqlCommand cmdUserExists = new SqlCommand(queryUserExists, Command.Connection);
                cmdUserExists.Parameters.AddWithValue("username", Username);
                int userCount = (int)cmdUserExists.ExecuteScalar();

                if (userCount == 0)
                {
                    // El usuario no existe
                    return 0;
                }

                // Si el usuario existe, verificar la contraseña
                string query = "SELECT * FROM ViewLogin WHERE username = @username AND password = @password AND userStatus = @status";
                SqlCommand cmd = new SqlCommand(query, Command.Connection);
                cmd.Parameters.AddWithValue("username", Username);
                cmd.Parameters.AddWithValue("password", Password);
                cmd.Parameters.AddWithValue("status", true);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    // Usuario y contraseña correctos
                    while (rd.Read())
                    {
                        SessionVar.Username = rd.GetString(0);
                        SessionVar.Password = rd.GetString(1);
                        SessionVar.RoleId = rd.GetInt32(3);
                        SessionVar.Access = rd.GetString(4);
                        SessionVar.FullName = rd.GetString(5);
                    }
                    return 2;
                }
                else
                {
                    // Usuario existe, pero la contraseña es incorrecta
                    return 1;
                }
            }
            catch (SqlException sqlex)
            {
                MessageBox.Show(sqlex.Message);
                return -1; // Error en la base de datos
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1; // Otro tipo de error
            }
            finally
            {
                getConnection().Close();
            }
        }

        //public bool ValidarLogin()
        //{
        //    try
        //    {
        //        Command.Connection = getConnection();
        //        string query = "SELECT * FROM ViewLogin WHERE username = @username AND password = @password AND userStatus = @status";
        //        SqlCommand cmd = new SqlCommand(query, Command.Connection);
        //        cmd.Parameters.AddWithValue("username", Username);
        //        cmd.Parameters.AddWithValue("password", Password);
        //        cmd.Parameters.AddWithValue("status", true);
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            SessionVar.Username = rd.GetString(0);
        //            SessionVar.Password = rd.GetString(1);
        //            SessionVar.RoleId = rd.GetInt32(3);
        //            SessionVar.Access = rd.GetString(4);
        //            SessionVar.FullName = rd.GetString(5);
        //        }
        //        return rd.HasRows;
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        MessageBox.Show(sqlex.Message);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        getConnection().Close();
        //    }
        //}
    }
}