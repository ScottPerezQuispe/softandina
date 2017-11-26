using Sistareo.entidades;
using Sistareo.entidades.Configuracion;
using Sistareo.logica.Configuracion;
using Sistareo.web.Helper;
using Sistareo.web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistareo.web.Controllers
{
    
    public class ConfiguracionController : Controller
    {
        #region "Campana"
        public ActionResult Campania()
        {
            if (string.IsNullOrEmpty(Session[Constantes.csVariableSesion] as string))
                return RedirectToAction("Logueo", "Home");

            return View();
        }

        [HttpPost]
        public JsonResult ListarCapania(string Nombres="")
        {
            var objResult = new object();
            try
            {
                ConfiguracionViewModel vm = new ConfiguracionViewModel();
                //Listas
                vm.ListaCampania = new CampaniaLG().ListarCampaniaPorNombre(Nombres).ToList();

                objResult = new
                {
                    iTipoResultado = 1,
                    ListaCampania = vm.ListaCampania,
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {

                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }

        }

        public ActionResult NuevoCampania(int IdCampania = 0)
        {

            ViewData["IdCampania"] = IdCampania;
            return View();
        }

        [HttpPost]
        public JsonResult RegistrarCampania(int IdCampania, string Nombre, string Descripcion)
        {
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            var objResult = new object();
            bool iResultado;
            try
            {
                //Listas

                ConfiguracionViewModel vm = new ConfiguracionViewModel();
                vm.Campania = new Campania();
                vm.Campania.IdCampania= IdCampania;
                vm.Campania.Nombre = Nombre;
                vm.Campania.Descripcion= Descripcion;
             
                
                vm.Campania.UsuarioCreacion = Auditoria.ObtenerNombreUsuario();
                vm.Campania.UsuarioModificacion = Auditoria.ObtenerNombreUsuario();

                if (IdCampania == 0)
                {
                    iResultado = new CampaniaLG().InsertarCampania(vm.Campania);
                }
                else
                {
                    iResultado = new CampaniaLG().ActualizarCampania(vm.Campania);
                }
                if (iResultado)
                {
                    if (IdCampania > 0)
                    {
                        mensaje = "Se modificó correctamente.";
                    }
                    else
                    {
                        mensaje = "Se registró correctamente.";
                    }
                }
                objResult = new
                {
                    iTipoResultado = iResultado,
                    vMensaje = mensaje
                };
                return Json(objResult);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }
        }

        [HttpPost]
        public JsonResult EliminarCampania(int IdCampania)
        {
            ;
            bool Resultado = false;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            var objResult = new object();
            try
            {

                Resultado = new CampaniaLG().EliminarCampania(IdCampania, Auditoria.ObtenerNombreUsuario());


                if (Resultado)
                {
                    mensaje = "Se eliminó correctamente.";
                }
                objResult = new
                {
                    iTipoResultado = Resultado,
                    vMensaje = mensaje
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = Resultado, vMensaje = ex.Message };
                return Json(objResult);

            }

        }

        [HttpPost]
        public JsonResult ObtenerCampania(int IdCampania = 0)
        {
            var objResult = new object();
            try
            {


                Campania Campania = new Campania();

                Campania = new CampaniaLG().ObtenerPorIdCampania(IdCampania);

                objResult = new
                {
                    iTipoResultado = 1,
                    Campania = Campania
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {

                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }
        }

        #endregion


        #region "Producto"
        public ActionResult Producto()
        {
            if (string.IsNullOrEmpty(Session[Constantes.csVariableSesion] as string))
                return RedirectToAction("Logueo", "Home");

            return View();
        }

        [HttpPost]
        public JsonResult ListarProducto(string Nombres = "")
        {
            var objResult = new object();
            try
            {
                ConfiguracionViewModel vm = new ConfiguracionViewModel();
                //Listas
                vm.ListaProducto = new ProductoLG().ListarProductoPorNombre(Nombres).ToList();

                objResult = new
                {
                    iTipoResultado = 1,
                    ListaProducto = vm.ListaProducto,
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {

                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }

        }

        public ActionResult NuevoProducto(int IdProducto = 0)
        {
           ViewData["IdProducto"] = IdProducto;
            return View();
        }

        [HttpPost]
        public JsonResult RegistrarProducto(int IdProducto =0, string CodigoProducto="", string CodigoBarra = "", string DescripcionProducto = "")
        {
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            var objResult = new object();
            bool iResultado;
            try
            {
                //Listas

                ConfiguracionViewModel vm = new ConfiguracionViewModel();
                vm.Producto = new Producto();
                vm.Producto.IdProducto = IdProducto;
                vm.Producto.CodigoProducto = CodigoProducto;
                vm.Producto.CodigoBarra = CodigoBarra;
                vm.Producto.DescripcionProducto = DescripcionProducto;
                vm.Producto.UsuarioCreacion = Auditoria.ObtenerNombreUsuario();
                vm.Producto.UsuarioModificacion = Auditoria.ObtenerNombreUsuario();

                if (IdProducto == 0)
                {
                    iResultado = new ProductoLG().InsertarProducto(vm.Producto);
                }
                else
                {
                    iResultado = new ProductoLG().ActualizarProducto(vm.Producto);
                }
                if (iResultado)
                {
                    if (IdProducto > 0)
                    {
                        mensaje = "Se modificó correctamente.";
                    }
                    else
                    {
                        mensaje = "Se registró correctamente.";
                    }
                }
                objResult = new
                {
                    iTipoResultado = iResultado,
                    vMensaje = mensaje
                };
                return Json(objResult);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }
        }

        [HttpPost]
        public JsonResult EliminarProducto(int IdProducto)
        {
            ;
            bool Resultado = false;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            var objResult = new object();
            try
            {

                Resultado = new ProductoLG().EliminarProducto(IdProducto, Auditoria.ObtenerNombreUsuario());


                if (Resultado)
                {
                    mensaje = "Se eliminó correctamente.";
                }
                objResult = new
                {
                    iTipoResultado = Resultado,
                    vMensaje = mensaje
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = Resultado, vMensaje = ex.Message };
                return Json(objResult);

            }

        }

        [HttpPost]
        public JsonResult ObtenerProductoPorId(int IdProducto = 0)
        {
            var objResult = new object();
            try
            {
                Producto Producto = new Producto();
                Producto = new ProductoLG().ObtenerPorIdProducto(IdProducto);
                objResult = new
                {
                    iTipoResultado = 1,
                    Producto = Producto
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {

                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }
        }

        #endregion
    }
}