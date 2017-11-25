using Sistareo.datos.Configuracion;
using Sistareo.entidades.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.logica.Configuracion
{
    public class CampaniaLG
    {
        public bool InsertarCampania(Campania oCampania)
        {
            return new CampaniaDA().InsertarCampania(oCampania);
        }

        public bool ActualizarCampania(Campania oCampania)
        {
            return new CampaniaDA().ActualizarCampania(oCampania);
        }

        public bool EliminarCampania(int IdCampania, string UsuarioModificacion)
        {
            return new CampaniaDA().EliminarCampania(IdCampania, UsuarioModificacion);
        }
        public Campania ObtenerPorIdCampania(int IdCampania)
        {
            return new CampaniaDA().ObtenerPorIdCampania(IdCampania);
        }

        public List<Campania> ListarCampaniaPorNombre(string Nombre)
        {
            return new CampaniaDA().ListarCampaniaPorNombre(Nombre);
        }
        public List<Campania> ListarTodoCampania()
        {
            return new CampaniaDA().ListarTodoCampania();
        }
    }
}
