using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.entidades.Proceso
{
    public class Retoque: Entity
    {
        public int IdRetoque { get; set; }
        public int IdOperario { get; set; }
        public int IdCampania { get; set; }
        public DateTime FechaApertura { get; set; }
        public string Jefatura { get; set; }
        public string Coordinador { get; set; }
        public string vFechaApertura { get; set; }
        public string NombreCompleto { get; set; }
        public string Compania { get; set; }
        public string Producto { get; set; }
        public string Operario { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string TotalHoras { get; set; }
        public string Descipcion { get; set; }


        public List<Retoque> ListaRetoque { get; set; }
        public int IdOpcion { get; set; }
        public string NombreCampania { get; set; }
        public string TotalHorasGeneral { get; set; }
        public string TotalDetalle { get; set; }
    }
}
