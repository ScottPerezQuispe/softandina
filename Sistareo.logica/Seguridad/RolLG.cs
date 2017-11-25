using Sistareo.datos.Seguridad;
using Sistareo.entidades.Seguridad;
using System.Transactions;
using System.Collections.Generic;
using System;

namespace Sistareo.logica.Seguridad
{
    public class RolLG
    {
        public int InsertarRol(Rol oRol , List<RolPagina> ListaRolPagina)
        {

            int resultado = -1;
            int IdRol = 0;
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {


                    IdRol =  new RolDA().InsertarRol(oRol);
                    if (IdRol != -1 && IdRol != 0)
                        {
                            foreach (RolPagina item in ListaRolPagina)
                            {
                                item.IdRol = IdRol;
                                item.UsuarioCreacion = oRol.UsuarioCreacion;
               
                                resultado = new RolDA().InsertarRolPagina(item);
                                if (resultado == 0)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    
                    trans.Complete();
                    return (resultado);
                }
         
            }
            catch (Exception ex)
            {

                throw ex;
            }

          
        }
        public int ActualizarRol(Rol oRol, List<RolPagina> ListaRolPagina)
        {
            int resultado = -1;
         
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {

                    resultado = new RolDA().EliminarRol(oRol.IdRol);
                    resultado = new RolDA().ActualizarRol(oRol);
                    if (resultado != -1 && resultado != 0)
                    {
                        foreach (RolPagina item in ListaRolPagina)
                        {
                            item.IdRol = oRol.IdRol;
                            item.UsuarioCreacion = oRol.UsuarioCreacion;

                            resultado = new RolDA().InsertarRolPagina(item);
                            if (resultado == 0)
                            {
                                throw new Exception();
                            }
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }

                    trans.Complete();
                    return (resultado);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool EliminarRol(int IdUsuario, string UsuarioModificacion)
        {
            return new RolDA().EliminarRol(IdUsuario, UsuarioModificacion);
        }
        public Rol ObtenerPorIdRol(int IdRol)
        {
            return new RolDA().ObtenerPorIdRol(IdRol);
        }
        public List<Rol> ListarRolPorNombre(string Nombres)
        {
            return new RolDA().ListarRolPorNombre(Nombres);
        }

        public List<Rol> ListarRol()
        {
            return new RolDA().ListarRol();
        }
        public List<Pagina> ListarPaginaPorRol(int IdRol)
        {
            return new RolDA().ListarPaginaPorRol(IdRol);
        }



        public List<Pagina> ListarMenuPorUsuario(string NombreUsuario)
        {
            return new RolDA().ListarMenuPorUsuario(NombreUsuario);
        }
    }
}
