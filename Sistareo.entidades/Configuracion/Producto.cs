using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.entidades
{
    public class Producto : Entity
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoBarra { get; set; }
        public string DescripcionProducto { get; set; }
    
    }
}
