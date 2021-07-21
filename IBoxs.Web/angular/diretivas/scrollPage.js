var scrollPage = angular.module("appIBoxs.scrollPages", []);

scrollPage.directive("scrollDetector", ["$document", "$window", '$injector', function ($document, $window, $injector) {
    var $skip = 0;
    return {
        restrict: "A",
        scope: true,
        controller: ["$scope", function ($scope) {
            $scope.viewMore = true;
        }],
        link: function ($scope, $elem, $attrs) {
            var $top = 4;
            var blockUi = $injector.get("blockUI");

            angular.element($window).bind("scroll", function () {
                if (($(window).scrollTop() + $(window).innerHeight()) >= (document.documentElement.scrollHeight) &&
                    $scope.viewMore &&
                    window.$($elem).is(":visible")) {
                    listarMais($(window).scrollTop());
                }
            });
            var listarMais = function (scroll) {
                blockUi.start("Aguarde...");

                $skip += $top;

                if (isNaN($skip)) {
                    return;
                }
                var pesquisa = "$skip=" + $skip + "&$top=" + $top + "&$orderby=Valor asc";
                var produtoService = $injector.get($scope.serviceForFilter);
                produtoService.BuscarProdutosPaginacao($scope.TokenProdutos, $scope.LojaId, pesquisa).then(function (result) {

                    $scope.viewMore = (result.data.length > 0 && result.data.length == $top);
                    if (result.data.length == 0) {
                        $skip = $scope.ListaProdutos.length;
                        return false;
                    }

                    for (var i = 0; i < result.data.length; i++) {
                        $scope.ListaProdutos.push(result.data[i]);
                    }

                    var idCarrinho = cookies.verificarCookies($scope.LojaId + $scope.TokenProdutos);
                    if (idCarrinho != undefined) {
                        $scope.verificarCarrinho(idCarrinho);
                    }

                }, function (error) { throw error; }).finally(function () {
                    blockUi.stop();
                });
            }
        }
    }
}]);