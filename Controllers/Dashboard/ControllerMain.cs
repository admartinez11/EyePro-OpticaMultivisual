using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpticaMultivisual.Controllers.Helper;
using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Dashboard;
using OpticaMultivisual.Views.Dashboard.Optometrista;
using OpticaMultivisual.Views.Login;
using OpticaMultivisual.Views.ScheduleAppointment;

namespace OpticaMultivisual.Controllers.Dashboard
{
    internal class ControllerMain
    {
        ViewMain ObjMain;
        Form currentForm;

        // Constructor que recibe la vista ViewMain como parámetro
        public ControllerMain(ViewMain View, string username)
        {
            //Se asigna al objeto ObjMain todo lo que proviene el objeto View del constructor
            ObjMain = View;
            //Se utiliza el evento Load, el cual se ejecuta de forma inmediata cuando el formulario se inicia.
            View.Load += new EventHandler(EventosIniciales);
            //Al componente lblUsername se le asigna el valor de variable de sesión
            ObjMain.lblUsername.Text = SessionVar.Username;
            //Se invoca al evento AbrirFormularioAdminUsuarios para que este puede ser mostrado según el boton que el usuario presione.
            ObjMain.btnAdmin.Click += new EventHandler(AbrirFormularioAdminUsuarios);
            ObjMain.btnClientes.Click += new EventHandler(AbrirFormularioChooseClient);
            ObjMain.btnReceta.Click += new EventHandler(AbrirFormularioRecetaBase);
            ObjMain.btnExit.Click += new EventHandler(CerrarSesion);
            ObjMain.FormClosing += new FormClosingEventHandler(cerrarPrograma);
            ObjMain.btnVisita.Click += new EventHandler(AbrirFormularioVis);
        }

        private void AbrirFormularioVis(object sender, EventArgs e)
        {
            AbrirFormulario<ViewSearchScheduleAppointment>();
        }

        private void cerrarPrograma(Object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Desea cerrar el programa directamente, considere que se cerrará la sesión de forma automática", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void CerrarSesion(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LimpiarVariablesSesion();
                ViewLogin backForm = new ViewLogin();
                backForm.Show();
                ObjMain.Dispose();
            }
        }

        void LimpiarVariablesSesion()
        {
            SessionVar.Username = string.Empty;
            SessionVar.Password = string.Empty;
            SessionVar.FullName = string.Empty;
            SessionVar.Access = string.Empty;
            SessionVar.RoleId = 0;
        }

        void EventosIniciales(object sender, EventArgs e)
        {
            Acceso();
        }

        public void Acceso()
        {
            //Estructura selectiva para evaluar los posibles valores de la vraible Access
            switch (SessionVar.Access)
            {
                case "Administrador":
                    break;
                case "Optometrista":
                    ObjMain.btnAdmin.Visible = false;
                    ObjMain.btnVisita.Visible = false;
                    break;
                case "Empleado":
                    ObjMain.btnAdmin.Visible = false;
                    ObjMain.btnReceta.Visible = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Método para abrir formularios dentro del panel contenedor del formulario principal
        /// </summary>
        /// <typeparam name="MiForm"></typeparam>
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            //Se declara objeto de tipo Form llamada formulario
            Form formulario;
            //Se guarda en el panel contenedor del formulario principal todos los controles del formulario que desea abrir <MiForm> posteriormente se guarda todo en el objeto de tipo formulario
            formulario = ObjMain.PanelContenedor.Controls.OfType<MiForm>().FirstOrDefault();
            //Si el formulario no existe se procederá a crearlo de lo contrario solo se traerá al frente (ver clausula else)
            if (formulario == null)
            {
                //Se define un nuevo formulario para guardarse como nuevo objeto MiForm
                formulario = new MiForm();
                //Se especifica que el formulario debe mostrarse como ventana
                formulario.TopLevel = false;
                //Se eliminan los bordes del formulario
                formulario.FormBorderStyle = FormBorderStyle.None;
                //Se establece que se abrira en todo el espacio del formulario padre
                formulario.Dock = DockStyle.Fill;
                //Se le asigna una opacidad de 0.75
                formulario.Opacity = 0.75;
                //Se evalua el formulario actual para verificar si es nulo
                if (currentForm != null)
                {
                    //Se cierra el formulario actual para mostrar el nuevo formulario
                    currentForm.Close();
                    //Se eliminan del panel contenedor todos los controles del formulario que se cerrará
                    ObjMain.PanelContenedor.Controls.Remove(currentForm);
                }
                //Se establece como nuevo formulario actual el formulario que se está abriendo
                currentForm = formulario;
                //Se agregan los controles del nuevo formulario al panel contenedor
                ObjMain.PanelContenedor.Controls.Add(formulario);
                //Tag es una propiedad genérica disponible para la mayoría de los controles en aplicaciones .NET, incluyendo los paneles.
                ObjMain.PanelContenedor.Tag = formulario;
                //Se muestra el formulario en el panel contenedor
                formulario.Show();
                //Se trae al frente el formulario armado
                formulario.BringToFront();
            }
            else
            {
                formulario.BringToFront();
            }
        }

        private void AbrirFormularioAdminUsuarios(object sender, EventArgs e)
        {
            AbrirFormulario<ViewAdminEmp>();
        }

        private void AbrirFormularioChooseClient(object sender, EventArgs e)
        {
            AbrirFormulario<ViewSearchScheduleAppointment>();
        }

        private void AbrirFormularioRecetaBase(object sender, EventArgs e)
        {
            AbrirFormulario<ViewRecetaBase>();
        }
    }
}
