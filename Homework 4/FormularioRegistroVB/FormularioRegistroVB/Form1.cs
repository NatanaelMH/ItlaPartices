

using System;
using System.Windows.Forms;

namespace FormularioRegistroVB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nombre: " + txtNombre.Text + Environment.NewLine +
                            "Apellido: " + txtApellido.Text + Environment.NewLine +
                            "Edad: " + txtEdad.Text,
                            "Datos del Usuario");
        }

        
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
            txtNombre.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
