using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.entidades.Configuracion
{
   public  class Campania :Entity
    {
        public int IdCampania { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
   
    }
}
