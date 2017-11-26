using Sistareo.entidades.Configuracion;
using Sistareo.entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistareo.web.Models.ViewModel
{
    public class SeguridadViewModel
    {
        public Usuario Usuario { get; set; }
        public List<Usuario> ListaUsuario { get; set; }
        public Rol Rol { get; set; }
        public List<Rol> ListaRol { get; set; }
        public List<RolPagina> ListaRolPagina { get; set; }

        public RolPagina RolPagina { get; set; }
        public List<Pagina> ListaPagina { get; set; }
        public List<Pagina> ListaPaginaTemp { get; set; }

        public Pagina Pagina { get; set; }


        public List<Tipo> ListaTipoUsuario { get; set; }
    }
}