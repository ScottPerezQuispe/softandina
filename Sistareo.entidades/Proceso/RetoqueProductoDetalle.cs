using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.entidades.Proceso
{
    public class RetoqueProductoDetalle :Entity
    {
        public int IdRetoqueProductoDetalle { get; set; }
        public int IdRetoqueProducto { get; set; }
        public string DescripcionRetoqueProductoDetalle { get; set; }
        public DateTime FechaApertura { get; set; }
        public string HoraInicioRetoqueProductoDetalle { get; set; }
        public string HoraFinRetoqueProductoDetalla { get; set; }
        public string TotalRetoqueProductoDetalle { get; set; }
        public TimeSpan TotalHoras { get; set; }
    }
}
