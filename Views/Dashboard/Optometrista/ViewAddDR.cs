using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpticaMultivisual.Controllers.Dashboard.Optometrista;

namespace OpticaMultivisual.Views.Dashboard.Optometrista
{
    public partial class ViewAddDR : Form
    {
        //Constructor utilizado para la inserción de datos
        public ViewAddDR(int accion)
        {
            InitializeComponent();
            //Se invoca al controlador de la vista y se le envía el formulario, la acción y los datos que recibió la vista, osea que la vista al recibir los datos de un controlador externo los reenvia a su propio controlador.
            ControllerAddDR objAddDR = new ControllerAddDR(this, accion);
        }


        //Constructor utilizado para la actualización de datos
        public ViewAddDR(int accion, int DR_ID, string OD_esfera, double OD_cilindro, double OD_eje, int OD_prisma, int OD_adicion, int OD_AO, double OD_AP, double OD_DP, string OI_esfera, double OI_cilindro, double OI_eje, int OI_prisma, int OI_adicion, int OI_AO, double OI_AP, double OI_DP)
        {
            InitializeComponent();
            ControllerAddDR objAddDR = new ControllerAddDR(this, accion, DR_ID, OD_esfera, OD_cilindro, OD_eje, OD_prisma, OD_adicion, OD_AO, OD_AP, OD_DP, OI_esfera, OI_cilindro, OI_eje, OI_prisma, OI_adicion, OI_AO, OI_AP, OI_DP);
        }
    }
}
