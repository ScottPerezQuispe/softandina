using Sistareo.datos.Configuracion;
using Sistareo.entidades.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.logica.Configuracion
{
    public class TipoLG
    {
        public List<Tipo> ListarTipoUsuario(int IdSupertipo)
        {
            return new TipoDA().ListarTipoUsuario(IdSupertipo);
        }
    }
}
