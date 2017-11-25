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
    public class OperadorDA
    {
        public List<Operador> ListarTodoOperador(int IdUsuario)
        {
            Operador oOperador;
            List<Operador> ListaOperario = new List<Operador>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                {

                    using (SqlCommand cmd = new SqlCommand("Operario_ListarTodo_SP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        
                        using (SqlDataReader oReader = cmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                oOperador = new Operador();
                                oOperador.IdOperario =  Convert.ToInt32(oReader["IdOperario"]);
                                oOperador.NombreCompleto = Convert.ToString(oReader["NombreCompleto"]);

                                ListaOperario.Add(oOperador);
                            }
                            oReader.Close();
                        }

                        return ListaOperario;
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
