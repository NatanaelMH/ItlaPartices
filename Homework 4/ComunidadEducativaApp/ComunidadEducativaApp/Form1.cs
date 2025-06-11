using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComunidadEducativaApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Método corregido: ahora se llama como el diseñador espera
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            // Aquí puedes poner el código que desees ejecutar al hacer clic
            listBox1.Items.Add("Ejemplo mostrado");
        }

        // Si no vas a usar este evento, puedes eliminarlo
        private void lstSalida(object sender, EventArgs e)
        {
        }
    }
}
