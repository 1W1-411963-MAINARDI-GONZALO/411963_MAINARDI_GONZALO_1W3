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
        private readonly string _connectionString;
        public IFacturaRepository Facturas { get; }

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
            Facturas = new FacturaRepository(_connectionString);
        }

        public void Save()
        {
            // Confirmar transacciones si se implementan
        }

        public void Dispose() { }
    }
}
