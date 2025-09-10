using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _411963_MAINARDI_GONZALO_1W3.Data
{
    public class UnitOfWork : IDisposable
    {
        public IFacturaRepository Facturas { get; private set; }

        public UnitOfWork(string connectionString)
        {
            Facturas = new IFacturaRepository (connectionString);
        }

        public void Dispose()
        {
            
        }
    }
}
