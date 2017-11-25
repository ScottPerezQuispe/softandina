using Sistareo.entidades.Proceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Sistareo.datos
{
    public class Conexion
    {
        public static string conexion
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["cnSITAREO"].ToString();
            }
        }
    }
}
