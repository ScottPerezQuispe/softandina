using Sistareo.entidades.Proceso;
using Sistareo.logica.Proceso;
using Sistareo.web.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistareo.web.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult ReporteRetoqueDiseño()
        {
            if (string.IsNullOrEmpty(Session[Constantes.csVariableSesion] as string))
                return RedirectToAction("Logueo", "Home");


            CultureInfo culture = new CultureInfo("es-ES");
            var FechaActual = DateTime.Now.ToString("dd/MM/yyyy", culture);
            ViewBag.FechaActual = FechaActual;
        
    
            return View();
        }

        [HttpPost]
        public JsonResult ConsultarRetoque(int IdOpcion = 0, int IdCampania = 0, int IdOperario = 0, int IdProducto = 0,int IdTipoUsuario=0, string FechaInicio = "", string FechaFin = "")
        {

            var objResult = new object();
            CultureInfo culture = new CultureInfo("es-PE");
            DateTime dFechaInicio = Convert.ToDateTime(FechaInicio,culture);
            DateTime dFechaFin = Convert.ToDateTime(FechaFin, culture);

            try
            {
                Retoque retoque = new Retoque();
                if (IdOpcion == 0)
                {

                    retoque.ListaRetoque = new RetoqueLG().ListarRetoqueCampania(IdCampania, IdOperario, IdProducto, IdTipoUsuario, dFechaInicio, dFechaFin);
                }
                else if (IdOpcion == 1)
                {
                    retoque.ListaRetoque = new RetoqueLG().ListarRetoqueOperador(IdCampania, IdOperario, IdProducto, IdTipoUsuario, dFechaInicio, dFechaFin);
                }
                else if (IdOpcion == 2)
                {
                    retoque.ListaRetoque = new RetoqueLG().ListarRetoqueProducto(IdCampania, IdOperario, IdProducto, IdTipoUsuario, dFechaInicio, dFechaFin);
                }
                else if (IdOpcion == 3)
                {
                    retoque.ListaRetoque = new RetoqueLG().ListarRetoqueDiseño(IdCampania, IdOperario, IdProducto, IdTipoUsuario, dFechaInicio, dFechaFin);
                }
                else
                {
                    retoque.ListaRetoque = new RetoqueLG().ListarRetoqueProductoDetallado(IdCampania, IdOperario, IdProducto, IdTipoUsuario, dFechaInicio, dFechaFin);
                }

                retoque.IdOpcion = IdOpcion;
                Auditoria.SetRetoque(retoque);

                objResult = new
                {
                    iTipoResultado = 1
                };
                return Json(objResult);
            }
            catch (Exception ex)
            {

                objResult = new { iTipoResultado = 2, Mensaje = ex.Message };
                return Json(objResult);
            }
        }
    }
}