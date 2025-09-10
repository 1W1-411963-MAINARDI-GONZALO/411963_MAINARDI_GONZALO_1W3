using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _411963_MAINARDI_GONZALO_1W3.Data;
using _411963_MAINARDI_GONZALO_1W3.Data.Implementations;
using _411963_MAINARDI_GONZALO_1W3.Data.Interfaces;
using _411963_MAINARDI_GONZALO_1W3.Domain;

namespace _411963_MAINARDI_GONZALO_1W3.Services
{
    public class FacturaService
    {
        private readonly UnitOfWork _unitOfWork;

        public FacturaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int CrearFactura(Factura factura)
        {
            if (factura.Detalles.Count == 0)
                throw new Exception("Debe tener al menos un detalle.");

            return _unitOfWork.Facturas.InsertarFactura(factura);
        }
    }
}
