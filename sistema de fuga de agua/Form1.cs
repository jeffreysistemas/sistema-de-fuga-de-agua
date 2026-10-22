using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace sistema_de_fuga_de_agua
{
    public partial class Form1 : Form
    {
        
       
        private int totalFugas = 0;
        private int totalDesperdicios = 0;

        private List<string> listaCodigos = new List<string>();
        private List<string> listaNombres = new List<string>();
        private List<string> listaDirecciones = new List<string>();
        private List<string> listaTipos = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        
        private bool ValidarEntradas(string nombre, string direccion, string tipo, string descripcion)
        {
            return !string.IsNullOrWhiteSpace(nombre) &&
                   !string.IsNullOrWhiteSpace(direccion) &&
                   !string.IsNullOrWhiteSpace(tipo) &&
                   !string.IsNullOrWhiteSpace(descripcion);
        }

      
        private string GenerarCodigoTicket()
        {
            Random rand = new Random();

            return "REP-" + rand.Next(1000, 9999);
        }

       
        private void ActualizarContadores(string tipo)
        {
            if (tipo.Equals("Fuga de agua", StringComparison.OrdinalIgnoreCase))
            {
                totalFugas++;
            }
            else if (tipo.Equals("Desperdicio de agua", StringComparison.OrdinalIgnoreCase))
            {
                totalDesperdicios++;
            }
        }

        
        private void ActualizarListaVisual()
        {
            lstreportes.Items.Clear();

            
            for (int i = 0; i < listaCodigos.Count; i++)
            {
                lstreportes.Items.Add($"=== {listaCodigos[i]} ===");
                lstreportes.Items.Add($"• Ciudadano : {listaNombres[i]}");
                lstreportes.Items.Add($"• Dirección : {listaDirecciones[i]}");
                lstreportes.Items.Add($"• Problema  : {listaTipos[i]}");
                lstreportes.Items.Add(""); 
            }
        }

        
        private void LimpiarFormulario()
        {
            txtnombre.Clear();
            txtdireccion.Clear();
            txtdescripcion.Clear();
            cmbtipo.SelectedIndex = -1;
            txtnombre.Focus();
        }

        
        private void MostrarResumenEstadistico()
        {
            int totalreportes = listaCodigos.Count;

            
            string resumen = "========================================\n" +
                             "      RESUMEN ESTADÍSTICO DE SEDAPAL    \n" +
                             "========================================\n\n" +
                             $"• Total de incidencias registradas : {totalreportes}\n" +
                             $"• Fugas de agua detectadas        : {totalFugas}\n" +
                             $"• Desperdicios de agua reportados : {totalDesperdicios}\n\n" +
                             "----------------------------------------\n" +
                             "Estado del sistema: Operativo y Sincronizado";

            MessageBox.Show(resumen, "Reporte Estadístico Consolidado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       
        private void MostrarMensaje(string mensaje, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, "Sistema de Reportes de Agua", MessageBoxButtons.OK, icono);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnregistrar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarEntradas(txtnombre.Text, txtdireccion.Text, cmbtipo.Text, txtdescripcion.Text))
            {
                MostrarMensaje("Por favor, complete todos los campos obligatorios.", MessageBoxIcon.Warning);
                return;
            }

            string tipoSeleccionado = cmbtipo.Text;
            ActualizarContadores(tipoSeleccionado);

            
            string codigoTicket = GenerarCodigoTicket();

            
            listaCodigos.Add(codigoTicket);
            listaNombres.Add(txtnombre.Text.Trim());
            listaDirecciones.Add(txtdireccion.Text.Trim());
            listaTipos.Add(tipoSeleccionado);

            
            ActualizarListaVisual();

            MostrarMensaje($"¡Reporte registrado exitosamente!\nCódigo asignado: {codigoTicket}", MessageBoxIcon.Information);
            LimpiarFormulario();
        }

        private void btnresumen_Click(object sender, EventArgs e)
        {
            MostrarResumenEstadistico();
        }
    }
}
