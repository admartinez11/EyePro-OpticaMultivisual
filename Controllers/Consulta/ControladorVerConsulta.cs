﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdministrarClientes.View.RegistroCliente;
using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views;
using OpticaMultivisual.Views.Consultas;

namespace OpticaMultivisual.Controllers.Consulta
{
    internal class ControladorVerConsulta
    {
        AñadirConsulta ObjAañadirConsulta;
        private int accion;


        public ControladorVerConsulta(AñadirConsulta objAañadirConsulta, int accion)
        {
            ObjAañadirConsulta = objAañadirConsulta;
            objAañadirConsulta.Load += new EventHandler(CargaInicio);
            this.accion = accion;
            verificarAccion();
            objAañadirConsulta.btnAgendar.Click += new EventHandler(NuevaConsulta);
        }
        void CargaInicio(object sender, EventArgs e)
        {
            LlenarComboDui();
            LlenarComboVisita();
            LlenarComboEmpleados();
        }
        void LlenarComboDui()
        {
            DAOConsulta DaoDui = new DAOConsulta();
            DataSet dataSet = DaoDui.ObtenerDUI();
            ObjAañadirConsulta.cmbDUI.DataSource = dataSet.Tables["Cliente"];
            ObjAañadirConsulta.cmbDUI.DisplayMember = "cli_nombre";
            ObjAañadirConsulta.cmbDUI.ValueMember = "cli_dui";

        }
        void LlenarComboVisita()
        {
            DAOConsulta DaoDui = new DAOConsulta();
            DataSet dataSet = DaoDui.ObtenerVisita();
            ObjAañadirConsulta.cmbVisita.DataSource = dataSet.Tables["Visita"];
            ObjAañadirConsulta.cmbVisita.DisplayMember = "vis_dui";
            ObjAañadirConsulta.cmbVisita.ValueMember = "vis_ID";
        }
        void LlenarComboEmpleados()
        {
            DAOConsulta DaoDui = new DAOConsulta();
            DataSet dataSet = DaoDui.ObtenerEmpleado();
            ObjAañadirConsulta.cmbEmpleado.DataSource = dataSet.Tables["Empleado"];
            ObjAañadirConsulta.cmbEmpleado.DisplayMember = "emp_nombre";
            ObjAañadirConsulta.cmbEmpleado.ValueMember = "emp_ID";
        }
        public void NuevaConsulta(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                // Crear una nueva instancia de DAOConsulta con los valores correctos
                DAOConsulta Consulta = new DAOConsulta
                {
                    Con_fechahora = DateTime.Parse(ObjAañadirConsulta.DTPfechaconsulta.Text.Trim()),
                    Con_obser = ObjAañadirConsulta.txtObservaciones.Text.Trim(),
                    Cli_DUI = ObjAañadirConsulta.cmbDUI.SelectedValue.ToString().Trim(),
                    Vis_ID = ObjAañadirConsulta.cmbVisita.SelectedValue.ToString().Trim(),
                    Emp_ID = ObjAañadirConsulta.cmbEmpleado.SelectedValue.ToString().Trim(),
                };

                // Registrar la consulta en la base de datos
                int valorRetornado = Consulta.RegistrarCliente();

                // Verificar el resultado de la inserción
                if (valorRetornado == 1)
                {
                    MessageBox.Show("Los datos han sido registrados exitosamente",
                                    "Proceso completado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    ObjAañadirConsulta.Close();
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

        public ControladorVerConsulta(AñadirConsulta Vista, int p_accion, string cli_DUI, string vis_ID, DateTime con_fechahora, string con_obser, string emp_ID, int con_ID)
        {
            ObjAañadirConsulta = Vista;
            ObjAañadirConsulta.Load += new EventHandler(CargaInicio);
            this.accion = p_accion;
            verificarAccion();
            CargarValores(cli_DUI, vis_ID, con_fechahora, con_obser, emp_ID, con_ID);
            ObjAañadirConsulta.btnActualizar.Click += new EventHandler(AcualizarRegistro);
        }
        public void CargarValores(string cli_DUI, string vis_ID, DateTime con_fechahora, string con_obser, string emp_ID, int con_ID)
        {
            try
            {
                ObjAañadirConsulta.txtObservaciones.Text = con_obser;
                ObjAañadirConsulta.cmbDUI.Text = cli_DUI;
                ObjAañadirConsulta.cmbVisita.Text = vis_ID;
                ObjAañadirConsulta.cmbEmpleado.Text = emp_ID;
                ObjAañadirConsulta.DTPfechaconsulta.Value = con_fechahora;
                ObjAañadirConsulta.txtConID.Text = con_ID.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar valores: {ex.Message}");
            }
        }
        public void AcualizarRegistro(object sender, EventArgs e)
        {
            try
            {
                // Verificar que los campos estén correctamente llenos y validados
                if (ValidarCampos())
                {
                    DAOConsulta DAOActualizar = new DAOConsulta();

                    // Verificar si los elementos seleccionados en los ComboBox son válidos
                    if (ObjAañadirConsulta.cmbDUI.SelectedItem == null ||
                        ObjAañadirConsulta.cmbVisita.SelectedItem == null ||
                        ObjAañadirConsulta.cmbEmpleado.SelectedItem == null)
                    {
                        MessageBox.Show("Uno o más valores seleccionados no son válidos. Por favor, selecciona un valor existente.",
                                        "Error de selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Accediendo directamente a las propiedades de DataRowView para los ComboBox
                    DAOActualizar.Cli_DUI = ((DataRowView)ObjAañadirConsulta.cmbDUI.SelectedItem)["cli_DUI"].ToString().Trim();
                    DAOActualizar.Vis_ID = ((DataRowView)ObjAañadirConsulta.cmbVisita.SelectedItem)["vis_ID"].ToString().Trim();
                    DAOActualizar.Emp_ID = ((DataRowView)ObjAañadirConsulta.cmbEmpleado.SelectedItem)["emp_ID"].ToString().Trim();

                    // Solo lectura del TextBox para con_ID
                    ObjAañadirConsulta.txtConID.Enabled = false;

                    // Asignación de los demás valores con validación de fecha
                    if (!VerificarFecha())
                    {
                        return; // Si la fecha no es válida, salir del método
                    }

                    DAOActualizar.Con_fechahora = DateTime.Parse(ObjAañadirConsulta.DTPfechaconsulta.Text.Trim());
                    DAOActualizar.Con_obser = ObjAañadirConsulta.txtObservaciones.Text.Trim();
                    DAOActualizar.Con_ID = int.Parse(ObjAañadirConsulta.txtConID.Text.Trim());

                    int valorRetornado = DAOActualizar.ActualizarConsulta();

                    if (valorRetornado == 1)
                    {
                        MessageBox.Show("Los datos han sido actualizados exitosamente",
                                        "Proceso completado",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        ObjAañadirConsulta.Close();
                    }
                    else if (valorRetornado == 0)
                    {
                        MessageBox.Show("Los datos no pudieron ser actualizados completamente",
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
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Ha ocurrido un error al intentar actualizar la consulta. Asegúrese de que todos los campos sean válidos y no estén vacíos.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            // Primero, verifica que todos los campos obligatorios estén llenos
            if (!VerificacionCamposLlenos())
            {
                return false;
            }

            // Luego, verifica que la fecha sea válida
            if (!VerificarFecha())
            {
                return false;
            }

            // Finalmente, verifica que el texto en txtObservaciones sea válido
            if (!VerificarTexto(ObjAañadirConsulta.txtObservaciones.Text))
            {
                return false;
            }
            //if (!CoincidanValores())
            //{
            //    return false;
            //}
            // Si todas las validaciones pasan, retorna true
            return true;
        }
       // private bool CoincidanValores()
       // {

       //     if (ObjAañadirConsulta.cmbDUI.SelectedItem?.ToString() == ObjAañadirConsulta.cmbVisita.SelectedItem?.ToString())
            //{
            //    return true;
            //}
            //else
            //{
            //    MessageBox.Show("Los valores no coinciden.");
            //}
            //return true;
        //}

        private bool VerificacionCamposLlenos()
        {
            // Validar que los campos obligatorios estén llenos
            if (string.IsNullOrWhiteSpace(ObjAañadirConsulta.cmbDUI.Text))
            {
                MessageBox.Show("Por favor, seleccione un DUI.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(ObjAañadirConsulta.cmbVisita.Text))
            {
                MessageBox.Show("Por favor, seleccione una Visita.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(ObjAañadirConsulta.cmbEmpleado.Text))
            {
                MessageBox.Show("Por favor, seleccione un Empleado.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(ObjAañadirConsulta.txtObservaciones.Text))
            {
                MessageBox.Show("Por favor, ingrese alguna Observación.", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Si todas las validaciones pasan, se retorna true.
            return true;
        }

        private bool VerificarFecha()
        {
            DateTime fechaSeleccionada = ObjAañadirConsulta.DTPfechaconsulta.Value;
            DateTime fechaActual = DateTime.Now;

            // Compara si la fecha seleccionada es anterior a la fecha y hora actuales.
            if (fechaSeleccionada < fechaActual)
            {
                MessageBox.Show("La fecha y hora no pueden ser anteriores al momento actual.", "Fecha Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Si la fecha seleccionada pasa ambas validaciones, se retorna true, indicando que la fecha es válida.
            return true;
        }
        private bool VerificarTexto(string texto)
        {
            // Define un conjunto de caracteres que no están permitidos en secuencia.
            char[] caracteresNoPermitidos = { ',', '.', ';', ':', '/', '\\', '-', '_', '!', '?', '@', '#', '$', '%', '^', '&', '*', '(', ')', '[', ']', '{', '}', '<', '>', '|', '`', '~' };

            // Recorre el texto para verificar si hay signos repetidos consecutivos.
            for (int i = 0; i < texto.Length - 1; i++)
            {
                // Compara el carácter actual con el siguiente carácter en la cadena.
                if (caracteresNoPermitidos.Contains(texto[i]) && caracteresNoPermitidos.Contains(texto[i + 1]))
                {
                    // Si se encuentran signos repetidos consecutivos, muestra un mensaje de advertencia
                    // y retorna false para indicar que la validación ha fallado.
                    MessageBox.Show("El texto no puede tener signos consecutivos.", "Texto Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Verifica si la longitud del texto supera los 100 caracteres.
            if (texto.Length > 100)
            {
                // Si el texto es demasiado largo, muestra un mensaje de advertencia
                // y retorna false para indicar que la validación ha fallado.
                MessageBox.Show("El texto no puede tener más de 100 caracteres.", "Texto Demasiado Largo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Si el texto pasa todas las verificaciones, retorna true,
            // indicando que la validación ha sido exitosa.
            return true;
        }

        public void verificarAccion()
        {
            if (accion == 1) // Acción 1: Agregar
            {
                ObjAañadirConsulta.btnAgendar.Enabled = true;
                ObjAañadirConsulta.btnActualizar.Enabled = false;
            }
            else if (accion == 2) // Acción 2: Actualizar
            {
                ObjAañadirConsulta.btnAgendar.Enabled = false;
                ObjAañadirConsulta.btnActualizar.Enabled = true;
            }
        }
    }
}
