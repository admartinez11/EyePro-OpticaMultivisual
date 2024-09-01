using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Article;
using OpticaMultivisual.Views.ScheduleAppointment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpticaMultivisual.Models.DAO.DAOArticle;
using System.Windows.Forms;

namespace OpticaMultivisual.Controllers.Article
{
    internal class ControllerAddArticle
    {
        ViewAddArticle ObjVistaR;
        private int accion;

        public ControllerAddArticle(ViewAddArticle Vista, int accion)
        {
            //Acciones iniciales
            ObjVistaR = Vista;
            this.accion = accion;
            Vista.Load += new EventHandler(CargaInicial);
            verificarAccion();
            //Métodos iniciales: estos metodos se ejecutan cuando el formulario está cargando
            ObjVistaR.btnAgendarArt.Click += new EventHandler(NuevoArticulo);
        }
        void CargaInicial(object sender, EventArgs e)
        {
            LlenarComboMaterial();
            LlenarComboColor();
            LlenarComboTipoArticulo();
            LlenarComboModelo();
        }
        void LlenarComboMaterial()
        {
            DAOArticle daoFuncion = new DAOArticle();
            DataSet ds = daoFuncion.ObtenerMaterial();
            ObjVistaR.cmbMaterialArt.DataSource = ds.Tables["Material"];
            ObjVistaR.cmbMaterialArt.DisplayMember = "material_nombre";
            ObjVistaR.cmbMaterialArt.ValueMember = "material_ID";
        }
        void LlenarComboColor()
        {
            DAOArticle daoFuncion = new DAOArticle();
            DataSet ds = daoFuncion.ObtenerColor();
            ObjVistaR.cmbColorArt.DataSource = ds.Tables["Color"];
            ObjVistaR.cmbColorArt.DisplayMember = "color_nombre";
            ObjVistaR.cmbColorArt.ValueMember = "color_ID";
        }
        void LlenarComboTipoArticulo()
        {
            DAOArticle daoFuncion = new DAOArticle();
            DataSet ds = daoFuncion.ObtenerTipoArticulo();
            ObjVistaR.cmbTipoArt.DataSource = ds.Tables["TipoArt"];
            ObjVistaR.cmbTipoArt.DisplayMember = "tipoart_nombre";
            ObjVistaR.cmbTipoArt.ValueMember = "tipoart_ID";
        }
        void LlenarComboModelo()
        {
            DAOArticle daoFuncion = new DAOArticle();
            DataSet ds = daoFuncion.ObtenerModelo();
            ObjVistaR.cmbModeloArt.DataSource = ds.Tables["Modelo"];
            ObjVistaR.cmbModeloArt.DisplayMember = "mod_nombre";
            ObjVistaR.cmbModeloArt.ValueMember = "mod_ID";
        }
        public void NuevoArticulo(object sender, EventArgs e)
        {
            DAOArticle DAOIngresarR = new DAOArticle
            {
                Art_codigo = int.Parse(ObjVistaR.txtArCodigo.Text.Trim()),
                Art_nombre = ObjVistaR.txtArNombre.Text.Trim(),
                Art_descripcion = ObjVistaR.txtDescArt.Text.Trim(),
                Tipoart_ID = (int)ObjVistaR.cmbTipoArt.SelectedValue,
                Mod_ID = (int)ObjVistaR.cmbModeloArt.SelectedValue,
                Art_medidas = ObjVistaR.txtMedidas.Text.Trim(),
                Material_ID = (int)ObjVistaR.cmbMaterialArt.SelectedValue,
                Color_ID = (int)ObjVistaR.cmbColorArt.SelectedValue,
                Art_urlimagen = ObjVistaR.txtUrlImagenArt.Text.Trim(),
                Art_comentarios = ObjVistaR.txtComentariosArt.Text.Trim(),
                Art_punitario = ObjVistaR.txtPUnitario.Text.Trim(),
            };

            int valorRetornado = DAOIngresarR.IngresarArticulo();

            if (valorRetornado == 1)
            {
                MessageBox.Show("Los datos han sido registrados exitosamente",
                                "Proceso completado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                ObjVistaR.Close();
            }
            else
            {
                MessageBox.Show("Los datos no pudieron ser registrados",
                                "Proceso interrumpido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        public ControllerAddArticle(ViewAddArticle Vista, int p_accion, string art_codigo, string art_nombre, string art_descripcion, int tipoart_ID, int mod_ID, string art_medidas, int material_ID, int color_ID, string art_urlimagen, string art_comentarios, string art_punitario)
        {
            // Acciones iniciales
            ObjVistaR = Vista;
            this.accion = p_accion;
            Vista.Load += new EventHandler(CargaInicial);
            // Verificar la acción a realizar
            verificarAccion();
            // Cargar los valores en la vista
            Cargarvalores(art_codigo, art_nombre, art_descripcion, tipoart_ID, mod_ID, art_medidas, material_ID, color_ID, art_urlimagen, art_comentarios, art_punitario);
            // Métodos que se ejecutan al ocurrir eventos
            ObjVistaR.btnActualizarArt.Click += new EventHandler(ActualizarRegistro);
            // ObjAddUser.btnFoto.Click += new EventHandler(ChargePhoto);
        }
        public void Cargarvalores(string art_codigo, string art_nombre, string art_descripcion, int tipoart_ID, int mod_ID, string art_medidas, int material_ID, int color_ID, string art_urlimagen, string art_comentarios, string art_punitario)
        {
            try
            {
                // Asignación correcta
                ObjVistaR.txtArCodigo.Text = art_codigo; // Campo para Codigo
                ObjVistaR.txtArNombre.Text = art_nombre; // Campo para Nombre
                ObjVistaR.txtDescArt.Text = art_descripcion; // Campo para descripcion  
                ObjVistaR.cmbTipoArt.SelectedValue = tipoart_ID; // Campo para tipo de articulo
                ObjVistaR.cmbModeloArt.SelectedValue = mod_ID; // Campo para Modelo
                ObjVistaR.txtMedidas.Text = art_medidas; // Campo para Medidas
                ObjVistaR.cmbMaterialArt.SelectedValue = material_ID; // Campo para Material
                ObjVistaR.cmbColorArt.SelectedValue = color_ID; //Campo para Color
                ObjVistaR.txtUrlImagenArt.Text = art_urlimagen; //Campo para El URL de la imagen
                ObjVistaR.txtComentariosArt.Text = art_comentarios; //Campo para Comentarios
                ObjVistaR.txtPUnitario.Text = art_punitario; //Campo para Precio
            }
            catch (Exception ex)
            {
                // Muestra el mensaje de error si ocurre una excepción
                MessageBox.Show($"{ex.Message}", "Error al cargar valores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ActualizarRegistro(object sender, EventArgs e)
        {
            DAOArticle DAOIngresarR = new DAOArticle
            {
                Art_codigo = int.Parse(ObjVistaR.txtArCodigo.Text.Trim()),
                Art_nombre = ObjVistaR.txtArNombre.Text.Trim(),
                Art_descripcion = ObjVistaR.txtDescArt.Text.Trim(),
                Tipoart_ID = (int)ObjVistaR.cmbTipoArt.SelectedValue,
                Mod_ID = (int)ObjVistaR.cmbModeloArt.SelectedValue,
                Art_medidas = ObjVistaR.txtMedidas.Text.Trim(),
                Material_ID = (int)ObjVistaR.cmbMaterialArt.SelectedValue,
                Color_ID = (int)ObjVistaR.cmbColorArt.SelectedValue,
                Art_urlimagen = ObjVistaR.txtUrlImagenArt.Text.Trim(),
                Art_comentarios = ObjVistaR.txtComentariosArt.Text.Trim(),
                Art_punitario = ObjVistaR.txtPUnitario.Text.Trim(),
            };

            int valorRetornado = DAOIngresarR.ActualizarArticulo();

            if (valorRetornado > 0)
            {
                MessageBox.Show("Los datos han sido actualizados exitosamente",
                                "Proceso completado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                ObjVistaR.Close();

            }
            else if (valorRetornado == 0)
            {
                MessageBox.Show("No se encontró el registro para actualizar",
                                "Proceso interrumpido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Los datos no pudieron ser actualizados debido a un error inesperado",
                                "Proceso interrumpido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        public void verificarAccion()
        {
            if (accion == 1)
            {
                ObjVistaR.btnAgendarArt.Enabled = true;
                ObjVistaR.btnActualizarArt.Enabled = false;
            }
            else if (accion == 2)
            {
                ObjVistaR.btnAgendarArt.Enabled = false;
                ObjVistaR.btnActualizarArt.Enabled = true;
            }
        }
    }
}

