using Sistareo.entidades.Proceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistareo.web.Models.ViewModel
{
    public class ProcesoViewModel
    {
        public Retoque retoque { get; set; }
        public RetoqueProducto RetoqueProducto { get; set; }
        public RetoqueProductoDetalle RetoqueProductoDetalle { get; set; }
        public List<Retoque> ListaRetoque { get; set; }
        public List<RetoqueProducto> ListaRetoqueProducto { get; set; }
        public List<RetoqueProductoDetalle> ListaRetoqueProductoDetalle { get; set; }
    }
}