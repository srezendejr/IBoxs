var lojaServices = angular.module('appIBoxs.loja', []);

lojaServices.factory('lojaService', ['$http', function ($http) {
    var lojaService = {};
    var url = window.location.origin + window.location.pathname;

    lojaService.BuscarLoja = function (IdLoja) {
        return $http.get(url + 'api/Loja/BuscarLoja?IdLoja=' + IdLoja);
    };

    lojaService.BuscarLojaPorCNPJCPF = function (cnpjcpf) {
        return $http.get(url + 'api/Loja/BuscarLojaPorCNPJCPF?CNPJCPF=' + cnpjcpf);
    };

    lojaService.ListarLojas = function () {
        return $http.get(url + 'api/loja/ListarLojas');
    };

    lojaService.Salvar = function (model) {
        return $http.post(url + 'api/Loja/Salvar', model);
    }

    lojaService.ExcluirLoja = function (Id) {
        return $http.delete(url + 'api/Loja/ExcluirLoja?Id=' + Id);
    }

    lojaService.Cadastro = function (model) {
        return $http.post(url + 'api/Loja/Cadastro', model);
    }
    return lojaService;
}]);