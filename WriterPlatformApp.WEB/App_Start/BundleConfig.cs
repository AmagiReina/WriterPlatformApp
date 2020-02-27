using System.Web;
using System.Web.Optimization;

namespace WriterPlatformApp.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                       "~/Scripts/popper.min.js"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-lumen.css",
                      "~/Content/fontawesome-all.css",
                      "~/Content/alertifyjs/alertify.css",
                      "~/Content/alertifyjs/alertify.rtl.css",
                      "~/Content/alertifyjs/themes/bootstrap.css",
                      "~/Content/alertifyjs/themes/bootstrap.rtl.css",
                      "~/Content/site.css"));

            // Helper scripts(moment, alertify)

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/alertify").Include(
                "~/Scripts/alertify.js"));

            // Custom scripts

            bundles.Add(new ScriptBundle("~/bundles/ajaxtitlescript").Include(
                      "~/Scripts/ajaxtitlescript.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxmessagescript").Include(
                     "~/Scripts/ajaxmessagescript.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxratingscript").Include(
                     "~/Scripts/ajaxratingscript.js"));

            bundles.Add(new ScriptBundle("~/bundles/titleindexscript").Include(
                     "~/Scripts/titleindexscript.js"));

            bundles.Add(new ScriptBundle("~/bundles/changepassword").Include(
                     "~/Scripts/changepwdscript.js"));

            bundles.Add(new ScriptBundle("~/bundles/editinfo").Include(
                     "~/Scripts/editinfoscript.js"));

            bundles.Add(new ScriptBundle("~/bundles/titlecreatescript").Include(
                     "~/Scripts/titlecreatescript.js"));
        }
    }
}
