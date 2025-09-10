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
    public class FacturaRepository : IFacturaRepository
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _tran;

        public FacturaRepository(SqlConnection conn, SqlTransaction tran)
        {
            _conn = conn;
            _tran = tran;
        }

        public int Add(Factura factura)
        {
            // 1) Insertar cabecera y obtener NroFactura
            using (var cmd = new SqlCommand("sp_InsertarFactura", _conn, _tran))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@Cliente", factura.Cliente);
                cmd.Parameters.AddWithValue("@IdFormaPago", factura.FormaPago.IdFormaPago);

                var outParam = new SqlParameter("@NroFactura", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outParam);

                cmd.ExecuteNonQuery();

                int nro = Convert.ToInt32(outParam.Value);
                // 2) Insertar detalles (si repite articulo, el SP suma)
                foreach (var det in factura.Detalles)
                {
                    using (var cmdDet = new SqlCommand("sp_InsertarOActualizarDetalle", _conn, _tran))
                    {
                        cmdDet.CommandType = CommandType.StoredProcedure;
                        cmdDet.Parameters.AddWithValue("@NroFactura", nro);
                        cmdDet.Parameters.AddWithValue("@IdArticulo", det.Articulo.IdArticulo);
                        cmdDet.Parameters.AddWithValue("@Cantidad", det.Cantidad);
                        cmdDet.ExecuteNonQuery();
                    }
                }

                return nro;
            }
        }

        public Factura GetById(int nroFactura)
        {
            var factura = new Factura();
            using (var cmd = new SqlCommand("sp_GetFacturaConDetalles", _conn, _tran))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NroFactura", nroFactura);

                using (var reader = cmd.ExecuteReader())
                {
                    // primera resultset: cabecera
                    if (reader.Read())
                    {
                        factura.NroFactura = Convert.ToInt32(reader["NroFactura"]);
                        factura.Fecha = Convert.ToDateTime(reader["Fecha"]);
                        factura.Cliente = reader["Cliente"].ToString();
                        factura.FormaPago = new FormaPago { IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]), Nombre = reader["FormaPago"].ToString() };
                    }

                    // pasar al segundo resultset: detalles
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            var art = new Articulo
                            {
                                IdArticulo = Convert.ToInt32(reader["IdArticulo"]),
                                Nombre = reader["Articulo"].ToString(),
                                PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"])
                            };
                            var det = new DetalleFactura
                            {
                                IdDetalle = Convert.ToInt32(reader["IdDetalle"]),
                                Articulo = art,
                                Cantidad = Convert.ToInt32(reader["Cantidad"])
                            };
                            factura.Detalles.Add(det);
                        }
                    }
                }
            }
            return factura;
        }

        public IEnumerable<Factura> GetAll()
        {
            // Implementación simplificada: leer las facturas (sin detalles) o usar otro SP
            var list = new List<Factura>();
            using (var cmd = new SqlCommand("SELECT NroFactura, Fecha, Cliente, IdFormaPago FROM Factura", _conn, _tran))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Factura
                    {
                        NroFactura = Convert.ToInt32(reader["NroFactura"]),
                        Fecha = Convert.ToDateTime(reader["Fecha"]),
                        Cliente = reader["Cliente"].ToString(),
                        FormaPago = new FormaPago { IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]) }
                    });
                }
            }
            return list;
        }

        public void Delete(int nroFactura)
        {
            // Eliminar detalles y luego cabecera dentro de la misma transacción
            using (var cmd = new SqlCommand("DELETE FROM DetalleFactura WHERE NroFactura = @Nro; DELETE FROM Factura WHERE NroFactura = @Nro;", _conn, _tran))
            {
                cmd.Parameters.AddWithValue("@Nro", nroFactura);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
