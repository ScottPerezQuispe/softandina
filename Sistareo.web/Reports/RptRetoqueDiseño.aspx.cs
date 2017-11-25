using Microsoft.Reporting.WebForms;
using Sistareo.entidades.Proceso;
using Sistareo.logica.Proceso;
using Sistareo.web.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistareo.web.Reports
{
    public partial class RptRetoqueDiseño : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var a =Sistareo.web.Helper.Auditoria.ObtenerIdUsuario();
            List<Retoque> Listar = new List<Retoque>();
            Retoque retoque = new Retoque();
            if (!IsPostBack)
            {
              
                try
                {
                    retoque = Auditoria.ObtenerRetoque();
                    Listar = retoque.ListaRetoque;
                    string nombre = GetNameReporte(retoque.IdOpcion);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\Reports\\Reporte\\" + nombre);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DsDiseñoRetoque", Listar);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
               
            }
        }

        public string GetNameReporte(int IdOpcion)
        {
            string Nombre;
            switch (IdOpcion)
            {
                case 0:
                    Nombre = "RptDiseñoRetoqueCampania.rdlc";
                    break;
                case 1:
                    Nombre = "RptDiseñoRetoqueOperario.rdlc";
                    break;

                case 2:
                    Nombre = "RptDiseñoRetoqueProducto.rdlc";
                    break;
                default:
                    Nombre = "RptDiseñoRetoque.rdlc";
                    break;

            }
            return Nombre;
        }
        //public List<Retoque> ListarRetoque()
        //{
        //    List<Retoque> Listar = new List<Retoque>();
        //    try
        //    {
        //      return  Listar = new  RetoqueLG().ListarRetoqueDiseño();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Listar;
        //        throw ex;
        //    }
            
        //}
    }
}
