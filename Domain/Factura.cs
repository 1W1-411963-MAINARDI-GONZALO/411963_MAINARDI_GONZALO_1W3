using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _411963_MAINARDI_GONZALO_1W3.Domain
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Cliente { get; set; }
        public FormaPago FormaPago { get; set; }
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();

        // Controla que, si se agrega el mismo artículo, sume la cantidad
        public void AgregarDetalle(Articulo articulo, int cantidad)
        {
            var existente = Detalles.FirstOrDefault(d => d.Articulo != null && d.Articulo.IdArticulo == articulo.IdArticulo);
            if (existente != null)
            {
                existente.Cantidad += cantidad;
            }
            else
            {
                Detalles.Add(new DetalleFactura { Articulo = articulo, Cantidad = cantidad });
            }
        }
    }
}
