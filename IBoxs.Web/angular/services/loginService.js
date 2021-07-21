var loginServices = angular.module('appIBoxs.login', []);

loginServices.factory('loginService', ['$http', function ($http) {
    var loginService = {};
    var url = window.location.origin + window.location.pathname;

    loginService.Autenticar = function (login) {
        return $http.post(url + 'api/Login/Autenticar', login);
    };

    return loginService;
}]);