using Sistareo.entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.datos.Configuracion
{
    public class ProductoDA
    {
        public bool InsertarProducto(Producto oProducto)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Maestro.Producto_Insertar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodigoProducto", oProducto.CodigoProducto);
                        cmd.Parameters.AddWithValue("@CodigoBarra", oProducto.CodigoBarra);
                        cmd.Parameters.AddWithValue("@DescripcionProducto", oProducto.DescripcionProducto);
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oProducto.UsuarioCreacion);

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

        public bool ActualizarProducto(Producto oProducto)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Maestro.Producto_Actualizar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdProducto", oProducto.IdProducto);
                        cmd.Parameters.AddWithValue("@CodigoProducto", oProducto.CodigoProducto);
                        cmd.Parameters.AddWithValue("@CodigoBarra", oProducto.CodigoBarra);
                        cmd.Parameters.AddWithValue("@DescripcionProducto", oProducto.DescripcionProducto);
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oProducto.UsuarioCreacion);
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

        public bool EliminarProducto(int IdProducto, string UsuarioModificacion)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Maestro.[Producto_Eliminar_SP]", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdProducto", IdProducto);
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

        public Producto ObtenerPorIdProducto(int IdProducto)
        {
            Producto oProducto = new Producto();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Maestro.Producto_ObtenerId_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdProducto", IdProducto);


                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.Read())
                            {
                                oProducto = new Producto();
                                oProducto.IdProducto = Convert.ToInt32(oReader["IdProducto"]);
                                oProducto.CodigoBarra = Convert.ToString(oReader["CodigoBarra"]);
                                oProducto.CodigoProducto = Convert.ToString(oReader["CodigoProducto"]);
                                oProducto.DescripcionProducto = Convert.ToString(oReader["DescripcionProducto"]);
          

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            return oProducto;
        }


        public List<Producto> ListarProductoPorNombre(string Nombre)
        {
            Producto oProducto;
            List<Producto> ListaProducto = new List<Producto>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Maestro.Producto_ListarPorNombre_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", Nombre);
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oProducto = new Producto();
                                oProducto.IdProducto = Convert.ToInt32(oReader["IdProducto"]);
                                oProducto.CodigoBarra = Convert.ToString(oReader["CodigoBarra"]);
                                oProducto.CodigoProducto = Convert.ToString(oReader["CodigoProducto"]);
                                oProducto.DescripcionProducto = Convert.ToString(oReader["DescripcionProducto"]);
                                ListaProducto.Add(oProducto);
                            }
                            oReader.Close();
                        }

                        return ListaProducto;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }



        public List<Producto> ListarTodoProducto()
        {
            Producto oProducto;
            List<Producto> ListaProducto = new List<Producto>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Producto_ListarTodo_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oProducto = new Producto();
                                oProducto.IdProducto = Convert.ToInt32(oReader["IdProducto"]);
                                oProducto.DescripcionProducto = Convert.ToString(oReader["DescripcionProducto"]);


                                ListaProducto.Add(oProducto);
                            }
                            oReader.Close();
                        }

                        return ListaProducto;
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
