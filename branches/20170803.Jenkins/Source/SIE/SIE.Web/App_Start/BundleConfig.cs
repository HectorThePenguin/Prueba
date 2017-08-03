
using System.Web.Optimization;

namespace SIE.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254726
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts y Estilos comunes
            bundles.Add(new ScriptBundle("~/bundles/jscomunScript").Include(
                  "~/Scripts/jscomun.js"));

            //bundles.Add(new StyleBundle("~/bundles/TarjetaRecepcionGanadoEstilo").Include(
            //    "~/assets/css/TarjetaRecepcionGanado.css"));
            #endregion Scripts y Estilos comunes



            bundles.Add(new StyleBundle("~/bundles/Estilos_Comunes").Include(
                "~/assets/plugins/bootstrap/css/bootstrap.css",
                "~/assets/plugins/bootstrap/css/bootstrap-responsive.min.css",
                "~/assets/plugins/font-awesome/css/font-awesome.css",
                "~/assets/css/style-metro.css",
                "~/assets/css/style.css",
                "~/assets/css/style-responsive.css",
                "~/assets/plugins/bootstrap/css/bootstrap-modal.css",
                "~/assets/css/media-queries.css",
                "~/assets/css/Comun.css",
                "~/assets/plugins/select2/select2.css",
                "~/assets/plugins/select2/select2-metronic.css",
                "~/assets/plugins/data-tables/DT_bootstrap.css",
                "~/assets/plugins/jquery-timepicker/jquery.timepicker.css"));

            
            bundles.Add(new ScriptBundle("~/bundles/Scripts_Comunes").Include(
                   "~/assets/plugins/jquery-1.7.1.min.js",
                                        "~/Scripts/json2.js",
                                        "~/assets/plugins/jquery-ui-1.10.1.custom.min.js",
                                        "~/assets/plugins/bootstrap-modal/js/bootstrap-modal.js",
                                        "~/assets/plugins/bootstrap/js/bootstrap.min.js",
                                        "~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js",
                                        "~/assets/scripts/jquery-jtemplates.js",
                                        "~/assets/plugins/bootstrap-modal/js/bootstrap-modalmanager.js",
                                        "~/assets/scripts/ui-modals.js",
                                        "~/assets/plugins/jquery.blockui.min.js",
                                        "~/assets/scripts/app.js",
                                        "~/assets/plugins/spin.js",
                                        "~/assets/plugins/jquery.spin.js",
                                        "~/assets/plugins/numericInput/jquery-numericInput.min.js",
                                        "~/assets/plugins/jquery-linq/linq.js",
                                        "~/assets/plugins/jquery-alphanumeric/jquery-alphanumeric.js",
                                        "~/assets/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js",
                                        "~/assets/plugins/bootstrap-typeahead/typeahead.bundle.min.js",
                                        "~/assets/plugins/jquery-timepicker/jquery.timepicker.min.js",
                                        "~/assets/plugins/select2/select2.min.js",
                                        "~/assets/plugins/data-tables/jquery.dataTables.min.js",
                                        "~/assets/plugins/data-tables/DT_bootstrap.js",
                                        "~/assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                                        "~/assets/plugins/accounting/accounting.min.js"
                                        ));

            #region Archivos Pantalla Recepción/CalificacionGanado
            bundles.Add(new ScriptBundle("~/bundles/CalificacionGanadoScript").Include(
                  "~/Scripts/CalificacionCalidad.js"));

            bundles.Add(new StyleBundle("~/bundles/CalificacionGanadoEstilo").Include(
                "~/assets/css/CalificacionCalidad.css"));
            #endregion Archivos Pantalla Recepción/CalificacionGanado

            #region Archivos Pantalla Recepción/TarjetaRecepcionGanado
            bundles.Add(new ScriptBundle("~/bundles/TarjetaRecepcionGanadoScript").Include(
                  "~/Scripts/TarjetaRecepcionGanado.js"));

            bundles.Add(new StyleBundle("~/bundles/TarjetaRecepcionGanadoEstilo").Include(
                "~/assets/css/TarjetaRecepcionGanado.css"));
            #endregion Archivos Pantalla Recepción/CalificacionGanado

            #region Archivos Pantalla Manejo/Disponibilidad
            bundles.Add(new ScriptBundle("~/bundles/DisponibilidadScript").Include(
                  "~/Scripts/Disponibilidad.js"));

            bundles.Add(new StyleBundle("~/bundles/DisponibilidadEstilo").Include(
                "~/assets/css/Disponibilidad.css"));
            #endregion Archivos Pantalla Manejo/Disponibilidad

            #region Archivos Pantalla Manejo/RevisionImplante
            bundles.Add(new ScriptBundle("~/bundles/RevisionImplanteScript").Include(
                  "~/Scripts/RevisionImplante.js"));

            bundles.Add(new StyleBundle("~/bundles/RevisionImplanteEstilo").Include(
                "~/assets/css/RevisionImplante.css"));
            #endregion Archivos Pantalla Manejo/RevisionImplante

            #region Archivos Pantalla PlanAlimentos/AutorizacionSolicitudProductosAlmacen
            bundles.Add(new ScriptBundle("~/bundles/AutorizacionSolicitudProductosAlmacenScript").Include(
                  "~/Scripts/AutorizacionSolicitudProductosAlmacen.js"));

            bundles.Add(new StyleBundle("~/bundles/AutorizacionSolicitudProductosAlmacenEstilo").Include(
                "~/assets/css/AutorizacionSolicitudProductosAlmacen.css"));
            #endregion Archivos Pantalla Recepción/CalificacionGanado

            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                  "~/Scripts/WebForms/WebForms.js",
                  "~/Scripts/WebForms/WebUIValidation.js",
                  "~/Scripts/WebForms/MenuStandards.js",
                  "~/Scripts/WebForms/Focus.js",
                  "~/Scripts/WebForms/GridView.js",
                  "~/Scripts/WebForms/DetailsView.js",
                  "~/Scripts/WebForms/TreeView.js",
                  "~/Scripts/WebForms/WebParts.js"));

            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            BundleTable.EnableOptimizations = true;
        }
    }
}