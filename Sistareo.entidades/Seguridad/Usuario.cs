using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.entidades.Seguridad
{
    public class Usuario:Entity
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave{ get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string ApellidoMaterno { get; set; }
        public string DNI { get; set; }
        public string NombreCompleto { get; set; }
        public string Estilo { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public bool SiSuperAdmi { get; set; }
        public string ApellidoPaterno { get; set; }
        public int IdTipoUsuario { get; set; }
        public bool Jefatura { get; set; }
        public bool Coordinador { get; set; }
       
    }
}
