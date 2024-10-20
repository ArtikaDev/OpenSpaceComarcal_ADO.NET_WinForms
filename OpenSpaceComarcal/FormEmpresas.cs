﻿using OpenSpaceComarcal.Libraries;
using OpenSpaceComarcal.Models;
using System;
using System.Windows.Forms;

namespace OpenSpaceComarcal
{
    public partial class FormEmpresas : Form
    {
        public FormEmpresas()
        {
            InitializeComponent();

            // Ocultar barra de progreso
            progressBarArchivo.Visible = false;
        }

        private void actualizarTextBoxes()
        {
            if (dataGridViewEmpresas.SelectedRows.Count == 1)
            {
                DataGridViewRow fila = dataGridViewEmpresas.SelectedRows[0];

                textBoxCif.Text = fila.Cells[1].Value.ToString();
                textBoxSiglas.Text = fila.Cells[2].Value.ToString();
                textBoxNombre.Text = fila.Cells[3].Value.ToString();
                textBoxTelefono.Text = fila.Cells[4].Value.ToString();
                textBoxMail.Text = fila.Cells[5].Value.ToString();
                textBoxContacto.Text = fila.Cells[6].Value.ToString();
                textBoxNotas.Text = fila.Cells[7].Value.ToString();
            }
        }

        private bool camposRellenados()
        {
            bool esValido = false;
            if (textBoxCif.Text != "" &&
                textBoxNombre.Text != "" &&
                textBoxSiglas.Text != ""
                )
                esValido = true;
            return esValido;
        }

        private void Empresas_Load(object sender, EventArgs e)
        {
            dataGridViewEmpresas.Columns[0].FillWeight = 35;
            dataGridViewEmpresas.Columns[1].FillWeight = 70;
            dataGridViewEmpresas.Columns[2].FillWeight = 70;
            dataGridViewEmpresas.Columns[3].FillWeight = 130;
            dataGridViewEmpresas.Columns[4].FillWeight = 70;
            dataGridViewEmpresas.Columns[5].FillWeight = 130;

            comboBoxTakeEmpresas.Items.Add("1000");
            comboBoxTakeEmpresas.Items.Add("100");
            comboBoxTakeEmpresas.Items.Add("50");
            comboBoxTakeEmpresas.Items.Add("10");
            comboBoxTakeEmpresas.SelectedIndex = 3;

            bindingSourceEmpresa.DataSource = EmpresaOrm.Select(Int32.Parse(comboBoxTakeEmpresas.SelectedItem.ToString()));
        }

        private void buttonCrearEmpresa_Click(object sender, EventArgs e)
        {
            if (camposRellenados())
            {
                String mensajeError = "";

                empresa _empresa = new empresa();

                _empresa.cif = textBoxCif.Text;
                _empresa.siglas = textBoxSiglas.Text;
                _empresa.nombre = textBoxNombre.Text;
                _empresa.telefono = textBoxTelefono.Text;
                _empresa.email = textBoxMail.Text;
                _empresa.persona_contacto = textBoxContacto.Text;
                _empresa.notas = textBoxNotas.Text;

                mensajeError = EmpresaOrm.Insert(_empresa);

                if (mensajeError == "")
                {
                    MessageBox.Show("Se ha creado una nueva empresa llamada " + textBoxNombre.Text, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSourceEmpresa.DataSource = EmpresaOrm.Select(Int32.Parse(comboBoxTakeEmpresas.SelectedItem.ToString()));
                }
                else
                {
                    MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ha dejado campos vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonModificarEmpresa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está seguro de modificar la empresa?", "Modificar Empresa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                if (camposRellenados())
                {
                    String mensajeError = "";

                    if (dataGridViewEmpresas.SelectedRows.Count == 1)
                    {
                        DataGridViewRow fila = dataGridViewEmpresas.SelectedRows[0];
                        empresa _empresa = (empresa)fila.DataBoundItem;

                        _empresa.cif = textBoxCif.Text;
                        _empresa.siglas = textBoxSiglas.Text;
                        _empresa.nombre = textBoxNombre.Text;
                        _empresa.telefono = textBoxTelefono.Text;
                        _empresa.email = textBoxMail.Text;
                        _empresa.persona_contacto = textBoxContacto.Text;
                        _empresa.notas = textBoxNotas.Text;

                        mensajeError = EmpresaOrm.Update(_empresa);

                        if (mensajeError == "")
                        {
                            MessageBox.Show("Se ha actualizado " + textBoxNombre.Text, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bindingSourceEmpresa.DataSource = EmpresaOrm.Select(Int32.Parse(comboBoxTakeEmpresas.SelectedItem.ToString()));
                        }
                        else
                        {
                            MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay una empresa seleccionada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No debe dejar campos en blanco", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonActualizarEmpresas_Click(object sender, EventArgs e)
        {
            // Limpiar textBoxBusqueda
            textBoxBusqueda.Text = "";
            bindingSourceEmpresa.DataSource = EmpresaOrm.Select(Int32.Parse(comboBoxTakeEmpresas.SelectedItem.ToString()));
        }

        private void dataGridViewEmpresas_SelectionChanged(object sender, EventArgs e)
        {
            actualizarTextBoxes();
        }

        private void buttonEliminarEmpresa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está seguro de que desea eliminar la empresa?", "Eliminar Empresa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                if (camposRellenados())
                {
                    String mensajeError = "";

                    if (dataGridViewEmpresas.SelectedRows.Count == 1)
                    {
                        DataGridViewRow fila = dataGridViewEmpresas.SelectedRows[0];
                        empresa _empresa = (empresa)fila.DataBoundItem;

                        mensajeError = EmpresaOrm.Delete(_empresa);

                        if (mensajeError == "")
                        {
                            MessageBox.Show("Se ha eliminado " + textBoxNombre.Text, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bindingSourceEmpresa.DataSource = EmpresaOrm.Select(Int32.Parse(comboBoxTakeEmpresas.SelectedItem.ToString()));
                        }
                        else
                        {
                            MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay una empresa seleccionada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ha dejado campos vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            String busqueda = textBoxBusqueda.Text;
            bindingSourceEmpresa.DataSource = EmpresaOrm.Select(busqueda);
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxBusqueda.Text = "";
            textBoxCif.Text = "";
            textBoxSiglas.Text = "";
            textBoxNombre.Text = "";
            textBoxTelefono.Text = "";
            textBoxMail.Text = "";
            textBoxContacto.Text = "";
            textBoxNotas.Text = "";
            dataGridViewEmpresas.ClearSelection();
        }

        private void toolStripMenuExportar_Click(object sender, EventArgs e)
        {

            // Preguntar al usuario si desea exportar los datos
            DialogResult result = MessageBox.Show("¿Desea exportar los datos seleccionados?", "Exportar datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Exportar los datos a Excel
                Exportar.ExportarDataGridViewExcel(dataGridViewEmpresas, "Empresas", progressBarArchivo);

                // Ocultar la barra de progreso al finalizar la exportación
                progressBarArchivo.Visible = false;
            }
        }

        private void ToolStripMenuImportar_Click(object sender, EventArgs e)
        {

        }
    }
}
