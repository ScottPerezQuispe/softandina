using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.entidades.Seguridad
{
    public class Rol : Entity
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool SiSuperAdmi { get; set; }
        public string Estilo { get; set; }
        public int iTotalUsuarios { get; set; }
        public string TotalUsuario { get; set; }
    }
}
