using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Controllers.Helper
{
    public class CommonClasses
    {
        /// <summary>
        /// Método para crear bordes redondos en el formulario
        /// </summary>
        /// <param name="nLeftRect"></param>
        /// <param name="nTopRect"></param>
        /// <param name="nRightRect"></param>
        /// <param name="nBottomRect"></param>
        /// <param name="nWidthEllipse"></param>
        /// <param name="nHeightEllipse"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public string ComputeSha256Hash(string rawData)
        {
            // Crear una instancia de SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computar el hash - devuelve un arreglo de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convertir byte array a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool TieneAlMenos8Caracteres(string contrasena)
        {
            return contrasena.Length >= 8;
        }
        public bool ContieneAlMenosUnNumero(string contrasena)
        {
            return contrasena.Any(char.IsDigit);
        }
        public bool ContieneAlMenosUnaMayuscula(string contrasena)
        {
            return contrasena.Any(char.IsUpper);
        }
        public bool ContieneAlMenosUnaMinuscula(string contrasena)
        {
            return contrasena.Any(char.IsLower);
        }
        public bool ContieneAlMenosUnSimbolo(string contrasena)
        {
            string simbolos = "@$#_";
            return contrasena.Any(simbolo => simbolos.Contains(simbolo));
        }

        public bool EsCorreoValido(string correo)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, patron);
        }

        // Método para validar los comandos de teclado
        public void ValidarComandos(object sender, KeyEventArgs e)
        {
            // Detectar si se presiona Ctrl+C, Ctrl+X o Ctrl+V
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                // Cancelar la operación
                e.SuppressKeyPress = true;
            }
        }

        // Método para validar el formato del teléfono
        public bool ValidarTelefono(string telefono)
        {
            // Expresión regular que permite números, guiones, paréntesis y espacios
            string patron = @"^[0-9\-\(\)\s]+$";

            // Verificar que el teléfono coincida con el patrón
            return Regex.IsMatch(telefono, patron);
        }

        // Método para validar la fecha de nacimiento
        public bool ValidarFechaNacimiento(DateTime fechaNacimiento)
        {
            // Calcular la edad
            int edad = DateTime.Today.Year - fechaNacimiento.Year;

            // Restar un año si la fecha de nacimiento aún no ha ocurrido este año
            if (fechaNacimiento > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }

            // Validar que la edad sea mayor o igual a 18
            if (edad < 18)
            {
                return false;
            }

            // Validar que la fecha no sea mayor a la fecha actual
            if (fechaNacimiento > DateTime.Today)
            {
                return false;
            }

            // Validar que la fecha no sea demasiado antigua (más de 120 años)
            if (fechaNacimiento < DateTime.Today.AddYears(-120))
            {
                return false;
            }

            return true;
        }

        public bool ValidarDecimal(string valor)
        {
            // Eliminar espacios adicionales
            valor = valor.Trim();

            // Separar el valor en partes usando el punto como delimitador
            string[] partes = valor.Split('.');

            // Verificar la longitud de la parte antes del punto
            if (partes.Length > 0 && partes[0].Length > 5)
            {
                MessageBox.Show("La parte antes del punto ha excedido el máximo de 5 caracteres.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Verificar la longitud de la parte después del punto, si existe
            if (partes.Length > 1 && partes[1].Length > 2)
            {
                MessageBox.Show("La parte después del punto ha excedido el máximo de 2 caracteres.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // La validación fue exitosa
            return true;
        }

        public string GenerarPin()
        {
            int longitud = 6;
            byte[] data = new byte[longitud / 2];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            string hex = BitConverter.ToString(data);
            return hex.Replace("-", "").Substring(0, longitud);
        }

        public bool ValidarNumero(string numero)
        {
            // Expresión regular que permite solo números y un punto
            string patron = @"^[0-9]+$";

            // Verificar que el número coincida con el patrón
            return Regex.IsMatch(numero, patron);
        }

        public bool ValidarNumeroConPunto(string numero)
        {
            // Expresión regular que permite solo números y un punto
            string patron = @"^[0-9.]+$";

            // Verificar que el número coincida con el patrón
            return Regex.IsMatch(numero, patron);
        }

        public bool ValidateEmail(string email)
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

        public bool EsValida(string contrasena)
        {
            return TieneAlMenos8Caracteres(contrasena) &&
                   ContieneAlMenosUnNumero(contrasena) &&
                   ContieneAlMenosUnaMayuscula(contrasena) &&
                   ContieneAlMenosUnaMinuscula(contrasena) &&
                   ContieneAlMenosUnSimbolo(contrasena);
        }
    }
}
