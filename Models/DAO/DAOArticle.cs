﻿using OpticaMultivisual.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Models.DAO
{
    internal class DAOArticle : DTOArticle
    {
        SqlCommand command = new SqlCommand();

        public DataSet ObtenerInfoArticulo()
        {
            try
            {
                command.Connection = getConnection();
                string query = "SELECT * FROM ViewArt";
                SqlCommand cmd = new SqlCommand(query, command.Connection);
                cmd.Parameters.AddWithValue("valor", true);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "ViewArt");
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                getConnection().Close();
            }
        }
        public DataSet ObtenerTipoArticulo()
        {
            try
            {
                command.Connection = getConnection();
                string query = "SELECT tipoart_ID, tipoart_nombre FROM TipoArt";
                SqlCommand cmdSelect = new SqlCommand(query, command.Connection);
                cmdSelect.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(cmdSelect);
                DataSet ds = new DataSet();
                adp.Fill(ds, "TipoArt");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} Error al obtener los detalles, verifique su conexion a internet o que el acceso al servidor o base de datos esten activos", "Error de ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                command.Connection.Close();
            }
        }
        public DataSet ObtenerModelo()
        {
            try
            {
                command.Connection = getConnection();
                string query = "SELECT mod_ID, mod_nombre FROM Modelo";
                SqlCommand cmdSelect = new SqlCommand(query, command.Connection);
                cmdSelect.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(cmdSelect);
                DataSet ds = new DataSet();
                adp.Fill(ds, "Modelo");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} Error al obtener los detalles, verifique su conexion a internet o que el acceso al servidor o base de datos esten activos", "Error de ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                command.Connection.Close();
            }
        }
        public DataSet ObtenerMaterial(int tipoArticuloID)
        {
            try
            {
                command.Connection = getConnection();
                string query = "SELECT Material.material_nombre, Material.material_ID " +
                               "FROM MaterialTipoArt " +
                               "INNER JOIN TipoArt ON TipoArt.tipoart_ID = MaterialTipoArt.tipoart_ID " +
                               "INNER JOIN Material ON Material.material_ID = MaterialTipoArt.material_ID " +
                               "WHERE TipoArt.tipoart_ID = @TipoArticulo";
                SqlCommand cmdSelect = new SqlCommand(query, command.Connection);
                cmdSelect.Parameters.AddWithValue("@TipoArticulo", tipoArticuloID); // Asegúrate de que sea un entero
                SqlDataAdapter adp = new SqlDataAdapter(cmdSelect);
                DataSet ds = new DataSet();
                adp.Fill(ds, "MaterialTipoArt");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} Error al obtener los detalles, verifique su conexion a internet o que el acceso al servidor o base de datos esten activos", "Error de ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                command.Connection.Close();
            }
        }

        public DataSet ObtenerColor()
        {
            try
            {
                command.Connection = getConnection();
                string query = "SELECT color_ID, color_nombre FROM Color";
                SqlCommand cmdSelect = new SqlCommand(query, command.Connection);
                cmdSelect.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(cmdSelect);
                DataSet ds = new DataSet();
                adp.Fill(ds, "Color");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} Error al obtener los detalles, verifique su conexion a internet o que el acceso al servidor o base de datos esten activos", "Error de ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                command.Connection.Close();
            }
        }
        public int IngresarArticulo()
        {
            try
            {
                command.Connection = getConnection();
                string query = "INSERT INTO Articulo (art_codigo, art_nombre, art_descripcion, tipoart_ID, mod_ID, art_medidas, material_ID, color_ID, art_urlimagen, art_comentarios, art_punitario) VALUES (@Codigo, @Nombre, @Descripcion, @TipoArt, @Modelo, @Medidas, @Material, @Color, @URLimagen, @Comentarios, @Precio)";
                SqlCommand cmd = new SqlCommand(query, command.Connection);

                cmd.Parameters.AddWithValue("@Codigo", Art_codigo);
                cmd.Parameters.AddWithValue("@Nombre", Art_nombre);
                cmd.Parameters.AddWithValue("@Descripcion", Art_descripcion);
                cmd.Parameters.AddWithValue("@TipoArt", Tipoart_ID);
                cmd.Parameters.AddWithValue("@Modelo", Mod_ID);
                cmd.Parameters.AddWithValue("@Medidas", Art_medidas);
                cmd.Parameters.AddWithValue("@Material", Material_ID);
                cmd.Parameters.AddWithValue("@Color", Color_ID);
                cmd.Parameters.AddWithValue("@URLimagen", Art_urlimagen);
                cmd.Parameters.AddWithValue("@Comentarios", Art_comentarios);
                cmd.Parameters.AddWithValue("@Precio", Art_punitario);
                int respuesta = cmd.ExecuteNonQuery();
                return respuesta;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error al insertar el Articulo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                getConnection().Close();
            }
        }
        public int ActualizarArticulo()
        {
            try
            {
                command.Connection = getConnection();
                string query = "UPDATE Visita SET art_codigo = @Codigo, art_nombre = @Nombre, art_descripcion = @Descripcion, tipoart_ID = @TipoArt, mod_ID = @Modelo, art_medidas = @Medidas, material_ID = @Material, color_ID = @Color, art_urlimagen = @URLimagen, art_comentarios = @Comentarios, art_punitario = @Precio " +
                    "WHERE art_codigo = @Codigo";

                SqlCommand cmd = new SqlCommand(query, command.Connection);
                cmd.Parameters.AddWithValue("@Codigo", Art_codigo);
                cmd.Parameters.AddWithValue("@Nombre", Art_nombre);
                cmd.Parameters.AddWithValue("@Descripcion", Art_descripcion);
                cmd.Parameters.AddWithValue("@TipoArt", Tipoart_ID);
                cmd.Parameters.AddWithValue("@Modelo", Mod_ID);
                cmd.Parameters.AddWithValue("@Medidas", Art_medidas);
                cmd.Parameters.AddWithValue("@Material", Material_ID);
                cmd.Parameters.AddWithValue("@Color", Color_ID);
                cmd.Parameters.AddWithValue("@URLimagen", Art_urlimagen);
                cmd.Parameters.AddWithValue("@Comentarios", Art_comentarios);
                cmd.Parameters.AddWithValue("@Precio", Art_punitario);
                int respuesta = cmd.ExecuteNonQuery();
                return respuesta;
            }
            catch (SqlException ex)
            {
                // Mostrar el mensaje del error para fines de depuración
                Console.WriteLine("Error de SQL: " + ex.Message);
                return -1;
            }
            finally
            {
                // Asegúrate de cerrar la conexión después de usarla
                if (command.Connection.State == ConnectionState.Open)
                {
                    command.Connection.Close();
                }
            }
        }
        public int EliminarArticulo()
        {
            try
            {
                command.Connection = getConnection();
                string query = "DELETE Articulo WHERE art_codigo = @Codigo";
                SqlCommand cmd = new SqlCommand(query, command.Connection);
                cmd.Parameters.AddWithValue("@Codigo", Art_codigo);
                int respuesta = cmd.ExecuteNonQuery();
                return respuesta;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                getConnection().Close();
            }
        }
        public DataSet BuscarArticulo(string valor)
        {
            try
            {
                command.Connection = getConnection();
                string query = $"SELECT * FROM Articulo WHERE art_codigo LIKE '%{valor}%' OR art_nombre LIKE '%{valor}%'";
                SqlCommand cmd = new SqlCommand(query, command.Connection);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                //Rellenamos con el Adaptador el DataSet diciendole de que tabla provienen los datos
                adp.Fill(ds, "Articulo");
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                getConnection().Close();
            }
        }
    }
}