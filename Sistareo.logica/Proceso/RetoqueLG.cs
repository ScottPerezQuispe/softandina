using Sistareo.datos.Proceso;
using Sistareo.entidades.Proceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.logica.Proceso
{
    public class RetoqueLG
    {
        public bool InsertarRetoque(Retoque oRetoque)
        {
            return new RetoqueDA().InsertarRetoque(oRetoque);
        }
        public bool ActualizarRetoque(Retoque oRetoque)
        {
            return new RetoqueDA().ActualizarRetoque(oRetoque);
        }
        public bool EliminarRetoque(int IdRetoque, string UsuarioModificacion)
        {
            return new RetoqueDA().EliminarRetoque(IdRetoque, UsuarioModificacion);
        }
        public Retoque ObtenerPorIdRetoque(int IdRetoque)
        {
            return new RetoqueDA().ObtenerPorIdRetoque(IdRetoque);
        }
        public List<Retoque> ListarFechaPorOperario(DateTime FechaApertura, int IdOperario, int IdUsuario)
        {
            return new RetoqueDA().ListarFechaPorOperario(FechaApertura, IdOperario, IdUsuario);
        }


        #region "Reporte"

        public List<Retoque> ListarRetoqueDiseño(int IdCampania, int IdOperario, int IdProducto, int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            return new RetoqueDA().ListarRetoqueDiseño(IdCampania, IdOperario, IdProducto, IdTipoUsuario, FechaInicio, FechaFin);
        }
        public List<Retoque> ListarRetoqueCampania(int IdCampania, int IdOperario, int IdProducto, int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            return new RetoqueDA().ListarRetoqueCampania(IdCampania, IdOperario, IdProducto, IdTipoUsuario, FechaInicio, FechaFin);
        }

        public List<Retoque> ListarRetoqueOperador(int IdCampania, int IdOperario, int IdProducto, int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            return new RetoqueDA().ListarRetoqueOperador(IdCampania, IdOperario, IdProducto, IdTipoUsuario, FechaInicio, FechaFin);
        }
        public List<Retoque> ListarRetoqueProducto(int IdCampania, int IdOperario, int IdProducto, int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            return new RetoqueDA().ListarRetoqueProducto(IdCampania, IdOperario, IdProducto, IdTipoUsuario, FechaInicio, FechaFin);
        }

        #endregion
    }
}
