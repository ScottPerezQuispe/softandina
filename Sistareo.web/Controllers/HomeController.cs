using Sistareo.entidades.Seguridad;
using Sistareo.logica.Seguridad;
using Sistareo.web.Helper;
using Sistareo.web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistareo.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { //Valida Sesión

            if (string.IsNullOrEmpty(Session[Constantes.csVariableSesion] as string))
                return RedirectToAction("Logueo", "Home");
            
            return View();
        }

        public ActionResult Logueo()
        {
           

            return View();
        }


        public ActionResult ValidarAccesos(string txtUsuario, string txtContrasena)
        {
            try
            {
                Usuario oUsuario = new Usuario();

                if (txtUsuario == null)
                {
                    ViewBag.sMensaje = "Ingrese su usuario Y/O contraseña.";
                    ViewBag.Session = 0;
                    return View("Logueo");
                }

                if (txtUsuario.Length < 3)
                {
                    ViewBag.sMensaje = "3 caracteres como mínimo para el codigo de usuario.";
                    ViewBag.Session = 0;
                    return View("Logueo");
                }

                oUsuario = new UsuarioLG() .ObtenerUsuario( txtUsuario.Trim(), txtContrasena.Trim());
                if (oUsuario.NombreUsuario != null)
                {

                    Auditoria.SetSessionValues(oUsuario);
                    List<Pagina> loenMenu = new List<Pagina>();
                    loenMenu = new RolLG().ListarMenuPorUsuario(oUsuario.NombreUsuario).ToList<Pagina>();
                    Auditoria.SetSessionMenu(loenMenu);
                    ViewBag.sMensaje = Auditoria.ObtenerPrimeroNombre() + ", bienvenido al sistema";
                   // ViewData["pRutaMenu"] = "/Home/Index";

                   

                    return View("Index");
                  
                }
                else
                {
                    ViewBag.sMensaje = Constantes.msgErrorLogueo;
                    ViewBag.Session = 0;
                    return View("Logueo");
                    //return RedirectToAction("Logueo", "Home", new { ActionValidacion = 0 });
                }
                // }
            }
            catch (Exception ex)
            {
                
                ViewBag.sMensaje = Constantes.msgErrorGeneral;
                ViewBag.Session = 0;
                return View("Logueo");
            }
        }



        [HttpPost]
        public JsonResult ObtenerInformacionUsuario()
        {
            var objResult = new object();

            try
            {
               
                Usuario oenUsuario = new Usuario();
                
                List<Pagina> loenMenu = new List<Pagina>();

            
                oenUsuario = new  UsuarioLG().ObtenerPorIdUsuario(Auditoria.ObtenerIdUsuario());
                loenMenu = Auditoria.ObtenerOpcionesMenu();
               
                objResult = new
                {
                    iTipoResultado = 1,
                    loenMenu = loenMenu,
                    oenUsuario = oenUsuario
                };
                return Json(objResult);
            }
            catch (ApplicationException aex)
            {
              
                objResult = new { Tipo = 999, vError = Constantes.msgErrorSesion };
                return Json(objResult);
            }
            catch (Exception ex)
            {
              
                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }
        }


        public ActionResult Salir()
        {
            Session.Clear();
            Session.Abandon();
            return (RedirectToAction("Logueo"));
        }
    }
}