using Sistareo.datos.Proceso;
using Sistareo.entidades.Proceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.logica.Proceso
{
    public class RetoqueProductoLG
    {
        public bool InsertarRetoqueProducto(RetoqueProducto oRetoque)
        {
            return new RetoqueProductoDA().InsertarRetoqueProducto(oRetoque);
        }
        public bool ActualizarRetoqueProducto(RetoqueProducto oRetoque)
        {
            return new RetoqueProductoDA().ActualizarRetoqueProducto(oRetoque);
        }
        public bool EliminarRetoqueProducto(int IdRetoqueProducto, string UsuarioModificacion)
        {
            return new RetoqueProductoDA().EliminarRetoqueProducto(IdRetoqueProducto, UsuarioModificacion);
        }
        public RetoqueProducto ObtenerPorIdRetoqueProducto(int IdRetoqueProducto)
        {
            return new RetoqueProductoDA().ObtenerPorIdRetoqueProducto(IdRetoqueProducto);
        }
        public List<RetoqueProducto> ListarPorIdRetoque(int IdRetoque)
        {
            return new RetoqueProductoDA().ListarPorIdRetoque(IdRetoque);
        }
        public int ValidarHorasRetoqueProducto(RetoqueProducto oRetoqueProducto)
        {
            return new RetoqueProductoDA().ValidarHorasRetoqueProducto(oRetoqueProducto);
        }
        public bool ActualizarRetoqueProductoTotal(RetoqueProducto oRetoqueProducto)
        {
            return new RetoqueProductoDA().ActualizarRetoqueProductoTotal(oRetoqueProducto);
        }

    }
}
