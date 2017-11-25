using Sistareo.entidades.Proceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.datos.Proceso
{
    public class RetoqueProductoDA
    {
        public bool InsertarRetoqueProducto(RetoqueProducto oRetoqueProducto)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("RetoqueProducto_Insertar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoque", oRetoqueProducto.IdRetoque);
                        cmd.Parameters.AddWithValue("@IdProducto", oRetoqueProducto.IdProducto);
                        cmd.Parameters.AddWithValue("@DescripcionRetoqueProducto", oRetoqueProducto.DescripcionRetoqueProducto);
                        cmd.Parameters.AddWithValue("@FechaApertura", oRetoqueProducto.FechaApertura);
                        cmd.Parameters.AddWithValue("@HoraInicioRetoqueProducto", oRetoqueProducto.HoraInicioRetoqueProducto);
                        cmd.Parameters.AddWithValue("@HoraFinRetoqueProducto", oRetoqueProducto.HoraFinRetoqueProducto);
                        cmd.Parameters.AddWithValue("@TotalRetoqueProducto", oRetoqueProducto.TotalRetoqueProducto);
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oRetoqueProducto.UsuarioCreacion);

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

        public bool ActualizarRetoqueProducto(RetoqueProducto oRetoqueProducto)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("RetoqueProducto_Actualizar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoqueProducto", oRetoqueProducto.IdRetoqueProducto);
                        //cmd.Parameters.AddWithValue("@IdRetoque", oRetoqueProducto.IdRetoque);
                        cmd.Parameters.AddWithValue("@IdProducto", oRetoqueProducto.IdProducto);
                        cmd.Parameters.AddWithValue("@DescripcionRetoqueProducto", oRetoqueProducto.DescripcionRetoqueProducto);
                        cmd.Parameters.AddWithValue("@HoraInicioRetoqueProducto", oRetoqueProducto.HoraInicioRetoqueProducto);
                        cmd.Parameters.AddWithValue("@HoraFinRetoqueProducto", oRetoqueProducto.HoraFinRetoqueProducto);
                        cmd.Parameters.AddWithValue("@TotalRetoqueProducto", oRetoqueProducto.TotalRetoqueProducto);
                        cmd.Parameters.AddWithValue("@UsuarioModificacion", oRetoqueProducto.UsuarioModificacion);

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

        public bool EliminarRetoqueProducto(int IdRetoqueProducto, string UsuarioModificacion)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("RetoqueProducto_Eliminar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoqueProducto", IdRetoqueProducto);
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

        public RetoqueProducto ObtenerPorIdRetoqueProducto(int IdRetoqueProducto)
        {
            RetoqueProducto oRetoqueProducto = new RetoqueProducto();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("RetoqueProducto_ObtenerPorIdRetoqueProducto_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoqueProducto", IdRetoqueProducto);


                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.Read())
                            {
                                oRetoqueProducto = new RetoqueProducto();
                                oRetoqueProducto.IdRetoqueProducto = Convert.ToInt32(oReader["IdRetoqueProducto"]);
                                oRetoqueProducto.IdRetoque = Convert.ToInt32(oReader["IdRetoque"]);
                                oRetoqueProducto.IdProducto = Convert.ToInt32(oReader["IdProducto"]);
                                oRetoqueProducto.CodigoProducto = Convert.ToString(oReader["CodigoProducto"]);
                                oRetoqueProducto.DescripcionProducto = Convert.ToString(oReader["DescripcionProducto"]);
                                oRetoqueProducto.DescripcionRetoqueProducto = Convert.ToString(oReader["DescripcionRetoqueProducto"]);
                                oRetoqueProducto.HoraInicioRetoqueProducto = Convert.ToString(oReader["HoraInicioRetoqueProducto"]);
                                oRetoqueProducto.HoraFinRetoqueProducto = Convert.ToString(oReader["HoraFinRetoqueProducto"]);
                                oRetoqueProducto.vFechaApertura = Convert.ToString(oReader["FechaApertura"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            return oRetoqueProducto;
        }

        public List<RetoqueProducto> ListarPorIdRetoque(int IdRetoque)
        {
            RetoqueProducto oRetoqueProducto;
            List<RetoqueProducto> ListaRetoqueProducto = new List<RetoqueProducto>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("RetoqueProducto_ListarPorIdRetoque", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoque", IdRetoque);
                       

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRetoqueProducto = new RetoqueProducto();
                                oRetoqueProducto.IdRetoqueProducto = Convert.ToInt32(oReader["IdRetoqueProducto"]);
                                oRetoqueProducto.IdRetoque = Convert.ToInt32(oReader["IdRetoque"]);
                                oRetoqueProducto.CodigoBarra = Convert.ToString(oReader["CodigoBarra"]);
                                oRetoqueProducto.DescripcionProducto = Convert.ToString(oReader["DescripcionProducto"]);
                                oRetoqueProducto.DescripcionRetoqueProducto = Convert.ToString(oReader["DescripcionRetoqueProducto"]);
                                oRetoqueProducto.HoraInicioRetoqueProducto = Convert.ToString(oReader["HoraInicioRetoqueProducto"]);
                                oRetoqueProducto.HoraFinRetoqueProducto = Convert.ToString(oReader["HoraFinRetoqueProducto"]);
                                oRetoqueProducto.TotalRetoqueProducto = Convert.ToString(oReader["TotalRetoqueProducto"]);
                                oRetoqueProducto.TotalHoras = (TimeSpan)(oReader["TotalHoras"]);
                                

                                ListaRetoqueProducto.Add(oRetoqueProducto);
                            }
                            oReader.Close();
                        }

                        return ListaRetoqueProducto;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public int ValidarHorasRetoqueProducto(RetoqueProducto oRetoqueProducto)
        {
            
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("RetoqueProducto_ValidarHoras_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoque", oRetoqueProducto.IdRetoque);
                        cmd.Parameters.AddWithValue("@HoraInicioRetoqueProducto", oRetoqueProducto.HoraInicioRetoqueProducto);
                        cmd.Parameters.AddWithValue("@HoraFinRetoqueProducto", oRetoqueProducto.HoraFinRetoqueProducto);


                        return Convert.ToInt32(cmd.ExecuteScalar());
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }


        public bool ActualizarRetoqueProductoTotal(RetoqueProducto oRetoqueProducto)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("RetoqueProductoTotal_Actualizar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoqueProducto", oRetoqueProducto.IdRetoqueProducto);
                        cmd.Parameters.AddWithValue("@TotalRetoqueProducto", oRetoqueProducto.TotalDetalleRetoqueProducto);
                        cmd.Parameters.AddWithValue("@UsuarioModificacion", oRetoqueProducto.UsuarioModificacion);

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
    }
}
