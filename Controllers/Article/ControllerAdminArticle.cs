using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Article;
using OpticaMultivisual.Views.ScheduleAppointment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Controllers.Article
{
    internal class ControllerAdminArticle
    {
        ViewAdminArticle ObjVista;

        public ControllerAdminArticle(ViewAdminArticle Vista)
        {
            ObjVista = Vista;
            ObjVista.Load += new EventHandler(CargarInfo);
            ObjVista.btnBuscar.Click += new EventHandler(BuscarArticuloControlador);
            ObjVista.btnNuevoArt.Click += new EventHandler(AgregarArticulo);
            ObjVista.btnEliminarArt.Click += new EventHandler(EliminarArticulo);
            ObjVista.btnActArt.Click += new EventHandler(ActualizarArticulo);
        }
        public void CargarInfo(object sender, EventArgs e)
        {
            ActualizarDatos();
        }
        public void ActualizarDatos()
        {
            DAOArticle ObjRegistro = new DAOArticle();
            DataSet ds = ObjRegistro.ObtenerInfoArticulo();
            ObjVista.dgvInfoArticulo.DataSource = ds.Tables["ViewArt"];
        }
        public void BuscarArticuloControlador(object sender, EventArgs e)
        {
            DAOArticle Objadminregistro = new DAOArticle();
            DataSet ds = Objadminregistro.BuscarArticulo(ObjVista.txtBuscar.Text.Trim());
            ObjVista.dgvInfoArticulo.DataSource = ds.Tables["Articulo"];
        }
        public void AgregarArticulo(object sender, EventArgs e)
        {
            ViewAddArticle openForm = new ViewAddArticle(1);
            openForm.ShowDialog();
            ActualizarDatos();
        }
        private void EliminarArticulo(object sender, EventArgs e)
        {
            int pos = ObjVista.dgvInfoArticulo.CurrentRow.Index;
            if (MessageBox.Show($"¿Esta seguro que desea elimar a:\n {ObjVista.dgvInfoArticulo[1, pos].Value.ToString()} {ObjVista.dgvInfoArticulo[2, pos].Value.ToString()}.\nConsidere que dicha acción no se podrá revertir.", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DAOArticle daoDel = new DAOArticle();
                daoDel.Art_codigo = int.Parse(ObjVista.dgvInfoArticulo[0, pos].Value.ToString());
                int valorRetornado = daoDel.EliminarArticulo();
                if (valorRetornado == 1)
                {
                    MessageBox.Show("Registro eliminado", "Acción completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarDatos();
                }
                else
                {
                    MessageBox.Show("Registro no pudo ser eliminado, verifique que el registro no tenga datos asociados.", "Acción interrumpida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        public void ActualizarArticulo(object sender, EventArgs e)
        {
            int pos = ObjVista.dgvInfoArticulo.CurrentRow.Index;
            ViewAddArticle openForm = new ViewAddArticle(2,
            ObjVista.dgvInfoArticulo[0, pos].Value.ToString(),// Codigo del articulo
            ObjVista.dgvInfoArticulo[1, pos].Value.ToString(),// Nombre
            ObjVista.dgvInfoArticulo[2, pos].Value.ToString(),// Descripcion
            int.Parse(ObjVista.dgvInfoArticulo[3, pos].Value.ToString()),// Tipo
            int.Parse(ObjVista.dgvInfoArticulo[4, pos].Value.ToString()),// Modelo
            ObjVista.dgvInfoArticulo[5, pos].Value.ToString(),   // Medidas
            int.Parse(ObjVista.dgvInfoArticulo[6, pos].Value.ToString()),// Material
            int.Parse(ObjVista.dgvInfoArticulo[7, pos].Value.ToString()),// Color
            ObjVista.dgvInfoArticulo[8, pos].Value.ToString(),   // Url Imagen
            ObjVista.dgvInfoArticulo[9, pos].Value.ToString(),   // Comentarios
            ObjVista.dgvInfoArticulo[10, pos].Value.ToString() // Precio Unitario
            );

            openForm.ShowDialog();
            ActualizarDatos();
        }
    }
}