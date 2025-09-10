using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _411963_MAINARDI_GONZALO_1W3.Domain;
using Microsoft.Data.SqlClient;

namespace _411963_MAINARDI_GONZALO_1W3.Data
{
    public class IFacturaRepository
    {
        private readonly string _connectionString;

        public IFacturaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int InsertarFactura(Factura factura)
        {
            int idFactura = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarFactura", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@IdFormaPago", factura.FormaPago.IdFormaPago);
                cmd.Parameters.AddWithValue("@Cliente", factura.Cliente);

                SqlParameter outputId = new SqlParameter("@IdFactura", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                con.Open();
                cmd.ExecuteNonQuery();
                idFactura = (int)outputId.Value;

                // Insertar detalles
                foreach (var detalle in factura.Detalles)
                {
                    SqlCommand cmdDetalle = new SqlCommand("sp_InsertarDetalleFactura", con);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@IdFactura", idFactura);
                    cmdDetalle.Parameters.AddWithValue("@IdArticulo", detalle.Articulo.IdArticulo);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                }
            }
            return idFactura;
        }
    }
}
