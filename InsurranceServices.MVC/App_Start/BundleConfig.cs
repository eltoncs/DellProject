using System.Web;
using System.Web.Optimization;

namespace InsuranceServices.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/partner").Include(
                "~/Scripts/AngularControllers/partner.js"                     
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/customer").Include(
                "~/Scripts/AngularControllers/customer.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/dashboard").Include(
                "~/Scripts/AngularControllers/dashboard.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/simulation").Include(
                "~/Scripts/AngularControllers/simulation.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/app-menu").Include(
                "~/app-assets/js/core/app-menu.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/app-assets/js/libraries/jquery.min.js",
                "~/app-assets/vendors/js/ui/tether.min.js",
                "~/app-assets/js/libraries/bootstrap.min.js",
                "~/Scripts/respond.js",
                "~/Scripts/jquery.mask.js",
                "~/app-assets/vendors/js/ui/perfect-scrollbar.jquery.min.js",
                "~/app-assets/vendors/js/ui/unison.min.js",
                "~/app-assets/js/core/app-menu.js",
                "~/app-assets/js/core/app.js",
                "~/app-assets/vendors/js/charts/Chart.bundle.js",
                "~/app-assets/vendors/js/extensions/sweetalert.min.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/app-assets/css/bootstrap.css",
                "~/app-assets/css/bootstrap-extended.css",
                "~/app-assets/vendors/css/extensions/pace.css",
                "~/app-assets/css/app.css",
                "~/app-assets/css/colors.css",
                "~/app-assets/css/core/menu/menu-types/vertical-menu.css",
                "~/app-assets/css/core/colors/palette-gradient.css",
                "~/app-assets/vendors/css/ui/prism.css",
                "~/app-assets/vendors/css/extensions/sweetalert.css",
                "~/app-assets/css/core/menu/menu-types/vertical-overlay-menu.css",
                "~/Content/site.css",
                "~/app-assets/vendors/css/modal/sweetalert.css"
                    ).Include("~/app-assets/fonts/icomoon.css", new CssRewriteUrlTransform()
            ));
        }
    }
}
