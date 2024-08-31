using System;
using System.Collections.Generic;
using System.Linq;
using OpticaMultivisual.Views.Dashboard;
using System.Text;
using OpticaMultivisual.Models.DAO;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using OpticaMultivisual.Controllers.Helper;

namespace OpticaMultivisual.Controllers.Dashboard
{
    class ControllerAdminEmp
    {
        ViewAdminEmp ObjAdminEmp;
        public ControllerAdminEmp(ViewAdminEmp Vista)
        {
            ObjAdminEmp = Vista;
            ObjAdminEmp.Load += new EventHandler(LoadData);
            //Evento click de botón
            ObjAdminEmp.btnNuevo.Click += new EventHandler(NewUser);
            ObjAdminEmp.btnEditar.Click += new EventHandler(UpdateUser);
            ObjAdminEmp.btnEliminar.Click += new EventHandler(DeleteUser);
            ObjAdminEmp.btnBuscar.Click += new EventHandler(BuscarPersonasControllerEvent);
            ObjAdminEmp.txtSearch.KeyPress += new KeyPressEventHandler(Search);
        }

        public void Search(object sender, KeyPressEventArgs e)
        {
            BuscarPersonasController();
        }

        public void BuscarPersonasControllerEvent(object sender, EventArgs e) { BuscarPersonasController(); }
        void BuscarPersonasController()
        {
            //Objeto de la clase DAOAdminUsuarios
            DAOAdminEmp objAdmin = new DAOAdminEmp();
            //Declarando nuevo DataSet para que obtenga los datos del método ObtenerPersonas
            DataSet ds = objAdmin.BuscarEmpleados(ObjAdminEmp.txtSearch.Text.Trim());
            //Llenar DataGridView
            ObjAdminEmp.dgvEmpleados.DataSource = ds.Tables["ViewEmp"];
        }

        public void ConfigurarValidacionDeComandos()
        {
            CommonClasses commonClasses = new CommonClasses();
            // Asociar el evento KeyDown a los TextBox que se quiere proteger
            ObjAdminEmp.txtSearch.KeyDown += commonClasses.ValidarComandos;

            // Deshabilitar el menú contextual
            ObjAdminEmp.txtSearch.ContextMenuStrip = new ContextMenuStrip();
        }

        public void LoadData(object sender, EventArgs e)
        {
            RefrescarData();
            ConfigurarValidacionDeComandos();
        }

        public void RefrescarData()
        {
            //Objeto de la clase DAOAdminUsuarios
            DAOAdminEmp objAdmin = new DAOAdminEmp();
            //Declarando nuevo DataSet para que obtenga los datos del metodo ObtenerPersonas
            DataSet ds = objAdmin.ObtenerPersonas();
            //Llenar DataGridView
            ObjAdminEmp.dgvEmpleados.DataSource = ds.Tables["ViewEmp"];
            ObjAdminEmp.dgvEmpleados.Columns[0].Visible = false;
        }

        private void NewUser(object sender, EventArgs e)
        {
            /*Se invoca al formulario ViewAddUser y se le envía un numero, este numero servirá para indicarle que tipo de acción se quiere realizar, donde 1 significa Inserción y 2 significa Actualización*/
            ViewAddUser openForm = new ViewAddUser(1);
            //Se muestra el formulario
            openForm.ShowDialog();
            //Cuando el formulario ha sido cerrado se procede a refrescar el DataGrid para que se puedan observar los nuevo datos ingresados o actualizados.
            RefrescarData();
        }

        private void UpdateUser(object sender, EventArgs e)
        {
            //Se captura el numero de la fila a la cual se le dió click, sabiendo que la primer fila tiene como valor cero.
            int pos = ObjAdminEmp.dgvEmpleados.CurrentRow.Index;
            /*Se invoca al formulario llamado ViewAddUser y se crea un objeto de el, posteriormente se envían los datos del datagrid al constructor del formulario según el orden establecido (se sugiere ver el código del formulario para observar ambos constructores).
             * El numero dos indicado en la linea posterior significa que la acción que se desea realizar es una actualización.*/
            ViewAddUser openForm = new ViewAddUser(2,
                int.Parse(ObjAdminEmp.dgvEmpleados[0, pos].Value.ToString()),
                ObjAdminEmp.dgvEmpleados[1, pos].Value.ToString(),
                ObjAdminEmp.dgvEmpleados[2, pos].Value.ToString(),
                ObjAdminEmp.dgvEmpleados[3, pos].Value.ToString(),
                DateTime.Parse(ObjAdminEmp.dgvEmpleados[4, pos].Value.ToString()),
                ObjAdminEmp.dgvEmpleados[5, pos].Value.ToString(),
                ObjAdminEmp.dgvEmpleados[6, pos].Value.ToString(),
                ObjAdminEmp.dgvEmpleados[7, pos].Value.ToString(),
                ObjAdminEmp.dgvEmpleados[8, pos].Value.ToString(),
                ObjAdminEmp.dgvEmpleados[9, pos].Value.ToString(),
                ObjAdminEmp.dgvEmpleados[10, pos].Value.ToString());
            //Una vez los datos han sido enviados al constructor de la vista se procede a mostrar el formulario (se sugiere ver el código del constructor que esta en la vista)
            openForm.ShowDialog();
            //Una vez el formulario se haya cerrado se procederá a refrescar el dataGrid para mostrar los nuevos datos.
            RefrescarData();
        }

        private void DeleteUser(object sender, EventArgs e)
        {
            int pos = ObjAdminEmp.dgvEmpleados.CurrentRow.Index;
            if (MessageBox.Show($"¿Esta seguro que desea elimar a:\n {ObjAdminEmp.dgvEmpleados[1, pos].Value.ToString()} {ObjAdminEmp.dgvEmpleados[2, pos].Value.ToString()}.\nConsidere que dicha acción no se podrá revertir.", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DAOAdminEmp daoDel = new DAOAdminEmp();
                daoDel.Id = int.Parse(ObjAdminEmp.dgvEmpleados[0, pos].Value.ToString());
                daoDel.User = ObjAdminEmp.dgvEmpleados[9, pos].Value.ToString();
                int valorRetornado = daoDel.EliminarUsuario();
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
    }
}
