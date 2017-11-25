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
    public class UsuarioDA
    {
        public int InsertarUsuario(Usuario oUsuario)
        {
            int Codigo = -1;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.Usuario_Insertar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NombreUsuario", oUsuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@Clave", oUsuario.Clave);
                        cmd.Parameters.AddWithValue("@Nombres", oUsuario.Nombres);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", oUsuario.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", oUsuario.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@DNI", oUsuario.DNI);
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oUsuario.UsuarioCreacion);

                        Codigo = (Convert.ToInt16(cmd.ExecuteScalar()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Codigo;
        }

        public bool ActualizarUsuario(Usuario oUsuario)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.Usuario_Actualizar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", oUsuario.IdUsuario);
                        cmd.Parameters.AddWithValue("@NombreUsuario", oUsuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@Clave", oUsuario.Clave);
                        cmd.Parameters.AddWithValue("@Nombres", oUsuario.Nombres);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", oUsuario.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", oUsuario.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@DNI", oUsuario.DNI);
                        cmd.Parameters.AddWithValue("@UsuarioModificacion", oUsuario.UsuarioModificacion);

                        if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarUsuario(int IdUsuario, string UsuarioModificacion)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.Usuario_Eliminar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        cmd.Parameters.AddWithValue("@UsuarioModificacion", UsuarioModificacion);

                        if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario ObtenerPorIdUsuario(int IdUsuario)
        {
            Usuario oUsuario = new Usuario();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Seguridad.Usuario_ObtenerId_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);


                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.Read())
                            {
                                oUsuario = new Usuario();
                                oUsuario.IdUsuario = Convert.ToInt32(oReader["IdUsuario"]);
                                oUsuario.NombreUsuario = Convert.ToString(oReader["NombreUsuario"]);
                                oUsuario.Clave = Convert.ToString(oReader["Clave"]);
                                oUsuario.Nombres = Convert.ToString(oReader["Nombres"]);
                                oUsuario.ApellidoPaterno = Convert.ToString(oReader["ApellidoPaterno"]);
                                oUsuario.ApellidoMaterno = Convert.ToString(oReader["ApellidoMaterno"]);
                                oUsuario.Apellidos = Convert.ToString(oReader["Apellidos"]);
                                oUsuario.DNI = Convert.ToString(oReader["DNI"]);
                                oUsuario.IdRol = Convert.ToInt32(oReader["IdRol"]);
                                oUsuario.NombreRol = Convert.ToString(oReader["NombreRol"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            return oUsuario;
        }

        public List<Usuario> ListarUsuarioPorNombre(string Nombres,string NombreUsuario,int IdUsuario)
        {
            Usuario oUsuario;
            List<Usuario> ListaUsuario = new List<Usuario>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Seguridad.Usuario_ListarPorNombre_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombres", Nombres);
                        cmd.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);
                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oUsuario = new Usuario();
                                oUsuario.IdUsuario = Convert.ToInt32(oReader["IdUsuario"]);
                                oUsuario.NombreUsuario = Convert.ToString(oReader["NombreUsuario"]);
                                oUsuario.NombreCompleto = Convert.ToString(oReader["NombreCompleto"]);
                                oUsuario.DNI = Convert.ToString(oReader["DNI"]);
                                oUsuario.Activo = Convert.ToBoolean(oReader["Activo"]);
                                oUsuario.Estilo = Convert.ToString(oReader["Estilo"]);
                                

                                ListaUsuario.Add(oUsuario);
                            }
                            oReader.Close();
                        }

                        return ListaUsuario;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }


        public Usuario ObtenerUsuario(string NombreUsuario,string Clave)
        {
            Usuario oUsuario = new Usuario();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Seguridad.[Usuario_ObtenerUsuario_SP]", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);
                        cmd.Parameters.AddWithValue("@Clave", Clave);
                        

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.Read())
                            {
                                oUsuario = new Usuario();
                                oUsuario.IdUsuario = Convert.ToInt32(oReader["IdUsuario"]);
                                oUsuario.NombreUsuario = Convert.ToString(oReader["NombreUsuario"]);
                                
                                oUsuario.Nombres = Convert.ToString(oReader["Nombres"]);
                                oUsuario.Apellidos = Convert.ToString(oReader["Apellidos"]);
                                oUsuario.DNI = Convert.ToString(oReader["DNI"]);
                                oUsuario.IdRol = Convert.ToInt32(oReader["IdRol"]);
                                oUsuario.NombreRol = Convert.ToString(oReader["NombreRol"]);
                                oUsuario.SiSuperAdmi = Convert.ToBoolean(oReader["SiSuperAdmi"]);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            return oUsuario;
        }

        #region Usuario Rol

        public int InsertarUsuarioRol(UsuarioRol oUsuarioRol)
        {
            int Codigo = -1;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.UsuarioRol_Insert_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", oUsuarioRol.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdRol", oUsuarioRol.IdRol);
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oUsuarioRol.UsuarioCreacion);

                        Codigo = (Convert.ToInt16(cmd.ExecuteScalar()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Codigo;
        }

        public int ActualizarUsuarioRol(UsuarioRol oUsuarioRol)
        {
            int Codigo = -1;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Seguridad.UsuarioRol_Actualizar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", oUsuarioRol.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdRol", oUsuarioRol.IdRol);
                        cmd.Parameters.AddWithValue("@UsuarioModificacion", oUsuarioRol.UsuarioModificacion);

                        Codigo = (Convert.ToInt16(cmd.ExecuteScalar()));
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

    }
}
