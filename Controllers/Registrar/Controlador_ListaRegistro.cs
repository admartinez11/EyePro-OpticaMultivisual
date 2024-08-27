﻿using OpticaMultivisual.Models.DTO;
using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdministrarClientes.View.RegistroCliente;



namespace OpticaMultivisual.Controllers
{
    class Controlador_ListaRegistro
    {
        AdministrarCientes ObjVista;
        public Controlador_ListaRegistro(AdministrarCientes Vista)
        {
            ObjVista = Vista;
            ObjVista.Load += new EventHandler(CargarInfo);
            ObjVista.btnBuscar.Click += new EventHandler(BuscarClientesControlador);
            ObjVista.btnNuevoCli.Click += new EventHandler(AgregarCliente);
            ObjVista.btnEliminarCli.Click += new EventHandler(EliminarCliente);
            ObjVista.btnActCli.Click += new EventHandler(ActualizarCliente);
        }
        public void CargarInfo(object sender, EventArgs e)
        {
            ActualizarDatos();
        }
        public void ActualizarDatos()
        {
            DAORegistro ObjRegistro = new DAORegistro();
            DataSet ds = ObjRegistro.ObtenerInfoClientes();
            ObjVista.dgvInfoClientes.DataSource = ds.Tables["Cliente"];

        }
        public void BuscarClientesControlador(object sender, EventArgs e)
        {
            DAORegistro Objadminregistro = new DAORegistro();
            DataSet ds = Objadminregistro.BuscarClientes(ObjVista.txtBuscar.Text.Trim());
            ObjVista.dgvInfoClientes.DataSource = ds.Tables["Cliente"];
        }
        public void AgregarCliente(object sender, EventArgs e)
        {
            RegistroClientes openForm = new RegistroClientes(1);
            openForm.ShowDialog();
            ActualizarDatos();
        }
        private void EliminarCliente(object sender, EventArgs e)
        {
            int pos = ObjVista.dgvInfoClientes.CurrentRow.Index;
            if (MessageBox.Show($"¿Esta seguro que desea elimar a:\n {ObjVista.dgvInfoClientes[1, pos].Value.ToString()} {ObjVista.dgvInfoClientes[2, pos].Value.ToString()}.\nConsidere que dicha acción no se podrá revertir.", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DAORegistro daoDel = new DAORegistro();
                daoDel.DUI = ObjVista.dgvInfoClientes[0, pos].Value.ToString();
                int valorRetornado = daoDel.EliminarUsuario();
                if (valorRetornado == 1)
                {
                    MessageBox.Show("Registro eliminado", "Acción completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarDatos();
                }
                else
                {
                    MessageBox.Show("Registro no pudo ser eliminado, verifique que el registro no tenga datos asociados.", "Acción interrumpida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        public void ActualizarCliente(object sender, EventArgs e)
        {
            int pos = ObjVista.dgvInfoClientes.CurrentRow.Index;
            RegistroClientes openForm = new RegistroClientes(2,
            ObjVista.dgvInfoClientes[0, pos].Value.ToString(),        // DUI
            ObjVista.dgvInfoClientes[1, pos].Value.ToString(),     // Nombre
            ObjVista.dgvInfoClientes[2, pos].Value.ToString(),   // Apellido
            ObjVista.dgvInfoClientes[3, pos].Value.ToString(),   // Teléfono
            char.Parse(ObjVista.dgvInfoClientes[5, pos].Value.ToString()), // Género
            ObjVista.dgvInfoClientes[4, pos].Value.ToString(), // Edad
            ObjVista.dgvInfoClientes[6, pos].Value.ToString(),   // Correo_E
            ObjVista.dgvInfoClientes[7, pos].Value.ToString(),  // Profesión
            ObjVista.dgvInfoClientes[8, pos].Value.ToString() // Padecimientos
            );

            openForm.ShowDialog();
            ActualizarDatos();
        }

    }
}
