using System.Web;
using System.Web.Optimization;

namespace IBoxs.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                      "~/Scripts/umd/popper.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css").Include(
                      "~/Content/Carrinho.css").Include(
                      "~/Content/Credenciamento.css"));

            bundles.Add(new StyleBundle("~/Content/loja").Include(
                      "~/Content/Loja.css"));

            bundles.Add(new StyleBundle("~/Content/sweetalert").Include(
                      "~/Content/sweetalert.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js").Include(
                        "~/Scripts/angular-ui-router.min.js").Include(
                        "~/Scripts/i18n/angular-locale_pt-br.js").Include(
                        "~/Scripts/angular*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymask").Include(
                        "~/Scripts/jquery-mask/angular-jquery-mask.js",
                        "~/Scripts/jquery-mask/mask.js",
                        "~/Scripts/jquery-mask/jquery.mask.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modulos").Include(
                       "~/angular/iboxs-route.js",

                       "~/angular/services/lojaService.js",
                       "~/angular/controller/lojaController.js",

                       "~/angular/services/boxProdutoService.js",
                       "~/angular/controller/boxProdutoController.js",

                       "~/angular/services/carrinhoService.js",
                       "~/angular/controller/carrinhoController.js",

                       "~/angular/services/pagamentoService.js",
                       "~/angular/controller/pagamentoController.js",

                       "~/angular/services/cepService.js",

                       "~/angular/diretivas/scrollPage.js",
                       "~/angular/diretivas/fileupload.js",

                       "~/angular/controller/perfilController.js",

                       "~/angular/services/loginService.js",
                       "~/angular/controller/loginController.js",

                       "~/angular/iboxs.js",
                       "~/angular/iboxs-provide.js",
                       "~/angular/iboxs-compile-provide.js"));

            bundles.Add(new ScriptBundle("~/bundles/ui-route-core").Include(
                //"~/assets/angular/angular-ui-router.min.js",
                "~/Scripts/ui/ui-router-core.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ui-route").Include(
                //"~/assets/angular/ui-router-extras.js"
                "~/Scripts/ui/ui-router-angularjs.js",
                "~/Scripts/ui/ui-router-dsr.min.js",
                "~/Scripts/ui/ui-router-sticky-states.js",
                "~/Scripts/ui/ui-router-visualizer.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/blockUI").Include(
                "~/Scripts/blockUI/angular-block-ui.min.js"));

            bundles.Add(new StyleBundle("~/bundles/blockUIcss").Include(
                 "~/Scripts/blockUI/angular-block-ui.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/Messaging").Include(
                "~/Scripts/Messaging.js").Include(
                "~/Scripts/sweetalert.js").Include(
                "~/Scripts/toastr/toastr.js"));

            bundles.Add(new StyleBundle("~/bundles/toasterCss").Include(
                "~/Scripts/toastr/toastr.css"));

            bundles.Add(new ScriptBundle("~/bundles/cookies").Include(
                "~/Scripts/cookies.js"));
        }
    }
}
