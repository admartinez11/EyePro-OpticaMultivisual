﻿using OpticaMultivisual.Controllers.Article.TipoArticulo;
using OpticaMultivisual.Controllers.Dashboard.ScheduleAppointment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Views.Dashboard.Article.TipoArticulo
{
    public partial class ViewAdminTipoArt : Form
    {
        public ViewAdminTipoArt()
        {
            InitializeComponent();
            ControllerAdminTipoArt objAppointment = new ControllerAdminTipoArt(this);
        }
    }
}
