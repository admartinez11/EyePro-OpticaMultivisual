﻿using OpticaMultivisual.Controllers.Helper;
using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Dashboard.Optometrista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Controllers.Dashboard.Optometrista
{
    internal class ControllerAddLens
    {
        ViewAddLens ObjAddLens;
        private int accion;

        public ControllerAddLens(ViewAddLens Vista, int accion, int lens_ID, int con_ID, string OD_esfera, string OD_cilindro, string OD_eje, string OD_prisma, string OD_adicion, string OI_esfera, string OI_cilindro, string OI_eje, string OI_prisma, string OI_adicion)
        {
            //Acciones Iniciales
            ObjAddLens = Vista;
            this.accion = accion;
            Vista.Load += new EventHandler(CargaInicial);
            //Métodos iniciales: estos metodos se ejecutan cuando el formulario está cargando
            verificarAccion();
            ChargeValues(lens_ID, con_ID, OD_esfera, OD_cilindro, OD_eje, OD_prisma, OD_adicion, OI_esfera, OI_cilindro, OI_eje, OI_prisma, OI_adicion);
            //Métodos que se ejecutan al ocurrir eventos
            ObjAddLens.btnGuardar.Click += new EventHandler(NewRegister);
            ObjAddLens.btnActualizar.Click += new EventHandler(UpdateRegister);

        }

        public ControllerAddLens(ViewAddLens frmAddLens, int accion)
        {
            ObjAddLens = frmAddLens;
            this.accion = accion;
            frmAddLens.Load += new EventHandler(CargaInicial);
            verificarAccion();
            ObjAddLens.btnActualizar.Click += new EventHandler(UpdateRegister);
            ObjAddLens.btnGuardar.Click += new EventHandler(NewRegister);
        }

        void CargaInicial(object sender, EventArgs e)
        {
            LlenarcomboCodigoArt();
        }

        void LlenarcomboCodigoArt()
        {
            DAOLens daoArt = new DAOLens();
            DataSet ds = daoArt.ObtenerConsulta();
            ObjAddLens.cbcon_ID.DataSource = ds.Tables["Consulta"];
            ObjAddLens.cbcon_ID.DisplayMember = "con_ID";
            ObjAddLens.cbcon_ID.ValueMember = "con_ID";
        }

        public void verificarAccion()
        {
            if (accion == 1)
            {
                ObjAddLens.btnGuardar.Enabled = true;
                ObjAddLens.btnActualizar.Enabled = false;
            }
            else if (accion == 2)
            {
                ObjAddLens.btnGuardar.Enabled = false;
                ObjAddLens.btnActualizar.Enabled = true;
            }
        }

        public void NewRegister(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                //Se crea una instancia de la clase DAOLens llamada DAOInsert
                DAOLens DAOInsert = new DAOLens();
                //Datos para creación de persona
                //OD
                DAOInsert.con_ID1 = int.Parse(ObjAddLens.cbcon_ID.SelectedValue.ToString());
                DAOInsert.OD_esfera1 = ObjAddLens.txtODEsfera.Text.Trim();
                DAOInsert.OD_cilindro1 = ObjAddLens.txtODCilindro.Text.Trim();
                DAOInsert.OD_eje1 = ObjAddLens.txtODEje.Text.Trim();
                DAOInsert.OD_prisma1 = ObjAddLens.txtODPrisma.Text.Trim();
                DAOInsert.OD_adicion1 = ObjAddLens.txtODAdicion.Text.Trim();
                //OI
                DAOInsert.OI_esfera1 = ObjAddLens.txtOIEsfera.Text.Trim();
                DAOInsert.OI_cilindro1 = ObjAddLens.txtOICilindro.Text.Trim();
                DAOInsert.OI_eje1 = ObjAddLens.txtOIEje.Text.Trim();
                DAOInsert.OI_prisma1 = ObjAddLens.txtOIPrisma.Text.Trim();
                DAOInsert.OI_adicion1 = ObjAddLens.txtOIAdicion.Text.Trim();

                int valorRetornado = DAOInsert.InsertarLens();
                MessageBox.Show($"{valorRetornado}",
                                    "Proceso interrumpido",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                //Se verifica el valor que retornó el metodo anterior y que fue almacenado en la variable valorRetornado
                if (valorRetornado == 1)
                {
                    MessageBox.Show("Los datos han sido registrados exitosamente",
                                    "Proceso completado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    ObjAddLens.Close();
                }
                else
                {
                    MessageBox.Show("EPV006 - Los datos no pudieron ser registrados",
                                    "Proceso interrumpido",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            
        }

        public void UpdateRegister(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                DAOLens daoUpdate = new DAOLens();
                daoUpdate.lens_ID1 = int.Parse(ObjAddLens.txtDRid.Text.ToString());
                daoUpdate.con_ID1 = int.Parse(ObjAddLens.cbcon_ID.SelectedValue.ToString());
                daoUpdate.OD_esfera1 = ObjAddLens.txtODEsfera.Text.Trim();
                daoUpdate.OD_cilindro1 = ObjAddLens.txtODCilindro.Text.Trim();
                daoUpdate.OD_eje1 = ObjAddLens.txtODEje.Text.Trim();
                daoUpdate.OD_prisma1 = ObjAddLens.txtODPrisma.Text.Trim();
                daoUpdate.OD_adicion1 = ObjAddLens.txtODAdicion.Text.Trim();
                //OI
                daoUpdate.OI_esfera1 = ObjAddLens.txtOIEsfera.Text.Trim();
                daoUpdate.OI_cilindro1 = ObjAddLens.txtOICilindro.Text.Trim();
                daoUpdate.OI_eje1 = ObjAddLens.txtOIEje.Text.Trim();
                daoUpdate.OI_prisma1 = ObjAddLens.txtOIPrisma.Text.Trim();
                daoUpdate.OI_adicion1 = ObjAddLens.txtOIAdicion.Text.Trim();

                int valorRetornado = daoUpdate.ActualizarLens();
                if (valorRetornado == 1)
                {
                    MessageBox.Show("Los datos han sido actualizado exitosamente",
                                    "Proceso completado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    ObjAddLens.Close();
                }
                else if (valorRetornado == 2)
                {
                    MessageBox.Show("EPV002 -Los datos no pudieron ser actualizados completamente",
                                    "Proceso interrumpido",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("EPV001 - Error inesperado",
                                    "Proceso interrumpido",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarCampos()
        {
            CommonClasses commonClasses = new CommonClasses();

            string OD_esfera = ObjAddLens.txtODEsfera.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OD_esfera))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!Regex.IsMatch(OD_esfera, @"^\d+$"))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Derecho Esfera",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }

            string OD_cilindro = ObjAddLens.txtODCilindro.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OD_cilindro))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!OD_cilindro.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Derecho Cilindro",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }
            // Validamos la cantidad de puntos decimales
            int dotCount1 = OD_cilindro.Split('.').Length - 1;
            if (dotCount1 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Derecho Cilindro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OD_eje = ObjAddLens.txtODEje.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OD_eje))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!OD_eje.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Derecho Eje",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }
            // Validamos la cantidad de puntos decimales
            int dotCount2 = OD_eje.Split('.').Length - 1;
            if (dotCount2 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Derecho Eje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OD_prisma = ObjAddLens.txtODPrisma.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OD_prisma))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!Regex.IsMatch(OD_prisma, @"^\d+$"))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Derecho Prisma",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }

            string OD_adicion = ObjAddLens.txtODAdicion.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OD_adicion))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!Regex.IsMatch(OD_adicion, @"^\d+$"))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Derecho Adicion",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }

            string OI_esfera = ObjAddLens.txtOIEsfera.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OI_esfera))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!Regex.IsMatch(OI_esfera, @"^\d+$"))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Izquierdo Esfera",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }

            string OI_cilindro = ObjAddLens.txtOICilindro.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OI_cilindro))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!OI_cilindro.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Izquierdo Cilindro",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }
            // Validamos la cantidad de puntos decimales
            int dotCount3 = OI_cilindro.Split('.').Length - 1;
            if (dotCount3 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Izquierdo Cilindro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OI_eje = ObjAddLens.txtOIEje.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OI_eje))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!OI_eje.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Izquierdo Eje",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }
            // Validamos la cantidad de puntos decimales
            int dotCount4 = OI_eje.Split('.').Length - 1;
            if (dotCount4 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Izquierdo Eje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OI_prisma = ObjAddLens.txtOIPrisma.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OI_prisma))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!Regex.IsMatch(OI_prisma, @"^\d+$"))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Izquierdo Prisma",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }

            string OI_adicion = ObjAddLens.txtOIAdicion.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OI_adicion))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!Regex.IsMatch(OI_adicion, @"^\d+$"))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Izquierdo Adicion",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }


            if (ObjAddLens.txtODEsfera.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Derecho Esfera.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumeroConPunto(ObjAddLens.txtODEsfera.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho Esfera contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddLens.txtODCilindro.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho Cilindro contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validar el valor decimal usando el método ValidarDecimal
            if (!commonClasses.ValidarDecimal(OD_cilindro))
            {
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddLens.txtODEje.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho Eje contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validar el valor decimal usando el método ValidarDecimal
            if (!commonClasses.ValidarDecimal(OD_eje))
            {
                return false;
            }

            if (ObjAddLens.txtODPrisma.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Derecho Prisma.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddLens.txtODPrisma.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho Prisma contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (ObjAddLens.txtODAdicion.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Derecho Adicion.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddLens.txtODAdicion.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho Adicion contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (ObjAddLens.txtOIEsfera.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Izquierdo Esfera.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumeroConPunto(ObjAddLens.txtOIEsfera.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo Esfera contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddLens.txtOICilindro.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo Cilindro contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validar el valor decimal usando el método ValidarDecimal
            if (!commonClasses.ValidarDecimal(OI_cilindro))
            {
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddLens.txtOIEje.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo Eje contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validar el valor decimal usando el método ValidarDecimal
            if (!commonClasses.ValidarDecimal(OI_eje))
            {
                return false;
            }

            if (ObjAddLens.txtOIPrisma.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Izquierdo Prisma.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddLens.txtOIPrisma.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo Prisma contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (ObjAddLens.txtOIAdicion.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Izquierdo Adicion.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddLens.txtOIAdicion.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo Adicion contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Si todas las validaciones pasan, se puede continuar con el proceso
            return true;
        }

        public void ChargeValues(int lens_ID, int con_ID, string OD_esfera, string OD_cilindro, string OD_eje, string OD_prisma, string OD_adicion, string OI_esfera, string OI_cilindro, string OI_eje, string OI_prisma, string OI_adicion)
        {
            //Asigna los valores recibidos a los campos correpondientes a la vista ObjAddLens
            //Valores de Ojo Derecho
            ObjAddLens.txtDRid.Text = lens_ID.ToString();
            ObjAddLens.cbcon_ID.SelectedValue = lens_ID.ToString();
            ObjAddLens.txtODEsfera.Text = OD_esfera;
            ObjAddLens.txtODCilindro.Text = OD_cilindro;
            ObjAddLens.txtODEje.Text = OD_eje;
            ObjAddLens.txtODPrisma.Text = OD_prisma;
            ObjAddLens.txtODAdicion.Text = OD_adicion;
            //Valores de Ojo Izquierdo
            ObjAddLens.txtOIEsfera.Text = OI_esfera;
            ObjAddLens.txtOICilindro.Text = OI_cilindro;
            ObjAddLens.txtOIEje.Text = OI_eje;
            ObjAddLens.txtOIPrisma.Text = OI_prisma;
            ObjAddLens.txtOIAdicion.Text = OI_adicion;
        }
    }
}
