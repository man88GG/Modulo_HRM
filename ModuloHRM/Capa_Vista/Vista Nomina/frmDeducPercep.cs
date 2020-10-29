﻿using Capa_Controlador.Controlador_Nomina;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Vista.Vista_Nomina
{
    public partial class frmDeducPercep : Form
    {
        public frmDeducPercep()
        {
            InitializeComponent();
        }

        clsControladorNomina ConsNom = new clsControladorNomina();

        string NombreCob, DescCob, NomOriginalCob;
        double MontoCob;
        bool Validado;
        private void btnIngresoDedPer_Click(object sender, EventArgs e)
        {
            if (funcValidarCamposIngreso() == true)
            {
                NombreCob = txtIngresoNomCob.Text;
                MontoCob = Convert.ToDouble(txtIngresoMontoCob.Text);
                DescCob = rtxtIngresoDescCob.Text;

                if (rbtnDed.Checked == true)
                {
                    ConsNom.funcInsertarDeduccion(NombreCob, MontoCob, DescCob);
                }
                else
                {
                    ConsNom.funcInsertarPercepcion(NombreCob, MontoCob, DescCob);
                }
                funcLimpiarCampos();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(funcValidarCamposModificar() == true)
            {
                NombreCob = txtModificarNomCob.Text;
                MontoCob = Convert.ToDouble(txtModificarMontoCob.Text);
                DescCob = rtxtModificarDescCob.Text;

                if (rbtnModificarDed.Checked == true)
                {
                    ConsNom.funcModificarDeduccion(NombreCob, MontoCob, DescCob, NomOriginalCob);
                }
                else if (rbtnModificarPer.Checked == true)
                {
                    ConsNom.funcModificarPercepcion(NombreCob, MontoCob, DescCob, NomOriginalCob);
                }
                else
                {
                    MessageBox.Show("Seleccione el tipo de cobro para realizar la busqueda.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                funcLimpiarCampos();
            }
        }

        private void btnBuscarModificar_Click(object sender, EventArgs e)
        {
            NomOriginalCob = txtModificarNomCob.Text;
            if (rbtnModificarDed.Checked == true)
            {
                OdbcDataReader Lector = ConsNom.funcBuscarDeduccion(txtModificarNomCob.Text);
                if (Lector.HasRows == true)
                {
                    while (Lector.Read())
                    {
                        txtModificarNomCob.Text = Lector.GetString(0);
                        txtModificarMontoCob.Text = Convert.ToString(Lector.GetDouble(1));
                        rtxtModificarDescCob.Text = Lector.GetString(2);
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: El nombre de la deducción no es valido o no se encuentra registrado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (rbtnModificarPer.Checked == true)
            {
                OdbcDataReader Lector = ConsNom.funcBuscarPercepcion(txtModificarNomCob.Text);
                if (Lector.HasRows == true)
                {
                    while (Lector.Read())
                    {
                        txtModificarNomCob.Text = Lector.GetString(0);
                        txtModificarMontoCob.Text = Convert.ToString(Lector.GetDouble(1));
                        rtxtModificarDescCob.Text = Lector.GetString(2);
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: El nombre de la percepcion no es valido o no se encuentra registrado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione el tipo de cobro para realizar la busqueda..", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (funcValidarCamposEliminar() == true)
            {
                if (rbtnEliminarDed.Checked == true)
                {
                    ConsNom.funcEliminarDeduccion(txtEliminarNomCob.Text);
                }
                else if (rbtnEliminarPer.Checked == true)
                {
                    ConsNom.funcEliminarPercepcion(txtEliminarNomCob.Text);
                }
                else
                {
                    MessageBox.Show("Seleccione el tipo de cobro que desea eliminar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                funcLimpiarCampos();
            }
        }

        private void btnEliminarBuscar_Click(object sender, EventArgs e)
        {
            if (rbtnEliminarDed.Checked == true)
            {
                OdbcDataReader Lector = ConsNom.funcBuscarDeduccion(txtEliminarNomCob.Text);
                if (Lector.HasRows == true)
                {
                    while (Lector.Read())
                    {
                        txtEliminarNomCob.Text = Lector.GetString(0);
                        txtEliminarMontoCob.Text = Convert.ToString(Lector.GetDouble(1));
                        rtxtEliminarDescCob.Text = Lector.GetString(2);
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: El nombre de la deducción no es valido o no se encuentra registrado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (rbtnEliminarPer.Checked == true)
            {
                OdbcDataReader Lector = ConsNom.funcBuscarPercepcion(txtEliminarNomCob.Text);
                if (Lector.HasRows == true)
                {
                    while (Lector.Read())
                    {
                        txtEliminarNomCob.Text = Lector.GetString(0);
                        txtEliminarMontoCob.Text = Convert.ToString(Lector.GetDouble(1));
                        rtxtEliminarDescCob.Text = Lector.GetString(2);
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: El nombre de la percepcion no es valido o no se encuentra registrado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione el tipo de cobro que desea eliminar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool funcValidarCamposIngreso()
        {
            if (rbtnDed.Checked == false && rbtnPer.Checked == false)
            {
                MessageBox.Show("No se ha seleccionado el tipo de cobro.", "Tipo de Cobro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Validado = false;
            }
            else if (txtIngresoNomCob.Text == "" || txtIngresoMontoCob.Text == "" || rtxtIngresoDescCob.Text == "")
            {
                MessageBox.Show("Uno o mas campos se encuentran vacios.", "Campos Vacios.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Validado = false;
            }
            else
            {
                Validado = true;
            }

            return Validado;
        }

        private bool funcValidarCamposModificar()
        {
            if (rbtnModificarDed.Checked == false && rbtnModificarPer.Checked == false)
            {
                MessageBox.Show("No se ha seleccionado el tipo de cobro.", "Tipo de Cobro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Validado = false;
            }
            else if (txtModificarNomCob.Text == "" || txtModificarMontoCob.Text == "" || rtxtModificarDescCob.Text == "")
            {
                MessageBox.Show("Uno o mas campos se encuentran vacios.", "Campos Vacios.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Validado = false;
            }
            else
            {
                Validado = true;
            }

            return Validado;
        }

        private bool funcValidarCamposEliminar()
        {
            if (rbtnEliminarDed.Checked == false && rbtnEliminarPer.Checked == false)
            {
                MessageBox.Show("No se ha seleccionado el tipo de cobro.", "Tipo de Cobro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Validado = false;
            }
            else if (txtEliminarNomCob.Text == "" || txtEliminarMontoCob.Text == "" || rtxtEliminarDescCob.Text == "")
            {
                MessageBox.Show("Uno o mas campos se encuentran vacios.", "Campos Vacios.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Validado = false;
            }
            else
            {
                Validado = true;
            }

            return Validado;
        }

        private void funcLimpiarCampos()
        {
            rbtnDed.Checked = false;
            rbtnPer.Checked = false;
            txtIngresoNomCob.Text = "";
            txtIngresoMontoCob.Text = "";
            rtxtIngresoDescCob.Text = "";
            rbtnModificarDed.Checked = false;
            rbtnModificarPer.Checked = false;
            txtModificarNomCob.Text = "";
            txtModificarMontoCob.Text = "";
            rtxtModificarDescCob.Text = "";
            rbtnEliminarDed.Checked = false;
            rbtnEliminarPer.Checked = false;
            txtEliminarNomCob.Text = "";
            txtEliminarMontoCob.Text = "";
            rtxtEliminarDescCob.Text = "";

        }
    }
}