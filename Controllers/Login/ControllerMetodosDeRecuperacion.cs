using OpticaMultivisual.Views.Dashboard;
using OpticaMultivisual.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticaMultivisual.Controllers.Login
{
    internal class ControllerMetodosDeRecuperacion
    {
        ViewRecuperaciónPass vista;
        public ControllerMetodosDeRecuperacion(ViewRecuperaciónPass vista)
        {
            this.vista = vista;
            vista.btnAdmin.Click += new EventHandler(openFormIntervencionAdmin);
            vista.BtnGmail.Click += new EventHandler(openFormMail);
            vista.BtnSecurityQ.Click += new EventHandler(openFormSecQ);
        }

        void openFormSecQ(object sender, EventArgs e)
        {
            ViewByQuestion openForm = new ViewByQuestion();
            openForm.ShowDialog();
        }

        void openFormMail(object sender, EventArgs e)
        {
            ViewRecoverPassword openForm = new ViewRecoverPassword();
            openForm.ShowDialog();
        }

        void openFormIntervencionAdmin(object sender, EventArgs e)
        {
            ViewCambiarClave openForm = new ViewCambiarClave();
            openForm.ShowDialog();
        }
    }
}
