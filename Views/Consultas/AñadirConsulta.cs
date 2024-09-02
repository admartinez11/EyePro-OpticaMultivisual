﻿using OpticaMultivisual.Controllers.Consulta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Views.Consultas
{
    public partial class AñadirConsulta : Form
    {
        // Constructor para abrir el formulario sin parámetros adicionales
        public AñadirConsulta(int accion)
        {
            InitializeComponent();
            ControladorVerConsulta controladorVerConsulta = new ControladorVerConsulta(this, accion);
        }

        // Constructor para abrir el formulario con parámetros adicionales
        public AñadirConsulta(int accion, string cli_DUI, string vis_ID, DateTime con_fechahora, string con_obser, string emp_ID, int con_ID)
        {
            InitializeComponent(); // Asegúrate de inicializar los componentes del formulario
            ControladorVerConsulta controladorConsulta = new ControladorVerConsulta(this, accion, cli_DUI, vis_ID, con_fechahora, con_obser, emp_ID, con_ID);
        }

        private void txtConID_TextChanged(object sender, EventArgs e)
        {
            txtConID.Enabled = false;
        }
    }
}
