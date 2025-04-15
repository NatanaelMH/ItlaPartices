using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaVentas.Datos;

namespace SistemaVentas
{
    public partial class FrmPagos : Form
    {
        public FrmPagos()
        {
            InitializeComponent();
            this.Load += FrmPagos_Load;
        }

        private void FrmPagos_Load(object sender, EventArgs e)
        {
            CargarFacturas();

            if (cbxMetodoPago.Items.Count == 0)
            {
                cbxMetodoPago.Items.Add("Efectivo");
                cbxMetodoPago.Items.Add("Tarjeta");
                cbxMetodoPago.Items.Add("Transferencia");
            }

            cbxMetodoPago.SelectedIndex = -1;
            MostrarPagos(); // Cargar la tabla al iniciar
        }

        private void CargarFacturas()
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string query = @"
                        SELECT 
                            f.FacturaID, 
                            CONCAT('Factura #', f.FacturaID, ' - ', c.Nombre) AS NombreFactura
                        FROM Facturas f
                        INNER JOIN Clientes c ON f.ClienteID = c.ClienteID";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbxFactura.DataSource = dt;
                    cbxFactura.DisplayMember = "NombreFactura";
                    cbxFactura.ValueMember = "FacturaID";
                    cbxFactura.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cargar facturas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarPagos()
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string query = @"
                        SELECT 
                            p.PagoID,
                            f.FacturaID,
                            c.Nombre AS Cliente,
                            p.FechaPago,
                            p.Monto,
                            p.MetodoPago
                        FROM Pagos p
                        INNER JOIN Facturas f ON p.FacturaID = f.FacturaID
                        INNER JOIN Clientes c ON f.ClienteID = c.ClienteID
                        ORDER BY p.PagoID DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPagos.DataSource = dt;
                    dgvPagos.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al mostrar pagos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarPago_Click(object sender, EventArgs e)
        {
            if (cbxFactura.SelectedIndex == -1 || cbxMetodoPago.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Monto inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int facturaID = Convert.ToInt32(cbxFactura.SelectedValue);
            string metodo = cbxMetodoPago.SelectedItem.ToString();

            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string query = @"INSERT INTO Pagos (FacturaID, FechaPago, Monto, MetodoPago) 
                                     VALUES (@FacturaID, GETDATE(), @Monto, @MetodoPago)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FacturaID", facturaID);
                    cmd.Parameters.AddWithValue("@Monto", monto);
                    cmd.Parameters.AddWithValue("@MetodoPago", metodo);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Pago registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    MostrarPagos(); // Actualiza la tabla luego de guardar
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al guardar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarPago_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            cbxFactura.SelectedIndex = -1;
            cbxMetodoPago.SelectedIndex = -1;
            txtMonto.Clear();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Método vacío para evitar errores por eventos asignados
        }
    }
}
