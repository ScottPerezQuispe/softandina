using Sistareo.datos.Seguridad;
using Sistareo.entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Sistareo.logica.Seguridad
{
    public class UsuarioLG
    {
        public int InsertarUsuario(Usuario oUsuario)
        {
            int Resultado = -1;
            int IdUsuario = 0;
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    IdUsuario =  new UsuarioDA().InsertarUsuario(oUsuario);
                    if (IdUsuario != -1 && IdUsuario != 0)
                        {
                            UsuarioRol oUsuarioRol = new UsuarioRol();

                             oUsuarioRol.IdUsuario = IdUsuario;
                             oUsuarioRol.IdRol = oUsuario.IdRol;
                             oUsuarioRol.UsuarioCreacion= oUsuario.UsuarioCreacion;

                            Resultado =  new  UsuarioDA().InsertarUsuarioRol(oUsuarioRol);
                            if (Resultado == 0)
                            {
                                throw new Exception();
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                
                    trans.Complete();
                    return (Resultado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }
        public int ActualizarUsuario(Usuario oUsuario)
        {

            int Resultado = -1;
            bool IdUsuario =false;
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    IdUsuario = new  UsuarioDA().ActualizarUsuario(oUsuario);
                    if (IdUsuario)
                    {
                        UsuarioRol oUsuarioRol = new UsuarioRol();

                        oUsuarioRol.IdUsuario = oUsuario.IdUsuario;
                        oUsuarioRol.IdRol = oUsuario.IdRol;
                        oUsuarioRol.UsuarioCreacion = oUsuario.UsuarioCreacion;

                        Resultado = new UsuarioDA().ActualizarUsuarioRol(oUsuarioRol);
                        if (Resultado == 0)
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }

                    trans.Complete();
                    
                }
                return (Resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool EliminarUsuario(int IdUsuario, string UsuarioModificacion)
        {
            return new UsuarioDA().EliminarUsuario(IdUsuario, UsuarioModificacion);
        }
        public Usuario ObtenerPorIdUsuario(int IdUsuario)
        {
            return new UsuarioDA().ObtenerPorIdUsuario(IdUsuario);
        }
        public List<Usuario> ListarUsuarioPorNombre(string Nombres, string NombreUsuario, int IdUsuario)
        {
            return new UsuarioDA().ListarUsuarioPorNombre(Nombres, NombreUsuario,IdUsuario);
        }

        public Usuario ObtenerUsuario(string NombreUsuario, string Clave)
        {
            return new UsuarioDA().ObtenerUsuario(NombreUsuario, Clave);
        }

        public List<Usuario> ListarTodoJefatura()
        {
            return new UsuarioDA().ListarTodoJefatura();
        }
        public List<Usuario> ListarTodoCoordinador()
        {
            return new UsuarioDA().ListarTodoCoordinador();
        }
    }
}
