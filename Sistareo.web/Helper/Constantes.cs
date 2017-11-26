using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistareo.web.Helper
{
    public class Constantes
    {

        //ConstantesError
        public const string msgErrorGeneralListado = "Ha ocurrido un error al momento de Listar la información. Si el problema persiste consulte con el administrador.";
        public const string msgErrorGeneral = "Ha ocurrido un error inesperado. Si el problema persiste consulte con el administrador.";
        public const string msgErrorGrabado = "Los datos no han podido ser guardados. Por favor vuelva intentarlo o consulte con el administrador.";
        public const string msgErrorSesion = "Su sesión ha expirado por exceso de tiempo de inactividad. Por favor ingrese nuevamente al Sistema.";
        public const string msgErrorSesionTrace = "Session Invalidate.";
        public const string msgErrorMenuSeguridad = "Se ha intentado ingresara una pantalla a la que no tiene acceso.";

        public static string msgErrorLogueo="El usuario y/o contraseña son incorrectos.";

        //CONTROL SESION
        public const string csVariableSesion = "SESSION_NOMBREUSUARIO";

        //PARAMETROS
        public const int TipoUsuario = 2;

    }
}