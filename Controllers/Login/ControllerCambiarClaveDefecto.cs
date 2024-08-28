using OpticaMultivisual.Controllers.Helper;
using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Controllers.Login
{
    internal class ControllerCambiarClaveDefecto
    {
        ViewCambiarClaveDefecto vista;

        public ControllerCambiarClaveDefecto(ViewCambiarClaveDefecto vista)
        {
            this.vista = vista;
            vista.btnCambiarClave1.Click += new EventHandler(CambiarClaveDefecto);
            vista.Load += new EventHandler(ConfigurarValidacionDeComandos);
        }

        public void ConfigurarValidacionDeComandos(object sender, EventArgs e)
        {
            CommonClasses commonClasses = new CommonClasses();
            // Asociar el evento KeyDown a los TextBox que se quiere proteger
            vista.txtUsuario1.KeyDown += commonClasses.ValidarComandos;
            vista.txtNuevaContra1.KeyDown += commonClasses.ValidarComandos;
            vista.txtConfirmarNuevaContra1.KeyDown += commonClasses.ValidarComandos;

            // Deshabilitar el menú contextual
            vista.txtUsuario1.ContextMenuStrip = new ContextMenuStrip();
            vista.txtNuevaContra1.ContextMenuStrip = new ContextMenuStrip();
            vista.txtConfirmarNuevaContra1.ContextMenuStrip = new ContextMenuStrip();
        }

        void CambiarClaveDefecto(object sender, EventArgs e)
        {
            DAOAdminEmp daoUpdatePassword = new DAOAdminEmp();
            CommonClasses common = new CommonClasses();
            daoUpdatePassword.User = vista.txtUsuario1.Text.Trim();
            if (vista.txtNuevaContra1.Text.Trim().Equals(vista.txtConfirmarNuevaContra1.Text.Trim()))
            {
                if (common.EsValida(vista.txtConfirmarNuevaContra1.Text.Trim()))
                {
                    daoUpdatePassword.Password = common.ComputeSha256Hash(vista.txtConfirmarNuevaContra1.Text.Trim());
                    if (daoUpdatePassword.RestablecerContrasena())
                    {
                        MessageBox.Show("Contraseña actualizada con éxito.", "Proceso finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        vista.Close();
                    }
                    else
                    {
                        MessageBox.Show("Contraseña no pudo ser actualizada.", "Proceso incompleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("La contraseña no cumple con los requisitos.", "Proceso incompleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden, favor escriba la misma contraseña.", "Proceso incompleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
