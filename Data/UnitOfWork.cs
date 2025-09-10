using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace _411963_MAINARDI_GONZALO_1W3.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public IFacturaRepository Facturas { get; private set; }

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            Facturas = new FacturaRepository(_connection, _transaction);
        }

        public void Commit()
        {
            _transaction.Commit();
            _transaction = _connection.BeginTransaction();
            // Recreate repos if necesitas (en este ejemplo reutilizamos el mismo objeto)
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = _connection.BeginTransaction();
        }

        public void Dispose()
        {
            try { _transaction?.Dispose(); } catch { }
            try { _connection?.Close(); _connection?.Dispose(); } catch { }
        }
    }
}
