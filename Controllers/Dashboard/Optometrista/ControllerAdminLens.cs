﻿using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Dashboard.Optometrista;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Controllers.Dashboard.Optometrista
{
    class ControllerAdminLens
    {
        ViewLens ObjAdminLens;
        public int accion;

        public ControllerAdminLens(ViewLens Vista)
        {
            ObjAdminLens = Vista;
            ObjAdminLens.Load += new EventHandler(LoadData);
            //Evento click de botón
            ObjAdminLens.btnAgregar.Click += new EventHandler(NewLens);
            ObjAdminLens.btnEditar.Click += new EventHandler(UpdateLens);
        }

        public void LoadData(object sender, EventArgs e)
        {
            RefrescarData();
        }

        //DataGridView
        public void RefrescarData()
        {
            //Objeto de la clase DAOAdminLens
            DAOLens objAdmin = new DAOLens();
            //Declarando nuevo DataSet para que obtenga los datos del metodo ObtenerPersonas
            DataSet ds = objAdmin.ObtenerPersonas();
            //Llenar DataGridView
            ObjAdminLens.dgvLens.DataSource = ds.Tables["ViewLensometria"];
            ObjAdminLens.dgvLens.Columns[0].Visible = true;
        }

        private void NewLens(object sender, EventArgs e)
        {
            /*Se invoca al formulario ViewAddLens y se le envía un numero, este numero servirá para indicarle que tipo de acción se quiere realizar, donde 1 significa Inserción y 2 significa Actualización*/
            ViewAddLens openForm = new ViewAddLens(1);
            //Se muestra el formulario
            openForm.ShowDialog();
            //Cuando el formulario ha sido cerrado se procede a refrescar el DataGrid para que se puedan observar los nuevo datos ingresados o actualizados.
            RefrescarData();
        }

        public ControllerAdminLens(ViewLens frmLens, int accion)
        {
            ObjAdminLens = frmLens;
            this.accion = accion;
            ObjAdminLens.Load += new EventHandler(LoadData);
            ObjAdminLens.btnEliminar.Click += new EventHandler(DeleteRegister);
        }

        public void DeleteRegister(object sender, EventArgs e)
        {
            int pos = ObjAdminLens.dgvLens.CurrentRow.Index;
            //Se verifica el valor que retornó el metodo anterior y que fue almacenado en la variable valorRetornado
            if (MessageBox.Show($"¿Esta seguro que desea elimar a:\n {ObjAdminLens.dgvLens[1, pos].Value.ToString()} {ObjAdminLens.dgvLens[2, pos].Value.ToString()}.\nConsidere que dicha acción no se podrá revertir.", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DAOLens daoDel = new DAOLens();
                daoDel.lens_ID1 = int.Parse(ObjAdminLens.dgvLens[0, pos].Value.ToString());
                int valorRetornado = daoDel.EliminarLens();

                if (valorRetornado == 1)
                {
                    MessageBox.Show("Registro eliminado", "Acción completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarData();
                }
                else
                {
                    MessageBox.Show("Registro no pudo ser eliminado, verifique que el registro no tenga datos asociados.", "Acción interrumpida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateLens(object sender, EventArgs e)
        {
            int pos = ObjAdminLens.dgvLens.CurrentRow.Index;

            int lens_ID, OD_prisma, OD_adicion, OI_prisma, OI_adicion;
            double OD_cilindro, OD_eje, OI_cilindro, OI_eje;
            string OD_esfera, OI_esfera;

            lens_ID = int.Parse(ObjAdminLens.dgvLens[0, pos].Value.ToString());
            OD_esfera = ObjAdminLens.dgvLens[1, pos].Value.ToString();
            OD_cilindro = double.Parse(ObjAdminLens.dgvLens[2, pos].Value.ToString());
            OD_eje = double.Parse(ObjAdminLens.dgvLens[3, pos].Value.ToString());
            OD_prisma = int.Parse(ObjAdminLens.dgvLens[4, pos].Value.ToString());
            OD_adicion = int.Parse(ObjAdminLens.dgvLens[5, pos].Value.ToString());

            OI_esfera = ObjAdminLens.dgvLens[6, pos].Value.ToString();
            OI_cilindro = double.Parse(ObjAdminLens.dgvLens[7, pos].Value.ToString());
            OI_eje = double.Parse(ObjAdminLens.dgvLens[8, pos].Value.ToString());
            OI_prisma = int.Parse(ObjAdminLens.dgvLens[9, pos].Value.ToString());
            OI_adicion = int.Parse(ObjAdminLens.dgvLens[10, pos].Value.ToString());

            ViewAddLens openForm = new ViewAddLens(2, lens_ID, OD_esfera, OD_cilindro, OD_eje, OD_prisma, OD_adicion, OI_esfera, OI_cilindro, OI_eje, OI_prisma, OI_adicion);

            openForm.ShowDialog();
            RefrescarData();
        }
    }
}
