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
    public class TipoDA
    {
        public List<Tipo> ListarTipoUsuario(int IdSupertipo)
        {
            Tipo oTipo;
            List<Tipo> ListaTipoUsuario = new List<Tipo>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {
                   using (SqlCommand cmd = new SqlCommand("[Maestro].[TipoUsuario_ListarTodo_SP]", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdSupertipo", IdSupertipo);
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oTipo = new Tipo();
                                oTipo.IdTipo = Convert.ToInt32(oReader["IdTipo"]);
                                oTipo.Nombre= Convert.ToString(oReader["Nombre"]);
                                ListaTipoUsuario.Add(oTipo);
                            }
                            oReader.Close();
                        }

                        return ListaTipoUsuario;
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
