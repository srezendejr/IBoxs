var pagamentoServices = angular.module('appIBoxs.pagamento', []);

pagamentoServices.factory('pagamentoService', ['$http', '$q', function ($http, $q) {
    var pagamentoService = [];
    var url = window.location.origin + window.location.pathname;

    pagamentoService.Pagar = function (model) {
        return $http.post(url + '/api/pagamento/pagar', model);
    };

    pagamentoService.RecuperarTipoDocumento = function (token) {
        return $http.get("https://api.mercadopago.com/v1/identification_types?public_key=" + token);
    };

    pagamentoService.PesquisaCep = function (cep) {
        return $http.get("https://viacep.com.br/ws/" + cep + "/json");
    }

    pagamentoService.CalculaFrete = function (model) {
        return $http.post(url + 'api/pagamento/CalculaFrete', model);
    }

    pagamentoService.FinalizarCompra = function (model) {
        return $http.post(url + 'api/pagamento/FinalizarCompra', model);
    }

    return pagamentoService;
}]);