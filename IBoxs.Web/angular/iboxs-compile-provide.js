var boxCommerce = angular.module('appIBoxs');

boxCommerce.config(['$compileProvider', function ($compileProvider) {
    $compileProvider.debugInfoEnabled(false);
}]);