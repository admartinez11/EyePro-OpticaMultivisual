using OpticaMultivisual.Views.Dashboard;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpticaMultivisual.Views;
using OpticaMultivisual.Controllers.Helper;
using OpticaMultivisual.Views.ScheduleAppointment;
using static OpticaMultivisual.Models.DAO.DAOScheduleAppointment;
using OpticaMultivisual.Models.DTO;
using OpticaMultivisual.Models.DAO;
using System.Text.RegularExpressions;

namespace OpticaMultivisual.Controllers.ScheduleAppointment
{
    class ControllerScheduleAppointment
    {
        ViewScheduleAppointment ObjVistaR;
        private int accion;

        public ControllerScheduleAppointment(ViewScheduleAppointment Vista, int accion)
        {
            //Acciones iniciales
            ObjVistaR = Vista;
            this.accion = accion;
            verificarAccion();
            //Métodos iniciales: estos metodos se ejecutan cuando el formulario está cargando
            ObjVistaR.btnAgendar.Click += new EventHandler(NuevaVisita);
        }
        public void NuevaVisita(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                DAOScheduleAppointment DAOIngresarR = new DAOScheduleAppointment
                {
                    Vis_dui = ObjVistaR.txtCiDUI.Text.Trim(),
                    Vis_tel = ObjVistaR.txtCiTel.Text.Trim(),
                    Vis_obser = ObjVistaR.txtCiObs.Text.Trim(),
                    Vis_fcita = ObjVistaR.DTPfechacita.Value,
                    Vis_nombre = ObjVistaR.txtCiNombre.Text.Trim(),
                    Vis_apellido = ObjVistaR.txtCiApellido.Text.Trim(),
                    Vis_correo = ObjVistaR.txtCiCorreo.Text.Trim(),
                };

                int valorRetornado = DAOIngresarR.RegistrarVisita();

                if (valorRetornado == 1)
                {
                    MessageBox.Show("Los datos han sido registrados exitosamente",
                                    "Proceso completado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    ObjVistaR.Close();
                }
                else
                {
                    MessageBox.Show("Los datos no pudieron ser registrados",
                                    "Proceso interrumpido",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
        private bool ValidarCampos()
        {
            string telefono = ObjVistaR.txtCiTel.Text.Trim();
            if (!EsValido(telefono))
            {
                MessageBox.Show("El campo Teléfono solo puede contener números y un solo guion.", "Validación de Teléfono", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string DUI = ObjVistaR.txtCiDUI.Text.Trim();
            if (!EsValido(DUI))
            {
                MessageBox.Show("El campo DUI solo puede contener números y un solo guion.", "Validación de DUI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string correo = ObjVistaR.txtCiCorreo.Text.Trim();
            CommonClasses commonClasses = new CommonClasses();
            if (!commonClasses.EsCorreoValido(correo))
            {
                MessageBox.Show("El campo Correo Electrónico no tiene un formato válido.", "Validación de Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(ObjVistaR.txtCiNombre.Text.Trim()) ||
                string.IsNullOrEmpty(ObjVistaR.txtCiApellido.Text.Trim()) ||
                string.IsNullOrEmpty(ObjVistaR.txtCiObs.Text.Trim())
                )
            {
                MessageBox.Show("Existen campos vacíos, complete cada uno de los apartados", "Proceso interrumpido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        
        private bool EsValido(string valor)
        {
            int guionCount = 0;
            foreach (char c in valor)
            {
                if (c == '-')
                {
                    guionCount++;
                }
                else if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return guionCount <= 1;
        }
        public ControllerScheduleAppointment(ViewScheduleAppointment Vista, int p_accion, DateTime Vis_fcita, string Vis_nombre, string Vis_apellido, string Vis_tel, string Vis_correo, string Vis_dui, string Vis_obser)
        {
            // Acciones iniciales
            ObjVistaR = Vista;
            this.accion = p_accion;

            // Verificar la acción a realizar
            verificarAccion();

            // Cargar los valores en la vista
            Cargarvalores(Vis_fcita, Vis_nombre, Vis_apellido, Vis_tel, Vis_correo, Vis_dui, Vis_obser);

            // Métodos que se ejecutan al ocurrir eventos
            ObjVistaR.btnActualizar.Click += new EventHandler(ActualizarRegistro);
            // ObjAddUser.btnFoto.Click += new EventHandler(ChargePhoto);
        }

        public void Cargarvalores(DateTime Vis_fcita, string Vis_nombre, string Vis_apellido, string Vis_tel, string Vis_correo, string Vis_dui, string Vis_obser)
        {
            try
            {
                // Asignación correcta
                ObjVistaR.txtCiDUI.Text = Vis_obser; // Campo para DUI
                ObjVistaR.txtCiTel.Text = Vis_tel; // Campo para Teléfono
                ObjVistaR.txtCiObs.Text = Vis_dui; // Campo para Observaciones
                ObjVistaR.DTPfechacita.Value = Vis_fcita; // Control DateTimePicker para Fecha
                ObjVistaR.txtCiNombre.Text = Vis_nombre; // Campo para Nombre
                ObjVistaR.txtCiApellido.Text = Vis_apellido; // Campo para Apellido
                ObjVistaR.txtCiCorreo.Text = Vis_correo; // Campo para Correo
            }
            catch (Exception ex)
            {
                // Muestra el mensaje de error si ocurre una excepción
                MessageBox.Show($"{ex.Message}", "Error al cargar valores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarRegistro(object sender, EventArgs e)
        {
            DAOScheduleAppointment DAOActualizar = new DAOScheduleAppointment
            {
                Vis_dui = ObjVistaR.txtCiDUI.Text.Trim(),
                Vis_tel = ObjVistaR.txtCiTel.Text.Trim(),
                Vis_obser = ObjVistaR.txtCiObs.Text.Trim(),
                Vis_fcita = ObjVistaR.DTPfechacita.Value,
                Vis_nombre = ObjVistaR.txtCiNombre.Text.Trim(),
                Vis_apellido = ObjVistaR.txtCiApellido.Text.Trim(),
                Vis_correo = ObjVistaR.txtCiCorreo.Text.Trim()
            };

            int valorRetornado = DAOActualizar.ActualizarVisita();
            if (valorRetornado > 0)
            {
                MessageBox.Show("Los datos han sido actualizados exitosamente",
                                "Proceso completado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                ObjVistaR.Close();

            }
            else if (valorRetornado == 0)
            {
                MessageBox.Show("No se encontró el registro para actualizar",
                                "Proceso interrumpido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Los datos no pudieron ser actualizados debido a un error inesperado",
                                "Proceso interrumpido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        public void verificarAccion()
        {
            if (accion == 1)
            {
                ObjVistaR.btnAgendar.Enabled = true;
                ObjVistaR.btnActualizar.Enabled = false;
            }
            else if (accion == 2)
            {
                ObjVistaR.btnAgendar.Enabled = false;
                ObjVistaR.btnActualizar.Enabled = true;
            }
        }
    }
}
