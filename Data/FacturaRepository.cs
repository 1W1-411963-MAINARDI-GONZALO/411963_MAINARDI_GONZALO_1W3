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
        private readonly string _connectionString;

        public FacturaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Factura factura)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_InsertFactura", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
            cmd.Parameters.AddWithValue("@FormaPagoId", factura.FormaPago.Id);
            cmd.Parameters.AddWithValue("@ClienteId", factura.Cliente.Id);

            factura.NroFactura = Convert.ToInt32(cmd.ExecuteScalar());

            // Aquí podrías agregar los detalles usando otro procedimiento almacenado
        }

        public void Update(Factura factura) { /* Implementar */ }
        public void Delete(Factura factura) { /* Implementar */ }
        public Factura GetById(int nroFactura) { /* Implementar */ return null; }
        public IEnumerable<Factura> GetAll() { /* Implementar */ return null; }
    }
