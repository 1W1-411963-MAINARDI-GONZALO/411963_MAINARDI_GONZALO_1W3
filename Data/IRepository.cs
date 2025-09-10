using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _411963_MAINARDI_GONZALO_1W3.Data
{
    public interface IRepositorio<T> where T : class
    {
        void Agregar(T entidad);
        void Actualizar(T entidad);
        void Eliminar(T entidad);
        T ObtenerPorId(int id);
        List<T> ObtenerTodos();
    }
}
