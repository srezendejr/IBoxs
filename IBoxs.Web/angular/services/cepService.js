var cepServices = angular.module('appIBoxs.cep', []);

cepServices.factory('cepService', ['$http', function ($http) {
    var cepService = {};

    cepService.PesquisaCep = function (cep) {
        return $http.get("https://viacep.com.br/ws/" + cep + "/json");
    }

    cepService.removeMascara = function (value) {
        var result = value.toString().replace(/ /g, '').replace(/\.+$/, '').replace('.', '').replace('-', '').replace('/', '').replace('string', '');
        return result;
    };

    return cepService;
}]);