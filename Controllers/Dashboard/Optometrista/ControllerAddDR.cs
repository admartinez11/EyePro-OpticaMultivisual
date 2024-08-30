using OpticaMultivisual.Views.Dashboard.Optometrista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpticaMultivisual.Models.DAO;
using System.Windows.Forms;
using System.Security.Cryptography;
using OpticaMultivisual.Controllers.Helper;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace OpticaMultivisual.Controllers.Dashboard.Optometrista
{
    internal class ControllerAddDR
    {
        ViewAddDR ObjAddDR;
        private int accion;
        //pruebaaaa

        public ControllerAddDR(ViewAddDR Vista, int accion, int DR_ID, string OD_esfera, double OD_cilindro, double OD_eje, int OD_prisma, int OD_adicion, int OD_AO, double OD_AP, double OD_DP, string OI_esfera, double OI_cilindro, double OI_eje, int OI_prisma, int OI_adicion, int OI_AO, double OI_AP, double OI_DP)
        {
            //Acciones Iniciales
            ObjAddDR = Vista;
            this.accion = accion;
            //Métodos iniciales: estos metodos se ejecutan cuando el formulario está cargando
            verificarAccion();
            ChargeValues(DR_ID, OD_esfera, OD_cilindro, OD_eje, OD_prisma, OD_adicion, OD_AO, OD_AP, OD_DP, OI_esfera, OI_cilindro, OI_eje, OI_prisma, OI_adicion, OI_AO, OI_AP, OI_DP);
            //Métodos que se ejecutan al ocurrir eventos
            ObjAddDR.btnGuardar.Click += new EventHandler(NewRegister);
            ObjAddDR.btnActualizar.Click += new EventHandler(UpdateRegister);

        }

        public ControllerAddDR(ViewAddDR frmAddDR, int accion)
        {
            ObjAddDR = frmAddDR;
            this.accion = accion;
            verificarAccion();
            ObjAddDR.btnActualizar.Click += new EventHandler(UpdateRegister);
            ObjAddDR.btnGuardar.Click += new EventHandler(NewRegister);
        }


        public void verificarAccion()
        {
            if (accion == 1)
            {
                ObjAddDR.btnGuardar.Enabled = true;
                ObjAddDR.btnActualizar.Enabled = false;
            }
            else if (accion == 2)
            {
                ObjAddDR.btnGuardar.Enabled = false;
                ObjAddDR.btnActualizar.Enabled = true;
            }
        }

        public void NewRegister(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                //Se crea una instancia de la clase DAO_DR llamada DAOInsert
                DAO_DR DAOInsert = new DAO_DR();
                //Datos para creación de persona
                //OD
                //DAOInsert.DR_ID1 = int.Parse(ObjAddDR.txtDRid.Text.ToString());
                DAOInsert.OD_esfera1 = ObjAddDR.txtODEsfera.Text.Trim();
                DAOInsert.OD_cilindro1 = ObjAddDR.txtODCilindro.Text.Trim();
                DAOInsert.OD_eje1 = ObjAddDR.txtODEje.Text.Trim();
                DAOInsert.OD_prisma1 = ObjAddDR.txtODPrisma.Text.Trim();
                DAOInsert.OD_adicion1 = ObjAddDR.txtODAdicion.Text.Trim();
                DAOInsert.OD_DP1 = ObjAddDR.txtODdp.Text.Trim();
                DAOInsert.OD_AO1 = ObjAddDR.txtODao.Text.Trim();
                DAOInsert.OD_AP1 = ObjAddDR.txtODap.Text.Trim();
                //OI
                DAOInsert.OI_esfera1 = ObjAddDR.txtOIEsfera.Text.Trim();
                DAOInsert.OI_cilindro1 = ObjAddDR.txtOICilindro.Text.Trim();
                DAOInsert.OI_eje1 = ObjAddDR.txtOIEje.Text.Trim();
                DAOInsert.OI_prisma1 = ObjAddDR.txtOIPrisma.Text.Trim();
                DAOInsert.OI_adicion1 = ObjAddDR.txtOIAdicion.Text.Trim();
                DAOInsert.OI_DP1 = ObjAddDR.txtOIdp.Text.Trim();
                DAOInsert.OI_AO1 = ObjAddDR.txtOIao.Text.Trim();
                DAOInsert.OI_AP1 = ObjAddDR.txtOIap.Text.Trim();

                int valorRetornado = DAOInsert.InsertarDR();
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
                    ObjAddDR.Close();
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

        public void UpdateRegister(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                DAO_DR daoUpdate = new DAO_DR();

                daoUpdate.DR_ID1 = int.Parse(ObjAddDR.txtDRid.Text.ToString());
                daoUpdate.OD_esfera1 = ObjAddDR.txtODEsfera.Text.Trim();
                daoUpdate.OD_cilindro1 = ObjAddDR.txtODCilindro.Text.ToString();
                daoUpdate.OD_eje1 = ObjAddDR.txtODEje.Text.ToString();
                daoUpdate.OD_prisma1 = ObjAddDR.txtODPrisma.Text.ToString();
                daoUpdate.OD_adicion1 = ObjAddDR.txtODAdicion.Text.ToString();
                daoUpdate.OD_DP1 = ObjAddDR.txtODdp.Text.ToString();
                daoUpdate.OD_AO1 = ObjAddDR.txtODao.Text.ToString();
                daoUpdate.OD_AP1 = ObjAddDR.txtODap.Text.ToString();
                //OI
                daoUpdate.OI_esfera1 = ObjAddDR.txtOIEsfera.Text.Trim();
                daoUpdate.OI_cilindro1 = ObjAddDR.txtOICilindro.Text.ToString();
                daoUpdate.OI_eje1 = ObjAddDR.txtOIEje.Text.ToString();
                daoUpdate.OI_prisma1 = ObjAddDR.txtOIPrisma.Text.ToString();
                daoUpdate.OI_adicion1 = ObjAddDR.txtOIAdicion.Text.ToString();
                daoUpdate.OI_DP1 = ObjAddDR.txtOIdp.Text.ToString();
                daoUpdate.OI_AO1 = ObjAddDR.txtOIao.Text.ToString();
                daoUpdate.OI_AP1 = ObjAddDR.txtOIap.Text.ToString();

                int valorRetornado = daoUpdate.ActualizarDR();
                if (valorRetornado == 1)
                {
                    MessageBox.Show("Los datos han sido actualizado exitosamente",
                                    "Proceso completado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    ObjAddDR.Close();
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
        }

        private bool ValidarCampos()
        {
            CommonClasses commonClasses = new CommonClasses();

            string OD_esfera = ObjAddDR.txtODEsfera.Text.Trim();
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

            string OD_cilindro = ObjAddDR.txtODCilindro.Text.Trim();
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
            int dotCount = OD_cilindro.Split('.').Length - 1;
            if (dotCount != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Derecho Cilindro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OD_eje = ObjAddDR.txtODEje.Text.Trim();
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
            int dotCount1 = OD_eje.Split('.').Length - 1;
            if (dotCount1 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Derecho Eje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OD_prisma = ObjAddDR.txtODPrisma.Text.Trim();
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

            string OD_adicion = ObjAddDR.txtODAdicion.Text.Trim();
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

            string OD_DP = ObjAddDR.txtODdp.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OD_DP))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!OD_DP.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Derecho DP",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }
            // Validamos la cantidad de puntos decimales
            int dotCount2 = OD_DP.Split('.').Length - 1;
            if (dotCount2 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Derecho DP",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OD_AO = ObjAddDR.txtODao.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OD_AO))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!Regex.IsMatch(OD_AO, @"^\d+$"))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Derecho AO",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }

            string OD_AP = ObjAddDR.txtODap.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OD_AP))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!OD_AP.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Derecho AP",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }
            // Validamos la cantidad de puntos decimales
            int dotCount3 = OD_AP.Split('.').Length - 1;
            if (dotCount3 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Derecho AP",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OI_esfera = ObjAddDR.txtODEsfera.Text.Trim();
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

            string OI_cilindro = ObjAddDR.txtOICilindro.Text.Trim();

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
            int dotCount4 = OD_cilindro.Split('.').Length - 1;
            if (dotCount4 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Izquierdo Cilindro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OI_eje = ObjAddDR.txtOIEje.Text.Trim();
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
            int dotCount5 = OI_eje.Split('.').Length - 1;
            if (dotCount5 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Izquierdo Eje",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OI_prisma = ObjAddDR.txtOIPrisma.Text.Trim();
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

            string OI_adicion = ObjAddDR.txtOIAdicion.Text.Trim();
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

            string OI_DP = ObjAddDR.txtOIdp.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OI_DP))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!OI_DP.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Izquierdo DP",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }
            // Validamos la cantidad de puntos decimales
            int dotCount6 = OI_DP.Split('.').Length - 1;
            if (dotCount6 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Izquierdo DP",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            string OI_AO = ObjAddDR.txtOIao.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OI_AO))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!Regex.IsMatch(OI_AO, @"^\d+$"))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Izquierdo AO",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }

            string OI_AP = ObjAddDR.txtOIap.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OI_AP))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (!OI_AP.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Izquierdo AP",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }
            // Validamos la cantidad de puntos decimales
            int dotCount7 = OI_AP.Split('.').Length - 1;
            if (dotCount7 != 1)  // Verifica si no hay o hay más de un punto decimal
            {
                MessageBox.Show("El tipo de valores ingresados es incorrecto. Debe contener un solo punto decimal.",
                                "Validación de Ojo Izquierdo AP",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            if (ObjAddDR.txtODEsfera.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Derecho Esfera.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtODEsfera.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho Esfera contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtODCilindro.Text.Trim()))
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

            if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtODEje.Text.Trim()))
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

            if (ObjAddDR.txtODPrisma.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Derecho Prisma.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddDR.txtODPrisma.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho Prisma contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (ObjAddDR.txtODAdicion.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Derecho Adicion.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddDR.txtODAdicion.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho Adicion contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtODdp.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho DP contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validar el valor decimal usando el método ValidarDecimal
            if (!commonClasses.ValidarDecimal(OD_DP))
            {
                return false;
            }

            if (ObjAddDR.txtODao.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Derecho AO.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddDR.txtODao.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho AO contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtODap.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Derecho AP contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validar el valor decimal usando el método ValidarDecimal
            if (!commonClasses.ValidarDecimal(OD_AP))
            {
                return false;
            }


            if (ObjAddDR.txtOIEsfera.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Izquierdo Esfera.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtOIEsfera.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo Esfera contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtOICilindro.Text.Trim()))
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

            if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtOIEje.Text.Trim()))
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

            if (ObjAddDR.txtOIPrisma.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Izquierdo Prisma.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddDR.txtOIPrisma.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo Prisma contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (ObjAddDR.txtOIAdicion.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Izquierdo Adicion.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddDR.txtOIAdicion.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo Adicion contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtOIdp.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo DP contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validar el valor decimal usando el método ValidarDecimal
            if (!commonClasses.ValidarDecimal(OI_DP))
            {
                return false;
            }

            if (ObjAddDR.txtOIao.Text.Length > 5)
            {
                MessageBox.Show("El campo ha excedido el máximo de caracteres en Ojo Izquierdo AO.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!commonClasses.ValidarNumero(ObjAddDR.txtOIao.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo AO contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!commonClasses.ValidarNumeroConPunto(ObjAddDR.txtOIap.Text.Trim()))
            {
                MessageBox.Show("El campo de Ojo Izquierdo AP contiene caracteres no válidos. Solo se permiten puntos y números.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validar el valor decimal usando el método ValidarDecimal
            if (!commonClasses.ValidarDecimal(OI_AP))
            {
                return false;
            }

            // Si todas las validaciones pasan, se puede continuar con el proceso
            return true;
        }
        //finish validations

        public void ChargeValues(int DR_ID, string OD_esfera, double OD_cilindro, double OD_eje, int OD_prisma, int OD_adicion, int OD_AO, double OD_AP, double OD_DP, string OI_esfera, double OI_cilindro, double OI_eje, int OI_prisma, int OI_adicion, int OI_AO, double OI_AP, double OI_DP)
        {
            //Asigna los valores recibidos a los campos correspondientes a la vista ObjAddDR
            //Valores de Ojo Derecho
            ObjAddDR.txtDRid.Text = DR_ID.ToString();
            ObjAddDR.txtODEsfera.Text = OD_esfera;
            ObjAddDR.txtODCilindro.Text = OD_cilindro.ToString();
            ObjAddDR.txtODEje.Text = OD_eje.ToString();
            ObjAddDR.txtODPrisma.Text = OD_prisma.ToString();
            ObjAddDR.txtODAdicion.Text = OD_adicion.ToString();
            ObjAddDR.txtODdp.Text = OD_DP.ToString();
            ObjAddDR.txtODao.Text = OD_AO.ToString();
            ObjAddDR.txtODap.Text = OD_AP.ToString();
            //Valores de Ojo Izquierdo
            ObjAddDR.txtOIEsfera.Text = OI_esfera;
            ObjAddDR.txtOICilindro.Text = OI_cilindro.ToString();
            ObjAddDR.txtOIEje.Text = OI_eje.ToString();
            ObjAddDR.txtOIPrisma.Text = OI_prisma.ToString();
            ObjAddDR.txtOIAdicion.Text = OI_adicion.ToString();
            ObjAddDR.txtOIdp.Text = OI_DP.ToString();
            ObjAddDR.txtOIao.Text = OI_AO.ToString();
            ObjAddDR.txtOIap.Text = OI_AP.ToString();
        }
    }
}
