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
        private readonly UnitOfWork _unitOfWork;

        public FacturaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CrearFactura(Factura factura)
        {
            _unitOfWork.Facturas.Add(factura);
            _unitOfWork.Save();
        }

        public Factura ObtenerFactura(int nroFactura)
        {
            return _unitOfWork.Facturas.GetById(nroFactura);
        }
    }
}
