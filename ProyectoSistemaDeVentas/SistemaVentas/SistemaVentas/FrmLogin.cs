using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SistemaVentas.Datos;
using System.Data.SqlClient;

namespace SistemaVentas
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text.Trim();

            if (usuario == "")
            {
                MessageBox.Show("Por favor, ingrese el nombre de usuario.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (clave == "")
            {
                MessageBox.Show("Por favor, ingrese la contraseña.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClave.Focus();
                return;
            }

            if (usuario == "Natanael" && clave == "1234")
            {
                try
                {
                    SqlConnection con = Conexion.ObtenerConexion();
                    con.Open();

                    MessageBox.Show("✅ Bienvenido, Natanael", "Acceso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();

                    this.Hide();
                    FrmInicio frm = new FrmInicio();
                    frm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al conectar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("❌ Usuario o contraseña incorrectos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

