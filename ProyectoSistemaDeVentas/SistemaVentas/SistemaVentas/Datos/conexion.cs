using System.Data.SqlClient;

namespace SistemaVentas.Datos
{
    public class Conexion
    {
        private static string cadenaConexion = "Server=MSI\\SQLEXPRESS01;Database=SistemaVentas;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            return conexion;
        }
    }
}
