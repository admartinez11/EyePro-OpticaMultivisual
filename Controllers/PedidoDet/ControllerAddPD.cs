using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Dashboard.Optometrista;
using OpticaMultivisual.Views.Dashboard.PedidoDet;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpticaMultivisual.Controllers.Helper;

namespace OpticaMultivisual.Controllers.Dashboard.PedidoDetalle
{
    internal class ControllerAddPD
    {
        ViewAddPedidoDet ObjAddPD;
        public int accion;

        public ControllerAddPD(ViewAddPedidoDet frmAddDR, int accion)
        {
            ObjAddPD = frmAddDR;
            this.accion = accion;
            frmAddDR.Load += new EventHandler(CargaInicial);
            verificarAccion();
            //ObjAddPD.btnActualizar.Click += new EventHandler(UpdateRegister);
            ObjAddPD.btnAgregar.Click += new EventHandler(NewRegister);
        }

        //Actualizar shit
        public ControllerAddPD(ViewAddPedidoDet Vista, int accion, int pd_ID, int con_ID, DateTime pd_fpedido, DateTime pd_fprogramada, string art_codigo, int art_cant, string pd_obser, int pd_recetalab)
        {
            //Acciones Iniciales
            ObjAddPD = Vista;
            this.accion = accion;
            Vista.Load += new EventHandler(CargaInicial);
            //Métodos iniciales: estos metodos se ejecutan cuando el formulario está cargando
            verificarAccion();
            ChargeValues(pd_ID, con_ID, pd_fpedido, pd_fprogramada, art_codigo, art_cant, pd_obser, pd_recetalab);
            //Métodos que se ejecutan al ocurrir eventos
            //ObjAddPD.btnAgregar.Click += new EventHandler(NewRegister);
            ObjAddPD.btnActualizar.Click += new EventHandler(UpdateRegister);
        }


        void CargaInicial(object sender, EventArgs e)
        {
            LlenarcomboCodigoArt();
            LlenarcomboConsultaID();
        }

        void LlenarcomboCodigoArt()
        {
            DAOPedidoDet daoArt = new DAOPedidoDet();
            DataSet ds = daoArt.ObtenerArticulos();
            ObjAddPD.cbart_codigo.DataSource = ds.Tables["Articulo"];
            ObjAddPD.cbart_codigo.DisplayMember = "art_codigo";
            ObjAddPD.cbart_codigo.ValueMember = "art_codigo";
        }

        void LlenarcomboConsultaID()
        {
            DAOPedidoDet daoCon = new DAOPedidoDet();
            DataSet ds = daoCon.ObtenerConsulta();
            ObjAddPD.cbcon_ID.DataSource = ds.Tables["Consulta"];
            ObjAddPD.cbcon_ID.DisplayMember = "con_ID";
            ObjAddPD.cbcon_ID.ValueMember = "con_ID";
        }

        public void verificarAccion()
        {
            if (accion == 1)
            {
                ObjAddPD.btnAgregar.Enabled = true;
                ObjAddPD.btnActualizar.Enabled = false;
            }
            else if (accion == 2)
            {
                ObjAddPD.btnAgregar.Enabled = false;
                ObjAddPD.btnActualizar.Enabled = true;
            }
        }

        public void NewRegister(object sender, EventArgs e)
        {
            //Se crea una instancia de la clase DAOPedidoDet llamada DAOInsert
            DAOPedidoDet DAOInsert = new DAOPedidoDet();
            DAOInsert.con_ID1 = ObjAddPD.cbcon_ID.SelectedValue.ToString();
            DAOInsert.pd_fpedido1 = ObjAddPD.DTPpd_fpedido.Value;
            DAOInsert.pd_fprogramada1 = ObjAddPD.DTPpd_fprogramada.Value;
            DAOInsert.art_codigo1 = ObjAddPD.cbart_codigo.SelectedValue.ToString();
            DAOInsert.art_cant1 = int.Parse(ObjAddPD.txtart_cant.Text.ToString());
            if (ObjAddPD.cbpd_recetalab.Checked == true)
            {
                DAOInsert.pd_recetalab1 = 1;
            }
            else
            {
                DAOInsert.pd_recetalab1 = 0;
            }
            DAOInsert.pd_obser1 = ObjAddPD.txtpd_obser.Text.Trim();
            int valorRetornado = DAOInsert.InsertarPedido();
            MessageBox.Show($"Valor Retonado{valorRetornado}",
                                "xx",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            //Se verifica el valor que retornó el método anterior y que fue almacenado en la variable valorRetornado
            if (valorRetornado == 1)
            {
                MessageBox.Show("Los datos han sido registrados exitosamente",
                                "Proceso completado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                ObjAddPD.Close();
            }
            else
            {
                MessageBox.Show("Los datos no pudieron ser registrados",
                                "Proceso interrumpido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        public void UpdateRegister(object sender, EventArgs e)
        {
            DAOPedidoDet daoUpdate = new DAOPedidoDet();

            daoUpdate.pd_ID1 = int.Parse(ObjAddPD.txtpd_ID.Text.ToString());
            daoUpdate.con_ID1 = ObjAddPD.cbcon_ID.SelectedValue.ToString();
            daoUpdate.pd_fpedido1 = ObjAddPD.DTPpd_fpedido.Value;
            daoUpdate.pd_fprogramada1 = ObjAddPD.DTPpd_fprogramada.Value;
            daoUpdate.art_codigo1 = ObjAddPD.cbart_codigo.SelectedValue.ToString();
            daoUpdate.art_cant1 = int.Parse(ObjAddPD.txtart_cant.Text.ToString());
            if (ObjAddPD.cbpd_recetalab.Checked == true)
            {
                daoUpdate.pd_recetalab1 = 1;
            }
            else
            {
                daoUpdate.pd_recetalab1 = 0;
            }
            daoUpdate.pd_obser1 = ObjAddPD.txtpd_obser.Text.Trim();
            int valorRetornado = daoUpdate.ActualizarPD();
            MessageBox.Show($"valor retornado{valorRetornado}",
                                "xx",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            if (valorRetornado == 1)
            {
                MessageBox.Show("Los datos han sido actualizado exitosamente",
                                "Proceso completado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                ObjAddPD.Close();
            }
            else if (valorRetornado == 2)
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

        //private bool ValidarCampos()
        //{
        //    CommonClasses commonClasses = new CommonClasses();

        //    string art_codigo = ObjAddPD.cbart_codigo.SelectedValue.ToString();
        //    // Validaciones de campos NOT NULL y longitud de caracteres
        //    if (string.IsNullOrWhiteSpace(ObjAddPD.cbart_codigo.SelectedValue.Trim()))
        //    {
        //        MessageBox.Show("El campo del Código de Artículo", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    else if (ObjAddPD.cbart_codigo.SelectedValue.ToString().Length > 50)
        //    {
        //        MessageBox.Show("El campo del Código de Artículo no debe de exceder el máximo de caracteres.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //}

        public void ChargeValues(int pd_ID, int con_ID, DateTime pd_fpedido, DateTime pd_fprogramada, string art_codigo, int art_cant, string pd_obser, int pd_recetalab)
        {
            ObjAddPD.txtpd_ID.Text = pd_ID.ToString();
            ObjAddPD.cbcon_ID.SelectedValue = con_ID;
            ObjAddPD.DTPpd_fpedido.Value = pd_fpedido;
            ObjAddPD.DTPpd_fpedido.Value = pd_fprogramada;
            ObjAddPD.cbart_codigo.SelectedValue = art_codigo;
            ObjAddPD.txtart_cant.Text = art_cant.ToString();
            ObjAddPD.txtpd_obser.Text = pd_obser;
            if (pd_recetalab == 1)
            {
                ObjAddPD.cbpd_recetalab.Checked = true;
            }
            else
            {
                ObjAddPD.cbpd_recetalab.Checked = false;
            }
        }
    }
}