using System.Web.Optimization;

namespace Kbit.ControlCentre
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                    "~/vendor/vendor/jquery/dist/jquery.min.js",
                    "~/vendor/jquery-ui/jquery-ui.min.js",
                    "~/vendor/slimScroll/jquery.slimscroll.min.js",
                    "~/vendor/bootstrap/dist/js/bootstrap.min.js",
                    "~/vendor/jquery-flot/jquery.flot.js",
                    "~/vendor/jquery-flot/jquery.flot.resize.js",
                    "~/vendor/jquery-flot/jquery.flot.pie.js",
                    "~/vendor/flot.curvedlines/curvedLines.js",
                    "~/vendor/jquery.flot.spline/index.js",
                    "~/vendor/metisMenu/dist/metisMenu.min.js",
                    "~/vendor/iCheck/icheck.min.js",
                    "~/vendor/peity/jquery.peity.min.js",
                    "~/vendor/sparkline/index.js",
                    "~/scripts/homer.js",
                    "~/scripts/charts.js",

                    "~/vendor/datatables/media/js/jquery.dataTables.min.js",
                    "~/vendor/datatables.net-bs/js/dataTables.bootstrap.min.js",
                    "~/vendor/pdfmake/build/pdfmake.min.js",
                    "~/vendor/pdfmake/build/vfs_fonts.js",
                    "~/vendor/datatables.net-buttons/js/buttons.html5.min.js",
                    "~/vendor/datatables.net-buttons/js/buttons.print.min.js",
                    "~/vendor/datatables.net-buttons/js/dataTables.buttons.min.js",
                    "~/vendor/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                    "~/vendor/sweetalert/lib/sweet-alert.min.js",
                    "~/vendor/toastr/build/toastr.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Site.css",
                    "~/vendor/datatables.net-bs/css/dataTables.bootstrap.min.css",
                    "~/vendor/toastr/build/toastr.min.css",
                    "~/vendor/sweetalert/lib/sweet-alert.css",
                    "~/vendor/fontawesome/css/font-awesome.css",
                    "~/vendor/metisMenu/dist/metisMenu.css",
                    "~/vendor/animate.css/animate.css",
                    "~/vendor/bootstrap/dist/css/bootstrap.css",
                    "~/fonts/pe-icon-7-stroke/css/pe-icon-7-stroke.css",
                    "~/fonts/pe-icon-7-stroke/css/helper.css",
                    "~/styles/style.css",
                    "~/styles/static_custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}
