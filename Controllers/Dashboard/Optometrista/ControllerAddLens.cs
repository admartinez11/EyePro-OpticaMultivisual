using OpticaMultivisual.Controllers.Helper;
using OpticaMultivisual.Models.DAO;
using OpticaMultivisual.Views.Dashboard.Optometrista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticaMultivisual.Controllers.Dashboard.Optometrista
{
    internal class ControllerAddLens
    {
        ViewAddLens ObjAddLens;
        private int accion;

        public ControllerAddLens(ViewAddLens Vista, int accion, int lens_ID, string OD_esfera, double OD_cilindro, double OD_eje, int OD_prisma, int OD_adicion, string OI_esfera, double OI_cilindro, double OI_eje, int OI_prisma, int OI_adicion)
        {
            //Acciones Iniciales
            ObjAddLens = Vista;
            this.accion = accion;
            //Métodos iniciales: estos metodos se ejecutan cuando el formulario está cargando
            verificarAccion();
            ChargeValues(lens_ID, OD_esfera, OD_cilindro, OD_eje, OD_prisma, OD_adicion, OI_esfera, OI_cilindro, OI_eje, OI_prisma, OI_adicion);
            //Métodos que se ejecutan al ocurrir eventos
            ObjAddLens.btnGuardar.Click += new EventHandler(NewRegister);
            ObjAddLens.btnActualizar.Click += new EventHandler(UpdateRegister);

        }

        public ControllerAddLens(ViewAddLens frmAddLens, int accion)
        {
            ObjAddLens = frmAddLens;
            this.accion = accion;
            verificarAccion();
            ObjAddLens.btnActualizar.Click += new EventHandler(UpdateRegister);
            ObjAddLens.btnGuardar.Click += new EventHandler(NewRegister);
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
                DAOInsert.OD_esfera1 = ObjAddLens.txtODEsfera.Text.Trim();
                DAOInsert.OD_cilindro1 = double.Parse(ObjAddLens.txtODCilindro.Text.ToString());
                DAOInsert.OD_eje1 = double.Parse(ObjAddLens.txtODEje.Text.ToString());
                DAOInsert.OD_prisma1 = int.Parse(ObjAddLens.txtODPrisma.Text.ToString());
                DAOInsert.OD_adicion1 = int.Parse(ObjAddLens.txtODAdicion.Text.ToString());
                //OI
                DAOInsert.OI_esfera1 = ObjAddLens.txtOIEsfera.Text.Trim();
                DAOInsert.OI_cilindro1 = double.Parse(ObjAddLens.txtOICilindro.Text.ToString());
                DAOInsert.OI_eje1 = double.Parse(ObjAddLens.txtOIEje.Text.ToString());
                DAOInsert.OI_prisma1 = int.Parse(ObjAddLens.txtOIPrisma.Text.ToString());
                DAOInsert.OI_adicion1 = int.Parse(ObjAddLens.txtOIAdicion.Text.ToString());

                int valorRetornado = DAOInsert.InsertarLens();
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
                DAOLens daoUpdate = new DAOLens();

                daoUpdate.lens_ID1 = int.Parse(ObjAddLens.txtDRid.Text.ToString());
                daoUpdate.OD_esfera1 = ObjAddLens.txtODEsfera.Text.Trim();
                daoUpdate.OD_cilindro1 = double.Parse(ObjAddLens.txtODCilindro.Text.ToString());
                daoUpdate.OD_eje1 = double.Parse(ObjAddLens.txtODEje.Text.ToString());
                daoUpdate.OD_prisma1 = int.Parse(ObjAddLens.txtODPrisma.Text.ToString());
                daoUpdate.OD_adicion1 = int.Parse(ObjAddLens.txtODAdicion.Text.ToString());
                //OI
                daoUpdate.OI_esfera1 = ObjAddLens.txtOIEsfera.Text.Trim();
                daoUpdate.OI_cilindro1 = double.Parse(ObjAddLens.txtOICilindro.Text.ToString());
                daoUpdate.OI_eje1 = double.Parse(ObjAddLens.txtOIEje.Text.ToString());
                daoUpdate.OI_prisma1 = int.Parse(ObjAddLens.txtOIPrisma.Text.ToString());
                daoUpdate.OI_adicion1 = int.Parse(ObjAddLens.txtOIAdicion.Text.ToString());

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

            string OD_prisma = ObjAddLens.txtODPrisma.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OD_prisma))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (OD_prisma.Contains("."))
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
            if (OD_adicion.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Derecho Adicion",
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

            string OI_prisma = ObjAddLens.txtOIPrisma.Text.Trim();
            // Si está vacío, permitimos la inserción
            if (string.IsNullOrEmpty(OI_prisma))
            {
                return true;
            }
            // Si no está vacío, validamos el punto decimal
            if (OI_prisma.Contains("."))
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
            if (OI_adicion.Contains("."))
            {
                MessageBox.Show("El tipo de valores ingresados son incorrectos",
                                        "Validación de Ojo Izquierdo Adicion",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                return false;
            }

            string OD_esfera = ObjAddLens.txtODEsfera.Text.Trim();
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

            string OI_esfera = ObjAddLens.txtOIEsfera.Text.Trim();
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

        public void ChargeValues(int lens_ID, string OD_esfera, double OD_cilindro, double OD_eje, int OD_prisma, int OD_adicion, string OI_esfera, double OI_cilindro, double OI_eje, int OI_prisma, int OI_adicion)
        {
            //Asigna los valores recibidos a los campos correpondientes a la vista ObjAddLens
            //Valores de Ojo Derecho
            ObjAddLens.txtDRid.Text = lens_ID.ToString();
            ObjAddLens.txtODEsfera.Text = OD_esfera;
            ObjAddLens.txtODCilindro.Text = OD_cilindro.ToString();
            ObjAddLens.txtODEje.Text = OD_eje.ToString();
            ObjAddLens.txtODPrisma.Text = OD_prisma.ToString();
            ObjAddLens.txtODAdicion.Text = OD_adicion.ToString();
            //Valores de Ojo Izquierdo
            ObjAddLens.txtOIEsfera.Text = OI_esfera;
            ObjAddLens.txtOICilindro.Text = OI_cilindro.ToString();
            ObjAddLens.txtOIEje.Text = OI_eje.ToString();
            ObjAddLens.txtOIPrisma.Text = OI_prisma.ToString();
            ObjAddLens.txtOIAdicion.Text = OI_adicion.ToString();
        }
    }
}
