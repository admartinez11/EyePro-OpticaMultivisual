using OpticaMultivisual.Models;
using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpticaMultivisual.Views.Dashboard;
using OpticaMultivisual.Controllers.Helper;

namespace OpticaMultivisual.Controllers.Login
{
    public class ControllerLogin
    {
        //Objeto de la vista ViewLogin
        ViewLogin ObjLogin;

        /// <summary>
        /// Constructor de la clase ControllerLogin que inicia los eventos de la vista
        /// </summary>
        /// <param name="Vista"></param>
        public ControllerLogin(ViewLogin Vista)
        {
            ObjLogin = Vista;
            ObjLogin.Load += new EventHandler(ConfigurarValidacionDeComandos);
            ObjLogin.BtnStart.Click += new EventHandler(DataAccess);
            ObjLogin.BtnExit.Click += new EventHandler(QuitApplication);
            //Eventos de Probar Conexión
            ObjLogin.BtnTest.Click += new EventHandler(TestConnection);
            ObjLogin.Lblregistration.Click += new EventHandler(CreateUser);
            ObjLogin.LblForgotpass.Click += new EventHandler(RecoverPassword);
            ObjLogin.PasswordVisible.Click += new EventHandler(ShowPassword);
            ObjLogin.PasswordHide.Click += new EventHandler(HidePassword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void DataAccess(object sender, EventArgs e)
        {
            // Creando objeto de la clase DAOLogin
            DAOLogin DAOData = new DAOLogin();
            CommonClasses common = new CommonClasses();
            // Utilizando el objeto DAO para invocar a los métodos getter y setter del DTO
            DAOData.Username = ObjLogin.txtUsername.Text;
            string cadenaencriptada = common.ComputeSha256Hash(ObjLogin.txtPassword.Text);
            DAOData.Password = cadenaencriptada;
            // Invocando al método Login contenido en el DAO y capturando el resultado
            int answer = DAOData.ValidarLogin();
            // Evaluando el valor de la variable answer
            switch (answer)
            {
                case 0: // Usuario no existe
                    MessageBox.Show("Usuario inexistente, ingrese con un usuario existente o en el caso de no tener un usuario regístrese.", "Error al iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 1: // Contraseña incorrecta
                    MessageBox.Show("Contraseña incorrecta", "Error al iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 2: // Usuario y contraseña correctos
                    if (ObjLogin.txtPassword.Text.Trim() != DAOData.Username + "OP123")
                    {
                        ObjLogin.Hide();
                        ViewMain viewMain = new ViewMain(ObjLogin.txtUsername.Text);
                        viewMain.Show();
                    }
                    else if (ObjLogin.txtPassword.Text.Trim() == DAOData.Username + "OP123")
                    {
                        // Limpiar los campos txtPassword
                        ObjLogin.txtPassword.Text = "";
                        ViewCambiarClaveDefecto openForm = new ViewCambiarClaveDefecto();
                        openForm.ShowDialog();
                    }
                    break;
                default:
                    MessageBox.Show("Error desconocido", "Error al iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        public void ConfigurarValidacionDeComandos(object sender, EventArgs e)
        {
            CommonClasses commonClasses = new CommonClasses();
            // Asociar el evento KeyDown a los TextBox que se quiere proteger
            ObjLogin.txtUsername.KeyDown += commonClasses.ValidarComandos;
            ObjLogin.txtPassword.KeyDown += commonClasses.ValidarComandos;

            // Deshabilitar el menú contextual
            ObjLogin.txtUsername.ContextMenuStrip = new ContextMenuStrip();
            ObjLogin.txtPassword.ContextMenuStrip = new ContextMenuStrip();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestConnection(object sender, EventArgs e)
        {
            //Se hace referencia a la clase dbContext y su método getConnection y se evalúa
            //si el retorno es nulo o no, en caso de ser nulo se mostrará el primer mensaje
            //de lo contrario se mostrará el código del segmento else.
            if (dbContext.getConnection() == null)
            {
                MessageBox.Show("No fue posible realizar la conexión al servidor y/o la base de datos.", "Conexión fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("La conexión al servidor y la base de datos se ha ejecutado correctamente.", "Conexión exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CreateUser(object sender, EventArgs e)
        {
            ViewAddUser viewAddUser = new ViewAddUser();
            viewAddUser.Show();
        }

        private void RecoverPassword(object sender, EventArgs e)
        {
            ViewRecuperaciónPass viewRecuPass = new ViewRecuperaciónPass();
            viewRecuPass.Show();
            ObjLogin.Close();
        }

        /// <summary>
        /// El evento KeyEnter detecta cuando la tecla enter es presionada y realizará una
        /// determinada acción.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ShowPassword(object sender, EventArgs e)
        {
            // Mostrar la contraseña en texto plano
            ObjLogin.txtPassword.UseSystemPasswordChar = false;
            ObjLogin.txtPassword.PasswordChar = '\0'; // Mostrar el texto plano
            ObjLogin.PasswordVisible.Visible = false;
            ObjLogin.PasswordHide.Visible = true;
        }

        private void HidePassword(object sender, EventArgs e)
        {
            // Establecer el carácter de contraseña del sistema manualmente
            ObjLogin.txtPassword.UseSystemPasswordChar = true;
            ObjLogin.txtPassword.PasswordChar = (char)0x25CF; // Usar el punto negro (•), el carácter estándar para contraseñas
            ObjLogin.PasswordVisible.Visible = true;
            ObjLogin.PasswordHide.Visible = false;
        }
    }
}