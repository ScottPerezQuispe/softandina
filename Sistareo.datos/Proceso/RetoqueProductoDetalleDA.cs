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
    public  class RetoqueProductoDetalleDA
    {
        public bool InsertarRetoqueProductoDetalle(RetoqueProductoDetalle oRetoqueProductoDetalle)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("RetoqueProductoDetalle_Insertar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoqueProducto", oRetoqueProductoDetalle.IdRetoqueProducto);
                        cmd.Parameters.AddWithValue("@DescripcionRetoqueProductoDetalle", oRetoqueProductoDetalle.DescripcionRetoqueProductoDetalle);
                        cmd.Parameters.AddWithValue("@FechaApertura", oRetoqueProductoDetalle.FechaApertura);
                        cmd.Parameters.AddWithValue("@HoraInicioRetoqueProductoDetalle", oRetoqueProductoDetalle.HoraInicioRetoqueProductoDetalle);
                        cmd.Parameters.AddWithValue("@HoraFinRetoqueProductoDetalle", oRetoqueProductoDetalle.HoraFinRetoqueProductoDetalla);
                        cmd.Parameters.AddWithValue("@TotalRetoqueProductoDetalle", oRetoqueProductoDetalle.TotalRetoqueProductoDetalle);
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oRetoqueProductoDetalle.UsuarioCreacion);

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

        public bool ActualizarRetoqueProductoDetalle(RetoqueProductoDetalle oRetoqueProductoDetalle)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("RetoqueProductoDetalle_Actualizar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoqueProductoDetalle", oRetoqueProductoDetalle.IdRetoqueProductoDetalle);
                        cmd.Parameters.AddWithValue("@DescripcionRetoqueProducto", oRetoqueProductoDetalle.DescripcionRetoqueProductoDetalle);
                        cmd.Parameters.AddWithValue("@HoraInicioRetoqueProductoDetalle", oRetoqueProductoDetalle.HoraInicioRetoqueProductoDetalle);
                        cmd.Parameters.AddWithValue("@HoraFinRetoqueProductoDetalle", oRetoqueProductoDetalle.HoraFinRetoqueProductoDetalla);
                        cmd.Parameters.AddWithValue("@TotalRetoqueProductoDetalle", oRetoqueProductoDetalle.TotalRetoqueProductoDetalle);
                        cmd.Parameters.AddWithValue("@UsuarioModificacion", oRetoqueProductoDetalle.UsuarioModificacion);

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

        public bool EliminarRetoqueProductoDetalle(int IdRetoqueProductoDetalle, string UsuarioModificacion)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("RetoqueProductoDetalle_Eliminar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoqueProductoDetalle", IdRetoqueProductoDetalle);
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

        public RetoqueProductoDetalle ObtenerPorIdRetoqueProductoDetalle(int IdRetoqueProductoDetalle)
        {
            RetoqueProductoDetalle oRetoqueProducto = new RetoqueProductoDetalle();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("RetoqueProductoDetalle_ObtenerPorId_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoqueProductoDetalle", IdRetoqueProductoDetalle);


                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.Read())
                            {
                                oRetoqueProducto = new RetoqueProductoDetalle();
                                oRetoqueProducto.IdRetoqueProducto = Convert.ToInt32(oReader["IdRetoqueProducto"]);
                                oRetoqueProducto.IdRetoqueProductoDetalle = Convert.ToInt32(oReader["IdRetoqueProductoDetalle"]);
                             
                                oRetoqueProducto.DescripcionRetoqueProductoDetalle = Convert.ToString(oReader["DescripcionRetoqueProducto"]);
                                oRetoqueProducto.HoraInicioRetoqueProductoDetalle = Convert.ToString(oReader["HoraInicioRetoqueProducto"]);
                                oRetoqueProducto.HoraFinRetoqueProductoDetalla = Convert.ToString(oReader["HoraFinRetoqueProducto"]);
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

        public List<RetoqueProductoDetalle> ListarPorIdRetoqueDetalle(int IdRetoqueProducto)
        {
            RetoqueProductoDetalle oRetoqueProducto;
            List<RetoqueProductoDetalle> ListaRetoqueProducto = new List<RetoqueProductoDetalle>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("RetoqueProductoDetalle_ListarPorId", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoqueProducto", IdRetoqueProducto);
            

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRetoqueProducto = new RetoqueProductoDetalle();
                                oRetoqueProducto.IdRetoqueProductoDetalle = Convert.ToInt32(oReader["IdRetoqueProductoDetalle"]);
   
                                oRetoqueProducto.DescripcionRetoqueProductoDetalle = Convert.ToString(oReader["DescripcionRetoqueProducto"]);
                                oRetoqueProducto.HoraInicioRetoqueProductoDetalle = Convert.ToString(oReader["HoraInicioRetoqueProducto"]);
                                oRetoqueProducto.HoraFinRetoqueProductoDetalla = Convert.ToString(oReader["HoraFinRetoqueProducto"]);
                                oRetoqueProducto.TotalRetoqueProductoDetalle = Convert.ToString(oReader["TotalRetoqueProducto"]);
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
    }
}
