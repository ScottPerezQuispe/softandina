using Sistareo.entidades.Configuracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistareo.datos.Configuracion
{
    public class CampaniaDA
    {
        public bool InsertarCampania(Campania oCampania)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Maestro.Campania_Insertar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", oCampania.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", oCampania.Descripcion);
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oCampania.UsuarioCreacion);

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

        public bool ActualizarCampania(Campania oCampania)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Maestro.Campania_Actualizar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCampania", oCampania.IdCampania);
                        cmd.Parameters.AddWithValue("@Nombre", oCampania.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", oCampania.Descripcion);
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oCampania.UsuarioCreacion);
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

        public bool EliminarCampania(int IdCampania, string UsuarioModificacion)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Maestro.Campania_Eliminar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCampania", IdCampania);
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

        public Campania ObtenerPorIdCampania(int IdCampania)
        {
            Campania oCampania = new Campania();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Maestro.Campania_ObtenerId_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCampania", IdCampania);


                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.Read())
                            {
                                oCampania = new Campania();
                                oCampania.IdCampania = Convert.ToInt32(oReader["IdCampania"]);
                                oCampania.Nombre = Convert.ToString(oReader["Nombre"]);
                                oCampania.Descripcion = Convert.ToString(oReader["Descripcion"]);
                             
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            return oCampania;
        }


        public List<Campania> ListarCampaniaPorNombre( string Nombre)
        {
            Campania oCampania;
            List<Campania> ListaCampania = new List<Campania>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Maestro.Campania_ListarPorNombre_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", Nombre);
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oCampania = new Campania();
                                oCampania.IdCampania = Convert.ToInt32(oReader["IdCampania"]);
                                oCampania.Nombre = Convert.ToString(oReader["Nombre"]);
                                oCampania.Descripcion = Convert.ToString(oReader["Descripcion"]);
                                ListaCampania.Add(oCampania);
                            }
                            oReader.Close();
                        }

                        return ListaCampania;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public List<Campania> ListarTodoCampania()
        {
            Campania oCampania;
            List<Campania> ListaCampania = new List<Campania>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Maestro.Campania_ListarTodo_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oCampania = new Campania();
                                oCampania.IdCampania = Convert.ToInt32(oReader["IdCampania"]);
                                oCampania.Nombre= Convert.ToString(oReader["Nombre"]);

                                ListaCampania.Add(oCampania);
                            }
                            oReader.Close();
                        }

                        return ListaCampania;
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
