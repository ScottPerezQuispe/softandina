using Sistareo.datos.Configuracion;
using Sistareo.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.logica.Configuracion
{
    public class ProductoLG
    {

        public bool InsertarProducto(Producto oProducto)
        {
            return new ProductoDA().InsertarProducto(oProducto);
        }

        public bool ActualizarProducto(Producto oProducto)
        {
            return new ProductoDA().ActualizarProducto(oProducto);
        }

        public bool EliminarProducto(int IdProducto, string UsuarioModificacion)
        {
            return new ProductoDA().EliminarProducto(IdProducto, UsuarioModificacion);
        }
        public Producto ObtenerPorIdProducto(int IdProducto)
        {
            return new ProductoDA().ObtenerPorIdProducto(IdProducto);
        }

        public List<Producto> ListarProductoPorNombre(string Nombre)
        {
            return new ProductoDA().ListarProductoPorNombre(Nombre);
        }

        public List<Producto> ListarTodoProducto()
        {
            return new ProductoDA().ListarTodoProducto();
        }
    }
}
