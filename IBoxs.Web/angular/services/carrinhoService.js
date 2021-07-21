var carrinhoServices = angular.module('appIBoxs.carrinho', []);

carrinhoServices.factory('carrinhoService', ['$http', function ($http) {
    var carrinhoService = {};
    var url = window.location.origin + window.location.pathname;


    carrinhoService.AbrirCarrinho = function (id) {
        return $http.get(url + 'api/CarrinhoCompra/AbrirCarrinho?id='+id);
    };

    carrinhoService.RemoverItem = function (item) {
        return $http.put(url + 'api/CarrinhoCompra/RemoverItem', item);
    }

    carrinhoService.AtualizarItem = function (item) {
        return $http.put(url + 'api/CarrinhoCompra/AtualizarItem', item);
    }

    carrinhoService.TotalCarrinho = function (id) {
        return $http.get(url + 'api/CarrinhoCompra/TotalCarrinho?id=' + id);
    }

    carrinhoService.ConsultarEstoque = function (id) {
        return $http.get(url + 'api/CarrinhoCompra/ConsultarEstoqueCarrinho?id=' + id);
    }
    return carrinhoService;
}]);