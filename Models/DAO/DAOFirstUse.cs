﻿using OpticaMultivisual.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Models.DAO
{
    internal class DAOFirstUse : DTOFirstUse
    {
        readonly SqlCommand command = new SqlCommand();
        public bool RegistrarNegocio()
        {
            try
            {
                command.Connection = getConnection();
                string query = "INSERT INTO InfoNegocio VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7)";
                SqlCommand cmd = new SqlCommand(query, command.Connection);
                cmd.Parameters.AddWithValue("param1", NombreNegocio);
                cmd.Parameters.AddWithValue("param2", DireccionNegocio);
                cmd.Parameters.AddWithValue("param3", CorreoNegocio);
                cmd.Parameters.AddWithValue("param4", FechaNegocio);
                cmd.Parameters.AddWithValue("param5", TelefonoNegocio);
                cmd.Parameters.AddWithValue("param6", PbxNegocio);
                cmd.Parameters.AddWithValue("param7", ImagenNegocio);
                int retorno = cmd.ExecuteNonQuery();
                return retorno > 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("EPV006 - No se pudieron registrar los datos", "Error al procesar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("EPV009 - Error al conectar con la base de datos", "Error al procesar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                command.Connection.Close();
            }
        }

        /// <summary>
        /// El metodo se ejecuta al iniciar el programa
        /// </summary>
        /// <returns></returns>
        public int VerificarRegistroEmpresa()
        {
            try
            {
                command.Connection = getConnection();
                string query = "SELECT COUNT(*) FROM InfoNegocio";
                SqlCommand cmd = new SqlCommand(query, command.Connection);
                int totalEmpresa = (int)cmd.ExecuteScalar();
                return totalEmpresa;
            }
            catch (SqlException sqlex)
            {
                MessageBox.Show("Error: EPV010 - Error de excepción");
                return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: EPV010 - Error de excepción");
                return -1;
            }
            finally
            {
                command.Connection.Close();
            }
        }
    }
}
