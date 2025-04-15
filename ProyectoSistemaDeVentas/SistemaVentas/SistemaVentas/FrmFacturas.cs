using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaVentas.Datos;

namespace SistemaVentas
{
    public partial class FrmFacturas : Form
    {
        private DataTable tablaDetalle;

        public FrmFacturas()
        {
            InitializeComponent();
            InicializarTablaDetalle();
            cbxProducto.SelectedIndexChanged += cbxProducto_SelectedIndexChanged;
            dgvFacturas.CellClick += dgvFacturas_CellClick;
            this.Load += FrmFacturas_Load;
        }

        private void FrmFacturas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarProductos();
            MostrarFacturas();
        }

        private void InicializarTablaDetalle()
        {
            tablaDetalle = new DataTable();
            tablaDetalle.Columns.Add("ProductoID", typeof(int));
            tablaDetalle.Columns.Add("NombreProducto", typeof(string));
            tablaDetalle.Columns.Add("PrecioUnitario", typeof(decimal));
            tablaDetalle.Columns.Add("Cantidad", typeof(int));
            tablaDetalle.Columns.Add("Subtotal", typeof(decimal), "Cantidad * PrecioUnitario");

            dgvDetalleFactura.DataSource = tablaDetalle;
        }

        private void CargarClientes()
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string query = "SELECT ClienteID, Nombre FROM Clientes";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbxCliente.DataSource = dt;
                    cbxCliente.DisplayMember = "Nombre";
                    cbxCliente.ValueMember = "ClienteID";
                    cbxCliente.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string query = "SELECT ProductoID, Nombre, Precio FROM Productos";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbxProducto.DataSource = dt;
                    cbxProducto.DisplayMember = "Nombre";
                    cbxProducto.ValueMember = "ProductoID";
                    cbxProducto.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarFacturas()
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string query = @"SELECT f.FacturaID, c.Nombre AS ClienteNombre, f.Fecha, f.Total 
                                     FROM Facturas f
                                     INNER JOIN Clientes c ON f.ClienteID = c.ClienteID";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvFacturas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cargar facturas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDetalleFactura(int facturaID)
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    string query = @"SELECT df.ProductoID, p.Nombre AS NombreProducto, 
                                            df.PrecioUnitario, df.Cantidad, 
                                            (df.PrecioUnitario * df.Cantidad) AS Subtotal
                                     FROM DetallesFactura df
                                     INNER JOIN Productos p ON df.ProductoID = p.ProductoID
                                     WHERE df.FacturaID = @FacturaID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@FacturaID", facturaID);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDetalleFactura.DataSource = dt;

                        // Calcular total
                        decimal total = 0;
                        foreach (DataRow fila in dt.Rows)
                        {
                            total += Convert.ToDecimal(fila["Subtotal"]);
                        }
                        txtTotal.Text = total.ToString("0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cargar detalles de factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvFacturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int facturaID = Convert.ToInt32(dgvFacturas.Rows[e.RowIndex].Cells["FacturaID"].Value);
                MostrarDetalleFactura(facturaID);
            }
        }

        private void cbxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxProducto.SelectedItem != null && cbxProducto.SelectedIndex != -1)
            {
                DataRowView producto = (DataRowView)cbxProducto.SelectedItem;
                txtPrecio.Text = Convert.ToDecimal(producto["Precio"]).ToString("0.00");
            }
            else
            {
                txtPrecio.Clear();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbxProducto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView productoSeleccionado = cbxProducto.SelectedItem as DataRowView;
            int productoID = Convert.ToInt32(productoSeleccionado["ProductoID"]);
            string nombreProducto = productoSeleccionado["Nombre"].ToString();
            decimal precio = Convert.ToDecimal(txtPrecio.Text);

            tablaDetalle.Rows.Add(productoID, nombreProducto, precio, cantidad);

            CalcularTotal();
            LimpiarProducto();
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvDetalleFactura.CurrentRow != null)
            {
                dgvDetalleFactura.Rows.RemoveAt(dgvDetalleFactura.CurrentRow.Index);
                CalcularTotal();
            }
            else
            {
                MessageBox.Show("Seleccione un producto a eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cbxCliente.SelectedItem == null || tablaDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un cliente y agregar productos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int clienteID = Convert.ToInt32((cbxCliente.SelectedItem as DataRowView)["ClienteID"]);
            decimal total = Convert.ToDecimal(txtTotal.Text);

            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();

                    SqlTransaction transaccion = con.BeginTransaction();

                    string queryFactura = "INSERT INTO Facturas (ClienteID, Fecha, Total) VALUES (@ClienteID, GETDATE(), @Total); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdFactura = new SqlCommand(queryFactura, con, transaccion);
                    cmdFactura.Parameters.AddWithValue("@ClienteID", clienteID);
                    cmdFactura.Parameters.AddWithValue("@Total", total);
                    int facturaID = Convert.ToInt32(cmdFactura.ExecuteScalar());

                    foreach (DataRow fila in tablaDetalle.Rows)
                    {
                        string queryDetalle = "INSERT INTO DetallesFactura (FacturaID, ProductoID, Cantidad, PrecioUnitario) " +
                                              "VALUES (@FacturaID, @ProductoID, @Cantidad, @PrecioUnitario)";
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, con, transaccion);
                        cmdDetalle.Parameters.AddWithValue("@FacturaID", facturaID);
                        cmdDetalle.Parameters.AddWithValue("@ProductoID", fila["ProductoID"]);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", fila["Cantidad"]);
                        cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", fila["PrecioUnitario"]);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    transaccion.Commit();

                    MessageBox.Show("✅ Factura registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarTodo();
                    MostrarFacturas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al guardar factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarTodo();
        }

        private void CalcularTotal()
        {
            decimal total = 0;
            foreach (DataRow fila in tablaDetalle.Rows)
            {
                total += Convert.ToDecimal(fila["Subtotal"]);
            }
            txtTotal.Text = total.ToString("0.00");
        }

        private void LimpiarProducto()
        {
            cbxProducto.SelectedIndex = -1;
            txtPrecio.Clear();
            txtCantidad.Clear();
        }

        private void LimpiarTodo()
        {
            cbxCliente.SelectedIndex = -1;
            txtTotal.Clear();
            tablaDetalle.Clear();
            LimpiarProducto();
        }
    }
}
