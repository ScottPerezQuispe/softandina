using Sistareo.entidades.Proceso;
using Sistareo.entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistareo.web.Helper
{
   

    public class Auditoria
    {
        private const string SESSION_IDUSUARIO = "SESSION_IDUSUARIO";
     
        private const string SESSION_NOMBREUSUARIO = "SESSION_NOMBREUSUARIO";
        private const string SESSION_NOMBRECOMPLETO = "SESSION_NOMBRECOMPLETO";
        private const string SESSION_PRIMERNOMBRE = "SESSION_PRIMERNOMBRE";
        private const string SESSION_NOMBRES = "SESSION_NOMBRES";
        private const string SESSION_APELLIDOS = "SESSION_APELLIDOS";
        private const string SESSION_ROL = "SESSION_ROL";
        private const string SESSION_IDROL = "SESSION_IDROL";
        private const string SESSION_ACCESOS = "SESSION_ACCESOS";
        private const string SESSION_SUPERADMIN = "SESSION_SUPERADMIN";
        private const string SESSION_RETOQUE = "SESSION_RETOQUE";
        #region "Obtiene Datos del Usuario"

        public static int ObtenerIdUsuario()
        {
            return ((HttpContext.Current.Session[SESSION_IDUSUARIO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IDUSUARIO].ToString()));
        }
       
        public static int ObtenerIdRol()
        {
            return ((HttpContext.Current.Session[SESSION_IDROL] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IDROL].ToString()));
        }
        public static string ObtenerNombreUsuario()
        {
            return ((HttpContext.Current.Session[SESSION_NOMBREUSUARIO] == null) ? "" : HttpContext.Current.Session[SESSION_NOMBREUSUARIO].ToString());
        }
        public static string ObtenerNombreCompleto()
        {
            return ((HttpContext.Current.Session[SESSION_NOMBRECOMPLETO] == null) ? "" : HttpContext.Current.Session[SESSION_NOMBRECOMPLETO].ToString());
        }
        public static string ObtenerPrimeroNombre()
        {
            return ((HttpContext.Current.Session[SESSION_PRIMERNOMBRE] == null) ? "" : HttpContext.Current.Session[SESSION_PRIMERNOMBRE].ToString());
        }
        public static string ObtenerNombres()
        {
            return ((HttpContext.Current.Session[SESSION_NOMBRES] == null) ? "" : HttpContext.Current.Session[SESSION_NOMBRES].ToString());
        }
        public static string ObtenerApellidos()
        {
            return ((HttpContext.Current.Session[SESSION_APELLIDOS] == null) ? "" : HttpContext.Current.Session[SESSION_APELLIDOS].ToString());
        }

        public static int IsAdmin()
        {
            return ((Convert.ToBoolean( HttpContext.Current.Session[SESSION_SUPERADMIN])) ? 0 : Convert.ToInt32( HttpContext.Current.Session[SESSION_IDUSUARIO]));
        }

        public static string ObtenerFechaSistema()
        {
            return DateTime.Now.ToShortDateString();
        }
        public static string ObtenerAccesos()
        {
            return ((HttpContext.Current.Session[SESSION_ACCESOS] == null) ? "" : HttpContext.Current.Session[SESSION_ACCESOS].ToString());
        }

        public static void SetRetoque(Retoque retoque)
        {
             HttpContext.Current.Session[SESSION_RETOQUE] = retoque; 
        }

        public static Retoque ObtenerRetoque()
        {
            return  (Retoque) HttpContext.Current.Session[SESSION_RETOQUE] ;
        }
        public static void SetSessionValues(Usuario oUsuario)
        {
            HttpContext.Current.Session[SESSION_IDUSUARIO] = oUsuario.IdUsuario;
         
            HttpContext.Current.Session[SESSION_IDROL] = oUsuario.IdRol;
            HttpContext.Current.Session[SESSION_NOMBREUSUARIO] = oUsuario.NombreUsuario;
            HttpContext.Current.Session[SESSION_NOMBRECOMPLETO] = oUsuario.Nombres + " " + oUsuario.Apellidos;
            HttpContext.Current.Session[SESSION_NOMBRES] = oUsuario.Nombres;
            HttpContext.Current.Session[SESSION_APELLIDOS] = oUsuario.Apellidos;
            String Solonombre = oUsuario.Nombres;
            String[] Nombre = Solonombre.Split(' ');
            HttpContext.Current.Session[SESSION_PRIMERNOMBRE] = Nombre[0].Substring(0, 1).ToUpper() + Nombre[0].Substring(1).ToLower();
            HttpContext.Current.Session[SESSION_ROL] = oUsuario.NombreRol;
            HttpContext.Current.Session[SESSION_SUPERADMIN] = oUsuario.SiSuperAdmi;
            
        }

        //public static void SetSessionValues(enAcceso oenAcceso)
        //{
        //    HttpContext.Current.Session[SESSION_IDUSUARIO] = oenAcceso.idUsuario;
    
        //    HttpContext.Current.Session[SESSION_IDROL] = oenAcceso.iidRol;
        //    HttpContext.Current.Session[SESSION_NOMBREUSUARIO] = oenAcceso.vNombreUsuario;
        //    HttpContext.Current.Session[SESSION_NOMBRECOMPLETO] = oenAcceso.sNombres + " " + oenAcceso.sApellidos;
        //    HttpContext.Current.Session[SESSION_NOMBRES] = oenAcceso.sNombres;
        //    HttpContext.Current.Session[SESSION_APELLIDOS] = oenAcceso.sApellidos;
        //    String Solonombre = oenAcceso.sNombres;
        //    String[] Nombre = Solonombre.Split(' ');
        //    HttpContext.Current.Session[SESSION_PRIMERNOMBRE] = Nombre[0].Substring(0, 1).ToUpper() + Nombre[0].Substring(1).ToLower();

        //    HttpContext.Current.Session[SESSION_ACCESOS] = oenAcceso.Accesos;
        //}


        #endregion

        #region Obtener Menu
        private const string SESSION_MENU = "SESSION_MENU";

        public static List<Pagina> ObtenerOpcionesMenu()
        {
            List<Pagina> loenMenu = new List<Pagina>();
            return ((HttpContext.Current.Session[SESSION_MENU] == null) ? loenMenu : (List<Pagina>)HttpContext.Current.Session[SESSION_MENU]);
        }

        public static void SetSessionMenu(List<Pagina> lpoenmenu)
        {
            HttpContext.Current.Session[SESSION_MENU] = lpoenmenu;
        }

        public static string ObtenerNivel3(string path)
        {
            string resultado = "";
            List<Pagina> loenmenu = new List<Pagina>();
            loenmenu = ObtenerOpcionesMenu();
            if (loenmenu.Count > 0)
            {
                var newList = loenmenu.Where(x => x.URL == path).ToList<Pagina>();
                if (newList.Count > 0)
                {
                    string AccesoDirecto = "1";
                    if (AccesoDirecto.Equals("1"))
                    {
                        var ListHijos = loenmenu.Where(x => x.IdModulo == newList[0].IdModulo && x.IdTipoOpcionMenu == 2).ToList<Pagina>();
                        if (ListHijos.Count > 0)
                        {
                            int contador = 1;
                            foreach (Pagina item in ListHijos)
                            {
                                if (contador == ListHijos.Count)
                                {
                                    resultado = resultado + item.URL;
                                }
                                else
                                {
                                    resultado = resultado + item.URL + "|";
                                }
                                contador++;
                            }
                        }
                    }
                    else
                    {
                        var ListHijos = loenmenu.Where(x => x.IdPadre == newList[0].IdPadre).ToList<Pagina>();
                        if (ListHijos.Count > 0)
                        {
                            int contador = 1;
                            foreach (Pagina item in ListHijos)
                            {
                                if (contador == ListHijos.Count)
                                {
                                    resultado = resultado + item.URL;
                                }
                                else
                                {
                                    resultado = resultado + item.URL + "|";
                                }
                                contador++;
                            }
                        }
                    }
                }
            }
            return resultado;
        }

        public static string ObtenerRutaServidor(string Path, int iCodigoModulo)
        {
            string resultado = "";
            List<Pagina> loenmenu = new List<Pagina>();
            loenmenu = ObtenerOpcionesMenu();
            if (loenmenu.Count > 0)
            {
                var newList = loenmenu.Where(x => x.IdModulo == iCodigoModulo && x.Url == Path).ToList<Pagina>();
                if (newList.Count > 0)
                {
                    resultado = newList[0].URL;
                }
            }
            return resultado;
        }

        #endregion


  

     

    }
}