using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.entidades.Proceso
{
    public class RetoqueProducto : Entity
    {
        public int IdRetoqueProducto { get; set; }
        public int IdRetoque { get; set; }
        public int IdProducto { get; set; }
        public string DescripcionRetoqueProducto { get; set; }
        public string HoraInicioRetoqueProducto { get; set; }
        public string HoraFinRetoqueProducto { get; set; }
        public string TotalRetoqueProducto { get; set; }
        public string TotalDetalleRetoqueProducto { get; set; }
        public string CodigoBarra { get; set; }
        public string DescripcionProducto { get; set; }
        public DateTime FechaApertura { get; set; }
        public TimeSpan TotalHoras { get; set; }
        public string vFechaApertura { get; set; }
        public string CodigoProducto { get; set; }
    }
}
