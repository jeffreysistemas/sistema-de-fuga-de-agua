using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace sistema_de_fuga_de_agua
{
    public partial class Form1 : Form
    {
        // Estructuras de datos dinámicas y contadores globales
        private List<string> listaReportes = new List<string>();
        private int totalFugas = 0;
        private int totalDesperdicios = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // Función booleana para validar que los campos no estén vacíos
        private bool ValidarEntradas(string nombre, string direccion, string tipo, string descripcion)
        {
            return !string.IsNullOrWhiteSpace(nombre) &&
                   !string.IsNullOrWhiteSpace(direccion) &&
                   !string.IsNullOrWhiteSpace(tipo) &&
                   !string.IsNullOrWhiteSpace(descripcion);
        }

        // Función con retorno para generar un código único de ticket
        private string GenerarCodigoTicket()
        {
            Random rand = new Random();
            return "REP-" + rand.Next(1000, 9999);
        }

        // Método con parámetros para actualizar los contadores estadísticos
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

        // Método para actualizar los elementos visibles en el ListBox
        private void ActualizarListaVisual()
        {
            lstreportes.Items.Clear();
            foreach (string reporte in listaReportes)
            {
                lstreportes.Items.Add(reporte);
            }
        }

        // Método para reiniciar los controles del formulario
        private void LimpiarFormulario()
        {
            txtnombre.Clear();
            txtdireccion.Clear();
            txtdescripcion.Clear();
            cmbtipo.SelectedIndex = -1;
            txtnombre.Focus();
        }

        // Método para presentar el resumen estadístico
        private void MostrarResumenEstadistico()
        {
            int totalReportes = listaReportes.Count;
            string resumen = $"=== RESUMEN DE ATENCIÓN DE INCIDENCIAS ===\n\n" +
                             $"• Total de reportes registrados: {totalReportes}\n" +
                             $"• Fugas de agua: {totalFugas}\n" +
                             $"• Desperdicios de agua: {totalDesperdicios}";

            MessageBox.Show(resumen, "Estadísticas del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Método auxiliar para simplificar cuadros de diálogo
        private void MostrarMensaje(string mensaje, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, "Sistema de Reportes de Agua", MessageBoxButtons.OK, icono);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Método auxiliar requerido por el diseñador
        }

        private void btnregistrar_Click_1(object sender, EventArgs e)
        {
            // Validar entradas usando función booleana
            if (!ValidarEntradas(txtnombre.Text, txtdireccion.Text, cmbtipo.Text, txtdescripcion.Text))
            {
                MostrarMensaje("Por favor, complete todos los campos obligatorios.", MessageBoxIcon.Warning);
                return;
            }

            // Procesar el tipo de reporte y actualizar contadores mediante método
            string tipoSeleccionado = cmbtipo.Text;
            ActualizarContadores(tipoSeleccionado);

            // Generar código de ticket único mediante función
            string codigoTicket = GenerarCodigoTicket();

            // Formatear y almacenar el reporte
            string reporteCompleto = $"{codigoTicket} | Nombre: {txtnombre.Text.Trim()} | Dir: {txtdireccion.Text.Trim()} | Tipo: {tipoSeleccionado} | Desc: {txtdescripcion.Text.Trim()}";
            listaReportes.Add(reporteCompleto);

            // Actualizar interfaz visual
            ActualizarListaVisual();

            MostrarMensaje($"¡Reporte registrado exitosamente!\nCódigo de ticket: {codigoTicket}", MessageBoxIcon.Information);
            LimpiarFormulario();

        }

        private void btnresumen_Click(object sender, EventArgs e)
        {
            MostrarResumenEstadistico();
        }
    }
}
