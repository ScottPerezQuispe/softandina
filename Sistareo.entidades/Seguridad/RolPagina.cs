using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.entidades.Seguridad
{
    public class RolPagina:Entity
    {
        public string EstiloMenu { get; set; }
        public string EstiloModulo { get; set; }
        public int IdModulo { get; set; }
        public int IdOrden { get; set; }
        public int IdOrdenPadre { get; set; }
        public int IdPadre { get; set; }
        public int IdPadre1 { get; set; }
        public int IdPagina { get; set; }
        public int IdRol { get; set; }
        public int IdTipoOpcionMenu { get; set; }
        public string Modulo { get; set; }
        public string Nombre { get; set; }
        public bool Seleccion { get; set; }
        public string URL { get; set; }
    }
}
