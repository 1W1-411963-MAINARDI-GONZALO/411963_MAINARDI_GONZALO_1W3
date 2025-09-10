using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _411963_MAINARDI_GONZALO_1W3.Data;
using _411963_MAINARDI_GONZALO_1W3.Domain;

namespace _411963_MAINARDI_GONZALO_1W3.Services
{
    public class FacturaService
    {
        private readonly IUnitOfWork _uow;

        public FacturaService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public int RegistrarFactura(Factura factura)
        {
            try
            {
                // Lógica de negocio previa (validaciones) aquí...
                int nro = _uow.Facturas.Add(factura);
                _uow.Commit();
                return nro;
            }
            catch
            {
                _uow.Rollback();
                throw;
            }
        }

        public Factura ObtenerFactura(int nro)
        {
            return _uow.Facturas.GetById(nro);
        }
    }
}
