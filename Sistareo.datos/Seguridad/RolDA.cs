using Sistareo.entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.datos.Seguridad
{
    public class RolDA
    {
        #region Rol
        public int InsertarRol(Rol oRol)
        {
            int Codigo = -1;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.Rol_Insertar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", oRol.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", oRol.Descripcion);
                        cmd.Parameters.AddWithValue("@SiSuperAdmi", oRol.SiSuperAdmi);               
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oRol.UsuarioCreacion);

                        Codigo =(Convert.ToInt16(cmd.ExecuteScalar()));
                       
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Codigo;
        }

        public int ActualizarRol(Rol oRol)
        {
            int Codigo = -1;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.[Rol_Actualizar_SP]", cn))
                    {
                        cn.Open();
                    
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRol", oRol.IdRol);
                        cmd.Parameters.AddWithValue("@Nombre", oRol.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", oRol.Descripcion);
                        cmd.Parameters.AddWithValue("@SiSuperAdmi", oRol.SiSuperAdmi);
                        cmd.Parameters.AddWithValue("@UsuarioModificacion", oRol.UsuarioModificacion);
                        Codigo = Convert.ToInt16( cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Codigo;
        }

        public bool EliminarRol(int IdRol, string UsuarioModificacion)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.Rol_Eliminar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRol", IdRol);
                        cmd.Parameters.AddWithValue("@UsuarioModificacion", UsuarioModificacion);

                        return Convert.ToBoolean(cmd.ExecuteNonQuery());
                      
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Rol ObtenerPorIdRol(int IdRol)
        {
            Rol oRol = new Rol();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Seguridad.Rol_ListarPorId_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRol", IdRol);


                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.Read())
                            {
                                oRol = new Rol();
                                oRol.IdRol = Convert.ToInt32(oReader["IdRol"]);
                                oRol.Nombre = Convert.ToString(oReader["Nombre"]);
                                oRol.Descripcion = Convert.ToString(oReader["Descripcion"]);
                                oRol.SiSuperAdmi = Convert.ToBoolean(oReader["SiSuperAdmi"]);
         
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            return oRol;
        }
        public List<Rol> ListarRolPorNombre(string Nombres)
        {
            Rol oRol;
            List<Rol> ListaRol = new List<Rol>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Seguridad.Rol_ListarPorNombre", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombres", Nombres);
              


                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRol = new Rol();
                                oRol.IdRol = Convert.ToInt32(oReader["IdRol"]);
                                oRol.Nombre = Convert.ToString(oReader["Nombre"]);
                                oRol.Descripcion = Convert.ToString(oReader["Descripcion"]);
                                oRol.TotalUsuario = Convert.ToString(oReader["TotalUsuario"]);
                              


                                ListaRol.Add(oRol);
                            }
                            oReader.Close();
                        }

                        return ListaRol;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }
        public int EliminarRol(int IdRol)
        {
            int Codigo = -1;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.RolPagina_Eliminar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRol", IdRol);

                        Codigo = Convert.ToInt16(cmd.ExecuteNonQuery());


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Codigo;
        }

        public List<Rol> ListarRol()
        {
            Rol oRol;
            List<Rol> ListaRol = new List<Rol>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Seguridad.Rol_ListarTodo_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRol = new Rol();
                                oRol.IdRol = Convert.ToInt32(oReader["IdRol"]);
                                oRol.Nombre = Convert.ToString(oReader["Nombre"]);
                                ListaRol.Add(oRol);
                            }
                            oReader.Close();
                        }

                        return ListaRol;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        #endregion

        #region "Rol Pagina"
        public List<Pagina> ListarPaginaPorRol(int IdRol)
        {
            Pagina oPagina;
            List<Pagina> ListaPagina = new List<Pagina>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Seguridad.RolPagina_ListarPorRol_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRol", IdRol);

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oPagina = new Pagina();
                                oPagina.IdPagina = Convert.ToInt32(oReader["IdPagina"]);
                                oPagina.IdModulo = Convert.ToInt32(oReader["IdModulo"]);
                                oPagina.IdTipoOpcionMenu = Convert.ToInt32(oReader["IdTipoOpcionMenu"]);
                                oPagina.Modulo = Convert.ToString(oReader["Modulo"]);
                                oPagina.EstiloModulo = Convert.ToString(oReader["EstiloModulo"]);
                                oPagina.EstiloMenu = Convert.ToString(oReader["EstiloMenu"]);
                                oPagina.Nombre = Convert.ToString(oReader["Nombre"]);
                                oPagina.URL = Convert.ToString(oReader["URL"]);
                                oPagina.IdPadre = Convert.ToInt32(oReader["IdPadre"]);
                                oPagina.Seleccion = Convert.ToBoolean(oReader["Seleccion"]);
                                oPagina.IdOrdenPadre = Convert.ToInt32(oReader["IdOrdenPadre"]);
                                oPagina.IdPadre1 = Convert.ToInt32(oReader["IdPadre1"]);
                                oPagina.IdOrden = Convert.ToInt32(oReader["IdOrden"]);

                                ListaPagina.Add(oPagina);
                            }
                            oReader.Close();
                        }

                        return ListaPagina;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }


        public int InsertarRolPagina(RolPagina oRolPagina)
        {
            int Codigo = -1;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.RolPagina_Insertar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPagina", oRolPagina.IdPagina);
                        cmd.Parameters.AddWithValue("@IdRol", oRolPagina.IdRol);
               
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oRolPagina.UsuarioCreacion);
                    
                         Codigo =  Convert.ToInt16((cmd.ExecuteNonQuery()));
                    

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Codigo;
        }



        #endregion

        #region PAgina


        public List<Pagina> ListarMenuPorUsuario(string NombreUsuario)
        {
            Pagina oPagina;
            List<Pagina> ListaPagina = new List<Pagina>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Seguridad.Pagina_ListarPorUsuario_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oPagina = new Pagina();
                                oPagina.IdPagina = Convert.ToInt32(oReader["IdPagina"]);
                                oPagina.IdModulo = Convert.ToInt32(oReader["IdModulo"]);
                                oPagina.IdTipoOpcionMenu = Convert.ToInt32(oReader["IdTipoOpcionMenu"]);
                                oPagina.Modulo = Convert.ToString(oReader["Modulo"]);
                                oPagina.EstiloModulo = Convert.ToString(oReader["EstiloModulo"]);
                                oPagina.EstiloMenu = Convert.ToString(oReader["EstiloMenu"]);
                                oPagina.Nombre = Convert.ToString(oReader["Nombre"]);
                                oPagina.URL = Convert.ToString(oReader["URL"]);
                                oPagina.IdPadre = Convert.ToInt32(oReader["IdPadre"]);
                                oPagina.Seleccion = Convert.ToBoolean(oReader["IdPadre1"]);
                                oPagina.IdOrden = Convert.ToInt32(oReader["IdOrden"]);

                                ListaPagina.Add(oPagina);
                            }
                            oReader.Close();
                        }

                        return ListaPagina;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        #endregion

    }
}
