using System.Web.Optimization;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;

namespace ConjaWeb {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {

            BundleResolver.Current = new CustomBundleResolver();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js"
                ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //    "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //    "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //    "~/Scripts/bootstrap.js",
            //    "~/Scripts/respond.js"));

            var stylesBundle = new StyleBundle("~/bundles/styles");
            stylesBundle.Include("~/styles/styles.scss");
            stylesBundle.Transforms.Add(new StyleTransformer());
            bundles.Add(stylesBundle);
            //var stylesBundle = new StyleBundle("~/Skylight/Styles/css").Include(
            //    "~/Skylight/Styles/common.css",
            //    "~/Skylight/Styles/grid960.css",
            //    "~/Skylight/Styles/layout.scss",
            //    "~/Skylight/Styles/jQueryUI/skylight/jquery-ui-{version}.custom.css"
            //);

            //bundles.Add(stylesBundle);
        }
    }
}
