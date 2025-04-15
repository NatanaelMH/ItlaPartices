using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectosDeVentas
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            Inicio form = new Inicio();

              form.Show();
            this.Hide();


            
        }

        private void frm_closing(object sender , FormClosedEventArgs e)
        {

            txtdocumento.Text = "";
            txtclave.Text = "";
           
            this.Show();

        }


    }
}
