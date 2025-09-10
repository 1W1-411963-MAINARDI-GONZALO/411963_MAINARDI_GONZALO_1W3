using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _411963_MAINARDI_GONZALO_1W3.Domain
{
    public class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
    }
}
