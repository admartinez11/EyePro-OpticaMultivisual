using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpticaMultivisual.Views;
using AdministrarClientes.Controlador;
using AdministrarClientes.View.RegistroCliente;
using OpticaMultivisual.Views.ScheduleAppointment;
using OpticaMultivisual.Views.Login;
using OpticaMultivisual.Views.Dashboard.Optometrista;

namespace OpticaMultivisual
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializar y ejecutar el primer formulario
            Application.Run(new ViewRecetaBase());
        }
    }

}
