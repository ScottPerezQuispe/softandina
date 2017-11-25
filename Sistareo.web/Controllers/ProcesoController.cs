using Sistareo.entidades.Proceso;
using Sistareo.logica.Configuracion;
using Sistareo.logica.Proceso;
using Sistareo.web.Helper;
using Sistareo.web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistareo.web.Controllers
{
    public class ProcesoController : Controller
    {
        public ActionResult Index()
        {

        var strin=    Auditoria.ObtenerNivel3("/Proceso/Index");

            CultureInfo culture = new CultureInfo("es-ES");
            var FechaActual = DateTime.Now.ToString("dd/MM/yyyy", culture);
            ViewBag.FechaActual = FechaActual;
            return View();
        }

        #region "Retoque"
        [HttpPost]
        public JsonResult RegistrarRetoque(int IdRetoque, int IdOperario , int IdCampania, string Jefatura, string Coordinador,string FechaApertura)
        {
            CultureInfo culture = new CultureInfo("es-PE");
            DateTime fechaActual =Convert.ToDateTime( FechaApertura);
            bool Resultado=false;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            var objResult = new object();
            try
            {
            

                ProcesoViewModel oRetoque = new ProcesoViewModel();
                oRetoque.retoque = new Retoque()
                {
                    IdRetoque = IdRetoque,
                    FechaApertura = fechaActual,
                    IdOperario = IdOperario,
                    IdCampania = IdCampania,
                    Jefatura = Jefatura,
                    Coordinador = Coordinador,
                    UsuarioCreacion= Auditoria.ObtenerNombreUsuario(),
                    UsuarioModificacion = Auditoria.ObtenerNombreUsuario(),
                    FechaModificacion = DateTime.Now

                };
                if (IdRetoque > 0)
                {
                    Resultado = new RetoqueLG().ActualizarRetoque(oRetoque.retoque);
                }
                else
                {
                    Resultado = new RetoqueLG().InsertarRetoque(oRetoque.retoque);
                }
                if (Resultado )
                {
                    if (IdRetoque > 0)
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
                    iTipoResultado = Resultado,
                    vMensaje = mensaje
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = 2 , vMensaje  =ex.Message};
                return Json(objResult);

            }

        }


        [HttpPost]
        public JsonResult EliminarRetoque(int IdRetoque)
        {
          ;
            bool Resultado = false;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            var objResult = new object();
            try
            {

                Resultado = new RetoqueLG().EliminarRetoque(IdRetoque,Auditoria.ObtenerNombreUsuario());
            
                
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
        public JsonResult ListarFechaPorOperario(string FechaApertura="", int IdOperador=0 )
        {
            try
            {

                var objResult = new Object();
                CultureInfo culture = new CultureInfo("es-PE");
                DateTime dFechaApertura = Convert.ToDateTime(FechaApertura);

                ProcesoViewModel VM = new ProcesoViewModel();
                VM.ListaRetoque = new RetoqueLG().ListarFechaPorOperario(dFechaApertura, IdOperador,Auditoria.IsAdmin()).ToList();
                objResult = new
                {
                    iTipoResultado = 1,
                    ListaRetoque = VM.ListaRetoque.ToList()
                };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Retoque(int IdRetoque=0)
        {
         
            CultureInfo culture = new CultureInfo("es-ES");
            var FechaActual = DateTime.Now.ToString("dd/MM/yyyy", culture);
            ViewBag.FechaActual = FechaActual;
            ViewBag.IdRetoque = IdRetoque;
            return View();
        }


        [HttpPost]
        public JsonResult ObtenerPorIdRetoque(int IdRetoque=0)
        {
            try
            {

              
                ProcesoViewModel VM = new ProcesoViewModel();
                if (IdRetoque!=0)
                {
                    VM.retoque = new RetoqueLG().ObtenerPorIdRetoque(IdRetoque);
                   
                    return Json( new { iTipoResultado = 1, Retoque = VM.retoque } );
                }
                else
                {
                    return Json(new { iTipoResultado =2, vMensaje = "No se encontraron datos para mostrar." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { iTipoResultado = 3, vMensaje = ex.Message });
            }
        }

        #endregion

        #region "Retoque Detalle"

        public ActionResult RetoqueProducto(int IdRetoque)
        {
            try
            {
                var FechaApertura = new RetoqueLG().ObtenerPorIdRetoque(IdRetoque);
                ViewBag.FechaApertura = FechaApertura.vFechaApertura;
                ViewBag.IdRetoque = IdRetoque;
            }
            catch (Exception)
            {

               
            }
           
            return View();
        }

        [HttpPost]
        public JsonResult RegistrarRetoqueProducto(int IdRetoqueProducto ,int IdRetoque, int IdProducto, string Descripcion, string FechaApertura,string HoraInicio, string HoraFin)
        {
         
            bool Resultado = false;
            int isHoras;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            CultureInfo culture = new CultureInfo("es-PE");
            DateTime dFechaApertura = Convert.ToDateTime(FechaApertura);
            var objResult = new object();
            try
            {
                TimeSpan TotalHoras;
                TotalHoras = Convert.ToDateTime(HoraFin).Subtract(Convert.ToDateTime(HoraInicio));


                ProcesoViewModel oRetoqueProducto = new ProcesoViewModel();
                oRetoqueProducto.RetoqueProducto = new RetoqueProducto()
                {
                    IdRetoqueProducto = IdRetoqueProducto,
                    IdRetoque = IdRetoque,
                    IdProducto = IdProducto,
                    DescripcionRetoqueProducto = Descripcion,
                    FechaApertura= dFechaApertura,
                    HoraInicioRetoqueProducto = HoraInicio,
                    HoraFinRetoqueProducto = HoraFin,
                    TotalRetoqueProducto = TotalHoras.ToString(),
                    UsuarioCreacion = Auditoria.ObtenerNombreUsuario(),
                    UsuarioModificacion = Auditoria.ObtenerNombreUsuario(),
                    FechaModificacion = DateTime.Now
                };

                //Valida Horas

                //isHoras = new RetoqueProductoLG().ValidarHorasRetoqueProducto(oRetoqueProducto.RetoqueProducto);

                //if (isHoras!=0)
                //{
                //    objResult = new
                //    {
                //        iTipoResultado = false,
                //        iResultado=false,
                //        vMensaje = "El rango de horas se encuentra registrado."
                //    };

                //    return Json(objResult);
                //}

                //Registrar
                if (IdRetoqueProducto > 0)
                {
                    Resultado = new RetoqueProductoLG().ActualizarRetoqueProducto(oRetoqueProducto.RetoqueProducto);
                }
                else
                {
                   Resultado = new RetoqueProductoLG().InsertarRetoqueProducto(oRetoqueProducto.RetoqueProducto);
                }
                if (Resultado)
                {
                    if (IdRetoqueProducto > 0)
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
                    iTipoResultado = Resultado,
                    vMensaje = mensaje
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = Resultado, iResultado=true, vMensaje = ex.Message };
                return Json(objResult);

            }

        }


        [HttpPost]
        public JsonResult ListarPorIdRetoque(int IdRetoque)
        {
            var objResult = new Object();
            ProcesoViewModel VM = new ProcesoViewModel();
            try
            {
                VM.ListaRetoqueProducto = new RetoqueProductoLG().ListarPorIdRetoque(IdRetoque).ToList();

                TimeSpan TotalHoras = new TimeSpan();

                foreach (var item in VM.ListaRetoqueProducto)
                {
                    TotalHoras = TotalHoras + item.TotalHoras;
                }
                int hora = (int)TotalHoras.TotalHours;
                int min = TotalHoras.Minutes;

                objResult = new
                {
                    iTipoResultado = 1,
                    TotalHoras = hora.ToString() + ':' + min.ToString().PadLeft(2, '0'),
                    ListaRetoqueProducto = VM.ListaRetoqueProducto.ToList()
                };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = 2, Mensaje = ex.Message };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult EliminarRetoqueProducto(int IdRetoqueProducto)
        {
            ;
            bool Resultado = false;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            var objResult = new object();
            try
            {

                Resultado = new RetoqueProductoLG().EliminarRetoqueProducto(IdRetoqueProducto,Auditoria.ObtenerNombreUsuario());


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
        public JsonResult EditarRetoqueProducto(int IdRetoqueProducto = 0)
        {
            try
            {


                ProcesoViewModel VM = new ProcesoViewModel();
                if (IdRetoqueProducto != 0)
                {
                    VM.RetoqueProducto = new RetoqueProductoLG().ObtenerPorIdRetoqueProducto(IdRetoqueProducto);

                    return Json(new { iTipoResultado = 1, RetoqueProducto = VM.RetoqueProducto });
                }
                else
                {
                    return Json(new { iTipoResultado = 2, vMensaje = "No se encontraron datos para mostrar." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { iTipoResultado = 3, vMensaje = ex.Message });
            }
        }


        #endregion

        #region "Retoque Detalle Producto"
        public ActionResult  RetoqueProductoDetalle(int IdRetoqueProducto=0,int IdRetoque=0)
        {
            try
            {
                ProcesoViewModel VM = new ProcesoViewModel();
                VM.RetoqueProducto = new RetoqueProductoLG().ObtenerPorIdRetoqueProducto(IdRetoqueProducto);
                ViewBag.FechaActual = VM.RetoqueProducto.vFechaApertura;
                ViewBag.IdRetoque = VM.RetoqueProducto.IdRetoque;
                ViewBag.CodigoProducto = VM.RetoqueProducto.CodigoProducto;
                ViewBag.DescripcionProducto = VM.RetoqueProducto.DescripcionProducto;
                ViewBag.IdRetoqueProducto = VM.RetoqueProducto.IdRetoqueProducto;
            }
            catch (Exception)
            {

                throw;
            }
            
            return View();
        }


        [HttpPost]
        public JsonResult RegistrarRetoqueProductoDetalle(int IdRetoqueProductoDetalle, int IdRetoqueProducto, string FechaApertura, string Descripcion, string HoraInicio, string HoraFin)
        {

            bool Resultado = false;
            int isHoras;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            CultureInfo culture = new CultureInfo("es-PE");
            DateTime dFechaApertura = Convert.ToDateTime(FechaApertura);
            var objResult = new object();
            try
            {
                TimeSpan TotalHoras;
                TotalHoras = Convert.ToDateTime(HoraFin).Subtract(Convert.ToDateTime(HoraInicio));


                ProcesoViewModel oRetoqueProducto = new ProcesoViewModel();
                oRetoqueProducto.RetoqueProductoDetalle = new RetoqueProductoDetalle()
                {
                    IdRetoqueProductoDetalle = IdRetoqueProductoDetalle,
                    IdRetoqueProducto = IdRetoqueProducto,
                    DescripcionRetoqueProductoDetalle = Descripcion,
                    FechaApertura = dFechaApertura,
                    HoraInicioRetoqueProductoDetalle = HoraInicio,
                    HoraFinRetoqueProductoDetalla = HoraFin,
                    TotalRetoqueProductoDetalle = TotalHoras.ToString(),
                    UsuarioCreacion = Auditoria.ObtenerNombreUsuario(),
                    UsuarioModificacion = Auditoria.ObtenerNombreUsuario(),
                    FechaModificacion = DateTime.Now
                };

                //Valida Horas

                //isHoras = new RetoqueProductoLG().ValidarHorasRetoqueProducto(oRetoqueProducto.RetoqueProducto);

                //if (isHoras != 0)
                //{
                //    objResult = new
                //    {
                //        iTipoResultado = false,
                //        iResultado = false,
                //        vMensaje = "El rango de horas se encuentra registrado."
                //    };

                //    return Json(objResult);
                //}

                //Registrar
                if (IdRetoqueProductoDetalle > 0)
                {
                    Resultado = new RetoqueProductoDetalleLG().ActualizarRetoqueProductoDetalle(oRetoqueProducto.RetoqueProductoDetalle);
                }
                else
                {
                    Resultado = new RetoqueProductoDetalleLG().InsertarRetoqueProductoDetalle(oRetoqueProducto.RetoqueProductoDetalle);
                }
                if (Resultado)
                {
                    if (IdRetoqueProductoDetalle > 0)
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
                    iTipoResultado = Resultado,
                    vMensaje = mensaje
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = Resultado, iResultado = true, vMensaje = ex.Message };
                return Json(objResult);

            }

        }

        [HttpPost]
        public JsonResult EditarRetoqueProductoDetalle(int IdRetoqueProductoDetalle = 0)
        {
            try
            {


                ProcesoViewModel VM = new ProcesoViewModel();
                if (IdRetoqueProductoDetalle != 0)
                {
                    VM.RetoqueProductoDetalle = new RetoqueProductoDetalleLG().ObtenerPorIdRetoqueProductoDetalle(IdRetoqueProductoDetalle);

                    return Json(new { iTipoResultado = 1, RetoqueProductoDetalle = VM.RetoqueProductoDetalle });
                }
                else
                {
                    return Json(new { iTipoResultado = 2, vMensaje = "No se encontraron datos para mostrar." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { iTipoResultado = 3, vMensaje = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarRetoqueProductoDetalle(int IdRetoqueProductoDetalle)
        {
            ;
            bool Resultado = false;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            var objResult = new object();
            try
            {

                Resultado = new RetoqueProductoDetalleLG().EliminarRetoqueProductoDetalle(IdRetoqueProductoDetalle, Auditoria.ObtenerNombreUsuario());


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
        public JsonResult ListarPorIdProductoDetalle(int IdRetoqueProducto)
        {
            var objResult = new Object();
            ProcesoViewModel VM = new ProcesoViewModel();
            try
            {
                VM.ListaRetoqueProductoDetalle = new RetoqueProductoDetalleLG().ListarPorIdRetoqueDetalle(IdRetoqueProducto).ToList();

                TimeSpan TotalHoras = new TimeSpan();

                foreach (var item in VM.ListaRetoqueProductoDetalle)
                {
                    TotalHoras = TotalHoras + item.TotalHoras;
                }

                objResult = new
                {
                    iTipoResultado = 1,
                    TotalHoras = Convert.ToString(TotalHoras).Substring(0, 5),
                    ListaRetoqueProductoDetalle = VM.ListaRetoqueProductoDetalle.ToList()
                };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = 2, Mensaje = ex.Message };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region "Operador"

        [HttpGet]
        public ActionResult ListarTodoOperador()
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            var objResult = new Object();
            int IdUsuario = Auditoria.IsAdmin();
            VM.Operador = new entidades.Configuracion.Operador();
            VM.ListaOperador = new List<entidades.Configuracion.Operador>();
            try
            {
                if (IdUsuario==0)
                {
            
                    VM.Operador.IdOperario = 0;
                    VM.Operador.NombreCompleto = "Selecciona Operador";
                    VM.ListaOperador.Add(VM.Operador);
                }
                

                var lista = new OperadorLG().ListarTodoOperador(IdUsuario).ToList();

                foreach (var item in lista)
                {
                    VM.ListaOperador.Add(item);
                }



                objResult = new
                {
                    iTipoResultado = 1,
                    ListaOperador = VM.ListaOperador,

                };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                objResult = new { iTipoResultado = 2, Mensaje = ex.Message };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region "Producto"

        [HttpGet]
        public ActionResult ListarTodoProducto()
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            var objResult = new Object();

            try
            {
                VM.ListaProducto = new ProductoLG().ListarTodoProducto().ToList();
                objResult = new
                {
                    iTipoResultado = 1,
                    ListaProducto = VM.ListaProducto,

                };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                objResult = new { iTipoResultado = 2, Mensaje = ex.Message };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Producto

        [HttpGet]
        public ActionResult ListarTodoCampania()
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            var objResult = new Object();

            try
            {
                VM.ListaCampania = new CampaniaLG().ListarTodoCampania().ToList();
                objResult = new
                {
                    iTipoResultado = 1,
                    ListaCampania = VM.ListaCampania,

                };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                objResult = new { iTipoResultado = 2, Mensaje = ex.Message };
                return Json(objResult, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}