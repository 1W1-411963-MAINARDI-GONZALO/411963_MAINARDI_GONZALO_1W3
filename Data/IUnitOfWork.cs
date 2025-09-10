using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _411963_MAINARDI_GONZALO_1W3.Data
{
    public interface IUnitOfWork
    {
        IFacturaRepository Facturas { get; }
        void Commit();
        void Rollback();
        void Dispose();
    }
}
