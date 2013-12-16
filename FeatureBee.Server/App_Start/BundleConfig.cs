using System.Web.Optimization;

namespace FeatureBee.Server
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(
                new ScriptBundle("~/bundles/board").Include(
                    "~/Scripts/jquery.signalR-{version}.js",
                    "~/Scripts/boardify/*.js",
                    "~/Scripts/handlebars-v{version}.js"));
            bundles.Add(
                new ScriptBundle("~/bundles/startup-boardify").Include(
                    "~/Scripts/startup.boardify.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/boardify").Include("~/Content/boardify.css",
                        "~/Content/jquery.ui.theme.css"));
        }
    }
}