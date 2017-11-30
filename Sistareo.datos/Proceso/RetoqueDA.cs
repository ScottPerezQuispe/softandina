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
    public class RetoqueDA
    {

        public bool InsertarRetoque(Retoque oRetoque)
        {
        
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand  ("Retoque_Insertar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdOperario", oRetoque.IdOperario);
                        cmd.Parameters.AddWithValue("@IdCampania", oRetoque.IdCampania);
                        cmd.Parameters.AddWithValue("@FechaApertura", oRetoque.FechaApertura);
                        cmd.Parameters.AddWithValue("@Jefatura", oRetoque.Jefatura);
                        cmd.Parameters.AddWithValue("@Coordinador", oRetoque.Coordinador);
                        cmd.Parameters.AddWithValue("@UsuarioCreacion", oRetoque.UsuarioCreacion);

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

        public bool ActualizarRetoque(Retoque oRetoque)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Retoque_Actualizar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoque", oRetoque.IdRetoque);
                        cmd.Parameters.AddWithValue("@IdOperario", oRetoque.IdOperario);
                        cmd.Parameters.AddWithValue("@IdCampania", oRetoque.IdCampania);
                        cmd.Parameters.AddWithValue("@Jefatura", oRetoque.Jefatura);
                        cmd.Parameters.AddWithValue("@Coordinador", oRetoque.Coordinador);
                        cmd.Parameters.AddWithValue("@UsuarioModificacion", oRetoque.UsuarioModificacion);
                        cmd.Parameters.AddWithValue("@FechaModificacion", oRetoque.FechaModificacion);
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

        public bool EliminarRetoque(int IdRetoque ,string UsuarioModificacion)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("Retoque_Eliminar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoque", IdRetoque);
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

        public Retoque ObtenerPorIdRetoque(int IdRetoque)
        {
            Retoque oRetoque = new Retoque();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Retoque_ObtenerPorIdRetoque_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRetoque", IdRetoque);


                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.Read())
                            {
                                oRetoque = new Retoque();
                                oRetoque.IdRetoque = Convert.ToInt32(oReader["IdRetoque"]);
                                oRetoque.IdOperario = Convert.ToInt32(oReader["IdOperario"]);
                                oRetoque.IdCampania = Convert.ToInt32(oReader["IdCampania"]);
                                oRetoque.vFechaApertura = Convert.ToString(oReader["vFechaApertura"]);
                                oRetoque.Jefatura = Convert.ToString(oReader["Jefatura"]);
                                oRetoque.Coordinador = Convert.ToString(oReader["Coordinador"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            return oRetoque;
        }
        public List<Retoque> ListarFechaPorOperario(DateTime FechaApertura , int IdOperario, int IdUsuario)
        {
            Retoque oRetoque;
            List<Retoque> ListaRetoque = new List<Retoque>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Retoque_ListarFechaPorOperario", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaApertura", FechaApertura);
                        cmd.Parameters.AddWithValue("@IdOperario", IdOperario);
                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRetoque = new Retoque();
                                oRetoque.IdRetoque = Convert.ToInt32(oReader["IdRetoque"]);
                                oRetoque.NombreCampania = Convert.ToString(oReader["NombreCampania"]);
                                oRetoque.NombreCompleto = Convert.ToString(oReader["NombreCompleto"]);
                                oRetoque.vFechaApertura = Convert.ToString(oReader["vFechaApertura"]);
                                oRetoque.Jefatura = Convert.ToString(oReader["Jefatura"]);
                                oRetoque.Coordinador = Convert.ToString(oReader["Coordinador"]);

                                ListaRetoque.Add(oRetoque);
                            }
                            oReader.Close();
                        }

                        return ListaRetoque;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }


        #region "Reporte"

        public List<Retoque> ListarRetoqueDiseño( int IdCampania, int IdOperario, int IdProducto, int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            Retoque oRetoque;
            List<Retoque> ListaRetoque = new List<Retoque>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Reportes.Retoque_Listar_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        cmd.Parameters.AddWithValue("@IdCampania", IdCampania);
                        cmd.Parameters.AddWithValue("@Idproducto", IdProducto);
                        cmd.Parameters.AddWithValue("@IdOperario", IdOperario);
                        cmd.Parameters.AddWithValue("@IdTipoUsuario", IdTipoUsuario);

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRetoque = new Retoque();
                                oRetoque.Compania = Convert.ToString(oReader["Compania"]);
                                oRetoque.vFechaApertura = Convert.ToString(oReader["vFechaApertura"]);
                                oRetoque.Jefatura = Convert.ToString(oReader["Jefatura"]);
                                oRetoque.Coordinador = Convert.ToString(oReader["Coordinador"]);
                                oRetoque.Producto = Convert.ToString(oReader["Producto"]);
                                oRetoque.Descipcion = Convert.ToString(oReader["Descipcion"]);
                                oRetoque.Operario = Convert.ToString(oReader["Operario"]);
                                oRetoque.HoraInicio = Convert.ToString(oReader["HoraInicio"]);
                                oRetoque.HoraFin = Convert.ToString(oReader["HoraFin"]);
                                oRetoque.TotalHoras = Convert.ToString(oReader["TotalHoras"]);
                                oRetoque.TotalHorasGeneral = Convert.ToString(oReader["TotalHorasGeneral"]);
                                oRetoque.TotalDetalle = Convert.ToString(oReader["TotalDetalle"]);
                                ListaRetoque.Add(oRetoque);
                            }
                            oReader.Close();
                        }

                        return ListaRetoque;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public List<Retoque> ListarRetoqueCampania(int IdCampania, int IdOperario, int IdProducto,int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            Retoque oRetoque;
            List<Retoque> ListaRetoque = new List<Retoque>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Reportes.Retoque_ListarCampania_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        cmd.Parameters.AddWithValue("@IdCampania", IdCampania);
                        cmd.Parameters.AddWithValue("@Idproducto", IdProducto);
                        cmd.Parameters.AddWithValue("@IdOperario", IdOperario);
                        cmd.Parameters.AddWithValue("@IdTipoUsuario", IdTipoUsuario);

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRetoque = new Retoque();
                                oRetoque.vFechaApertura = Convert.ToString(oReader["vFechaApertura"]);

                                oRetoque.Jefatura = Convert.ToString(oReader["Jefatura"]);
                                oRetoque.Coordinador = Convert.ToString(oReader["Coordinador"]);
                                oRetoque.Compania = Convert.ToString(oReader["Compania"]);
                    
                               
                                //oRetoque.Producto = Convert.ToString(oReader["Producto"]);
                                //oRetoque.Descipcion = Convert.ToString(oReader["Descipcion"]);
                                //oRetoque.Operario = Convert.ToString(oReader["Operario"]);
                                //oRetoque.HoraInicio = Convert.ToString(oReader["HoraInicio"]);
                                //oRetoque.HoraFin = Convert.ToString(oReader["HoraFin"]);
                                oRetoque.TotalHoras = Convert.ToString(oReader["TotalHoras"]);
                                //oRetoque.TotalHorasGeneral = Convert.ToString(oReader["TotalHorasGeneral"]);
                                ListaRetoque.Add(oRetoque);
                            }
                            oReader.Close();
                        }

                        return ListaRetoque;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public List<Retoque> ListarRetoqueOperador(int IdCampania, int IdOperario, int IdProducto, int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            Retoque oRetoque;
            List<Retoque> ListaRetoque = new List<Retoque>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Reportes.[Retoque_ListarOperario_SP]", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        cmd.Parameters.AddWithValue("@IdCampania", IdCampania);
                        cmd.Parameters.AddWithValue("@Idproducto", IdProducto);
                        cmd.Parameters.AddWithValue("@IdOperario", IdOperario);
                        cmd.Parameters.AddWithValue("@IdTipoUsuario", IdTipoUsuario);

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRetoque = new Retoque();
                                oRetoque.Compania = Convert.ToString(oReader["Compania"]);
                                oRetoque.vFechaApertura = Convert.ToString(oReader["vFechaApertura"]);
                                oRetoque.Jefatura = Convert.ToString(oReader["Jefatura"]);
                                oRetoque.Coordinador = Convert.ToString(oReader["Coordinador"]);
                                oRetoque.Operario = Convert.ToString(oReader["Operario"]);
                                oRetoque.Producto = Convert.ToString(oReader["Producto"]);
                                oRetoque.TotalHoras = Convert.ToString(oReader["TotalHoras"]);
                                //oRetoque.TotalHorasGeneral = Convert.ToString(oReader["TotalHorasGeneral"]);
                                ListaRetoque.Add(oRetoque);
                            }
                            oReader.Close();
                        }

                        return ListaRetoque;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }


        public List<Retoque> ListarRetoqueProducto(int IdCampania, int IdOperario, int IdProducto, int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            Retoque oRetoque;
            List<Retoque> ListaRetoque = new List<Retoque>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Reportes.[Retoque_ListarProducto_SP]", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        cmd.Parameters.AddWithValue("@IdCampania", IdCampania);
                        cmd.Parameters.AddWithValue("@Idproducto", IdProducto);
                        cmd.Parameters.AddWithValue("@IdOperario", IdOperario);
                        cmd.Parameters.AddWithValue("@IdTipoUsuario", IdTipoUsuario);

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRetoque = new Retoque();
                                //oRetoque.Compania = Convert.ToString(oReader["Compania"]);
                                //oRetoque.vFechaApertura = Convert.ToString(oReader["vFechaApertura"]);
                                //oRetoque.Jefatura = Convert.ToString(oReader["Jefatura"]);
                                //oRetoque.Coordinador = Convert.ToString(oReader["Coordinador"]);
                                oRetoque.Producto = Convert.ToString(oReader["Producto"]);
                                //oRetoque.Descipcion = Convert.ToString(oReader["Descipcion"]);
                                //oRetoque.Operario = Convert.ToString(oReader["Operario"]);
                                //oRetoque.HoraInicio = Convert.ToString(oReader["HoraInicio"]);
                                //oRetoque.HoraFin = Convert.ToString(oReader["HoraFin"]);
                                oRetoque.TotalHoras = Convert.ToString(oReader["TotalHoras"]);
                                //oRetoque.TotalHorasGeneral = Convert.ToString(oReader["TotalHorasGeneral"]);
                                ListaRetoque.Add(oRetoque);
                            }
                            oReader.Close();
                        }

                        return ListaRetoque;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public List<Retoque> ListarRetoqueProductoDetallado(int IdCampania, int IdOperario, int IdProducto, int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            Retoque oRetoque;
            List<Retoque> ListaRetoque = new List<Retoque>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Reportes.[Retoque_ListarProductoDetalle_SP]", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        cmd.Parameters.AddWithValue("@IdCampania", IdCampania);
                        cmd.Parameters.AddWithValue("@Idproducto", IdProducto);
                        cmd.Parameters.AddWithValue("@IdOperario", IdOperario);
                        cmd.Parameters.AddWithValue("@IdTipoUsuario", IdTipoUsuario);

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oRetoque = new Retoque();
                                oRetoque.Compania = Convert.ToString(oReader["Compania"]);
                                oRetoque.vFechaApertura = Convert.ToString(oReader["vFechaApertura"]);
                                oRetoque.Jefatura = Convert.ToString(oReader["Jefatura"]);
                                oRetoque.Coordinador = Convert.ToString(oReader["Coordinador"]);
                                oRetoque.Producto = Convert.ToString(oReader["Producto"]);
                                //oRetoque.Descipcion = Convert.ToString(oReader["Descipcion"]);
                                //oRetoque.Operario = Convert.ToString(oReader["Operario"]);
                                //oRetoque.HoraInicio = Convert.ToString(oReader["HoraInicio"]);
                                //oRetoque.HoraFin = Convert.ToString(oReader["HoraFin"]);
                                oRetoque.TotalHoras = Convert.ToString(oReader["TotalHoras"]);
                                //oRetoque.TotalHorasGeneral = Convert.ToString(oReader["TotalHorasGeneral"]);
                                ListaRetoque.Add(oRetoque);
                            }
                            oReader.Close();
                        }

                        return ListaRetoque;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }


        public List<Retoque> ListarRetoqueHeader(int IdTipoUsuario, DateTime FechaInicio, DateTime FechaFin)
        {
            Retoque oRetoque;
            List<Retoque> ListaRetoque = new List<Retoque>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Reportes.[Retoque_Header_SP]", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);                     
                        cmd.Parameters.AddWithValue("@IdTipoUsuario", IdTipoUsuario);

                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            if (oReader.Read())
                            {
                                oRetoque = new Retoque();
                           
                                oRetoque.FechaInicio = Convert.ToString(oReader["FechaInicio"]);
                                oRetoque.FechaFin = Convert.ToString(oReader["FechaFin"]);
                                oRetoque.TipoUsuario = Convert.ToString(oReader["TipoUsuario"]);
                               
                                ListaRetoque.Add(oRetoque);
                            }
                            oReader.Close();
                        }

                        return ListaRetoque;
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
