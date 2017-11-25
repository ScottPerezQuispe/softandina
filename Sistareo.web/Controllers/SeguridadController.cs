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
    public class SeguridadController : Controller
    {
        // GET: Seguridad

        #region Usuario
        public ActionResult Usuario()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListarUsuario(string Nombres, string Usuario)
        {
            var objResult = new object();
            try
            {
                SeguridadViewModel vm = new SeguridadViewModel();
                //Listas
                int IdUsuario = Auditoria.IsAdmin();
                vm.ListaUsuario = new UsuarioLG().ListarUsuarioPorNombre(Nombres, Usuario, IdUsuario).ToList();

                objResult = new
                {
                    iTipoResultado = 1,

                    ListaUsuario = vm.ListaUsuario,

                };


                return Json(objResult);
            }
            catch (Exception ex)
            {

                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }

        }

        public ActionResult NuevoUsuario(int IdUsuario = 0)
        {

            ViewData["IdUsuario"] = IdUsuario;
            return View();
        }


        [HttpPost]
        public JsonResult RegistrarUsuario( int IdUsuario ,string Nombres,string ApellidoPaterno,string ApellidoMaterno, 
                                            string DNI,int IdRol,string NombreUsuario,string Clave)
        {
            
            var objResult = new object();
            int iResultado = 0;
            try
            {
                //Listas

                SeguridadViewModel vm = new SeguridadViewModel();
                vm.Usuario = new Usuario();
                vm.Usuario.IdUsuario = IdUsuario;
                vm.Usuario.Nombres = Nombres;
                vm.Usuario.ApellidoPaterno = ApellidoPaterno;
                vm.Usuario.ApellidoMaterno = ApellidoMaterno;
                vm.Usuario.DNI = DNI;
                vm.Usuario.NombreUsuario = NombreUsuario;
                vm.Usuario.Clave = Clave;
                vm.Usuario.Nombres = Nombres;
                vm.Usuario.IdRol = IdRol;
                vm.Usuario.UsuarioCreacion = Auditoria.ObtenerNombreUsuario();
                vm.Usuario.UsuarioModificacion = Auditoria.ObtenerNombreUsuario();

                if (IdUsuario == 0)
                {
                    iResultado = new UsuarioLG().InsertarUsuario(vm.Usuario);
                }
                else
                {
                    iResultado = new UsuarioLG().ActualizarUsuario(vm.Usuario);
                }
                if (iResultado >= 0)
                {
                    if (IdUsuario == 0)
                    {
                        objResult = new
                        {
                            iTipoResultado = 1
                        };
                    }
                    else
                    {
                        objResult = new
                        {
                            iTipoResultado = 3
                        };
                    }


                }
                else
                {
                    objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                }
                return Json(objResult);
            }
            catch (Exception ex)
            {
                
                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }
        }


        [HttpPost]
        public JsonResult EliminarUsuario(int IdUsuario)
        {
            ;
            bool Resultado = false;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
            var objResult = new object();
            try
            {

                Resultado = new UsuarioLG().EliminarUsuario(IdUsuario, Auditoria.ObtenerNombreUsuario());


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
        public JsonResult ObtenerUsuario(int IdUsuario = 0)
        {
            var objResult = new object();
            try
            {


                Usuario Usuario = new Usuario();

                Usuario = new UsuarioLG().ObtenerPorIdUsuario(IdUsuario);

                objResult = new
                {
                    iTipoResultado = 1,
                    Usuario = Usuario
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


        #region Rol

        public ActionResult Rol()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListarRoles(string Nombre="")
        {
            var objResult = new object();
            try
            {
                //Listas
                SeguridadViewModel vm = new SeguridadViewModel();

                vm.ListaRol = new RolLG().ListarRolPorNombre(Nombre).ToList();

                    objResult = new
                    {
                        iTipoResultado = 1,

                        ListaRol = vm.ListaRol,
                        TotalRegistros = vm.ListaRol.Count()

                    };
                
                return Json(objResult);
            }
            catch (Exception ex)
            {
                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneralListado };
                return Json(objResult);
            }

        }


        public ActionResult NuevoRol(int idRol)
        {
            ViewData["idRol"] = idRol;
            return View();
        }

        [HttpGet]
        public JsonResult ListarTodoRol()
        {
            SeguridadViewModel VM = new SeguridadViewModel();
            var objResult = new Object();

            try
            {
                VM.ListaRol = new RolLG().ListarRol().ToList();
                objResult = new
                {
                    iTipoResultado = 1,
                    ListaRol = VM.ListaRol,

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
        public JsonResult ListarMenuRol(int idRol)
        {
            var objResult = new object();
            try
            {
                //Listas
                SeguridadViewModel vm = new SeguridadViewModel();
                //List<enMenu> loenMenu = new List<enMenu>();
                //List<enMenu> loenMenuTEM = new List<enMenu>();

                vm.ListaPaginaTemp = new List<Pagina>();
                vm.ListaPagina = new List<Pagina>();


                vm.ListaPagina = new  RolLG().ListarPaginaPorRol(idRol).ToList();
               

                if (vm.ListaPagina != null)
                {
                    foreach (Pagina item in vm.ListaPagina)
                    {
                        var objList = vm.ListaPaginaTemp.Where(m => m.IdModulo == item.IdModulo).ToList<Pagina>();
                        if (objList.Count > 0)
                        {
                            vm.ListaPaginaTemp.Add(item);
                            if (item.Seleccion == true)
                            {
                                var objList2 = vm.ListaPaginaTemp.Where(m => m.IdPagina == item.IdModulo && m.IdOrden == -1).ToList<Pagina>();
                                if (objList2.Count > 0)
                                {
                                    objList2[0].Seleccion = true;
                                }
                            }
                        }
                        else
                        {
                            vm.Pagina = new Pagina();
                            vm.Pagina.IdPagina = item.IdModulo;
                            vm.Pagina.IdOrden = -1;
                            vm.Pagina.Nombre = item.Modulo;
                            vm.Pagina.EstiloMenu = item.EstiloModulo;
                            vm.ListaPaginaTemp.Add(vm.Pagina);
                            vm.ListaPaginaTemp.Add(item);
                        }
                    }
                    objResult = new
                    {
                        iTipoResultado = 1,
                        loenMenu = vm.ListaPaginaTemp
                    };
                }
                else
                {
                    objResult = new
                    {
                        iTipoResultado = 2,
                        vError = "Ocurrio un problema al obtener el detalle del Rol"
                    };
                }
                return Json(objResult);
            }
            catch (Exception ex)
            {
                
                objResult = new { iTipoResultado = 2, vError = Constantes.msgErrorGeneral };
                return Json(objResult);
            }
        }


        [HttpPost]
        public JsonResult RegistrarRol(int IdRol, string Nombre, string Descripcion,bool SiSuperAdmi, string cadenaMenu)
        {
            var objResult = new object();
            int iResultado = -1;
            string mensaje = "Ocurrio un error al intentar realizar la acción.";
         //   string Nombre = utlAuditoria.ObtenerPrimeroNombre();
            try
            {
                string[] stringSeparators = new string[] { "|" };
                SeguridadViewModel vm = new SeguridadViewModel();
                vm.ListaRolPagina = new List<RolPagina>();
                RolPagina RolPagina;


                string[] Codigos = cadenaMenu.Split(stringSeparators, StringSplitOptions.None);
                foreach (string item in Codigos)
                {
                    if (!item.Equals(""))
                    {
                        RolPagina = new RolPagina();
                        RolPagina.IdPagina = int.Parse(item);
                        vm.ListaRolPagina.Add(RolPagina);
                    }
                }

                vm.Rol = new Rol()
                {
                    IdRol = IdRol,
                    Nombre = Nombre,
                    Descripcion = Descripcion,
                    SiSuperAdmi= SiSuperAdmi,
                    UsuarioCreacion = Auditoria.ObtenerNombreUsuario(),
                    UsuarioModificacion= Auditoria.ObtenerNombreUsuario()
                   
                };
               

             
                if (IdRol > 0)
                {
                    iResultado = new RolLG().ActualizarRol(vm.Rol, vm.ListaRolPagina.ToList());
                }
                else
                {
                    iResultado = new RolLG().InsertarRol(vm.Rol, vm.ListaRolPagina.ToList());
                }

                if (iResultado > 0)
                {
                    if (IdRol > 0)
                    {
                        mensaje = "Se modificó correctamente el Rol.";
                    }
                    else
                    {
                        mensaje = "Se registró correctamente el Rol.";
                    }
                }

                objResult = new
                {
                    iResultado = iResultado,
                    vMensaje = mensaje
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {
               
                objResult = new { iResultado = 2, vMensaje = Constantes.msgErrorGrabado };
                return Json(objResult);
            }
        }


        [HttpPost]
        public JsonResult ObtenerRol(int IdRol = 0)
        {
            var objResult = new object();
            try
            {

              
                Rol Rol = new Rol();

                Rol = new RolLG().ObtenerPorIdRol(IdRol);

                objResult = new
                {
                    iTipoResultado=1,
                    Rol = Rol
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
        public JsonResult EliminarRol(int IdRol = 0)
        {
            var objResult = new object();
            bool iResultado = false;
            string mensaje = "Ocurrio un error al intentar eliminar el Rol.";
            //string Nombre = utlAuditoria.ObtenerPrimeroNombre();
            try
            {
               

             
                iResultado = new RolLG().EliminarRol(IdRol, Auditoria.ObtenerNombreUsuario());

                if (iResultado)
                {
                    mensaje = "Se elimino correctamente el Rol.";
                }

                objResult = new
                {
                    iResultado = iResultado,
                    vMensaje = mensaje
                };

                return Json(objResult);
            }
            catch (Exception ex)
            {
               
                objResult = new { iResultado = 0, vMensaje = Constantes.msgErrorGrabado };
                return Json(objResult);
            }
        }
        #endregion
    }
}