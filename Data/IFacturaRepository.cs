using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _411963_MAINARDI_GONZALO_1W3.Domain;

namespace _411963_MAINARDI_GONZALO_1W3.Data
{
    public interface IFacturaRepository
    {
        int Add(Factura factura); // devuelve NroFactura insertado
        Factura GetById(int nroFactura);
        IEnumerable<Factura> GetAll();
        void Delete(int nroFactura);
    }
}
