using Sistareo.entidades;
using Sistareo.entidades.Configuracion;
using Sistareo.entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistareo.web.Models.ViewModel
{
    public class ConfiguracionViewModel
    {
        public Operador Operador { get; set; }
        public List<Operador> ListaOperador{ get; set; }
        public List<Producto> ListaProducto { get; set; }
        public Producto Producto { get; set; }
        public List<Campania> ListaCampania { get; set; }
        public Campania Campania { get; set; }

        public List<Usuario> ListaJefatura{ get; set; }
        public List<Usuario> ListaCoordinador { get; set; }
    }
}