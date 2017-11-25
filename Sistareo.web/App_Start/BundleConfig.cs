using System.Web;
using System.Web.Optimization;

namespace Sistareo.web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/jquery.confirm.js",
                        "~/Scripts/plugins/metisMenu/jquery.metisMenu.js",
                        "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js",
                        "~/Scripts/jquery.utilitario.js",
                          "~/Scripts/intranet.js",
                        "~/Scripts/datapicker/bootstrap-datepicker.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));



            //DateTimePicker
            bundles.Add(new ScriptBundle("~/Content/DateTimePicker").Include(
                        //"~/Content/datetimepicker/bootstrap-datetimepicker.css",
                        "~/Scripts/datetimepicker/bootstrap-datetimepicker.min.js",
                        "~/Scripts/datetimepicker/es.js"
                        ));

            ////Gestion Documentaria
            //bundles.Add(new StyleBundle("~/Content/cssGDocumentaria").Include(
            //          "~/Content//GDocumentaria.css"
            //          ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/cssMaster.css",
                      "~/Content/cssSistareo.css",
                     "~/Content/Seguridad.css",
                      "~/Content/datetimepicker/bootstrap-datetimepicker.css"
                      ));

            //General
            bundles.Add(new StyleBundle("~/Content/cssGeneral").Include(
                 
                      "~/Content/master.css"));
        }

    }
}
