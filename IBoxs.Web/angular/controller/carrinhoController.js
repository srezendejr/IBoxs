var boxCarrinhoController = angular.module('appIBoxs.carrinho');

boxCarrinhoController.controller('carrinhoController', ['$rootScope', '$scope', '$injector', '$filter', '$state', '$timeout', 'carrinhoService', function ($rootScope, $scope, $injector, $filter, $state, $timeout, carrinhoService) {
    var blockUi = $injector.get("blockUI");
    var lojaService = $injector.get('lojaService');
    var nomeCookie = "";
    $scope.qtdProdutosSemEstoque = 0;
    $scope.IdCarrinho = $state.params.Id;
    $scope.TokenProdutos = "";
    $scope.LojaId = $state.params.LojaId;
    $scope.AbrirCarrinho = function () {
        blockUi.start("Aguarde, estamos ajudando você com seu carrinho compras.")
        carrinhoService.AbrirCarrinho($scope.IdCarrinho).then(function (result) {
            if (result.data.length == 0) {
                cookies.apagarCookies(nomeCookie, $scope.IdCarrinho);
                $state.go("home", { LojaId: $scope.LojaId, TokenProdutos: $scope.TokenProdutos });
            }
            else {
                $scope.CarrinhoCompra = result.data;
                $rootScope.CarrinhoCompra = $scope.CarrinhoCompra;
                nomeCookie = result.data[0].LojaId + result.data[0].TokenProdutos;
                $scope.TokenProdutos = result.data[0].TokenProdutos;
                getLoja();
            }
            blockUi.stop();
        });
    };

    var getLoja = function () {
        lojaService.BuscarLoja($scope.LojaId).then(function (result) {
            var loja = result.data;
            $rootScope.NomeLoja = loja.Nome;
            $rootScope.Whatsapp = loja.Whatsapp;

            $rootScope.QuemSomos = loja.QuemSomos;
            $rootScope.PoliticaPrivacidade = loja.PoliticaPrivacidade;
            $rootScope.PoliticaTrocaDevolucao = loja.PoliticaTrocaDevolucao;
            $scope.QtdMinima = loja.QtdMinima;
            if (loja.Logo) {
                if (loja.Logo !== undefined || loja.Logo !== "") {
                    var urlImage = window.location.origin + window.location.pathname + "/image/" + loja.Logo;
                    var cabecalho = document.getElementById("cabecalho");
                    cabecalho.style.backgroundImage = 'url(' + urlImage + ')';
                }
            }
        });
    };

    $scope.RemoverItem = function (item) {
        blockUi.start("Removendo item do carrinho, aguarde.");
        var itemExcluido = {
            IdCarrinho: item.Id,
            CodigoProdutoCompleto: item.CodigoCompletoProduto
        };
        carrinhoService.RemoverItem(itemExcluido).then(function () {
            Messaging.Success("Produto removido com sucesso");
            $scope.AbrirCarrinho();
        }, function (error) {
            console.log(error.message);
            console.log($scope.IdCarrinho);
            console.log(item.CodigoCompletoProduto);
            throw error;
        }).finally(function () {
            blockUi.stop();
        });
    };

    var atualizarItem = function (item) {
        carrinhoService.AtualizarItem(item).then(function (result) {
            $scope.CarrinhoCompra = result.data;
            Messaging.Success("Produto atualizado com sucesso");
        });
    };

    $scope.increaseValue = function (item) {
        var value = item.QuantidadeProduto;
        value = isNaN(value) ? 0 : value;
        value++;
        item.QuantidadeProduto = value;
        atualizarItem(item);
    };

    $scope.decreaseValue = function (item) {
        var value = item.QuantidadeProduto
        value = isNaN(value) ? 0 : value;
        value--;
        item.QuantidadeProduto = value;
        if (item.QuantidadeProduto == 0) {
            $scope.RemoverItem(item);
        }
        else {
            atualizarItem(item);
        }
    };

    $scope.TotalCarrinho = function () {
        if (angular.isUndefined($scope.CarrinhoCompra) || $scope.CarrinhoCompra.length == 0)
            return 0;
        var t = 0;
        for (var i in $scope.CarrinhoCompra) {
            t += parseFloat($scope.CarrinhoCompra[i].ValorTotal);
        }
        return t;
    };

    $scope.ContinuarComprando = function () {
        $state.go("home", { LojaId: $scope.CarrinhoCompra[0].LojaId, TokenProdutos: $scope.CarrinhoCompra[0].TokenProdutos });
    };

    $scope.FecharComprar = function (event) {
        event.preventDefault();
        var totalItens = 0;
        for (var i = 0; i < $scope.CarrinhoCompra.length; i++) {
            totalItens += $scope.CarrinhoCompra[i].QuantidadeProduto;
        }
        if (totalItens < $scope.QtdMinima) {
            Messaging.Information("Quantidade mínima", "A quantidade de peças no carrinho (" + totalItens +") está abaixo da quantidade mínima permitida (" + $scope.QtdMinima.toString() + ")");
            return;
        }
        blockUi.start("Aguarde, estamos validando o estoque dos seus itens.");
        carrinhoService.ConsultarEstoque($scope.IdCarrinho).then(function (result) {
            blockUi.stop();
            if (result) {
                var semEstoque = result.data;
                if (semEstoque) {
                    Messaging.Information("Produtos sem estoque", "Há produdos sem estoque neste carrinho, ajuste a quantidade ou remova-os para continuar.");
                }
                else {
                    $state.go("pagamento", { LojaId: $scope.CarrinhoCompra[0].LojaId, Id: $scope.IdCarrinho });
                }
            }
        });
    };
}]);