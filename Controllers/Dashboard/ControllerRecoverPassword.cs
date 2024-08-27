using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Dashboard;
using OpticaMultivisual.Models;
using System.Data;
using System.Windows.Forms;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using System.Data.SqlClient;
using OpticaMultivisual.Views.Login;
using System.Security.Cryptography;
using OpticaMultivisual.Controllers.Helper;

namespace OpticaMultivisual.Controllers.Dashboard
{
    // Controlador para la vista ViewRecoverPassword
    class ControllerRecoverPassword
    {
        public ViewRecoverPassword ObjRecoverPassword;
        // Constructor que inicializa la vista
        public ControllerRecoverPassword(ViewRecoverPassword view)
        {
            ObjRecoverPassword = view;
            ObjRecoverPassword.BtnSendEmail.Click += new EventHandler(SendRecoveryEmail);
            ObjRecoverPassword.linkLabel1.Click += new EventHandler(ShowNewForm);
        }

        // Método para enviar el correo de recuperación
        private void SendRecoveryEmail(object sender, EventArgs e)
        {
            string userEmail = ObjRecoverPassword.txtCorreo.Text.Trim();
            if (ValidateEmail(userEmail))
            {
                string verificationCode = GenerateVerificationCode();
                DAORecoverPassword DAOInsert = new DAORecoverPassword();

                bool isStored = DAOInsert.StoreVerificationCode(userEmail, verificationCode);
                if (isStored)
                {

                    if (SendEmail(userEmail, verificationCode))
                    {
                        // Almacenar el correo electrónico temporalmente en SessionVar
                        SessionVar.UserEmail = userEmail;

                        MessageBox.Show("Código de verificación enviado exitosamente.",
                                        "Proceso completado",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        ObjRecoverPassword.Close();
                        // Mostrar la vista para ingresar el código de verificación
                        ViewVerifyCode viewVerifyCode = new ViewVerifyCode();
                        viewVerifyCode.Show();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo enviar el código de verificación.",
                                        "Proceso interrumpido",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error almacenar el código de verificación",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un correo electrónico válido.",
                                "Correo inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        // Método para validar el correo electrónico
        private bool ValidateEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Método para generar un código de verificación
        private string GenerateVerificationCode()
        {
            //Utilizar otro metodo para generar el codigo aleatorio
            return new Random().Next(100000, 999999).ToString();
        }

        // Método para enviar el correo electrónico de recuperación con el código de verificación
        private bool SendEmail(string email, string verificationCode)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("ccom.ptc2024@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Recuperación de contraseña";
                mail.Body = $"Tu código de verificación es: {verificationCode}";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ccom.ptc2024@gmail.com", "ngqy wagb fchr uvfr");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar el correo: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            ObjRecoverPassword.Close();
            ViewByQuestion viewByQuestion = new ViewByQuestion();
            viewByQuestion.Show();
        }
    }
}
