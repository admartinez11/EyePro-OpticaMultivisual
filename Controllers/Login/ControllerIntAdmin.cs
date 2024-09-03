using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Dashboard;
using OpticaMultivisual.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Controllers.Login
{
    internal class ControllerIntAdmin
    {
        ViewIntAdmin ObjAdmin;
        public ControllerIntAdmin(ViewIntAdmin ObjAdmin)
        {
            this.ObjAdmin = ObjAdmin;
            this.ObjAdmin.BtnRest.Click += new EventHandler(CambiarClave);
        }

        void CambiarClave(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(ObjAdmin.txtUserEmp.Text.Trim()) ||
                string.IsNullOrEmpty(ObjAdmin.txtUserAd.Text.Trim()) ||
                string.IsNullOrEmpty(ObjAdmin.txtPassAd.Text.Trim())))
            {
                if (VerificarUsuario())
                {
                    if (VerificarAdmin())
                    {
                        //if (RestablecerClave())
                        //{
                        //    MessageBox.Show("Contraseña restablecida con éxito, ya puedes iniciar sesión con tu nueva contraseña.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    vista.Dispose();
                        //    ViewLogin viewLogin = new ViewLogin();
                        //    viewLogin.Show();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("La contraseña no pudo ser actualizada, vuelve a intentarlo, si el error persiste contacta al administrador del sistema.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                    }
                    else
                    {
                        MessageBox.Show("El PIN o el usuario son incorrectos, verifique la información proporcionada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Todos los campos requeridos, favor complétalos para establecer una nueva contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool VerificarUsuario()
        {
            string username = ObjAdmin.txtUserEmp.Text;
            DAOAdminEmp dAOAdminEmp = new DAOAdminEmp();
            dAOAdminEmp.User = username;
            if (dAOAdminEmp.VerificarUsuario(username))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Usuario inexistente, ingrese un usuario existente", "El usuario no existe.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        bool VerificarAdmin()
        {
            string username = ObjAdmin.txtUserAd.Text;
            string password = ObjAdmin.txtPassAd.Text;
            DAOAdminEmp dAOAdminEmp = new DAOAdminEmp();
            dAOAdminEmp.User = username;
            dAOAdminEmp.Password = password;
            if (dAOAdminEmp.ValidateAdminCredentials(username, password))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Administrador inexistente, ingrese un administrador existente", "El administrador no existe.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
