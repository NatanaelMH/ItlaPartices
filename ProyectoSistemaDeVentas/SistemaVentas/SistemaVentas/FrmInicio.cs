using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class FrmInicio : Form
    {
        public FrmInicio()
        {
            InitializeComponent();
        }

        private void menuclientes_Click(object sender, EventArgs e)
        {
            FrmClientes frm = new FrmClientes();
            frm.ShowDialog();
        }

        private void menuproductos_Click(object sender, EventArgs e)
        {
            FrmProductos frm = new FrmProductos();
            frm.ShowDialog();
        }

        private void menufacturas_Click(object sender, EventArgs e)
        {
            FrmFacturas frm = new FrmFacturas();
            frm.ShowDialog();
        }

      

        private void menupagos_Click(object sender, EventArgs e)
        {
            FrmPagos frm = new FrmPagos();
            frm.ShowDialog();
        }
    }
}
