using OpticaMultivisual.Controllers.Article;
using OpticaMultivisual.Controllers.ScheduleAppointment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Views.Article
{
    public partial class ViewAddArticle : Form
    {
        public ViewAddArticle(int accion)
        {
            InitializeComponent();
            ControllerAddArticle objAddArticle = new ControllerAddArticle(this, accion);
        }

        public ViewAddArticle(int accion, string art_codigo, string art_nombre, string art_descripcion, int tipoart_ID, int mod_ID, string art_medidas, int material_ID, int color_ID, string art_urlimagen, string art_comentarios, string art_punitario)
        {
            InitializeComponent();
            // Se invoca al controlador de la vista y se le envía el formulario, la acción y los datos que recibió la vista.
            // La vista al recibir los datos de un controlador externo los reenvia a su propio controlador.
            ControllerAddArticle objAddUser = new ControllerAddArticle(this, accion, art_codigo, art_nombre, art_descripcion, tipoart_ID, mod_ID, art_medidas, material_ID, color_ID, art_urlimagen, art_comentarios, art_punitario);
        }
    }
}
